using System;

// Приклад реалізації Abstract Factory
public interface ITouchScreen
{
    string ModelName();
}

public interface IPushButton
{
    string ModelName();
}

// Реалізація конкретних продуктів
public class Nokia6 : ITouchScreen
{
    public string ModelName() { return "Nokia 6 Model"; }
}

public class Guru1200 : IPushButton
{
    public string ModelName() { return "Guru1200 Model"; }
}

// Абстрактна фабрика
public interface IMobilePhoneFactory
{
    ITouchScreen CreateTouchScreen();
    IPushButton CreatePushButton();
}

// Конкретна реалізація абстрактної фабрики
public class NokiaFactory : IMobilePhoneFactory
{
    public ITouchScreen CreateTouchScreen()
    {
        return new Nokia6();
    }

    public IPushButton CreatePushButton()
    {
        return new Guru1200();
    }
}

// Клас, який використовує фабрику
public class Client
{
    private ITouchScreen _touchScreen;
    private IPushButton _pushButton;

    public Client(IMobilePhoneFactory factory)
    {
        _touchScreen = factory.CreateTouchScreen();
        _pushButton = factory.CreatePushButton();
    }

    public void UsePhone()
    {
        Console.WriteLine($"Touch Screen Model: {_touchScreen.ModelName()}");
        Console.WriteLine($"Push Button Model: {_pushButton.ModelName()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Створення фабрики конкретного виробника (наприклад, Nokia)
        IMobilePhoneFactory nokiaFactory = new NokiaFactory();

        // Клієнт використовує фабрику для створення об'єктів
        Client client = new Client(nokiaFactory);
        client.UsePhone();
    }
}
