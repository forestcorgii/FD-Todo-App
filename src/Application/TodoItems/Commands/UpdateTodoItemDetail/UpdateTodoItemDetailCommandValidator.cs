using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Todo_App.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;
public class UpdateTodoItemDetailCommandValidator : AbstractValidator<UpdateTodoItemDetailCommand>
{
    public UpdateTodoItemDetailCommandValidator()
    {
        RuleFor(x => x.Colour).Must((x, colourText, context) =>
        {
            try
            {
                _ = Colour.From(colourText);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }).WithMessage("Unsupported Color.");
    }
}
