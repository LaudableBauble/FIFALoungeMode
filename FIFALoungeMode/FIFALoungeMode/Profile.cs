using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A profile has a team and plays games against other profiles.
    /// </summary>
    public class Profile
    {
        #region Fields
        private string _Name;
        private int _Id;
        private List<Team> _Teams;
        private List<ProfileStatPackage> _Statistics;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a profile.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="team">The team that this profile uses.</param>
        public Profile(string name, Team team)
        {
            Initialize(name, -1, team);
        }
        /// <summary>
        /// Create a profile.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="id">The id of this profile.</param>
        /// <param name="team">The team that this profile uses.</param>
        public Profile(string name, int id, Team team)
        {
            Initialize(name, id, team);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the profile.
        /// </summary>
        /// <param name="name">The name of this profile.</param>
        /// <param name="id">The id of this profile.</param>
        /// <param name="team">The team that this profile uses.</param>
        private void Initialize(string name, int id, Team team)
        {
            //Initialize some values.
            _Name = name;
            _Id = id;
            _Teams = new List<Team>();
            _Teams.Add(team);
            _Statistics = new List<ProfileStatPackage>();

            //Get an id.
            if (_Id == -1) { Summary.Instance.GrantProfileId(this); }
        }

        /// <summary>
        /// Add a game to this profile. If the profile did not play the game, the game will be ignored.
        /// </summary>
        /// <param name="game">The game to add.</param>
        public void AddGame(Game game)
        {
            //Find the correct stat package.
            ProfileStatPackage stats = GetStatPackage(game.FIFAVersion);

            //If not null, try to add the game to it. Otherwise create a new stat package entry.
            if (stats != null) { stats.AddGame(game); }
            else
            {
                _Statistics.Add(new ProfileStatPackage(this, game.FIFAVersion));
                _Statistics[_Statistics.Count - 1].AddGame(game);
            }
        }
        /// <summary>
        /// Add a goal to a player.
        /// </summary>
        /// <param name="version">The FIFA version.</param>
        /// <param name="player">The player that scored.</param>
        public void AddScorer(int version, Player player)
        {
            AddScorer(version, player, 1);
        }
        /// <summary>
        /// Add a goalscorer.
        /// </summary>
        /// <param name="version">The FIFA version.</param>
        /// <param name="player">The player that has scored.</param>
        /// <param name="goals">The number of goals scored since last update.</param>
        public void AddScorer(int version, Player player, int goals)
        {
            //Find the correct stat package.
            ProfileStatPackage stats = GetStatPackage(version);

            //If not null, try to add the goal scorer to it.
            if (stats != null) { stats.AddScorer(player, goals); }
        }
        /// <summary>
        /// Clear the profile and reinitialize it. Note that the physical data (ie. the xml files) will still exist and not be deleted.
        /// </summary>
        public void Clear()
        {
            Initialize(_Name, _Id, _Teams[0]);
        }
        /// <summary>
        /// Force the profile to reload team data.
        /// First it will try to find its team in the summary and if that fails it will load it from file.
        /// </summary>
        public void ReloadTeam()
        {
            //The reloaded team.
            Team team = Summary.Instance.Teams.Find(t => (t.Id == _Teams[0].Id));

            //If the team exists in the summary, get it from there.
            if (team != null) { _Teams[0] = team; }
            //Otherwise reload from file.
            else { _Teams[0] = Helper.LoadTeam(_Teams[0].Id); }
        }
        /// <summary>
        /// Get the stat package that belongs to the specified FIFA version. A version of -1 is the same as all.
        /// </summary>
        /// <param name="version">The FIFA version.</param>
        /// <returns>The correct stat package if found, otherwise null.</returns>
        public ProfileStatPackage GetStatPackage(int version)
        {
            if (version != -1) { return _Statistics.Find(item => item.Version == version); }
            else { return GetAllStatPackages(); }
        }
        /// <summary>
        /// Get all stat packages summed up in one.
        /// </summary>
        /// <returns>A sum stat package.</returns>
        public ProfileStatPackage GetAllStatPackages()
        {
            //Create a new temporary stat package to rule them all.
            ProfileStatPackage stats = new ProfileStatPackage(this, -1);

            //For every version specific stat package, transfer the stats.
            foreach (ProfileStatPackage stat in _Statistics)
            {
                stats.Games += stat.Games;
                stats.Wins += stat.Wins;
                stats.Draws += stat.Draws;
                stats.Losses += stat.Losses;
                stats.Points += stat.Points;
                stats.GoalsScored += stat.GoalsScored;
                stats.GoalsConceded += stat.GoalsConceded;
                foreach (KeyValuePair<Player, int> scorer in stat.Scorers) { stats.AddScorer(scorer.Key, scorer.Value); }
            }

            //Return the full stat package.
            return stats;
        }
        /// <summary>
        /// Get the string equivalent of this profile.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return _Name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The name of this profile.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// The id of this profile.
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        /// <summary>
        /// The current team of this profile.
        /// </summary>
        public Team Team
        {
            get { return _Teams[0]; }
            set { _Teams[0] = value; }
        }
        /// <summary>
        /// The previous teams that this profile has used.
        /// </summary>
        public List<Team> PreviousTeams
        {
            get { return _Teams; }
            set { _Teams = value; }
        }
        /// <summary>
        /// The statistics.
        /// </summary>
        public List<ProfileStatPackage> Statistics
        {
            get { return _Statistics; }
            set { _Statistics = value; }
        }
        #endregion
    }
}
