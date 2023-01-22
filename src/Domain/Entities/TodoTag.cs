using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Todo_App.Domain.Entities;
public class TodoTag : BaseAuditableEntity
{
    public string? Name { get; set; }
    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();


    public TodoTag(string? name)
    {
        if (!string.IsNullOrEmpty(name))
            if (Regex.IsMatch(name, "^[a-zA-Z0-9_/-]+$"))
                Name = name;
            else
                throw new InvalidTodoTagException(name);
        else
            throw new NullReferenceException();
    }

}
