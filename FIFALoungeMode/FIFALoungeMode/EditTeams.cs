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
    public partial class EditTeams : Form
    {
        #region Fields
        private Team _Team;
        private Player _Player;
        #endregion

        #region Constructors
        /// <summary>
        /// Create the edit team form.
        /// </summary>
        public EditTeams()
        {
            Initialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the form.
        /// </summary>
        private void Initialize()
        {
            //Initialize some components.
            InitializeComponent();

            //Initialize some variables.
            _Team = null;
            _Player = null;

            //Make the edit controls invisible.
            grpbEditPlayer.Visible = false;
            lblPlayerName.Visible = false;
            txbPlayerName.Visible = false;

            //Rename the controls' text.
            lblTeamName.Text = "Name:";
            grpbEditPlayer.Text = "Edit Player";
            lblPlayers.Text = "The players";
            lblPlayerName.Text = "Player:";
            txbTeamName.Text = (_Team != null) ? _Team.Name : "";
            txbPlayerName.Text = "";
            btnAddPlayer.Text = "Add Player";
            btnAddTeam.Text = "Add Team";
            btnFinish.Text = "Finish";

            //Add items to the combobox.
            foreach (Team team in Summary.Instance.Teams) { cmbTeams.Items.Add(team); }
            cmbTeams.SelectedIndex = 0;

            //Select the first team in the list.
            SelectTeam(Summary.Instance.Teams[0]);

            //Select the first player in the list.
            lstbPlayers.SelectedIndex = (lstbPlayers.Items.Count > 0) ? 0 : -1;

            //Subscribe to events. NOTE: The lstbPlayer selection event is managed in the RefreshPlayerList method.
            cmbTeams.SelectedIndexChanged += OnTeamChange;
            btnAddPlayer.Click += OnAddPlayerClick;
            btnAddTeam.Click += OnAddTeamClick;
            btnFinish.Click += OnFinishClick;
        }

        /// <summary>
        /// Refresh the list of teams.
        /// </summary>
        private void RefreshTeamList()
        {
            //Refresh the list of teams.
            cmbTeams.Items.Clear();
            foreach (Team team in Summary.Instance.Teams) { cmbTeams.Items.Add(team); }
            cmbTeams.SelectedIndex = (cmbTeams.Items.Count > 0) ? 0 : -1;
        }
        /// <summary>
        /// Refresh the list of players.
        /// </summary>
        private void RefreshPlayerList()
        {
            //Start by unsubscribing from the player list's selection event.
            lstbPlayers.SelectedIndexChanged -= OnEditPlayerSelect;

            //If the list needs to be filled, fill it.
            if (_Team.Players.Count != lstbPlayers.Items.Count) { lstbPlayers.Items.Clear(); foreach (Player p in _Team.Players) { lstbPlayers.Items.Add(p); } }
            //Else, just refresh the list of players.
            else { for (int i = 0; i < lstbPlayers.Items.Count; i++) { lstbPlayers.Items[i] = _Team.Players[i]; } }

            //End by resubscribing to the player list's selection event.
            lstbPlayers.SelectedIndexChanged += OnEditPlayerSelect;
        }
        /// <summary>
        /// Select a team to edit.
        /// </summary>
        /// <param name="team">The team to edit.</param>
        private void SelectTeam(Team team)
        {
            //Perform the necessary arrangements.
            _Team = team;
            txbTeamName.Text = team.Name;
            RefreshPlayerList();
        }
        /// <summary>
        /// Select a player to edit.
        /// </summary>
        /// <param name="player">The player to edit.</param>
        private void SelectPlayer(Player player)
        {
            //Save the modified player name.
            if (_Player != null) { _Player.Name = txbPlayerName.Text; }

            //Perform the necessary arrangements.
            _Player = player;
            RefreshPlayerList();

            if (_Player != null)
            {
                //Enable modification of the player's name.
                txbPlayerName.Text = _Player.Name;

                //Make the edit controls visible.
                grpbEditPlayer.Visible = true;
                lblPlayerName.Visible = true;
                txbPlayerName.Visible = true;
            }
            else
            {
                //Make the edit controls invisible.
                grpbEditPlayer.Visible = false;
                lblPlayerName.Visible = false;
                txbPlayerName.Visible = false;
            }
        }
        /// <summary>
        /// If the user wants to change the team to be edited.
        /// </summary>
        private void OnTeamChange(object sender, EventArgs e)
        {
            //Save the modified team name.
            if (_Team != null) { _Team.Name = txbTeamName.Text; }
            //Save the team.
            Helper.SaveTeam(_Team);
            //Select the new team.
            SelectTeam(Summary.Instance.Teams[cmbTeams.SelectedIndex]);
        }
        /// <summary>
        /// If the user wants to add a team.
        /// </summary>
        private void OnAddTeamClick(object sender, EventArgs e)
        {
            //Create a new team and add him.
            Team team = new Team("team1");
            Summary.Instance.Teams.Add(team);
            //Add the team to the combo box.
            cmbTeams.Items.Add(team);
            //Select the team.
            cmbTeams.SelectedIndex = cmbTeams.Items.Count - 1;
        }
        /// <summary>
        /// If the user wants to add a player to a team.
        /// </summary>
        private void OnAddPlayerClick(object sender, EventArgs e)
        {
            //Create a new player and add him to the team.
            Player player = new Player("player1", _Team);
            _Team.Players.Add(player);
            //Add the player to the list box.
            lstbPlayers.Items.Add(player);
        }
        /// <summary>
        /// If the user selects a player to edit.
        /// </summary>
        private void OnEditPlayerSelect(object sender, EventArgs e)
        {
            //If an item truly has been selected.
            if (lstbPlayers.SelectedIndex == -1) { return; }

            //Put a lease on the selected player.
            SelectPlayer((Player)lstbPlayers.SelectedItem);
        }
        /// <summary>
        /// If the user is finished.
        /// </summary>
        private void OnFinishClick(object sender, EventArgs e)
        {
            //Save the modified team and player name.
            if (_Team != null) { _Team.Name = txbTeamName.Text; }
            if (_Player != null) { _Player.Name = txbPlayerName.Text; }

            //Save the team.
            Helper.SaveTeam(_Team);

            //Tell the summary to reload the profiles.
            Summary.Instance.LoadAllProfiles();

            //Let go of the player and the team.
            _Team = null;
            _Player = null;

            //Close the form.
            this.Close();
        }
        #endregion
    }
}
