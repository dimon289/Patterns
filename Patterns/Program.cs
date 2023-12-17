using System;
using System.Collections.Generic;

public readonly struct BaggageInfo
{
    public int FlightNumber { get; }
    public string From { get; }
    public int Carousel { get; }

    public BaggageInfo(int flightNumber, string from, int carousel)
    {
        FlightNumber = flightNumber;
        From = from;
        Carousel = carousel;
    }
}

public sealed class BaggageHandler : IObservable<BaggageInfo>
{
    private readonly HashSet<IObserver<BaggageInfo>> _observers = new HashSet<IObserver<BaggageInfo>>();
    private readonly HashSet<BaggageInfo> _flights = new HashSet<BaggageInfo>();

    public IDisposable Subscribe(IObserver<BaggageInfo> observer)
    {
        if (_observers.Add(observer))
        {
            foreach (BaggageInfo item in _flights)
            {
                observer.OnNext(item);
            }
        }

        return new Unsubscriber<BaggageInfo>(_observers, observer);
    }

    public class Unsubscriber<T> : IDisposable
    {
        private readonly HashSet<IObserver<T>> _observers;
        private readonly IObserver<T> _observer;

        public Unsubscriber(HashSet<IObserver<T>> observers, IObserver<T> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observer != null && _observers.Contains(_observer))
            {
                _observers.Remove(_observer);
            }
        }
    }

    public void BaggageStatus(int flightNumber) =>
        BaggageStatus(flightNumber, string.Empty, 0);

    public void BaggageStatus(int flightNumber, string from, int carousel)
    {
        var info = new BaggageInfo(flightNumber, from, carousel);

        if (carousel > 0 && _flights.Add(info))
        {
            foreach (IObserver<BaggageInfo> observer in _observers)
            {
                observer.OnNext(info);
            }
        }
        else if (carousel == 0)
        {
            if (_flights.RemoveWhere(flight => flight.FlightNumber == flightNumber) > 0)
            {
                foreach (IObserver<BaggageInfo> observer in _observers)
                {
                    observer.OnNext(info);
                }
            }
        }
    }

    public void LastBaggageClaimed()
    {
        foreach (IObserver<BaggageInfo> observer in _observers)
        {
            observer.OnCompleted();
        }

        _observers.Clear();
    }
}

public class ArrivalsMonitor : IObserver<BaggageInfo>
{
    private readonly string _name;
    private readonly List<string> _flights = new List<string>();
    private readonly string _format = "{0,-20} {1,5}  {2,3}";
    private IDisposable _cancellation;

    public ArrivalsMonitor(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        _name = name;
    }

    public virtual void Subscribe(BaggageHandler provider) =>
        _cancellation = provider.Subscribe(this);

    public virtual void Unsubscribe()
    {
        _cancellation.Dispose();
        _flights.Clear();
    }

    public virtual void OnCompleted() => _flights.Clear();

    // No implementation needed: Method is not called by the BaggageHandler class.
    public virtual void OnError(Exception e)
    {
        // No implementation.
    }

    // Update information.
    public virtual void OnNext(BaggageInfo info)
    {
        bool updated = false;

        // Flight has unloaded its baggage; remove from the monitor.
        if (info.Carousel == 0)
        {
            string flightNumber = string.Format("{0,5}", info.FlightNumber);
            for (int index = _flights.Count - 1; index >= 0; index--)
            {
                string flightInfo = _flights[index];
                if (flightInfo.Substring(21, 5).Equals(flightNumber))
                {
                    updated = true;
                    _flights.RemoveAt(index);
                }
            }
        }
        else
        {
            // Add flight if it doesn't exist in the collection.
            string flightInfo = string.Format(_format, info.From, info.FlightNumber, info.Carousel);
            if (!_flights.Contains(flightInfo))
            {
                _flights.Add(flightInfo);
                updated = true;
            }
        }

        if (updated)
        {
            _flights.Sort();
            Console.WriteLine($"Arrivals information from {_name}");
            foreach (string flightInfo in _flights)
            {
                Console.WriteLine(flightInfo);
            }

            Console.WriteLine();
        }
    }
}

internal class Program
{


    static void Main(string[] args)
    {
        BaggageHandler provider = new BaggageHandler();
        ArrivalsMonitor observer1 = new ArrivalsMonitor("BaggageClaimMonitor1");
        ArrivalsMonitor observer2 = new ArrivalsMonitor("SecurityExit");

        provider.BaggageStatus(712, "Detroit", 3);
        observer1.Subscribe(provider);

        provider.BaggageStatus(712, "Kalamazoo", 3);
        provider.BaggageStatus(400, "New York-Kennedy", 1);
        provider.BaggageStatus(712, "Detroit", 3);
        observer2.Subscribe(provider);

        provider.BaggageStatus(511, "San Francisco", 2);
        provider.BaggageStatus(712);
        observer2.Unsubscribe();

        provider.BaggageStatus(400);
        provider.LastBaggageClaimed();
    }
}

