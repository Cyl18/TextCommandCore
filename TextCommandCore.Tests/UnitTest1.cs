using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TextCommandCore.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var handler = new CommandHandler1();
            var sender = "debug";
            handler.ProcessCommandInput(sender, "Hello");
            handler.ProcessCommandInput(sender, "Nothing");
            handler.ProcessCommandInput(sender, "Nothing 1");
            handler.ProcessCommandInput(sender, "Echo");
            handler.ProcessCommandInput(sender, "Echo 1");
            handler.ProcessCommandInput(sender, "E 1");
            handler.ProcessCommandInput(sender, "Fork");
            handler.ProcessCommandInput(sender, "Fork 123");
            handler.ProcessCommandInput(sender, "Fork 人");
            handler.ProcessCommandInput(sender, "Fork2 2.02");
            handler.ProcessCommandInput(sender, "Fork2 ...1");
            handler.ProcessCommandInput(sender, "Fork3 1");
            handler.ProcessCommandInput(sender, "Combine 1 1 1");
            handler.ProcessCommandInput(sender, "Exception");
            handler.ProcessCommandInput(sender, "DefinitionError 1");
        }

        [TestMethod]
        public void TestMethod2()
        {
            string a = new Message("233");
            string b = new TargetID("233");
        }
    }

    
    [AttributeUsage(AttributeTargets.All)]
    public class TestPreProcessor : Attribute, IPreProcessor
    {
        public string Process<T>(MethodInfo message, string s, ICommandHandlerCollection<T> handlers) where T : ICommandHandlerCollection<T>
        {

            return s;
        }
    }

    public class CommandHandler1 : ICommandHandlerCollection<CommandHandler1>
    {
        public Action<TargetID, Message> MessageSender { get; } = (id, message) => Debug.WriteLine($"{id.ID}: {message.Content}");
        public Action<Message> ErrorMessageSender => msg => Debug.WriteLine($"Exception {msg.Content}");

        void Hello()
        {

        }

        [Matchers("Nothing")]
        void Nothing()
        {
            Trace.WriteLine("Nothing test succeed");
        }

        [Matchers("Echo", "E")]
        string Echo(string content = "fork")
        {
            return content;
        }

        [Matchers("Fork")]
        string Fork(BigInteger num)
        {
            return num.ToString();
        }

        [Matchers("Fork2")]
        string Fork2(double num)
        {
            return num.ToString();
        }

        [Matchers("Exception")]
        string Exception()
        {
            throw new Exception();
        }

        [Matchers("DefinitionError")]
        [CombineEnd]
        void DefinitionError(string a = "")
        {
            
        }

        [Matchers("Combine")]
        [CombineParams]
        string Combined(string param)
        {
            return param;
        }

    }
}
