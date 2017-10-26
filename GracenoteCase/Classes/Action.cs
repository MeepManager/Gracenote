using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GracenoteCase.Classes
{
    public class Action
    {
        public int ID { get; set; }
        public int MatchID { get; set; }
        public int TeamID { get; set; }

        public string Description { get; set; }
        public string Reason { get; set; }
        public string Info { get; set; }

        public string Period { get; set; }
        public int? StartTime { get; set; }
        public int? EndTime { get; set; }
        public string HomeOrAway { get; set; }

        public int PersonID { get; set; }
        public int? SubpersonID { get; set; }

        public Action(string[] data)
        {
            ID = Convert.ToInt32(data[0]);
            MatchID = Convert.ToInt32(data[2]);
            TeamID = Convert.ToInt32(data[9]);

            Description = data[4];
            Reason = data[15];
            Info = data[16];

            Period = (IsNULL(data[5]) == false) ? data[5] : null;
            StartTime = (IsNULL(data[6]) == false) ? Convert.ToInt32(data[6]) : (int?)null;
            EndTime = (IsNULL(data[7]) == false) ? Convert.ToInt32(data[7]) : (int?)null;

            PersonID = Convert.ToInt32(data[11]);
            SubpersonID = (IsNULL(data[17]) == false) ? Convert.ToInt32(data[17]) : (int?)null;
        }

        public static bool IsNULL(string field)
        {
            return field == "NULL";
        }
    }
}
