using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadTrip
{
    class AllTravelers
    {
        private Traveler[] Travelers;
        private int num_travelers;

        //Constructor

        public AllTravelers(int size)
        {
            this.Travelers = new Traveler[size];
            this.num_travelers = 0;
        }

        public void Add_Travelers(Traveler traveler) //adding a traveler to Travelers array
        { 
            if (this.num_travelers < Travelers.Length)
            {
                Travelers[this.num_travelers] = traveler;
                this.num_travelers++;
            }
        }

        public Traveler Get_Trav_By_index(int i) //get the traveler by index
        {
            if (i < num_travelers)
                return Travelers[i];
            return null;
        }
    }
}
