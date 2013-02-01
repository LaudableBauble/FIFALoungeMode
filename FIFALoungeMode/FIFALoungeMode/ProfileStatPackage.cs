using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A profile stat package contains the most common statistics for profile in a given FIFA version.
    /// </summary>
    public class ProfileStatPackage
    {
        #region Fields
        private Profile _Profile;
        private int _Version;
        private int _Games;
        private int _Wins;
        private int _Draws;
        private int _Losses;
        private int _Points;
        private int _GoalsScored;
        private int _GoalsConceded;
        private Dictionary<Player, int> _Scorers;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a profile stat package.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="version">The FIFA version of the statistics.</param>
        public ProfileStatPackage(Profile profile, int version)
        {
            Initialize(profile, version);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="version">The FIFA version of the statistics.</param>
        private void Initialize(Profile profile, int version)
        {
            //Initialize some values.
            _Profile = profile;
            _Version = version;
            _Games = 0;
            _Wins = 0;
            _Draws = 0;
            _Losses = 0;
            _Points = 0;
            _GoalsScored = 0;
            _GoalsConceded = 0;
            _Scorers = new Dictionary<Player, int>();
        }

        /// <summary>
        /// Add a game to this profile statistics package. If the profile did not play the game or if the FIFA versions do not match, the game will be ignored.
        /// </summary>
        /// <param name="game">The game to add.</param>
        public void AddGame(Game game)
        {
            //If the game's FIFA version does not match the package's, stop here.
            if (game.FIFAVersion != _Version) { return; }

            //The game facts to use.
            GameFacts facts = null;

            //See what side the profile was on.
            if (game.HomeFacts.Profile.Id == _Profile.Id) { facts = game.HomeFacts; }
            else if (game.AwayFacts.Profile.Id == _Profile.Id) { facts = game.AwayFacts; }
            else { return; }

            //Add the data to the profile.
            _Games += 1;
            _Wins += (facts.GoalsScored.Count > facts.GoalsConceded.Count) ? 1 : 0;
            _Draws += (facts.GoalsScored.Count == facts.GoalsConceded.Count) ? 1 : 0;
            _Losses += (facts.GoalsScored.Count < facts.GoalsConceded.Count) ? 1 : 0;
            _GoalsScored += facts.GoalsScored.Count;
            _GoalsConceded += facts.GoalsConceded.Count;

            //For every player in this profile's team that scored, increment his tally.
            foreach (Goal goal in facts.GoalsScored) { AddScorer(goal.Scorer, 1); }

            //Add points.
            if (game.ExtraTime)
            {
                //Win.
                if (facts.GoalsScored.Count > facts.GoalsConceded.Count) { _Points += 2; }
                //Loss.
                if (facts.GoalsScored.Count < facts.GoalsConceded.Count) { _Points += 1; }
            }
            else
            {
                //Win.
                if (facts.GoalsScored.Count > facts.GoalsConceded.Count) { _Points += 3; }
            }
        }
        /// <summary>
        /// Add a goalscorer.
        /// </summary>
        /// <param name="player">The player that has scored.</param>
        /// <param name="goals">The number of goals scored since last update.</param>
        public void AddScorer(Player player, int goals)
        {
            //If the input is not valid, stop here.
            if (player == null || goals < 0) { return; }

            //Try to find a source instance of the same player.
            Player source = Helper.ContainsPlayer(_Scorers.Keys.ToList(), player);

            //See if the player already exists in the list.
            if (source != null)
            {
                //Increment the player's number of goals.
                _Scorers[source] += goals;
            }
            //Else, create a new entry.
            else { _Scorers.Add(player, goals); }

            //Sort the dictionary.
            SortTopScorers();
        }
        /// <summary>
        /// Sort the list of top scorers.
        /// </summary>
        public void SortTopScorers()
        {
            //Deep copy the dictionary.
            Dictionary<Player, int> scorers = new Dictionary<Player, int>(_Scorers);

            //Clear the 'real' dictionary.
            _Scorers.Clear();

            //Sort the temporary dictionary.
            foreach (KeyValuePair<Player, int> item in scorers.OrderByDescending(key => key.Value))
            {
                //Add the items to the 'real' dictionary.
                _Scorers.Add(item.Key, item.Value);
            }
        }
        /// <summary>
        /// Clear the profile stat package and reinitialize it. Note that the physical data (ie. the xml files) will still exist and not be deleted.
        /// </summary>
        public void Clear()
        {
            Initialize(_Profile, _Version);
        }
        #endregion

        #region Properties
        /// <summary>
        /// The profile that the stats belong to.
        /// </summary>
        public Profile Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }
        /// <summary>
        /// The FIFA version.
        /// </summary>
        public int Version
        {
            get { return _Version; }
            set { _Version = value; }
        }
        /// <summary>
        /// The number of games that this profile has played.
        /// </summary>
        public int Games
        {
            get { return _Games; }
            set { _Games = value; }
        }
        /// <summary>
        /// The number of wins.
        /// </summary>
        public int Wins
        {
            get { return _Wins; }
            set { _Wins = value; }
        }
        /// <summary>
        /// The number of draws.
        /// </summary>
        public int Draws
        {
            get { return _Draws; }
            set { _Draws = value; }
        }
        /// <summary>
        /// The number of losses.
        /// </summary>
        public int Losses
        {
            get { return _Losses; }
            set { _Losses = value; }
        }
        /// <summary>
        /// The number of points.
        /// </summary>
        public int Points
        {
            get { return _Points; }
            set { _Points = value; }
        }
        /// <summary>
        /// The number of goals scored.
        /// </summary>
        public int GoalsScored
        {
            get { return _GoalsScored; }
            set { _GoalsScored = value; }
        }
        /// <summary>
        /// The number of goals conceded.
        /// </summary>
        public int GoalsConceded
        {
            get { return _GoalsConceded; }
            set { _GoalsConceded = value; }
        }
        /// <summary>
        /// The list of scorers.
        /// </summary>
        public Dictionary<Player, int> Scorers
        {
            get { return _Scorers; }
            set { _Scorers = value; }
        }
        #endregion
    }
}
