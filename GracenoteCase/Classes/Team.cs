using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GracenoteCase.Classes
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Dictionary<int, Person> Players { get; set; }

        public Team(int id, string name)
        {
            ID = id;
            Name = name;
            Players = new Dictionary<int, Person>(); //Initialize the dictionary
        }

        public void AddPerson(int personID, string name, string function, string shirtNr)
        {
            Person player = GetPerson(personID);
            player.Name = name;
            if(function != "NULL")
                player.Function = function;
            if (shirtNr != "NULL")
                player.ShirtNr = Convert.ToInt32(shirtNr);
        }
        public Person GetPerson(int personID)
        {
            Person player;

            Players.TryGetValue(personID, out player);
            if (player == null)
            {
                player = new Person();
                player.ID = personID;
                Players.Add(personID, player);
            }
            return player;
        }
    }
}
