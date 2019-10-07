using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoadTrip
{
    class Traveler
    {
        public int ID; //for index
        public string travelerName; //for traveler name
        public string travelerID; //for traveler id
        public string address; //for traveler address
        public string phoneNumber; //for traveler phone number
        public string tripName; //for the name of the trip he want
        public string depDate; //for the date he want
        public string pic; //for picture path

        //Constructors

        public Traveler()
        {
        }

        public Traveler(int ID, string travelerName, string travelerID, string address, string phoneNumber, string tripName, string depDate, string pic)
        {
            this.ID = ID;
            this.travelerName = travelerName;
            this.travelerID = travelerID;
            this.address = address;
            this.phoneNumber = phoneNumber;
            this.tripName = tripName;
            this.depDate = depDate;
            this.pic = pic;
        }

        public Traveler(Traveler traveler)
        {
            this.ID = traveler.get_ID();
            this.travelerName = traveler.get_travelerName();
            this.travelerID = traveler.get_travelerID();
            this.address = traveler.get_address();
            this.phoneNumber = traveler.get_phoneNumber();
            this.tripName = traveler.get_tripName();
            this.depDate = traveler.get_depDate();
            this.pic = traveler.get_pic();
        }

        //sets

        public void set_id(int id)
        {
            this.ID = id;
        }

        public void set_travelerName(string travelerName)
        {
            this.travelerName = travelerName;
        }

        public void set_travelerID(string travelerID)
        {
            this.travelerID = travelerID;
        }

        public void set_address(string address)
        {
            this.address = address;
        }

        public void set_phoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        public void set_tripName(string tripName)
        {
            this.tripName = tripName;
        }

        public void set_depDate(string depDate)
        {
            this.depDate = depDate;
        }

        public void set_pic(string pic)
        {
            this.pic = pic;
        }

        //gets

        public int get_ID()
        {
            return this.ID;
        }

        public string get_travelerName()
        {
            return this.travelerName;
        }

        public string get_travelerID()
        {
            return this.travelerID;
        }

        public string get_address()
        {
            return this.address;
        }

        public string get_phoneNumber()
        {
            return this.phoneNumber;
        }

        public string get_tripName()
        {
            return this.tripName;
        }

        public string get_depDate()
        {
            return this.depDate;
        }

        public string get_pic()
        {
            return this.pic;
        }
    }
}
