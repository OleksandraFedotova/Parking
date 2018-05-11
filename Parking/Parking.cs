using System;
using System.Collections.Generic;

namespace Parking
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> Lazy = new Lazy<Parking>(() => new Parking());

        public List<Car> CarList;
        public List<Transaction> TransactionList;
        public double Balanse { get; private set; }

        public static Parking GetInstance => Lazy.Value;

        public Parking()
        {
            Balanse = 0;
        }

        public static Parking GetParking() => Lazy.Value;

        public decimal GetTotalRevenue() => Balance;

    }
}
