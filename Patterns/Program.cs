using System;
using System.Text;

public class CodeBuilder
{
    private StringBuilder builder = new StringBuilder();
    private int indentLevel = 0;

    public CodeBuilder Indent()
    {
        indentLevel++;
        return this;
    }

    public CodeBuilder Outdent()
    {
        if (indentLevel > 0)
        {
            indentLevel--;
        }
        return this;
    }

    public CodeBuilder Append(string value)
    {
        builder.Append(new string(' ', 4 * indentLevel));
        builder.Append(value);
        return this;
    }

    public CodeBuilder Append(char value, int repeatCount)
    {
        builder.Append(value, repeatCount);
        return this;
    }

    public CodeBuilder AppendLine()
    {
        builder.AppendLine();
        return this;
    }

    public CodeBuilder AppendLine(string value)
    {
        builder.Append(new string(' ', 4 * indentLevel));
        builder.AppendLine(value);
        return this;
    }

    public override string ToString()
    {
        return builder.ToString();
    }
}

class Program
{
    public static void PrintTimeStamp(string name)
    {
        DateTime localDate = DateTime.Now;
        var culture = new System.Globalization.CultureInfo("ua-UA");
        Console.WriteLine("Дата та час компіляції: {0}", localDate.ToString(culture));
        Console.WriteLine("Автор програми: {0}", name);
    }

    static void Main()
    {
        CodeBuilder myBuilder = new CodeBuilder();
        myBuilder
            .Append("public class HelloWorld")
            .AppendLine()
            .AppendLine("{")
            .Indent()
            .AppendLine("public static void Main(string[] args)")
            .AppendLine("{")
            .Indent()
            .AppendLine("Console.WriteLine(\"Hello, World\");")
            .Outdent()
            .AppendLine("}")
            .Outdent()
            .AppendLine("}");

        Console.WriteLine(myBuilder.ToString());

        PrintTimeStamp("Лісун Іван, студент групи 2П-21");
    }
}
