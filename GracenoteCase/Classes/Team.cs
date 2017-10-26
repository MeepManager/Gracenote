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

        public Person GetPerson(Person person)
        {
            Person player;

            Players.TryGetValue(person.ID, out player);
            if (player == null)
            { 
                Players.Add(person.ID, person);
                player = person;
            }
            return player;
        }
    }
}
