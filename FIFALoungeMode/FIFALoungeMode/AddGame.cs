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
    public partial class AddGame : Form
    {
        #region Fields
        private AddGoal _AddGoalForm;
        #endregion

        #region Constructors
        /// <summary>
        /// Create the form.
        /// </summary>
        public AddGame()
        {
            //Initialize the form.
            Initialize();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the form.
        /// </summary>
        public void Initialize()
        {
            //Initialize the components.
            InitializeComponent();

            //Rename the controls' text.
            lblHomeShots.Text = "Number of shots:";
            lblHomeShotsOnTarget.Text = "Shots on target:";
            lblHomeShotAccuracy.Text = "Shot Accuracy:";
            lblHomePassAccuracy.Text = "Pass Accuracy:";
            lblHomeCorners.Text = "Corners:";
            lblHomePossession.Text = "";
            lblHomeYellowCards.Text = "Yellow Cards:";
            lblHomeRedCards.Text = "Red Cards:";
            lblAwayShots.Text = "Number of shots:";
            lblAwayShotsOnTarget.Text = "Shots on target:";
            lblAwayShotAccuracy.Text = "Shot Accuracy:";
            lblAwayPassAccuracy.Text = "Pass Accuracy:";
            lblAwayCorners.Text = "Corners:";
            lblAwayPossession.Text = "";
            lblAwayYellowCards.Text = "Yellow Cards:";
            lblAwayRedCards.Text = "Red Cards:";
            lblPossession.Text = "Ball Possession";
            lblHomeGoals.Text = "0";
            lblAwayGoals.Text = "0";
            btnHomeGoals.Text = "Home Goal";
            btnAwayGoals.Text = "Away Goal";
            btnFinish.Text = "Finish";
            ckbExtraTime.Text = "Extra Time";
            grpbHomeTeam.Text = "Home Team";
            grpbAwayTeam.Text = "Away Team";

            nmrHomeShotAccuracy.Value = 70;
            nmrHomePassAccuracy.Value = 80;
            nmrAwayShotAccuracy.Value = 70;
            nmrAwayPassAccuracy.Value = 80;

            //Add items to the comboboxes.
            foreach (Profile profile in Summary.Instance.Profiles) { cmbHomeProfile.Items.Add(profile); cmbAwayProfile.Items.Add(profile); }
            cmbHomeProfile.SelectedIndex = 0;
            cmbAwayProfile.SelectedIndex = 0;

            //Modify the components.
            lstvHomeGoals.View = View.List;
            lstvHomeGoals.LabelEdit = false;
            lstvHomeGoals.AllowColumnReorder = false;
            lstvHomeGoals.GridLines = true;
            lstvAwayGoals.View = View.List;
            lstvAwayGoals.LabelEdit = false;
            lstvAwayGoals.AllowColumnReorder = false;
            lstvAwayGoals.GridLines = true;

            //Subscribe to the buttons' click events.
            btnHomeGoals.Click += OnbtnHomeGoalsClick;
            btnAwayGoals.Click += OnbtnAwayGoalsClick;
            btnFinish.Click += OnFinishClick;
            trkbPossession.ValueChanged += OnPossessionChange;

            //Change the ball possession to 50-50.
            trkbPossession.Maximum = 100;
            trkbPossession.Value = 50;
        }

        /// <summary>
        /// Get facts about a side's performance in the game.
        /// </summary>
        /// <param name="side">The side to get facts about.</param>
        /// <returns>The game facts.</returns>
        public GameFacts GetGameFacts(MatchSide side)
        {
            //Create the game facts instance.
            GameFacts facts = new GameFacts(null);
            facts.MatchSide = side;

            //See which side to get data from.
            if (side == MatchSide.Home)
            {
                //Fill the facts instance with data.
                facts.Profile = (Profile)cmbHomeProfile.SelectedItem;
                facts.Team = facts.Profile.Team;
                facts.Shots = (int)nmrHomeShots.Value;
                facts.ShotsOnTarget = (int)nmrHomeShotsOnTarget.Value;
                facts.ShotAccuracy = (int)nmrHomeShotAccuracy.Value;
                facts.PassAccuracy = (int)nmrHomePassAccuracy.Value;
                facts.Corners = (int)nmrHomeCorners.Value;
                facts.Possession = (100 - trkbPossession.Value);

                //The goals scored and the goals conceded.
                foreach (ListViewItem item in lstvHomeGoals.Items)
                {
                    //Find out which goals
                    facts.GoalsScored.Add((Goal)item.Tag);
                }
                foreach (ListViewItem item in lstvAwayGoals.Items) { facts.GoalsConceded.Add((Goal)item.Tag); }
            }
            else
            {
                //Fill the facts instance with data.
                facts.Profile = (Profile)cmbAwayProfile.SelectedItem;
                facts.Team = facts.Profile.Team;
                facts.Shots = (int)nmrAwayShots.Value;
                facts.ShotsOnTarget = (int)nmrAwayShotsOnTarget.Value;
                facts.ShotAccuracy = (int)nmrAwayShotAccuracy.Value;
                facts.PassAccuracy = (int)nmrAwayPassAccuracy.Value;
                facts.Corners = (int)nmrAwayCorners.Value;
                facts.Possession = trkbPossession.Value;

                //The goals scored and the goals conceded.
                foreach (ListViewItem item in lstvAwayGoals.Items) { facts.GoalsScored.Add((Goal)item.Tag); }
                foreach (ListViewItem item in lstvHomeGoals.Items) { facts.GoalsConceded.Add((Goal)item.Tag); }
            }

            //Return the facts.
            return facts;
        }
        /// <summary>
        /// When the ball possession of the game changes.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnPossessionChange(object o, EventArgs e)
        {
            //Update the two labels.
            lblHomePossession.Text = (100 - trkbPossession.Value).ToString();
            lblAwayPossession.Text = trkbPossession.Value.ToString();
        }
        /// <summary>
        /// When the button has been clicked.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnbtnHomeGoalsClick(object o, EventArgs e)
        {
            //Create a form and show it to the user.
            ShowAddGoalForm((Profile)cmbHomeProfile.SelectedItem);
        }
        /// <summary>
        /// When the button has been clicked.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnbtnAwayGoalsClick(object o, EventArgs e)
        {
            //Create a form and show it to the user.
            ShowAddGoalForm((Profile)cmbAwayProfile.SelectedItem);
        }
        /// <summary>
        /// Show and create a add goal form.
        /// </summary>
        /// <param name="profile">The profile that scored the goal.</param>
        private void ShowAddGoalForm(Profile profile)
        {
            //If there already is an add goal form open, do nothing.
            if (_AddGoalForm != null) { return; }

            //Create a form and show it to the user.
            _AddGoalForm = new AddGoal(profile);
            _AddGoalForm.Show();

            //Subscribe to its events.
            _AddGoalForm.Button.Click += OnbtnAddGoalFormClick;
            _AddGoalForm.FormClosing += OnAddGoalFormClose;
        }
        /// <summary>
        /// When the add game form closes.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnAddGoalFormClose(object o, EventArgs e)
        {
            //Unsubscribe from the control and close it.
            _AddGoalForm.Button.Click -= OnbtnAddGoalFormClick;
            _AddGoalForm.Closing -= OnAddGoalFormClose;
            _AddGoalForm = null;
        }
        /// <summary>
        /// When the button has been clicked.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private void OnbtnAddGoalFormClick(object o, EventArgs e)
        {
            //Create a goal with the form's data and add it to the game.
            Goal goal = new Goal();
            goal.Scorer = _AddGoalForm.Scorer;
            goal.Minute = _AddGoalForm.Minute;
            goal.Type = _AddGoalForm.GoalType;

            //Find out where to accredit the goal.
            ListViewItem item = new ListViewItem(goal.ToString());
            item.Tag = goal;

            if (_AddGoalForm.Profile == (Profile)cmbHomeProfile.SelectedItem) { lstvHomeGoals.Items.Add(item); }
            else if (_AddGoalForm.Profile == (Profile)cmbAwayProfile.SelectedItem) { lstvAwayGoals.Items.Add(item); }

            //Update the result labels.
            lblHomeGoals.Text = lstvHomeGoals.Items.Count.ToString();
            lblAwayGoals.Text = lstvAwayGoals.Items.Count.ToString();

            //Close the form.
            _AddGoalForm.Close();
        }
        /// <summary>
        /// If the user is finished.
        /// </summary>
        private void OnFinishClick(object sender, EventArgs e)
        {
            //Create a game with the form's data and save it.
            Game game = new Game();
            game.ExtraTime = ckbExtraTime.Checked;
            game.HomeFacts = GetGameFacts(MatchSide.Home);
            game.AwayFacts = GetGameFacts(MatchSide.Away);
            Helper.SaveGame(game);

            //Add the game to the profiles and save them.
            game.HomeFacts.Profile.AddGame(game);
            game.AwayFacts.Profile.AddGame(game);
            Helper.SaveProfile(game.HomeFacts.Profile);
            Helper.SaveProfile(game.AwayFacts.Profile);

            //Add the game to the list.
            Summary.Instance.Games.Add(game);

            //Close the form.
            this.Close();
        }
        #endregion
    }
}
