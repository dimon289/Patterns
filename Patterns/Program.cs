using System;

public sealed class LoadBalancer
{
    private static readonly Lazy<LoadBalancer> _instance = new(() => new LoadBalancer());
    private bool _isOnline;

    private LoadBalancer()
    {
        // Ініціалізація класу
        _isOnline = false;
    }

    public static LoadBalancer Instance
    {
        get { return _instance.Value; }
    }

    public bool IsOnline
    {
        get { return _isOnline; }
    }

    public void SetOnlineStatus(bool isOnline)
    {
        _isOnline = isOnline;
        Console.WriteLine($"Web server is {(isOnline ? "online" : "offline")}");
    }
}

class Program
{
    static void Main()
    {
        // Клієнтський код
        LoadBalancer loadBalancer1 = LoadBalancer.Instance;
        LoadBalancer loadBalancer2 = LoadBalancer.Instance;

        Console.WriteLine($"Are both instances the same? {loadBalancer1 == loadBalancer2}");

        // Зміна статусу сервера через будь-який екземпляр класу
        loadBalancer1.SetOnlineStatus(true);

        // Перевірка статусу через інший екземпляр класу
        Console.WriteLine($"Is the server online? {loadBalancer2.IsOnline}");
    }
}
