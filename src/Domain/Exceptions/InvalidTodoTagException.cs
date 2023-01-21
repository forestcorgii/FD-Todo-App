using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo_App.Domain.Exceptions;
public class InvalidTodoTagException:Exception
{
    public InvalidTodoTagException(string value) :base($"An invalid special character was detected in {value}."){ }
}
