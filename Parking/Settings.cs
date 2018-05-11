using System.Collections.Generic;

namespace Parking
{
    public class Settings
    {
        public int Timeout = 3;
        public Dictionary<CarType, double> PriceList;
        public int ParkingSace;
        public double Fine;

    }
}
