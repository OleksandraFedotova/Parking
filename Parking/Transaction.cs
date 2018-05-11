using System;

namespace Parking
{
    public class Transaction
    {
        public DateTime DateOfCreation { get; }
        public Guid CarId { get; }
        public decimal Amount { get; }

        public Transaction(DateTime time, Guid carId, decimal amount)
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