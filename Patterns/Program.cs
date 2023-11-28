using System;

// Приклад реалізації Factory Method
public abstract class AbstractPhone
{
    public abstract void PlayMusic(string songName);
    public abstract void StopMusic();
}

public class Samson : AbstractPhone
{
    override public void PlayMusic(string songName) { /* реалізація */ }
    override public void StopMusic() { /* реалізація */ }
}

public class Nokla : AbstractPhone
{
    public override void PlayMusic(string songName) { /* реалізація */ }
    public override void StopMusic() { /* реалізація */ }
}

// Інші класи та реалізації

class Program
{
    static void Main()
    {
        // Створення об'єкта класу, що використовує Factory Method
        AbstractPhone phone;

        // Вибір конкретного телефону (Samson або Nokla) за допомогою Factory Method
        Console.Write("Виберіть телефон (Samson або Nokla): ");
        string phoneType = Console.ReadLine();

        switch (phoneType.ToLower())
        {
            case "samson":
                phone = new Samson();
                break;

            case "nokla":
                phone = new Nokla();
                break;

            default:
                Console.WriteLine("Невірний вибір!");
                return;
        }

        // Використання створеного телефону
        Console.WriteLine($"Вибраний телефон: {phoneType}");
        phone.PlayMusic("Some Song"); // Відтворення музики
        phone.StopMusic(); // Зупинка музики

        Console.ReadLine(); // Зупинка для перегляду результату
    }
}

