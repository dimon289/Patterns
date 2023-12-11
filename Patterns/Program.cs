using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public interface IHandler
{
    void SetTheNextHandler(IHandler handler);
    void Process(Request request);
}

public class Individual
{
    public string Name { get; set; }
    public int Age { get; set; }
}

abstract class BaseHandler : IHandler
{
    protected IHandler _nextHandler;

    public void SetTheNextHandler(IHandler handler)
    {
        _nextHandler = handler;
    }

    public abstract void Process(Request request);
}

public class Request
{
    public Individual Data { get; set; }
    public List<string> ValidationMessages;

    public Request()
    {
        ValidationMessages = new List<string>();
    }
}

class MaxHandlerForAge : BaseHandler
{
    public override void Process(Request request)
    {
        if (request.Data.Age > 60)
        {
            request.ValidationMessages.Add("Invalid age range");
        }

        if (_nextHandler != null)
        {
            _nextHandler.Process(request);
        }
    }
}

class MaxHandlerForNameLength : BaseHandler
{
    public override void Process(Request request)
    {
        if (request.Data.Name.Length > 12)
        {
            request.ValidationMessages.Add("Invalid name length");
        }

        if (_nextHandler != null)
        {
            _nextHandler.Process(request);
        }
    }
}
class Program
{
    public static void Main()
    {
        Individual individual = new Individual()
        {
            Name = "Article Writer: Nitro",
            Age = 65
        };

        Request request = new Request() { Data = individual };

        var maxHandlerForAge = new MaxHandlerForAge();
        var maxHandlerForNameLength = new MaxHandlerForNameLength();

        maxHandlerForAge.SetTheNextHandler(maxHandlerForNameLength);
        maxHandlerForAge.Process(request);

        foreach (string displayMsg in request.ValidationMessages)
        {
            Console.WriteLine(displayMsg);
        }

    }
}
