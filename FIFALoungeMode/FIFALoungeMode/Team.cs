using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A team has a name and a list of players.
    /// </summary>
    public class Team
    {
        #region Fields
        private string _Name;
        private int _Id;
        private List<Player> _Players;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a team.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        public Team(string name)
        {
            //Initialize the team.
            Initialize(name, -1);
        }
        /// <summary>
        /// Create a team.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="id">The id of the team.</param>
        public Team(string name, int id)
        {
            //Initialize the player.
            Initialize(name, id);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the team.
        /// </summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="id">The id of the team.</param>
        public void Initialize(string name, int id)
        {
            //Initialize some stuff.
            _Name = name;
            _Id = id;
            _Players = new List<Player>();

            //Get an id.
            if (_Id == -1) { Summary.Instance.GrantTeamId(this); }
        }

        /// <summary>
        /// Get the string equivalent of this player, ie. its name.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return _Name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The name of the team.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// The id of this team.
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        /// <summary>
        /// The list of players in the team.
        /// </summary>
        public List<Player> Players
        {
            get { return _Players; }
            set { _Players = value; }
        }
        #endregion
    }
}
