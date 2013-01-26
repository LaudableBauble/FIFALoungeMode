using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A player has a name and is a member of a team.
    /// </summary>
    public class Player
    {
        #region Fields
        private string _Name;
        private int _Id;
        private Team _Team;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="team">The team that the player belongs to.</param>
        public Player(string name, Team team)
        {
            Initialize(name, -1, team);
        }
        /// <summary>
        /// Create a player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="id">The id of the player.</param>
        /// <param name="team">The team that the player belongs to.</param>
        public Player(string name, int id, Team team)
        {
            Initialize(name, id, team);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the player.
        /// </summary>
        /// <param name="name">The name of the player.</param>
        /// <param name="id">The id of the player.</param>
        /// <param name="team">The team that the player belongs to.</param>
        protected void Initialize(string name, int id, Team team)
        {
            //Initialize some stuff.
            _Name = name;
            _Id = id;
            _Team = team;
        }

        /// <summary>
        /// Get the string equivalent of this player, ie. its name.
        /// </summary>
        /// <returns>The player's name.</returns>
        public override string ToString()
        {
            return _Name;
        }
        #endregion

        #region Properties
        /// <summary>
        /// The name.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        /// <summary>
        /// This is to be used as a player's unique id.
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        /// <summary>
        /// The team that this player belongs to.
        /// </summary>
        public Team Team
        {
            get { return _Team; }
            set { _Team = value; }
        }
        #endregion
    }
}
