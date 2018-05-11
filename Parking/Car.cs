using System;

namespace Parking
{
    public class Car
    {
        private readonly double _fineForNegativeBalance;

        private double _balance;
        
        public Car(double balance, CarType type)
        {
            Id = Guid.NewGuid();
            Type = type;
            _balance = balance;
            _fineForNegativeBalance = Settings.Fine * Settings.Prices[type];
        }

        public CarType Type { get; }

        public Guid Id { get; }

        public bool BalanceIsPositive => _balance >= 0;
        
        public void AddMoneyToBalance(double amountToAdd)
        {
            if(_balance < 0)
            {
                var negativePart = -_balance;
                
                amountToAdd -= negativePart * _fineForNegativeBalance;

                if (amountToAdd >= 0)
                    _balance += amountToAdd;
                else
                    _balance += amountToAdd / _fineForNegativeBalance;
            }

            _balance += amountToAdd;
        }

        public void ChargeMoneyForParking(double chargingAmount)
        {
            _balance -= chargingAmount;
        }
    }
}
