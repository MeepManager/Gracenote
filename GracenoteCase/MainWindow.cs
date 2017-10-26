using GracenoteCase.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GracenoteCase
{
    public partial class MainWindow : Form
    {
        private Dictionary<int, Match> matches;
        private Match selectedMatch;

        public MainWindow()
        {
            InitializeComponent();
            matches = new Dictionary<int, Match>();

            comboBox1.SelectedValueChanged += ComboBox1_SelectedValueChanged;
        }

        private void ComboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedMatch = (Match)comboBox1.SelectedItem;
            //Calculate statistics
            Dictionary<string, Statistic> Statistics = new Dictionary<string, Statistic>();
            Statistics.Add("Goal", new Statistic("Goals"));
            Statistics.Add("Free kick", new Statistic("Free kicks"));
            Statistics.Add("Yellow", new Statistic("Yellow"));
            Statistics.Add("Corner", new Statistic("Corners"));

            foreach (var action in selectedMatch.Actions)
            {

                Statistics.TryGetValue(action.Description, out Statistic stat);
                if (stat != null)
                    stat.Add(action);
            }
            List<KeyValuePair<string, Statistic>> list = new List<KeyValuePair<string, Statistic>>();
            list.AddRange(Statistics);

            dataGridView1.DataSource = list;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            List<string> homeTeam = new List<string>();
            foreach(var player in selectedMatch.HomeTeam.Players.Values)
            {
                homeTeam.Add(player.ShirtNr + " " + player.Name);
            }
            List<string> awayTeam = new List<string>();
            foreach (var player in selectedMatch.AwayTeam.Players.Values)
            {
                awayTeam.Add(player.ShirtNr + " " + player.Name);
            }

            listBox1.DataSource = homeTeam;
            listBox2.DataSource = awayTeam;

        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //TO:DO
            //Parse data file
            string csvData = File.ReadAllText(Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), @"Dataset 2rounds Eredivie 20172018.csv"));
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
                    if (data[8] == "Home")
                    {
                        if (match.HomeTeam == null)
                        {
                            match.HomeTeam = new Team(Convert.ToInt32(data[9]), data[10]);
                        }
                        match.HomeTeam.AddPerson(Convert.ToInt32(data[11]), data[12], data[13], data[14]); //add player to home team
                    }
                    else if (data[8] == "Away")
                    {
                        if (match.AwayTeam == null)
                        {
                            match.AwayTeam = new Team(Convert.ToInt32(data[9]), data[10]);
                        }
                        match.AwayTeam.AddPerson(Convert.ToInt32(data[11]), data[12], data[13], data[14]); //Add player to away team
                    }

                    if (match.HomeTeam != null && match.AwayTeam != null)
                        match.SetLabel();

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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
