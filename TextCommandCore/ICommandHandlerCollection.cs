using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TextCommandCore
{
    public interface ICommandHandlerCollection<T> where T : ICommandHandlerCollection<T>
    {
        Action<TargetID, Message> MessageSender { get; }
        Action<Message> ErrorMessageSender { get; }
    }

    public class CommandInfo
    {
        public Predicate<string> Matcher { get; }
        public MethodInfo Method { get; }

        public CommandInfo(MethodInfo method)
        {
            Matcher = method.GetCustomAttribute<MatcherAttribute>().Matcher;
            Method = method;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MatchersAttribute : MatcherAttribute
    {
        public MatchersAttribute(params string[] matchers) : base(msg => matchers.Any(match => match == msg))
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public class MatcherAttribute : Attribute
    {
        public Predicate<string> Matcher { get; }

        public MatcherAttribute(Predicate<string> matcher)
        {
            Matcher = matcher;
        }
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CombineParamsAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CombineStartAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method)]
    public sealed class CombineEndAttribute : Attribute
    {
    }
}
