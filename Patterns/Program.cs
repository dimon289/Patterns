using Mediator.Components;
using System;
using System.Collections.Generic;

namespace Mediator.Components
{
    public class SnackBar
    {
        protected IMediator _mediator;

        public SnackBar(IMediator mediator)
        {
            _mediator = mediator;
        }
    }

    public class HotDogStand : SnackBar
    {
        public HotDogStand(IMediator mediator) : base(mediator)
        {
        }

        public void Send(string message)
        {
            Console.WriteLine($"HotDog Stand says: {message}");
            _mediator.SendMessage(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"HotDog Stand gets message: {message}");
        }
    }

    public class FrenchFriesStand : SnackBar
    {
        public FrenchFriesStand(IMediator mediator) : base(mediator)
        {
        }

        public void Send(string message)
        {
            Console.WriteLine($"French Fries Stand says: {message}");
            _mediator.SendMessage(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"French Fries Stand gets message: {message}");
        }
    }
}

namespace Mediator
{
    public interface IMediator
    {
        void SendMessage(string message, SnackBar snackBar);
    }

    public class SnackBarMediator : IMediator
    {
        private HotDogStand hotDogStand;
        private FrenchFriesStand friesStand;

        public HotDogStand HotDogStand
        {
            set { hotDogStand = value; }
        }

        public FrenchFriesStand FriesStand
        {
            set { friesStand = value; }
        }

        public void SendMessage(string message, SnackBar snackBar)
        {
            if (snackBar == hotDogStand)
                friesStand.Notify(message);
            if (snackBar == friesStand)
                hotDogStand.Notify(message);
        }
    }
    class Program
    {

        public static void Main()
        {
            SnackBarMediator mediator = new SnackBarMediator();

            HotDogStand leftKitchen = new HotDogStand(mediator);
            FrenchFriesStand rightKitchen = new FrenchFriesStand(mediator);

            mediator.HotDogStand = leftKitchen;
            mediator.FriesStand = rightKitchen;

            leftKitchen.Send("Can you send more cooking oil?");
            rightKitchen.Send("Sure thing, Homer's on his way");

            rightKitchen.Send("Do you have any extra soda? We've had a rush on them over here.");
            leftKitchen.Send("Just a couple, we'll send Homer back with them");

            Console.ReadKey();

        }
    }
}
