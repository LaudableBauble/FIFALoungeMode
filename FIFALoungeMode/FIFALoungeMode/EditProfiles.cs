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
    public partial class EditProfiles : Form
    {
        #region Fields
        private Profile _Profile;
        #endregion

        #region Constructors
        /// <summary>
        /// Create the edit profiles form.
        /// </summary>
        public EditProfiles()
        {
            //Initialize the form.
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

            //Initialize soma variables.
            _Profile = null;

            //Rename the controls' text.
            lblProfileName.Text = "Name:";
            txbProfileName.Text = "";
            btnAddProfile.Text = "Add Profile";
            btnFinish.Text = "Finish";

            //Add items to the combobox.
            foreach (Profile profile in Summary.Instance.Profiles) { cmbProfiles.Items.Add(profile); }
            cmbProfiles.SelectedIndex = (cmbProfiles.Items.Count > 0) ? 0 : cmbTeam.SelectedIndex;

            //Select the first profile in the list.
            SelectProfile(Summary.Instance.Profiles[0]);

            //Subscribe to events.
            cmbProfiles.SelectedIndexChanged += OnProfileChange;
            cmbTeam.SelectedIndexChanged += OnTeamChange;
            btnAddProfile.Click += OnAddProfileClick;
            btnFinish.Click += OnFinishClick;
        }

        /// <summary>
        /// Refresh the list of teams.
        /// </summary>
        private void RefreshTeamList()
        {
            //Refresh the list of teams.
            cmbTeam.Items.Clear();
            foreach (Team team in Summary.Instance.Teams) { cmbTeam.Items.Add(team); }

            //Switch focus to the profile's current team.
            int index = 0;
            foreach (Team team in Summary.Instance.Teams)
            {
                //If a team matches the profile's team, save its index.
                if (_Profile.Team.Id == team.Id) { index = Summary.Instance.Teams.IndexOf(team); }
            }

            //Select the appropriate team.
            cmbTeam.SelectedIndex = (cmbProfiles.Items.Count > index) ? index : cmbTeam.SelectedIndex;
        }
        /// <summary>
        /// Select a profile to edit.
        /// </summary>
        /// <param name="profile">The profile to edit.</param>
        private void SelectProfile(Profile profile)
        {
            //Perform the necessary arrangements.
            _Profile = profile;
            txbProfileName.Text = profile.Name;
            RefreshTeamList();
        }
        /// <summary>
        /// If the user wants to change profile.
        /// </summary>
        private void OnProfileChange(object sender, EventArgs e)
        {
            //Save the modified profile name.
            if (_Profile != null) { _Profile.Name = txbProfileName.Text; }

            //Save the profile.
            Helper.SaveProfile(_Profile);
            //Select the new profile.
            SelectProfile((Profile)cmbProfiles.SelectedItem);
        }
        /// <summary>
        /// If the user wants to change the profile's team.
        /// </summary>
        private void OnTeamChange(object sender, EventArgs e)
        {
            //Select the new team.
            _Profile.PreviousTeams.Insert(0, (Team)cmbTeam.SelectedItem);
        }
        /// <summary>
        /// If the user wants to add a profile.
        /// </summary>
        private void OnAddProfileClick(object sender, EventArgs e)
        {
            //Create a new profile and add him.
            Profile profile = new Profile("profile1", (Team)cmbTeam.SelectedItem);
            Summary.Instance.Profiles.Add(profile);
            //Add the profile to the combo box.
            cmbProfiles.Items.Add(profile);
        }
        /// <summary>
        /// If the user is finished.
        /// </summary>
        private void OnFinishClick(object sender, EventArgs e)
        {
            //Save the modified profile name.
            if (_Profile != null) { _Profile.Name = txbProfileName.Text; }

            //Save the profile.
            Helper.SaveProfile(_Profile);

            //Let go of the profile.
            _Profile = null;

            //Close the form.
            this.Close();
        }
        #endregion
    }
}
