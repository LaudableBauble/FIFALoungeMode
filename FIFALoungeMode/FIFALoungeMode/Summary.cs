using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FIFALoungeMode
{
    /// <summary>
    /// A summary stores information to be shared between all other aspects and facets of the application.
    /// </summary>
    public class Summary
    {
        #region Fields
        private static Summary _Instance;
        private int _CurrentFIFAVersion;
        private int _LastProfileId;
        private int _LastTeamId;
        private int _LastGameId;
        private int _LastPlayerId;
        private List<Profile> _Profiles;
        private List<Team> _Teams;
        private List<Game> _Games;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a summary.
        /// </summary>
        /// <param name="lastProfileId">The id of the last created profile.</param>
        /// <param name="lastTeamId">The id of the last created team.</param>
        /// <param name="lastGameId">The id of the last created game.</param>
        /// <param name="lastPlayerId">The id of the last created player.</param>
        private Summary(int lastProfileId, int lastTeamId, int lastGameId, int lastPlayerId)
        {
            Initialize(lastProfileId, lastTeamId, lastGameId, lastPlayerId);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initialize the summary.
        /// </summary>
        /// <param name="lastProfileId">The id of the last created profile.</param>
        /// <param name="lastTeamId">The id of the last created team.</param>
        /// <param name="lastGameId">The id of the last created game.</param>
        /// <param name="lastPlayerId">The id of the last created player.</param>
        private void Initialize(int lastProfileId, int lastTeamId, int lastGameId, int lastPlayerId)
        {
            //Initialize some stuff.
            _CurrentFIFAVersion = 13;
            _LastProfileId = lastProfileId;
            _LastTeamId = lastTeamId;
            _LastGameId = lastGameId;
            _LastPlayerId = lastPlayerId;
            _Profiles = new List<Profile>();
            _Teams = new List<Team>();
            _Games = new List<Game>();
        }

        /// <summary>
        /// Give the profile some id.
        /// </summary>
        /// <param name="profile">The profile in question.</param>
        public void GrantProfileId(Profile profile)
        {
            //Increment the counter.
            _LastProfileId++;
            //Give the profile some id.
            profile.Id = _LastProfileId;
        }
        /// <summary>
        /// Give the team some id.
        /// </summary>
        /// <param name="team">The team in question.</param>
        public void GrantTeamId(Team team)
        {
            //Increment the counter.
            _LastTeamId++;
            //Give the team some id.
            team.Id = _LastTeamId;
        }
        /// <summary>
        /// Give the game some id.
        /// </summary>
        /// <param name="game">The game in question.</param>
        public void GrantGameId(Game game)
        {
            //Increment the counter.
            _LastGameId++;
            //Give the game some id.
            game.Id = _LastGameId;
        }
        /// <summary>
        /// Give the player some id.
        /// </summary>
        /// <param name="player">The player in question.</param>
        public void GrantPlayerId(Player player)
        {
            //Increment the counter.
            _LastPlayerId++;
            //Give the game some id.
            player.Id = _LastPlayerId;
        }
        /// <summary>
        /// Get a team with a certain id.
        /// </summary>
        /// <param name="id">The id of the team.</param>
        /// <returns>The team.</returns>
        public Team GetTeam(int id)
        {
            //The reloaded team.
            Team team = _Teams.Find(t => (t.Id == id));

            //If the team do not exist in the summary, load it from file.
            if (team == null) { team = Helper.LoadTeam(id); }

            //Return the team.
            return team;
        }
        /// <summary>
        /// Get the games that a particular profile was involved in, sorted by date in a ravashingly descending fashion.
        /// </summary>
        /// <param name="profile">The profile in question.</param>
        /// <returns>A list of games.</returns>
        public List<Game> GetGames(Profile profile)
        {
            //The games.
            List<Game> games = new List<Game>();

            //Add the games where the specified profile was involved to the list.
            foreach (Game game in _Games) { if (game.ContainsProfile(profile)) { games.Add(game); } }

            //Return the games; sorted by date.
            return games.OrderByDescending(g => g.Date).ToList<Game>();
        }
        /// <summary>
        /// Get the form curve of a profile.
        /// </summary>
        /// <param name="profile">The profile in question.</param>
        /// <param name="count">The number of games used.</param>
        /// <returns>The form curve.</returns>
        public string GetFormCurve(Profile profile, int count)
        {
            //The form curve.
            string form = "";
            //The number of games used.
            int index = 0;

            //Calculate the form curve.
            foreach (Game game in GetGames(profile))
            {
                //If enough games has been reviewed.
                if (index >= count) { break; }
                //Get the correct game facts.
                GameFacts facts = game.GetGameFacts(profile);

                //Check the result and write it down.
                if (facts.GoalsScored.Count > facts.GoalsConceded.Count) { form += "W"; }
                else if (facts.GoalsScored.Count < facts.GoalsConceded.Count) { form += "L"; }
                else if (facts.GoalsScored.Count == facts.GoalsConceded.Count) { form += "D"; }

                //Increment the index counter.
                index++;
            }

            //Return the form curve.
            return form;
        }
        /// <summary>
        /// Get a dictionary of all scorers.
        /// </summary>
        /// <param name="limitToVersion">Whether limit data from games of the current FIFA version.</param>
        public Dictionary<Player, int> GetScorers(bool limitToVersion)
        {
            //The complete and final list of top scorers.
            Dictionary<Player, int> scorers = new Dictionary<Player, int>();

            //For each profile.
            foreach (Profile profile in _Profiles)
            {
                //For each scorer.
                foreach (KeyValuePair<Player, int> item in profile.GetStatPackage(limitToVersion ? Summary.Instance.CurrentFIFAVersion : -1).Scorers)
                {
                    //Try to find a source instance of he same player.
                    Player source = Helper.ContainsPlayer(scorers.Keys.ToList(), item.Key);

                    //See if the player already exists in the list.
                    if (source != null)
                    {
                        //Increment the player's number of goals appropriately.
                        scorers[source] += item.Value;
                    }
                    //Else, create a new entry.
                    else { scorers.Add(item.Key, item.Value); }
                }
            }

            //Deep copy the dictionary, for sorting purposes.
            Dictionary<Player, int> dict = new Dictionary<Player, int>(scorers);

            //Clear the 'real' dictionary.
            scorers.Clear();

            //Sort the temporary dictionary.
            foreach (KeyValuePair<Player, int> item in dict.OrderByDescending(key => key.Value))
            {
                //Add the items to the 'real' dictionary.
                scorers.Add(item.Key, item.Value);
            }

            //Finally return the list.
            return scorers;
        }
        /// <summary>
        /// Clear all profiles of data. Note that this will not delete any physical xml files.
        /// </summary>
        public void ClearAllProfiles()
        {
            //For all profiles, clear them.
            foreach (Profile profile in _Profiles) { profile.Clear(); }
        }
        /// <summary>
        /// Reset all profiles. This will first clear them of data and then recount it by going through all their games.
        /// </summary>
        public void ResetAllProfiles()
        {
            //Clear all profiles.
            ClearAllProfiles();

            //For all profiles, reset them.
            foreach (Profile profile in _Profiles)
            {
                //For all games, try to add them.
                foreach (Game game in _Games) { profile.AddGame(game); }
            }
        }
        /// <summary>
        /// Load everything.
        /// </summary>
        /// <param name="limitToVersion">Whether only data from the current FIFA version will be loaded.</param>
        public void LoadAll(bool limitToVersion)
        {
            //Rebuild the team index.
            Helper.RebuildTeamIndex();

            //Load all profiles, teams and games.
            LoadAllProfiles();
            LoadAllTeams();
            LoadAllGames(limitToVersion);
        }
        /// <summary>
        /// Load all profiles.
        /// </summary>
        public void LoadAllProfiles()
        {
            //The list of profiles.
            _Profiles.Clear();

            //Loop through each id until the end.
            for (int id = 1; id <= _LastProfileId; id++)
            {
                //The profile.
                Profile p = Helper.LoadProfile(id);
                //If the profile carries substance, ie. not null, add it to the list.
                if (p != null) { _Profiles.Add(p); }
            }
        }
        /// <summary>
        /// Load all teams.
        /// </summary>
        public void LoadAllTeams()
        {
            //The list of teams.
            _Teams.Clear();

            //Loop through each id until the end.
            for (int id = 1; id <= _LastTeamId; id++)
            {
                //The team.
                Team t = Helper.LoadTeam(id);
                //If the team carries substance, ie. not null, add it to the list.
                if (t != null) { _Teams.Add(t); }
            }
        }
        /// <summary>
        /// Load all games.
        /// </summary>
        /// <param name="limitToVersion">Whether only data from the current FIFA version will be loaded.</param>
        public void LoadAllGames(bool limitToVersion)
        {
            //The list of games.
            _Games.Clear();

            //Loop through each id until the end.
            for (int id = 1; id <= _LastGameId; id++)
            {
                //The game.
                Game g = Helper.LoadGame(id);
                //If the game carries substance, ie. not null, and if is of the correct FIFA version, add it to the list.
                if (g != null && limitToVersion ? g.FIFAVersion == Summary.Instance.CurrentFIFAVersion : true) { _Games.Add(g); }
            }
        }
        /// <summary>
        /// Save all profiles.
        /// </summary>
        public void SaveAllProfiles()
        {
            //If there exists any profiles.
            if (_Profiles.Count == 0) { return; }

            //Save each profile.
            foreach (Profile profile in _Profiles) { Helper.SaveProfile(profile); }
        }
        /// <summary>
        /// Save all games.
        /// </summary>
        public void SaveAllGames()
        {
            //If there exists any games.
            if (_Games.Count == 0) { return; }

            //Save each game.
            foreach (Game game in _Games) { Helper.SaveGame(game); }
        }
        /// <summary>
        /// Clear the summary. This blanks the summary. Note that the physical data (ie. the xml files) will still exist and not be deleted.
        /// </summary>
        public void Clear()
        {
            Initialize(0, 0, 0, 0);
        }
        /// <summary>
        /// Perform a reset of the summary. This reinitializes the summary.
        /// </summary>
        public void Reset()
        {
            Initialize(_LastProfileId, _LastTeamId, _LastGameId, _LastPlayerId);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Get the static singleton instance of the Summary class.
        /// </summary>
        public static Summary Instance
        {
            get
            {
                //If an instance already exists, return it. If not, create a one.
                if (_Instance == null) { _Instance = new Summary(0, 0, 0, 0); }
                return _Instance;
            }
        }
        /// <summary>
        /// The current FIFA version.
        /// </summary>
        public int CurrentFIFAVersion
        {
            get { return _CurrentFIFAVersion; }
        }
        /// <summary>
        /// The id of the last created profile.
        /// </summary>
        public int LastProfileId
        {
            get { return _LastProfileId; }
            set { _LastProfileId = value; }
        }
        /// <summary>
        /// The id of the last created team.
        /// </summary>
        public int LastTeamId
        {
            get { return _LastTeamId; }
            set { _LastTeamId = value; }
        }
        /// <summary>
        /// The id of the last created game.
        /// </summary>
        public int LastGameId
        {
            get { return _LastGameId; }
            set { _LastGameId = value; }
        }
        /// <summary>
        /// The list of available profiles.
        /// </summary>
        public List<Profile> Profiles
        {
            get { return _Profiles; }
            set { _Profiles = value; }
        }
        /// <summary>
        /// The list of available teams.
        /// </summary>
        public List<Team> Teams
        {
            get { return _Teams; }
            set { _Teams = value; }
        }
        /// <summary>
        /// The list of available games.
        /// </summary>
        public List<Game> Games
        {
            get { return _Games; }
            set { _Games = value; }
        }
        #endregion
    }
}
