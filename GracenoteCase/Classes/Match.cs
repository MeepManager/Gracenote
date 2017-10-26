using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GracenoteCase.Classes
{
    public class Match
    {
        public int ID { get; set; }
        public string Competition { get; set; }
        public string Label { get; set; }
        public DateTime Date { get; set; }

        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Action> Actions { get; set; }

        public List<Person> Officials;

        public Match(int id)
        {
            ID = id;
            Officials = new List<Person>();
            Actions = new List<Action>();
        }

        public void SetLabel()
        {
            Label = Competition + " " + HomeTeam.Name + " tegen " + AwayTeam.Name;
        }
    }
}
