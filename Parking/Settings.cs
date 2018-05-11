using System.Collections.Generic;

namespace Parking
{
    public class Settings
    {
        public static int TimeoutInMilliseconds;
        public static int ParkingSpace;
        public static double Fine;
        public static Dictionary<CarType, double> Prices = new Dictionary<CarType, double>
        {
            [CarType.Passenger] = 1,
            [CarType.Truck] = 2,
            [CarType.Bus] = 4,
            [CarType.Motorcycle] = 5
        };

        static Settings()
        {
            //Milliseconds
            TimeoutInMilliseconds = 3000;
            ParkingSpace = 50;
            Fine = 3;
        }

    }
}
