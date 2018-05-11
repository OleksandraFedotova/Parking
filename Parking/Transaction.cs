using System;

namespace Parking
{
    public class Transaction
    {
        public DateTime DateOfCreation { get; }
        public Guid CarId { get; }
        public double Amount { get; }

        public Transaction(DateTime time, Guid carId, double amount)
        {
            DateOfCreation = time;
            CarId = carId;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"{DateOfCreation} {CarId} {Amount}";
        }
    }
}