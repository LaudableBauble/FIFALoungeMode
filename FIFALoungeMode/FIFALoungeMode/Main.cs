using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FIFALoungeMode
{
    public partial class Main : Form
    {
        #region Fields
        private AddGame _AddGameForm;
        private EditProfiles _EditProfilesForm;
        private EditTeams _EditTeamsForm;
        private ListViewColumnSorter _StandingsSorter;
        private ListViewColumnSorter _ScorersSorter;
        private ListViewColumnSorter _GamesSorter;
        #endregion

        #region Constructors
        public Main()
        {
            //Initialize components.
            InitializeComponent();

            //The title.
            this.Text = "FIFA Lounge Mode";

            //Set the window size.
            this.Width = 600;
            this.Height = 300;

            //Load the summary and all profiles.
            Helper.LoadSummary();
            Dictionary<Player, int> scores = Summary.Instance.GetTopScorers();

            //Create the tab control.
            tabMain.Width = this.Width;
            tabMain.Height = this.Height - this.MainMenuStrip.Height;
            tabMain.Location = new Point(0, this.MainMenuStrip.Height);

            //Create the board.
            lstvStandings.View = View.Details;
            lstvStandings.LabelEdit = false;
            lstvStandings.AllowColumnReorder = false;
            lstvStandings.GridLines = true;

            //Create an instance of a ListView column sorter and assign it to the ListView control.
            _StandingsSorter = new ListViewColumnSorter();
            _ScorersSorter = new ListViewColumnSorter();
            _GamesSorter = new ListViewColumnSorter();
            lstvStandings.ListViewItemSorter = _StandingsSorter;
            lstvScorers.ListViewItemSorter = _ScorersSorter;
            lstvGames.ListViewItemSorter = _GamesSorter;

            //Create the scorer's board.
            lstvScorers.View = View.Details;
            lstvScorers.LabelEdit = false;
            lstvScorers.AllowColumnReorder = false;
            lstvScorers.GridLines = true;

            //Create the list of recent games.
            lstvGames.View = View.Details;
            lstvGames.LabelEdit = false;
            lstvGames.AllowColumnReorder = false;
            lstvGames.GridLines = false;

            //Set up the boards.
            SetUpBoards();

            //Subscribe to events.
            this.FormClosed += OnFormClose;
            lstvStandings.ColumnClick += OnColumnClick;
            lstvScorers.ColumnClick += OnColumnClick;
            lstvGames.ColumnClick += OnColumnClick;
            mnuItemAddGame.Click += OnAddGameMenuClick;
            mnuItemEditProfiles.Click += OnEditProfilesMenuClick;
            mnuItemEditTeams.Click += OnEditTeamsMenuClick;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set up the boards.
        /// </summary>
        public void SetUpBoards()
        {
            //Reset the boards.
            lstvStandings.Clear();
            lstvScorers.Clear();
            lstvGames.Items.Clear();

            //Add the columns.
            lstvStandings.Columns.Add("Profile", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Points", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Played", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Wins", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Draws", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Losses", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Scored", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Conceded", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Difference", -2, HorizontalAlignment.Left);
            lstvStandings.Columns.Add("Form Curve", -2, HorizontalAlignment.Left);

            lstvScorers.Columns.Add("Position", -2, HorizontalAlignment.Left);
            lstvScorers.Columns.Add("Player", -2, HorizontalAlignment.Left);
            lstvScorers.Columns.Add("Goals", -2, HorizontalAlignment.Left);
            lstvScorers.Columns.Add("Team", -2, HorizontalAlignment.Left);

            lstvGames.Columns.Add("Date", -2, HorizontalAlignment.Left);
            lstvGames.Columns.Add("Home Profile", -2, HorizontalAlignment.Left);
            lstvGames.Columns.Add("Result", -2, HorizontalAlignment.Left);
            lstvGames.Columns.Add("Away Profile", -2, HorizontalAlignment.Left);
            lstvGames.Columns.Add("Home Team", -2, HorizontalAlignment.Left);
            lstvGames.Columns.Add("Away Team", -2, HorizontalAlignment.Left);

            //Add the boards.
            UpdateBoards();
        }
        /// <summary>
        /// Update the boards.
        /// </summary>
        public void UpdateBoards()
        {
            //Remove all items.
            lstvStandings.Items.Clear();
            lstvScorers.Items.Clear();
            lstvGames.Items.Clear();

            //Add the profiles.
            foreach (Profile profile in Summary.Instance.Profiles)
            {
                //Add a profile.
                ListViewItem p = new ListViewItem(profile.Name, 0);

                //The data.
                p.SubItems.Add(profile.Points.ToString());
                p.SubItems.Add(profile.Games.ToString());
                p.SubItems.Add(profile.Wins.ToString());
                p.SubItems.Add(profile.Draws.ToString());
                p.SubItems.Add(profile.Losses.ToString());
                p.SubItems.Add(profile.GoalsScored.ToString());
                p.SubItems.Add(profile.GoalsConceded.ToString());
                p.SubItems.Add((profile.GoalsScored - profile.GoalsConceded).ToString());
                p.SubItems.Add(Summary.Instance.GetFormCurve(profile, 5));

                //Add the profile row to the board.
                lstvStandings.Items.Add(p);
            }

            //The index.
            int index = 1;

            //Add the top scorers.
            foreach (KeyValuePair<Player, int> pair in Summary.Instance.GetTopScorers())
            {
                //Limit the list to 10 players.
                if (index > 10) { break; }

                //Add a player.
                ListViewItem p = new ListViewItem(index.ToString(), 0);

                //The data.
                p.SubItems.Add(pair.Key.ToString());
                p.SubItems.Add(pair.Value.ToString());
                p.SubItems.Add(pair.Key.Team.Name);

                //Add the player row to the board.
                lstvScorers.Items.Add(p);

                //Increment the index.
                index++;
            }

            //The list of games, reversed.
            List<Game> games = new List<Game>(Summary.Instance.Games);
            games.Reverse();

            //Add the recent games.
            foreach (Game game in games)
            {
                //Add a item.
                ListViewItem g = new ListViewItem(game.Date.ToString("d'/'M'/'y"), 0);

                //The data.
                g.SubItems.Add(game.HomeFacts.Profile.Name);
                g.SubItems.Add(game.GetResult());
                g.SubItems.Add(game.AwayFacts.Profile.Name);
                g.SubItems.Add(game.HomeFacts.Team.ToString());
                g.SubItems.Add(game.AwayFacts.Team.ToString());

                //Add the item row to the board.
                lstvGames.Items.Add(g);
            }
        }
        /// <summary>
        /// Update the profiles with data from the form's list of games.
        /// </summary>
        private void UpdateProfiles()
        {
            //For each profile.
            foreach (Profile profile in Summary.Instance.Profiles)
            {
                //For each game add it to the profile.
                foreach (Game game in Summary.Instance.Games) { profile.AddGame(game); }
            }
        }

        /// <summary>
        /// If a column in the board has been clicked, sort it.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnColumnClick(object o, ColumnClickEventArgs e)
        {
            //Get the list view and column sorter.
            ListView listView = (o as ListView);
            ListViewColumnSorter sorter = (listView.ListViewItemSorter as ListViewColumnSorter);

            // Determine if the clicked column is already the column that is being sorted.
            if (e.Column == sorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (sorter.Order == SortOrder.Ascending) { sorter.Order = SortOrder.Descending; }
                else { sorter.Order = SortOrder.Ascending; }
            }
            else
            {
                // Set the column number that is to be sorted; default to descending.
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Descending;
            }

            // Perform the sort with these new sort options.
            listView.Sort();
        }
        /// <summary>
        /// If the user wants to add a game to the mix.
        /// </summary>
        private void OnAddGameMenuClick(object sender, EventArgs e)
        {
            //If there already is an add game form open, do nothing.
            if (_AddGameForm != null) { return; }

            //Display a form where the user can enter game data.
            _AddGameForm = new AddGame();
            _AddGameForm.Show();

            //Subscribe to its events.
            _AddGameForm.Closing += OnAddGameFormClose;
        }
        /// <summary>
        /// If the user wants to edit the profiles.
        /// </summary>
        private void OnEditProfilesMenuClick(object sender, EventArgs e)
        {
            //If there already is an edit profiles form open, do nothing.
            if (_EditProfilesForm != null) { return; }

            //Display a form where the user can enter profile data.
            _EditProfilesForm = new EditProfiles();
            _EditProfilesForm.Show();

            //Subscribe to its events.
            _EditProfilesForm.Closing += OnEditProfilesFormClose;
        }
        /// <summary>
        /// If the user wants to edit the teams.
        /// </summary>
        private void OnEditTeamsMenuClick(object sender, EventArgs e)
        {
            //If there already is an edit teams form open, do nothing.
            if (_EditTeamsForm != null) { return; }

            //Display a form where the user can enter game data.
            _EditTeamsForm = new EditTeams();
            _EditTeamsForm.Show();

            //Subscribe to its events.
            _EditTeamsForm.Closing += OnEditTeamsFormClose;
        }
        /// <summary>
        /// When the add game form has been closed.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnAddGameFormClose(object o, EventArgs e)
        {
            //Redraw the boards.
            UpdateBoards();

            //Unsubscribe from the control and close it.
            _AddGameForm.Closing -= OnAddGameFormClose;
            _AddGameForm = null;
        }
        /// <summary>
        /// When the edit profiles form has been closed.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnEditProfilesFormClose(object o, EventArgs e)
        {
            //Unsubscribe from the control and close it.
            _EditProfilesForm.FormClosing -= OnEditProfilesFormClose;
            _EditProfilesForm = null;
        }
        /// <summary>
        /// When the edit teams form has been closed.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnEditTeamsFormClose(object o, EventArgs e)
        {
            //Unsubscribe from the control and close it.
            _EditTeamsForm.FormClosing -= OnEditTeamsFormClose;
            _EditTeamsForm = null;
        }
        /// <summary>
        /// When the main form has closed.
        /// </summary>
        /// <param name="e"></param>
        private void OnFormClose(object o, FormClosedEventArgs e)
        {
            //Save the summary.
            Helper.SaveSummary(Summary.Instance);
        }
        #endregion
    }
}
