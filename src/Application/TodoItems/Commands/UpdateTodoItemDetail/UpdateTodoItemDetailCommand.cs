using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Colour { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }

    public List<string> Tags { get; set; } = new List<string>();

}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.ListId = request.ListId;
        entity.Colour = request.Colour;
        entity.Priority = request.Priority;
        entity.Note = request.Note;

        entity.Tags = await MapToTagEntities(request.Tags, cancellationToken);

        RemoveUnusedTodoTagEntities(cancellationToken);


        await _context.SaveChangesAsync(cancellationToken);

        var iitem = await _context.TodoItems
            .FindAsync(new object[] { request.Id }, cancellationToken);

        return Unit.Value;
    }


    private async Task<List<TodoTag>> MapToTagEntities(List<string> rawTags, CancellationToken cancellationToken)
    {
        List<TodoTag> tags = new();
        foreach (string tag in rawTags)
        {
            TodoTag? tagEntity = await _context.TodoTags.Where(t => t.Name == tag).FirstOrDefaultAsync(cancellationToken);
            if (tagEntity == null)
                tags.Add(new TodoTag(tag));
            else
                tags.Add(tagEntity);
        }
        return tags;
    }

    private async void RemoveUnusedTodoTagEntities(CancellationToken cancellationToken)
    {
        // Check for unused tags to be deleted
        List<TodoTag> tags = await _context.TodoTags.Include(t=>t.Items).ToListAsync(cancellationToken);
        foreach (var tag in tags)
            if (!tag.Items.Any())
                _context.TodoTags.Remove(tag);
    }
}
