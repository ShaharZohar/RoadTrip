using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadTrip
{
    class AllTrips
    {
        private Trip[] Trips;
        private int num_trips;

        //Constructor

        public AllTrips(int size)
        {
            this.Trips = new Trip[size];
            this.num_trips = 0;
        }

        public void Add_Trips(Trip trip) //adding a Trip to Trips array
        {
            if (this.num_trips < Trips.Length)
            {
                Trips[this.num_trips] = trip;
                this.num_trips++;
            }
        }

        public Trip Get_Trip_By_index(int i) //get the Trip by index
        {
            if (i < num_trips)
                return Trips[i];
            return null;
        }
    }
}
