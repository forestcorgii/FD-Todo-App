using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Interfaces;

namespace Todo_App.Application.TodoTags.Queries.GetTodoTags;

public record GetTodoTagsQuery : IRequest<List<TodoTagDto>>;

public class GetTodoTagsQueryHandler : IRequestHandler<GetTodoTagsQuery, List<TodoTagDto>>
{

    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoTagsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<List<TodoTagDto>> Handle(GetTodoTagsQuery request, CancellationToken cancellationToken)
    {
        return await _context.TodoTags
                .AsNoTracking()
                .ProjectTo<TodoTagDto>(_mapper.ConfigurationProvider)
                .Select(t => new TodoTagDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Items = t.Items.Where(x => !x.Deleted).ToList()
                }).Where(t => t.Items.Any(i => !i.Deleted))
                .OrderByDescending(t => t.Items.Count)
                .ToListAsync(cancellationToken);
    }
}
