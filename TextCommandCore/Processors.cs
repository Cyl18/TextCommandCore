using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextCommandCore
{
    public interface IPreProcessor
    {
        string Process<T>(MethodInfo method, string msg, ICommandHandlerCollection<T> handlers) where T : ICommandHandlerCollection<T>;
    }

    public interface IPostProcessor
    {
        string Process<T>(MethodInfo method, string msg, string result, ICommandHandlerCollection<T> handlers) where T : ICommandHandlerCollection<T>;
    }
}
