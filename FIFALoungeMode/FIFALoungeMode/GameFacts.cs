using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// GameFacts contains a list of match specific facts about a profile's match.
    /// </summary>
    public class GameFacts
    {
        #region Fields
        private Profile _Profile;
        private Team _Team;
        private MatchSide _MatchSide;
        private int _Shots;
        private int _ShotsOnTarget;
        private int _ShotAccuracy;
        private int _PassAccuracy;
        private int _Corners;
        private int _Possession;
        private List<Goal> _GoalsScored;
        private List<Goal> _GoalsConceded;
        private List<BookingType> _Bookings;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a game facts instance.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public GameFacts(Profile profile)
        {
            //Initialize the game facts.
            Initialize(profile);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the game facts.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Initialize(Profile profile)
        {
            //Initialize some stuff.
            _Profile = profile;
            _Team = (profile != null) ? profile.Team : null;
            _Bookings = new List<BookingType>();
            _GoalsScored = new List<Goal>();
            _GoalsConceded = new List<Goal>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// The profile.
        /// </summary>
        public Profile Profile
        {
            get { return _Profile; }
            set { _Profile = value; }
        }
        /// <summary>
        /// The team.
        /// </summary>
        public Team Team
        {
            get { return _Team; }
            set { _Team = value; }
        }
        /// <summary>
        /// The match side.
        /// </summary>
        public MatchSide MatchSide
        {
            get { return _MatchSide; }
            set { _MatchSide = value; }
        }
        /// <summary>
        /// The bookings.
        /// </summary>
        public List<BookingType> Bookings
        {
            get { return _Bookings; }
            set { _Bookings = value; }
        }
        /// <summary>
        /// The number of shots.
        /// </summary>
        public int Shots
        {
            get { return _Shots; }
            set { _Shots = value; }
        }
        /// <summary>
        /// The number of shots on target.
        /// </summary>
        public int ShotsOnTarget
        {
            get { return _ShotsOnTarget; }
            set { _ShotsOnTarget = value; }
        }
        /// <summary>
        /// The shot accuracy.
        /// </summary>
        public int ShotAccuracy
        {
            get { return _ShotAccuracy; }
            set { _ShotAccuracy = value; }
        }
        /// <summary>
        /// The pass accuracy.
        /// </summary>
        public int PassAccuracy
        {
            get { return _PassAccuracy; }
            set { _PassAccuracy = value; }
        }
        /// <summary>
        /// The number of corners.
        /// </summary>
        public int Corners
        {
            get { return _Corners; }
            set { _Corners = value; }
        }
        /// <summary>
        /// The amount of possession.
        /// </summary>
        public int Possession
        {
            get { return _Possession; }
            set { _Possession = value; }
        }
        /// <summary>
        /// The goals scored.
        /// </summary>
        public List<Goal> GoalsScored
        {
            get { return _GoalsScored; }
            set { _GoalsScored = value; }
        }
        /// <summary>
        /// The goals conceded.
        /// </summary>
        public List<Goal> GoalsConceded
        {
            get { return _GoalsConceded; }
            set { _GoalsConceded = value; }
        }
        #endregion
    }
}
