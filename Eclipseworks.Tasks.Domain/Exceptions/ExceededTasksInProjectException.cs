using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipseworks.Tasks.Domain.Exceptions
{
    internal class ExceededTasksInProjectException : Exception
    {
        public ExceededTasksInProjectException(string message) : base(message)
        {
        }
    }
}
