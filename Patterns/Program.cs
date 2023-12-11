using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;

public class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }
}

public interface IDrawable
{
    void DrawPoint(Point p);
}

public abstract class VectorObject : Collection<Line>, IDrawable
{
    public abstract void Draw();
    public abstract void DrawPoint(Point p);
}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }

    public override void Draw()
    {
        foreach (var line in this)
        {
            Console.WriteLine($"Drawing line from ({line.Start.X}, {line.Start.Y}) to ({line.End.X}, {line.End.Y})");
        }
    }

    public override void DrawPoint(Point p)
    {
        Console.WriteLine($"Drawing point at ({p.X}, {p.Y})");
    }
}

public class LineToPointAdapter : Collection<Point>
{
    private static int count = 0;

    public LineToPointAdapter(Line line)
    {
        Console.WriteLine($"{++count}: Generating points for line " +
            $"[{line.Start.X},{line.Start.Y}]-[{line.End.X},{line.End.Y}] (no caching)");

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);

        if (right - left == 0)
        {
            for (int y = top; y <= bottom; ++y)
            {
                Add(new Point(left, y));
            }
        }
        else if (line.End.Y - line.Start.Y == 0)
        {
            for (int x = left; x <= right; ++x)
            {
                Add(new Point(x, top));
            }
        }
    }
}

public static class GraphicEditor
{
    private static readonly List<VectorObject> vectorObjects = new List<VectorObject>
    {
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6)
    };

    public static void DrawPoint(Point p)
    {
        Console.Write(".");
    }

    public static void Draw()
    {
        foreach (var vo in vectorObjects)
        {
            foreach (var line in vo)
            {
                var adapter = new LineToPointAdapter(line);
                foreach (var point in adapter)
                {
                    DrawPoint(point);
                }
            }
        }
    }
}

class Program
{
    static void Main()
    {
        GraphicEditor.Draw();
    }
}
