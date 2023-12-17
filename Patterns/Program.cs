using System;
using System.Collections.Generic;
using System.Text;
public interface InterfaceStrategy
{
    IEnumerable<string> PerformAlgorithm(List<string> list);
}
class DefaultConcreteStrategy : InterfaceStrategy
{
    public IEnumerable<string> PerformAlgorithm(List<string> list)
    {
        list.Sort();
        return list;
    }
}

class AlternativeConcreteStrategy : InterfaceStrategy
{
    public IEnumerable<string> PerformAlgorithm(List<string> list)
    {
        list.Sort();
        list.Reverse();
        return list;
    }
}
class Context
{
    private InterfaceStrategy _strategy;

    public Context()
    {
    }

    public Context(InterfaceStrategy strategy)
    {
        _strategy = strategy;
    }

    // here we can replace the current or default strategy if we choose
    public void SetStrategy(InterfaceStrategy strategy)
    {
        _strategy = strategy;
    }
    public void CarryOutWork()
    {
        Console.WriteLine("Context: Carrying out Sorting Work");
        var myResult = _strategy
            .PerformAlgorithm(new List<string>
            {
                "the",
                "boy",
                "is",
                "leaving"
            });

        Console.WriteLine(String.Join(",", myResult));
    }
}
class Program
{
    public static void Main(string[] args)
    {
        // client picks the default concrete strategy:
        Console.WriteLine("Sorting strategy has been set to alphabetical:");
        var context = new Context();

        context.SetStrategy(new DefaultConcreteStrategy());
        context.CarryOutWork();
        Console.WriteLine();

        // client picks the alternative concrete strategy
        Console.WriteLine("Sorting strategy has been set to reverse:");
        context.SetStrategy(new AlternativeConcreteStrategy());
        context.CarryOutWork();
    }
}
