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
        private int _Games;
        private int _Wins;
        private int _Draws;
        private int _Losses;
        private int _Points;
        private int _GoalsScored;
        private int _GoalsConceded;
        private Dictionary<Player, int> _TopScorers;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a profile.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="team">The team that this profile uses.</param>
        public Profile(string name, Team team)
        {
            //Initialize the profile.
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
            //Initialize the profile.
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
        public void Initialize(string name, int id, Team team)
        {
            //Initialize some values.
            _Name = name;
            _Id = id;
            _Teams = new List<Team>();
            _Teams.Add(team);
            _Games = 0;
            _Wins = 0;
            _Draws = 0;
            _Losses = 0;
            _Points = 0;
            _GoalsScored = 0;
            _GoalsConceded = 0;
            _TopScorers = new Dictionary<Player, int>();

            //Get an id.
            if (_Id == -1) { Summary.Instance.GrantProfileId(this); }
        }

        /// <summary>
        /// Add a game to this profile. If the profile did not play the game, the game will be ignored.
        /// </summary>
        /// <param name="game">The game to add.</param>
        public void AddGame(Game game)
        {
            //The game facts to use.
            GameFacts facts = null;

            //See what side the profile was on.
            if (game.HomeFacts.Profile.Id == _Id) { facts = game.HomeFacts; }
            else if (game.AwayFacts.Profile.Id == _Id) { facts = game.AwayFacts; }
            else { return; }

            //Add the data to the profile.
            _Games += 1;
            _Wins += (facts.GoalsScored.Count > facts.GoalsConceded.Count) ? 1 : 0;
            _Draws += (facts.GoalsScored.Count == facts.GoalsConceded.Count) ? 1 : 0;
            _Losses += (facts.GoalsScored.Count < facts.GoalsConceded.Count) ? 1 : 0;
            _GoalsScored += facts.GoalsScored.Count;
            _GoalsConceded += facts.GoalsConceded.Count;

            //For every player in this profile's team that scored, increment his tally.
            foreach (Goal goal in facts.GoalsScored) { AddScorer(goal.Scorer); }

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
        /// Add a goal to a player.
        /// </summary>
        /// <param name="player">The player that scored.</param>
        public void AddScorer(Player player)
        {
            AddScorer(player, 1);
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
            Player source = Helper.ContainsPlayer(_TopScorers.Keys.ToList(), player);

            //See if the player already exists in the list.
            if (source != null)
            {
                //Increment the player's number of goals.
                _TopScorers[source] += goals;
            }
            //Else, create a new entry.
            else { _TopScorers.Add(player, goals); }

            //Sort the dictionary.
            SortTopScorers();
        }
        /// <summary>
        /// Sort the list of top scorers.
        /// </summary>
        public void SortTopScorers()
        {
            //Deep copy the dictionary.
            Dictionary<Player, int> scorers = new Dictionary<Player, int>(_TopScorers);

            //Clear the 'real' dictionary.
            _TopScorers.Clear();

            //Sort the temporary dictionary.
            foreach (KeyValuePair<Player, int> item in scorers.OrderByDescending(key => key.Value))
            {
                //Add the items to the 'real' dictionary.
                _TopScorers.Add(item.Key, item.Value);
            }
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
        /// The list of top scorers.
        /// </summary>
        public Dictionary<Player, int> TopScorers
        {
            get { return _TopScorers; }
            set { _TopScorers = value; }
        }
        #endregion
    }
}
