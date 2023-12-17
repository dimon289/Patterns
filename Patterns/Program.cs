using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

public interface IInsurance
{
    void Accept(IVisitor visitor);
}
public interface IVisitor
{
    void VisitCarInsurance(CarInsurance element);
    void VisitMotorBikeInsurance(MotorBikeInsurance element);
}
public class CarInsurance : IInsurance
{
    public string RegistrationNumber { get; set; }
    public string PostCode { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int EngineCC { get; set; }
    public bool IsLeftHandDrive { get; set; }
    public bool IsModified { get; set; }
    public void Accept(IVisitor visitor)
    {
        visitor.VisitCarInsurance(this);
    }
}
public class MotorBikeInsurance : IInsurance
{
    public string RegistrationNumber { get; set; }
    public string PostCode { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public int EngineCC { get; set; }
    public void Accept(IVisitor visitor)
    {
        visitor.VisitMotorBikeInsurance(this);
    }
}
public class QuoteVisitor : IVisitor
{
    public void VisitCarInsurance(CarInsurance element)
    {
        Console.WriteLine($"Get Quote for Car as the element is {element.GetType()}");
    }
    public void VisitMotorBikeInsurance(MotorBikeInsurance element)
    {
        Console.WriteLine($"Get Quote for Motor Bike as the element is {element.GetType()}");
    }
}
public class CustomerCommunicationVisitor : IVisitor
{
    public void VisitCarInsurance(CarInsurance element)
    {
        Console.WriteLine($"Email is sent as the element class is {element.GetType()}");
    }
    public void VisitMotorBikeInsurance(MotorBikeInsurance element)
    {
        Console.WriteLine($"SNS is sent as the element class is {element.GetType()}");
    }
}

public static class Program
{

    public static void Main(string[] args)
    {
        var motorBikeInsurace = new MotorBikeInsurance();
        var carInsurace = new CarInsurance();
        var quoteVisitor = new QuoteVisitor();
        var customerCommVisitor = new CustomerCommunicationVisitor();
        motorBikeInsurace.Accept(quoteVisitor);
        carInsurace.Accept(quoteVisitor);
        Console.WriteLine("===========================================");
        motorBikeInsurace.Accept(customerCommVisitor);
        carInsurace.Accept(customerCommVisitor);
        Console.ReadLine();
    }
}
