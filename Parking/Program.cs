using System;
namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello User, Welcome to Parking emulator programm!");

            while (true)
            {
                Console.WriteLine(
@"Choose your destiny!
1 - new car
2 - all cars
3 - remove car
4- show parking balance
5 - show last minute transactions
6 - add balance to car");

                int choise = Convert.ToInt32(Console.ReadLine());

                switch (choise)
                {
                    case 1:
                        Console.WriteLine("You choose option to add car to Parking. PLease enter data");
                        Console.WriteLine("You have to enter balance first");
                        double balance = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Please enter CarType");
                        string carType = Console.ReadLine();
                        Car newCar = new Car(balance, (CarType)Enum.Parse(typeof(CarType), carType));
                        Parking.Instance.AddCar(newCar);
                        break;
                    case 2:
                        Console.WriteLine("You choose option to show all cars");
                        var cars = Parking.Instance.CarList;
                        foreach (var car in cars)
                        {
                            Console.WriteLine(car);
                        }
                        break;
                    case 3:
                        Console.WriteLine("You choose option to show remove a car");
                        var carId = Guid.Parse(Console.ReadLine());
                        if (Parking.Instance.RemoveCar(carId))
                        {
                            Console.WriteLine("Car removed successfully");
                        }
                        else
                        {
                            Console.WriteLine("Car not removed");
                        }
                        break;
                    case 4:
                        Console.WriteLine("You choose option to show balance of parking");
                        Console.WriteLine(Parking.Instance.GetTotalRevenue());
                        break;
                    case 5:
                        Console.WriteLine("You choose option to show last minute transactions");
                        foreach (var transaction in Parking.Instance.LastMinuteTransactions)
                        {
                            Console.WriteLine(transaction);
                        }
                        break;
                    case 6:
                        Console.WriteLine("You choose option to add money to car. Please enter data");
                        Console.WriteLine("You have to enter amount first");
                        double amount = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("Please enter Car id");
                        var carToAddMoneyId = Guid.Parse(Console.ReadLine());
                        Parking.Instance.AddMoneyToCar(carToAddMoneyId, amount);
                        break;
                    default:
                        Console.WriteLine("Please check your input and thank you for using Parking emulator programm! :)");
                        break;
                }
            }
        }

    }
}


