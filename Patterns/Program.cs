
public interface IComponent
{
    string Name { get; set; }
    int Quantity { get; set; }
    double GetCost();
}

// Листовий компонент
public class Part : IComponent
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public double Cost { get; set; }
    public Part(string name, int quantity, double cost)
    {
        Name = name;
        Quantity = quantity;
        Cost = cost;
    }

    public double GetCost() => Cost * Quantity;
}

// Композитний компонент
public class Assembly : IComponent
{
    public string Name { get; set; }
    public int Quantity { get; set; }

    private readonly List<IComponent> _components = new();

    public Assembly(string name, int quantity, IEnumerable<IComponent> components)
    {
        Name = name;
        Quantity = quantity;
        _components.AddRange(components);
    }

    public double GetCost() => _components.Sum(component => component.GetCost());
}

// Клієнт
public class Program
{
    public static void Main(string[] args)
    {

        // частини
        var engine = new Part("Двигун", 1, 5000.0);
        var tires = new Part("Шини", 4, 1000.0);

        // збірка
        var body = new Assembly(
            "Кузов",
            1,
            new List<IComponent> {
                new Part("Рама", 1, 2000.0),
                new Part("Двері", 4, 1000.0),
                new Part("Вікна", 6, 500.0)
            }
        );


        // автомобіль
        var car = new Assembly(
            "Автомобіль",
            1,
            new List<IComponent> {
                engine,
                tires,
                body
        });

        // Розрахунок вартості автомобіля
        var carCost = car.GetCost();

        Console.WriteLine($"Вартість автомобіля: {carCost:C}");
    }
}
