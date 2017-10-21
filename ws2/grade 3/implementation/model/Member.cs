using System;
using System.Linq;
using System.Collections.Generic;

namespace MemberRegistry.model
{
    public class Member
    {
        public string Name {get; set;}
        public int PersonalNumber {get; set;}
        public string Password {get; set;}
        public bool IsLoggedIn {get; set;}
        public int MemberID {get; set;}
        public List<Boat> Boats {get; set;}

        public Member(string name, string password, int number, int id)
        {
            this.Name = name;
            this.Password = password;
            this.PersonalNumber = number;
            this.MemberID = id;
            this.IsLoggedIn = false;

            this.Boats = new List<Boat>();
        }

        public model.Boat GetBoat(int id)
        {
			return this.Boats.Where(boat => boat.BoatID == id).ToList()[0];
        }

        public void AddBoat(BoatType type, int length)
        {
            int newId = GetNextBoatID();
			Boat newBoat = new Boat(type, length, newId);
			this.Boats.Add(newBoat);
        }

        public void RemoveBoat(model.Boat boat) 
        {
            this.Boats.Remove(boat);
        }

        public void UpdateBoat(model.Boat boat, BoatType type, int length) 
        {
            boat.Update(type, length);
        }

        private int GetNextBoatID()
        {
            int next = this.Boats.Count > 0 ? this.Boats[this.Boats.Count - 1].BoatID + 1 : 1;
			return next;
        } 
    }
}