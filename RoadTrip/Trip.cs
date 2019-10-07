using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadTrip
{
    class Trip
    {
        public int ID; //for index
        public string tripName; //for trip name
        public string pic; //for picture path
        public string video; //for video path
        public float cost; //for trip cost
        public string[] departureDate = new string[4]; //for 4 dates
        public int time; //for trip time

        //Constructors

        public Trip()
        {
        }

        public Trip(int ID, string tripName, string pic, string video, float cost, string date1, string date2 ,string date3 ,string date4, int time)
        {
            this.ID = ID;
            this.tripName = tripName;
            this.pic = pic;
            this.video = video;
            this.cost = cost;
            this.departureDate[0] = date1;
            this.departureDate[1] = date2;
            this.departureDate[2] = date3;
            this.departureDate[3] = date4;
            this.time = time;
        }

        public Trip(Trip trip)
        {
            this.ID = trip.get_id();
            this.tripName = trip.get_tripName();
            this.pic = trip.get_pic();
            this.video = trip.get_video();
            this.cost = trip.get_cost();
            this.departureDate[0] = trip.get_departureDate(0);
            this.departureDate[1] = trip.get_departureDate(1);
            this.departureDate[2] = trip.get_departureDate(2);
            this.departureDate[3] = trip.get_departureDate(3);
            this.time = trip.get_time();
        }

        //sets

        public void set_id(int id)
        {
            this.ID = id;
        }

        public void set_tripName(string tripName)
        {
            this.tripName = tripName;
        }

        public void set_pic(string pic)
        {
            this.pic = pic;
        }

        public void set_video(string video)
        {
            this.video = video;
        }

        public void set_cost(float cost)
        {
            this.cost = cost;
        }

        public void set_departureDate(string[] departureDate)
        {
            this.departureDate = departureDate;
        }

        public void set_time(int time)
        {
            this.time = time;
        }

        //gets

        public int get_id()
        {
            return this.ID;
        }

        public string get_tripName()
        {
            return this.tripName;
        }

        public string get_pic()
        {
            return this.pic;
        }

        public string get_video()
        {
            return this.video;
        }

        public float get_cost()
        {
            return this.cost;
        }

        public string get_departureDate(int ind)
        {
            return this.departureDate[ind];
        }

        public int get_time()
        {
            return this.time;
        }
    }
}
