using System.Collections.Generic;

namespace SharedTrip.ViewModels.Trips
{
    public class AddTripFormModel
    {
        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public string ImagePath { get; set; }

        public int Seats { get; set; }

        public string Description { get; set; }
    }
}
