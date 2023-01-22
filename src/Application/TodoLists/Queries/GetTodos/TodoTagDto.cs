using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Todo_App.Application.Common.Mappings;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoLists.Queries.GetTodoTags;
public class TodoTagDto : IMapFrom<TodoTag>
{

    public TodoTagDto()
    {
        Items = new List<TodoItemDto>();
    }

    public int Id { get; set; }

    public string? Name { get; set; }

    public IList<TodoItemDto> Items { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<TodoTag, string>().ConvertUsing(t => t.Name);
        profile.CreateMap<TodoTag, TodoTagDto>();
    }
}
