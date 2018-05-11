using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.IO;

namespace Parking
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> Lazy = new Lazy<Parking>(() => new Parking());

        private readonly IObservable<long> _carBalanceChargingObservable = Observable.Interval(TimeSpan.FromMilliseconds(Settings.TimeoutInMilliseconds));
        private readonly IObservable<long> _loggingTransactionObservable = Observable.Interval(TimeSpan.FromMinutes(1));

        private List<Car> _carList = new List<Car>();
        private List<Transaction> _transactionList = new List<Transaction>();
        public double Balance { get; private set; }

        public static Parking GetInstance => Lazy.Value;
        

        public Parking()
        {
            _carBalanceChargingObservable.Subscribe(t => ChargeMoneyFromCars());
            _loggingTransactionObservable.Subscribe(t => LogLastMinutesTransactions());
            Balance = 0;
        }

        public static Parking GetParking() => Lazy.Value;

        public double GetTotalRevenue() => Balance;

        public IReadOnlyCollection<Car> CarList => _carList.AsReadOnly();

        private void ChargeMoneyFromCars()
        {
            foreach(var car in CarList.ToList())
                car.ChargeMoneyForParking(Settings.Prices[car.Type]);
        }

        private void LogLastMinutesTransactions()
        {
            using (var fileStream = File.AppendText("Transactions.log"))
            {
                var lastMinuteTransactions = LastMinuteTransactions;

                var lastMinuteSum = lastMinuteTransactions.Sum(x => x.Amount);
                var lastMinuteStartTime = lastMinuteTransactions.Select(x => x.DateOfCreation).Min();
                fileStream.WriteLine($"{lastMinuteStartTime} : {lastMinuteSum}");
            }
        }

        public bool AddCar(Car car)
        {
            if (_carList.Count < Settings.ParkingSpace)
            {
                _carList.Add(car);
                return true;
            }
            return false;
        }

        public bool RemoveCar(Guid carId)
        {
            var car = _carList.FirstOrDefault(c => c.Id == carId);

            return RemoveCar(car);
        }

        public bool RemoveCar(Car car)
        {
            if (car == null || !_carList.Contains(car))
                return false;

            if (!car.BalanceIsPositive)
                return false;
            
            _carList.Remove(car);
            return true;
        }

        public Car GetCarById(Guid id)
        {
            return _carList.FirstOrDefault(c => c.Id == id);
        }

        public void AddMoneyToCar(Guid carId, double balance)
        {
            var car = _carList.FirstOrDefault(c => c.Id == carId);

            if (car != null)
            {
                car.AddMoneyToBalance(balance);
            }
        }

        public List<Transaction> LastMinuteTransactions => _transactionList.Where(tr => tr.DateOfCreation >= DateTime.UtcNow.AddMinutes(-1)).ToList();

        public int FreeSpaces => Settings.ParkingSpace - _carList.Count;
    }
}
