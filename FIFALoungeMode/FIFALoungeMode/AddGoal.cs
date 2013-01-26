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
    public partial class AddGoal : Form
    {
        #region Fields
        private Profile _Profile;
        #endregion

        #region Constructors
        /// <summary>
        /// Create the add goal form.
        /// </summary>
        /// <param name="profile">The profile that scored the goal.</param>
        public AddGoal(Profile profile)
        {
            //Initialize the form.
            Initialize(profile);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the form.
        /// </summary>
        /// <param name="profile">The profile that scored the goal.</param>
        public void Initialize(Profile profile)
        {
            //Initialize some components.
            InitializeComponent();

            //Initialize some variables.
            _Profile = profile;

            //Rename the controls' text.
            lblMinute.Text = "Minute:";
            btnFinish.Text = "Add the goal";
            txbMinute.Text = "0";

            //Add items to the comboboxes.
            foreach (Player player in _Profile.Team.Players) { cmbScorer.Items.Add(player); }
            cmbScorer.SelectedIndex = 0;

            cmbGoalType.Items.Add(GoalType.Shot);
            cmbGoalType.Items.Add(GoalType.Header);
            cmbGoalType.Items.Add(GoalType.Penalty);
            cmbGoalType.Items.Add(GoalType.Freekick);
            cmbGoalType.SelectedIndex = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get the profile that scored the goal.
        /// </summary>
        public Profile Profile
        {
            get { return _Profile; }
        }
        /// <summary>
        /// Get the scorer.
        /// </summary>
        public Player Scorer
        {
            get { return (cmbScorer.SelectedItem as Player); }
        }
        /// <summary>
        /// Get the minute the goal was scored.
        /// </summary>
        public int Minute
        {
            get { return int.Parse(txbMinute.Text); }
        }
        /// <summary>
        /// Get the type of the goal.
        /// </summary>
        public GoalType GoalType
        {
            get { return (GoalType)cmbGoalType.SelectedItem; }
        }
        /// <summary>
        /// The finish button.
        /// </summary>
        public Button Button
        {
            get { return btnFinish; }
        }
        #endregion
    }
}
