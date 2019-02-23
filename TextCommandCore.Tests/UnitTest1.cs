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
            ProcessCommandInput("Hello");
            ProcessCommandInput("Nothing");
            ProcessCommandInput("Nothing 1");
            ProcessCommandInput("Echo");
            ProcessCommandInput("Echo 1");
            ProcessCommandInput("E 1");
            ProcessCommandInput("Fork");
            ProcessCommandInput("Fork 123");
            ProcessCommandInput("Fork 人");
            ProcessCommandInput("Fork2 2.02");
            ProcessCommandInput("Fork2 ...1");
            ProcessCommandInput("Fork3 1");
            ProcessCommandInput("Combine 1 1 1");
            ProcessCommandInput("Exception");
            ProcessCommandInput("DefinitionError 1");

            void ProcessCommandInput(string message)
            {
                var sender = "debug";
                var handler = new CommandHandler1(sender, message);
                handler.ProcessCommandInput();
            }
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
        public string Process<T>(MethodInfo message, string s, ICommandHandler<T> handlers) where T : ICommandHandler<T>
        {

            return s;
        }
    }

    public class CommandHandler1 : ICommandHandler<CommandHandler1>
    {
        public CommandHandler1(string sender, string message)
        {
            Sender = sender;
            Message = message;
        }

        public Action<TargetID, Message> MessageSender { get; } = (id, message) => Debug.WriteLine($"{id.ID}: {message.Content}");
        public Action<Message> ErrorMessageSender => msg => Debug.WriteLine($"Exception {msg.Content}");

        public string Sender { get; }

        public string Message { get; }

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
