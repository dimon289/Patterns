using System;

public class EnemeyTrait
{
    public int BoostTime { get; set; }
    public int BoostDamage { get; set; }
}

public abstract class Enemy : ICloneable
{
    private int m_Health;
    private int m_Attack;
    private double m_Damage;
    private EnemeyTrait m_EnemyTrait = new EnemeyTrait();

    public int Health
    {
        get { return m_Health; }
        set { m_Health = value; }
    }

    public int Attack
    {
        get { return m_Attack; }
        set { m_Attack = value; }
    }

    public double Damage
    {
        get { return m_Damage; }
        set { m_Damage = value; }
    }

    public EnemeyTrait EnemyTrait
    {
        get { return m_EnemyTrait; }
        set { m_EnemyTrait = value; }
    }

    public virtual object Clone()
    {
        // Choose whether to perform a shallow or deep copy
        // return ShallowCopy();
        return DeepCopy();
    }

    private object ShallowCopy()
    {
        return this.MemberwiseClone();
    }

    private object DeepCopy()
    {
        Enemy cloned = this.MemberwiseClone() as Enemy;
        cloned.EnemyTrait = new EnemeyTrait
        {
            BoostDamage = this.EnemyTrait.BoostDamage,
            BoostTime = this.EnemyTrait.BoostTime
        };

        return cloned;
    }
}
public class Elephant : Enemy
{
    public override Enemy Clone()
    {
        return this.MemberwiseClone() as Elephant;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The code to demonstrate the classic Prototype Pattern
        Elephant elephant = new Elephant
        {
            Health = 100,
            Attack = 10,
            Damage = 5.0,
            EnemyTrait = { BoostTime = 7, BoostDamage = 7 }
        };

        Elephant playerToSave = elephant.Clone() as Elephant;
        playerToSave.EnemyTrait.BoostTime = 10;
        playerToSave.EnemyTrait.BoostDamage = 10;

        Console.WriteLine("Original Enemy stats:");
        Console.WriteLine($"BoostTime: {elephant.EnemyTrait.BoostTime}, BoostDamage: {elephant.EnemyTrait.BoostDamage}");

        Console.WriteLine("\nCopied Object Value:");
        Console.WriteLine($"BoostTime: {playerToSave.EnemyTrait.BoostTime}, BoostDamage: {playerToSave.EnemyTrait.BoostDamage}");
    }
}
