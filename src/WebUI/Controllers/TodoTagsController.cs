using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Application.TodoTags.Queries.GetTodoTags;

namespace Todo_App.WebUI.Controllers;
public class TodoTagsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TodoTagDto>>> Get()
    {
        return await Mediator.Send(new GetTodoTagsQuery());
    }

}
