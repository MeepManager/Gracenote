using GracenoteCase.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GracenoteCase
{
    public partial class MainWindow : Form
    {
        private Dictionary<int, Match> matches;
        private Match selectedMatch;
        private Person selectedPerson;

        public MainWindow()
        {
            InitializeComponent();
            matches = new Dictionary<int, Match>();
            selectedPerson = null;

            comboBox1.SelectedValueChanged += ComboBox1_SelectedValueChanged;
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //selectedMatch = GetMatch(Convert.ToInt32(matchListBox.SelectedValue));
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //TO:DO
            //Parse data file
            string csvData = File.ReadAllText(@"C:\Users\Jari\source\repos\GracenoteCase\GracenoteCase\Dataset 2rounds Eredivie 20172018.csv");
            //Execute a loop over the rows.  
            bool headers = false;
            foreach (string row in csvData.Split('\n'))
            {
                if(!headers)
                {
                    headers = true;
                    continue;
                }
                if (!string.IsNullOrEmpty(row))
                {
                    string[] data = row.Split(','); //Get the field data in a string array
                    //Create the match first
                    Match match = GetMatch(Convert.ToInt32(data[2]));
                    if(match.Competition == null) //means this is the first time the match is found in the dataset
                    {
                        match.Competition = data[1]; //8/11/2017  7:00:00 PM
                        //match.Date = DateTime.ParseExact(data[3], "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                        match.Date = DateTime.Parse(data[3]);
                    }
                    
                    //Create the team afterwards



                    //Add the actions to the match
                    match.Actions.Add(new Classes.Action(data));
                }
            }
            //Insert into matches

            //Fill the dropdown
            comboBox1.DataSource = matches.Values.ToList();
            comboBox1.DisplayMember = "Label";
            comboBox1.ValueMember = "ID";
            //OnSelect show match line up
            //OnHover show player info
        }

        //Used for checking if the match is already created
        private Match GetMatch(int matchID)
        {
            Match match;

            matches.TryGetValue(matchID, out match);
            if (match == null)
            {
                match = new Match(matchID);
                matches.Add(matchID, match);
            }
            return match;
        }
    }
}
