using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A goal is simply a goal in a match.
    /// </summary>
    public class Goal
    {
        #region Fields
        private Player _Scorer;
        private int _Minute;
        private GoalType _Type;
        #endregion

        #region Methods
        /// <summary>
        /// Get the string equivalent of this goal.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return (Minute.ToString() + "': " + Scorer.Name + " - " + Type.ToString());
        }
        #endregion

        #region Properties
        /// <summary>
        /// The scorer.
        /// </summary>
        public Player Scorer
        {
            get { return _Scorer; }
            set { _Scorer = value; }
        }
        /// <summary>
        /// The minute the goal was scored.
        /// </summary>
        public int Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }
        /// <summary>
        /// The type of goal.
        /// </summary>
        public GoalType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        #endregion
    }
}
