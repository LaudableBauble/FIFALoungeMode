using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A game is simply a match between two profiles and their respective teams.
    /// It contains a result and some other data.
    /// </summary>
    public class Game
    {
        #region Fields
        private int _Id;
        private DateTime _Date;
        private bool _ExtraTime;
        private GameFacts _HomeFacts;
        private GameFacts _AwayFacts;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a game.
        /// </summary>
        public Game()
        {
            //Initialize the game.
            Initialize(-1);
        }
        /// <summary>
        /// Create a game.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        public Game(int id)
        {
            //Initialize the game.
            Initialize(id);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the game.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        private void Initialize(int id)
        {
            //Initialize some stuff.
            _Id = id;
            _Date = DateTime.Now;
            _ExtraTime = false;
            _HomeFacts = new GameFacts(null);
            _AwayFacts = new GameFacts(null);

            //Get an id.
            if (_Id == -1) { Summary.Instance.GrantGameId(this); }
        }

        /// <summary>
        /// Get a string depicting the result of this game.
        /// </summary>
        /// <returns>The result of the game.</returns>
        public string GetResult()
        {
            //The result of the game.
            return _HomeFacts.GoalsScored.Count + " - " + _AwayFacts.GoalsScored.Count;
        }
        /// <summary>
        /// Get the correct game facts instance.
        /// </summary>
        /// <param name="profile">The profile in question.</param>
        /// <returns>The game facts belonging to the specified profile.</returns>
        public GameFacts GetGameFacts(Profile profile)
        {
            //If the profile was the home team.
            if (_HomeFacts.Profile.Id == profile.Id) { return _HomeFacts; }
            else if (_AwayFacts.Profile.Id == profile.Id) { return _AwayFacts; }

            //Nothing happened.
            return null;
        }
        /// <summary>
        /// Whether a certain profile participated in this game.
        /// </summary>
        /// <param name="profile">The profile in question.</param>
        /// <returns>True or false.</returns>
        public bool ContainsProfile(Profile profile)
        {
            return ((_HomeFacts.Profile.Id == profile.Id) || (_AwayFacts.Profile.Id == profile.Id));
        }
        #endregion

        #region Properties
        /// <summary>
        /// The id of the game.
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        /// <summary>
        /// When the game was played.
        /// </summary>
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        /// <summary>
        /// If the game reached extra time.
        /// </summary>
        public bool ExtraTime
        {
            get { return _ExtraTime; }
            set { _ExtraTime = value; }
        }
        /// <summary>
        /// The facts of the home team.
        /// </summary>
        public GameFacts HomeFacts
        {
            get { return _HomeFacts; }
            set { _HomeFacts = value; }
        }
        /// <summary>
        /// The facts of the away team.
        /// </summary>
        public GameFacts AwayFacts
        {
            get { return _AwayFacts; }
            set { _AwayFacts = value; }
        }
        #endregion
    }
}
