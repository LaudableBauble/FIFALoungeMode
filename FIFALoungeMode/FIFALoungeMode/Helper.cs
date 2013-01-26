﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FIFALoungeMode
{
    public static class Helper
    {
        /// <summary>
        /// Save the summary to an xml file.
        /// </summary>
        /// <param name="summary">The summary to save.</param>
        public static void SaveSummary(Summary summary)
        {
            //Create the xml writer.
            XmlTextWriter textWriter = new XmlTextWriter(GetSummaryPath(), null);
            //Set the formatting to use indent.
            textWriter.Formatting = Formatting.Indented;

            //Begin with the summary.
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Summary");

            //The last profile id.
            textWriter.WriteStartElement("LastProfileId");
            textWriter.WriteValue(summary.LastProfileId);
            textWriter.WriteEndElement();
            //The last team id.
            textWriter.WriteStartElement("LastTeamId");
            textWriter.WriteValue(summary.LastTeamId);
            textWriter.WriteEndElement();
            //The last game id.
            textWriter.WriteStartElement("LastGameId");
            textWriter.WriteValue(summary.LastGameId);
            textWriter.WriteEndElement();

            //End with the summary.
            textWriter.WriteEndElement();

            //End with the document.
            textWriter.WriteEndDocument();
            //Close the writer.
            textWriter.Close();
        }
        /// <summary>
        /// Load a summary.
        /// </summary>
        /// <returns>The loaded summary.</returns>
        public static void LoadSummary()
        {
            //In the all too likely event that everything inexplicably goes to hell.
            try
            {
                //Set up and load the xml file.
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(GetSummaryPath());

                //Parse the xml data.
                Summary.Instance.LastProfileId = int.Parse(xmlDocument.SelectSingleNode("/Summary/LastProfileId").InnerText);
                Summary.Instance.LastTeamId = int.Parse(xmlDocument.SelectSingleNode("/Summary/LastTeamId").InnerText);
                Summary.Instance.LastGameId = int.Parse(xmlDocument.SelectSingleNode("/Summary/LastGameId").InnerText);

                //Force the summary to load everything.
                Summary.Instance.LoadAll();
            }
            catch { return; }
        }
        /// <summary>
        /// Save a profile to an xml file.
        /// </summary>
        /// <param name="profile">The profile to save.</param>
        public static void SaveProfile(Profile profile)
        {
            //Create the xml writer.
            XmlTextWriter textWriter = new XmlTextWriter(GetProfilePath(profile.Id), null);
            //Set the formatting to use indent.
            textWriter.Formatting = Formatting.Indented;

            //Begin with the profile.
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Profile");

            //The id.
            textWriter.WriteStartElement("Id");
            textWriter.WriteValue(profile.Id);
            textWriter.WriteEndElement();
            //The name.
            textWriter.WriteStartElement("Name");
            textWriter.WriteValue(profile.Name);
            textWriter.WriteEndElement();
            //The team.
            textWriter.WriteStartElement("Team");
            textWriter.WriteValue(profile.Team.Id);
            textWriter.WriteEndElement();
            //The games.
            textWriter.WriteStartElement("Games");
            textWriter.WriteValue(profile.Games);
            textWriter.WriteEndElement();
            //The points.
            textWriter.WriteStartElement("Points");
            textWriter.WriteValue(profile.Points);
            textWriter.WriteEndElement();
            //The wins.
            textWriter.WriteStartElement("Wins");
            textWriter.WriteValue(profile.Wins);
            textWriter.WriteEndElement();
            //The draws.
            textWriter.WriteStartElement("Draws");
            textWriter.WriteValue(profile.Draws);
            textWriter.WriteEndElement();
            //The losses.
            textWriter.WriteStartElement("Losses");
            textWriter.WriteValue(profile.Losses);
            textWriter.WriteEndElement();
            //The goals scored.
            textWriter.WriteStartElement("GoalsScored");
            textWriter.WriteValue(profile.GoalsScored);
            textWriter.WriteEndElement();
            //The goals conceded.
            textWriter.WriteStartElement("GoalsConceded");
            textWriter.WriteValue(profile.GoalsConceded);
            textWriter.WriteEndElement();

            //Begin with the list of teams.
            textWriter.WriteStartElement("Teams");
            textWriter.WriteAttributeString("Count", profile.PreviousTeams.Count.ToString());

            //The teams.
            foreach (Team team in profile.PreviousTeams)
            {
                //Begin with a team.
                textWriter.WriteStartElement("Team");
                textWriter.WriteAttributeString("Id", team.Id.ToString());
                textWriter.WriteEndElement();

                //Save the team to the harddrive.
                SaveTeam(team);
            }

            //End with the teams.
            textWriter.WriteEndElement();

            //Begin with the list of scorers.
            textWriter.WriteStartElement("Scorers");
            textWriter.WriteAttributeString("Count", profile.TopScorers.Count.ToString());

            //The top scorers.
            foreach (KeyValuePair<Player, int> pair in profile.TopScorers)
            {
                //Begin with a scorer.
                textWriter.WriteStartElement("Scorer");
                textWriter.WriteAttributeString("Id", pair.Key.Team.Id.ToString() + ":" + pair.Key.Name);
                textWriter.WriteAttributeString("Goals", pair.Value.ToString());
                textWriter.WriteEndElement();
            }

            //End with the scorers.
            textWriter.WriteEndElement();

            //End with the profile.
            textWriter.WriteEndElement();

            //End with the document.
            textWriter.WriteEndDocument();
            //Close the writer.
            textWriter.Close();
        }
        /// <summary>
        /// Load a profile, either from memory or file.
        /// </summary>
        /// <param name="id">The id of the profile.</param>
        /// <returns>The loaded profile.</returns>
        public static Profile LoadProfile(int id)
        {
            //In the all too likely event that everything inexplicably goes to hell.
            try
            {
                //Set up and load the xml file.
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(GetProfilePath(id));

                //Create the profile.
                Profile profile = new Profile(xmlDocument.SelectSingleNode("/Profile/Name").InnerText,
                    int.Parse(xmlDocument.SelectSingleNode("/Profile/Id").InnerText),
                    Summary.Instance.GetTeam(int.Parse(xmlDocument.SelectSingleNode("/Profile/Team").InnerText)));

                //Parse the xml data.
                profile.Games = int.Parse(xmlDocument.SelectSingleNode("/Profile/Games").InnerText);
                profile.Points = int.Parse(xmlDocument.SelectSingleNode("/Profile/Points").InnerText);
                profile.Wins = int.Parse(xmlDocument.SelectSingleNode("/Profile/Wins").InnerText);
                profile.Draws = int.Parse(xmlDocument.SelectSingleNode("/Profile/Draws").InnerText);
                profile.Losses = int.Parse(xmlDocument.SelectSingleNode("/Profile/Losses").InnerText);
                profile.GoalsScored = int.Parse(xmlDocument.SelectSingleNode("/Profile/GoalsScored").InnerText);
                profile.GoalsConceded = int.Parse(xmlDocument.SelectSingleNode("/Profile/GoalsConceded").InnerText);

                //The previous teams.
                foreach (XmlNode teamNode in xmlDocument.SelectNodes("Profile/Teams/Team"))
                {
                    //Load a team.
                    profile.PreviousTeams.Add(LoadTeam(int.Parse(teamNode.Attributes.GetNamedItem("Id").Value)));
                }

                //The top scorers.
                foreach (XmlNode scorerNode in xmlDocument.SelectNodes("Profile/Scorers/Scorer"))
                {
                    //Load a scorer.
                    profile.AddScorer(GetPlayer(scorerNode.Attributes.GetNamedItem("Id").Value),
                        int.Parse(scorerNode.Attributes.GetNamedItem("Goals").Value));
                }

                //Return the profile.
                return profile;
            }
            catch { return null; }
        }
        /// <summary>
        /// Save a team to an xml file.
        /// </summary>
        /// <param name="team">The team to save.</param>
        public static void SaveTeam(Team team)
        {
            //Create the xml writer.
            XmlTextWriter textWriter = new XmlTextWriter(GetTeamPath(team.Id), null);
            //Set the formatting to use indent.
            textWriter.Formatting = Formatting.Indented;

            //Begin with the team.
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Team");
            textWriter.WriteAttributeString("Id", team.Id.ToString());
            textWriter.WriteAttributeString("Name", team.Name);

            //Begin with the players.
            textWriter.WriteStartElement("Players");
            textWriter.WriteAttributeString("Count", team.Players.Count.ToString());

            //The players.
            foreach (Player player in team.Players)
            {
                //Begin with a player.
                textWriter.WriteStartElement("Player");
                textWriter.WriteAttributeString("Name", player.Name);
                textWriter.WriteEndElement();
            }

            //End with the team.
            textWriter.WriteEndElement();

            //End with the document.
            textWriter.WriteEndDocument();
            //Close the writer.
            textWriter.Close();
        }
        /// <summary>
        /// Load a team.
        /// </summary>
        /// <param name="id">The id of the team.</param>
        /// <returns>The loaded team.</returns>
        public static Team LoadTeam(int id)
        {
            //In the all too likely event that everything inexplicably goes to hell.
            try
            {
                //Set up and load the xml file.
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(GetTeamPath(id));

                //Create the team.
                Team team = new Team(xmlDocument.GetElementsByTagName("Team")[0].Attributes[1].Value,
                    int.Parse(xmlDocument.GetElementsByTagName("Team")[0].Attributes[0].Value));

                //The players.
                foreach (XmlNode playerNode in xmlDocument.SelectNodes("Team/Players/Player"))
                {
                    //Create a player.
                    team.Players.Add(new Player(playerNode.Attributes.GetNamedItem("Name").Value, team));
                }

                //Return the team.
                return team;
            }
            catch { return null; }
        }
        /// <summary>
        /// Save a game to an xml file.
        /// </summary>
        /// <param name="game">The game to save.</param>
        public static void SaveGame(Game game)
        {
            //Create the xml writer.
            XmlTextWriter textWriter = new XmlTextWriter(GetGamePath(game.Id), null);
            //Set the formatting to use indent.
            textWriter.Formatting = Formatting.Indented;

            //Begin with the game.
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Game");

            //The id.
            textWriter.WriteStartElement("Id");
            textWriter.WriteValue(game.Id);
            textWriter.WriteEndElement();
            //The date.
            textWriter.WriteStartElement("Date");
            textWriter.WriteString(game.Date.ToString());
            textWriter.WriteEndElement();
            //The extra time.
            textWriter.WriteStartElement("ExtraTime");
            textWriter.WriteValue(game.ExtraTime);
            textWriter.WriteEndElement();

            #region HomeFacts
            //Begin with the home game facts.
            textWriter.WriteStartElement("HomeFacts");

            //The profile.
            textWriter.WriteStartElement("Profile");
            textWriter.WriteValue(game.HomeFacts.Profile.Id);
            textWriter.WriteEndElement();
            //The team.
            textWriter.WriteStartElement("Team");
            textWriter.WriteString(game.HomeFacts.Team.ToString());
            textWriter.WriteEndElement();
            //The match side.
            textWriter.WriteStartElement("MatchSide");
            textWriter.WriteString(game.HomeFacts.MatchSide.ToString());
            textWriter.WriteEndElement();
            //The number of shots.
            textWriter.WriteStartElement("Shots");
            textWriter.WriteValue(game.HomeFacts.Shots);
            textWriter.WriteEndElement();
            //The number of shots on target.
            textWriter.WriteStartElement("ShotsOnTarget");
            textWriter.WriteValue(game.HomeFacts.ShotsOnTarget);
            textWriter.WriteEndElement();
            //The shot accuracy.
            textWriter.WriteStartElement("ShotAccuracy");
            textWriter.WriteValue(game.HomeFacts.ShotAccuracy);
            textWriter.WriteEndElement();
            //The pass accuracy.
            textWriter.WriteStartElement("PassAccuracy");
            textWriter.WriteValue(game.HomeFacts.PassAccuracy);
            textWriter.WriteEndElement();
            //The number of corners.
            textWriter.WriteStartElement("Corners");
            textWriter.WriteValue(game.HomeFacts.Corners);
            textWriter.WriteEndElement();
            //The amount of possession.
            textWriter.WriteStartElement("Possession");
            textWriter.WriteValue(game.HomeFacts.Possession);
            textWriter.WriteEndElement();

            //Begin with the goals scored.
            textWriter.WriteStartElement("GoalsScored");
            textWriter.WriteAttributeString("Count", game.HomeFacts.GoalsScored.Count.ToString());

            //The goals scored.
            foreach (Goal goal in game.HomeFacts.GoalsScored)
            {
                //Begin with a goal.
                textWriter.WriteStartElement("Goal");

                //The player who scored.
                textWriter.WriteStartElement("Scorer");
                textWriter.WriteString(goal.Scorer.Team.Id.ToString() + ":" + goal.Scorer.Name);
                textWriter.WriteEndElement();
                //The time of the goal.
                textWriter.WriteStartElement("Minute");
                textWriter.WriteValue(goal.Minute.ToString());
                textWriter.WriteEndElement();
                //The type of goal.
                textWriter.WriteStartElement("Type");
                textWriter.WriteString(goal.Type.ToString());
                textWriter.WriteEndElement();

                //End with the goal.
                textWriter.WriteEndElement();
            }

            //End with the goals scored.
            textWriter.WriteEndElement();

            //Begin with the goals conceded.
            textWriter.WriteStartElement("GoalsConceded");
            textWriter.WriteAttributeString("Count", game.HomeFacts.GoalsConceded.Count.ToString());

            //The goals conceded.
            foreach (Goal goal in game.HomeFacts.GoalsConceded)
            {
                //Begin with a goal.
                textWriter.WriteStartElement("Goal");

                //The player who scored.
                textWriter.WriteStartElement("Scorer");
                textWriter.WriteString(goal.Scorer.Team.Id.ToString() + ":" + goal.Scorer.Name);
                textWriter.WriteEndElement();
                //The time of the goal.
                textWriter.WriteStartElement("Minute");
                textWriter.WriteString(goal.Minute.ToString());
                textWriter.WriteEndElement();
                //The type of goal.
                textWriter.WriteStartElement("Type");
                textWriter.WriteString(goal.Type.ToString());
                textWriter.WriteEndElement();

                //End with the goal.
                textWriter.WriteEndElement();
            }

            //End with the goals conceded.
            textWriter.WriteEndElement();

            //Begin with the bookings.
            textWriter.WriteStartElement("Bookings");
            textWriter.WriteAttributeString("Count", game.HomeFacts.Bookings.Count.ToString());

            //The bookings.
            foreach (BookingType booking in game.HomeFacts.Bookings)
            {
                //A booking.
                textWriter.WriteStartElement("Booking");
                textWriter.WriteString(booking.ToString());
                textWriter.WriteEndElement();
            }

            //End with the bookings.
            textWriter.WriteEndElement();

            //End with the home game facts.
            textWriter.WriteEndElement();
            #endregion

            #region AwayFacts
            //Begin with the away game facts.
            textWriter.WriteStartElement("AwayFacts");

            //The profile.
            textWriter.WriteStartElement("Profile");
            textWriter.WriteValue(game.AwayFacts.Profile.Id);
            textWriter.WriteEndElement();
            //The team.
            textWriter.WriteStartElement("Team");
            textWriter.WriteValue(game.AwayFacts.Team.Id);
            textWriter.WriteEndElement();
            //The match side.
            textWriter.WriteStartElement("MatchSide");
            textWriter.WriteString(game.AwayFacts.MatchSide.ToString());
            textWriter.WriteEndElement();
            //The number of shots.
            textWriter.WriteStartElement("Shots");
            textWriter.WriteValue(game.AwayFacts.Shots);
            textWriter.WriteEndElement();
            //The number of shots on target.
            textWriter.WriteStartElement("ShotsOnTarget");
            textWriter.WriteValue(game.AwayFacts.ShotsOnTarget);
            textWriter.WriteEndElement();
            //The shot accuracy.
            textWriter.WriteStartElement("ShotAccuracy");
            textWriter.WriteValue(game.AwayFacts.ShotAccuracy);
            textWriter.WriteEndElement();
            //The pass accuracy.
            textWriter.WriteStartElement("PassAccuracy");
            textWriter.WriteValue(game.AwayFacts.PassAccuracy);
            textWriter.WriteEndElement();
            //The number of corners.
            textWriter.WriteStartElement("Corners");
            textWriter.WriteValue(game.AwayFacts.Corners);
            textWriter.WriteEndElement();
            //The amount of possession.
            textWriter.WriteStartElement("Possession");
            textWriter.WriteValue(game.AwayFacts.Possession);
            textWriter.WriteEndElement();

            //Begin with the goals scored.
            textWriter.WriteStartElement("GoalsScored");
            textWriter.WriteAttributeString("Count", game.AwayFacts.GoalsScored.Count.ToString());

            //The goals scored.
            foreach (Goal goal in game.AwayFacts.GoalsScored)
            {
                //Begin with a goal.
                textWriter.WriteStartElement("Goal");

                //The player who scored.
                textWriter.WriteStartElement("Scorer");
                textWriter.WriteString(goal.Scorer.Team.Id.ToString() + ":" + goal.Scorer.Name);
                textWriter.WriteEndElement();
                //The time of the goal.
                textWriter.WriteStartElement("Minute");
                textWriter.WriteValue(goal.Minute);
                textWriter.WriteEndElement();
                //The type of goal.
                textWriter.WriteStartElement("Type");
                textWriter.WriteString(goal.Type.ToString());
                textWriter.WriteEndElement();

                //End with the goal.
                textWriter.WriteEndElement();
            }

            //End with the goals scored.
            textWriter.WriteEndElement();

            //Begin with the goals conceded.
            textWriter.WriteStartElement("GoalsConceded");
            textWriter.WriteAttributeString("Count", game.AwayFacts.GoalsConceded.Count.ToString());

            //The goals conceded.
            foreach (Goal goal in game.AwayFacts.GoalsConceded)
            {
                //Begin with a goal.
                textWriter.WriteStartElement("Goal");

                //The player who scored.
                textWriter.WriteStartElement("Scorer");
                textWriter.WriteString(goal.Scorer.Team.Id.ToString() + ":" + goal.Scorer.Name);
                textWriter.WriteEndElement();
                //The time of the goal.
                textWriter.WriteStartElement("Minute");
                textWriter.WriteString(goal.Minute.ToString());
                textWriter.WriteEndElement();
                //The type of goal.
                textWriter.WriteStartElement("Type");
                textWriter.WriteString(goal.Type.ToString());
                textWriter.WriteEndElement();

                //End with the goal.
                textWriter.WriteEndElement();
            }

            //End with the goals conceded.
            textWriter.WriteEndElement();

            //Begin with the bookings.
            textWriter.WriteStartElement("Bookings");
            textWriter.WriteAttributeString("Count", game.AwayFacts.Bookings.Count.ToString());

            //The bookings.
            foreach (BookingType booking in game.AwayFacts.Bookings)
            {
                //A booking.
                textWriter.WriteStartElement("Booking");
                textWriter.WriteString(booking.ToString());
                textWriter.WriteEndElement();
            }

            //End with the bookings.
            textWriter.WriteEndElement();

            //End with the away game facts.
            textWriter.WriteEndElement();
            #endregion

            //End with the game.
            textWriter.WriteEndElement();

            //End with the document.
            textWriter.WriteEndDocument();
            //Close the writer.
            textWriter.Close();
        }
        /// <summary>
        /// Load a game.
        /// </summary>
        /// <param name="id">The id of the game to load.</param>
        /// <returns>The loaded game.</returns>
        public static Game LoadGame(int id)
        {
            //In the all too likely event that everything inexplicably goes to hell.
            try
            {
                //Set up and load the xml file.
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(GetGamePath(id));

                //Create the game.
                Game game = new Game(int.Parse(xmlDocument.SelectSingleNode("/Game/Id").InnerText));

                //Parse the xml data.
                game.Date = DateTime.Parse(xmlDocument.SelectSingleNode("/Game/Date").InnerText);
                game.ExtraTime = bool.Parse(xmlDocument.SelectSingleNode("/Game/ExtraTime").InnerText);

                #region HomeFacts
                //The home facts.
                game.HomeFacts = new GameFacts(LoadProfile(int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/Profile").InnerText)));

                //Parse the xml data.
                game.HomeFacts.MatchSide = (MatchSide)Enum.Parse(typeof(MatchSide), xmlDocument.SelectSingleNode("/Game/HomeFacts/MatchSide").InnerText);
                game.HomeFacts.Shots = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/Shots").InnerText);
                game.HomeFacts.ShotsOnTarget = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/ShotsOnTarget").InnerText);
                game.HomeFacts.ShotAccuracy = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/ShotAccuracy").InnerText);
                game.HomeFacts.PassAccuracy = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/PassAccuracy").InnerText);
                game.HomeFacts.Corners = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/Corners").InnerText);
                game.HomeFacts.Possession = int.Parse(xmlDocument.SelectSingleNode("/Game/HomeFacts/Possession").InnerText);

                //The goals scored.
                foreach (XmlNode scoredNode in xmlDocument.SelectNodes("/Game/HomeFacts/GoalsScored/Goal"))
                {
                    //Create a goal.
                    Goal goal = new Goal();

                    //Parse the xml data.
                    goal.Scorer = GetPlayer(scoredNode.SelectSingleNode("Scorer").InnerText);
                    goal.Minute = int.Parse(scoredNode.SelectSingleNode("Minute").InnerText);
                    goal.Type = (GoalType)Enum.Parse(typeof(GoalType), scoredNode.SelectSingleNode("Type").InnerText);

                    //Add the goal to the home facts.
                    game.HomeFacts.GoalsScored.Add(goal);
                }

                //The goals conceded.
                foreach (XmlNode concededNode in xmlDocument.SelectNodes("/Game/HomeFacts/GoalsConceded/Goal"))
                {
                    //Create a goal.
                    Goal goal = new Goal();

                    //Parse the xml data.
                    goal.Scorer = GetPlayer(concededNode.SelectSingleNode("Scorer").InnerText);
                    goal.Minute = int.Parse(concededNode.SelectSingleNode("Minute").InnerText);
                    goal.Type = (GoalType)Enum.Parse(typeof(GoalType), concededNode.SelectSingleNode("Type").InnerText);

                    //Add the goal to the home facts.
                    game.HomeFacts.GoalsConceded.Add(goal);
                }

                //The bookings.
                foreach (XmlNode bookingNode in xmlDocument.SelectNodes("/Game/HomeFacts/Bookings/Booking"))
                {
                    //Parse the xml data.
                    //game.HomeFacts.Bookings.Add((BookingType)Enum.Parse(typeof(BookingType), bookingNode.SelectSingleNode("Booking").InnerText));
                }
                #endregion

                #region AwayFacts
                //The away facts.
                game.AwayFacts = new GameFacts(LoadProfile(int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/Profile").InnerText)));

                //Parse the xml data.
                game.AwayFacts.MatchSide = (MatchSide)Enum.Parse(typeof(MatchSide), xmlDocument.SelectSingleNode("/Game/AwayFacts/MatchSide").InnerText);
                game.AwayFacts.Shots = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/Shots").InnerText);
                game.AwayFacts.ShotsOnTarget = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/ShotsOnTarget").InnerText);
                game.AwayFacts.ShotAccuracy = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/ShotAccuracy").InnerText);
                game.AwayFacts.PassAccuracy = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/PassAccuracy").InnerText);
                game.AwayFacts.Corners = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/Corners").InnerText);
                game.AwayFacts.Possession = int.Parse(xmlDocument.SelectSingleNode("/Game/AwayFacts/Possession").InnerText);

                //The goals scored.
                foreach (XmlNode scoredNode in xmlDocument.SelectNodes("/Game/AwayFacts/GoalsScored/Goal"))
                {
                    //Create a goal.
                    Goal goal = new Goal();

                    //Parse the xml data.
                    goal.Scorer = GetPlayer(scoredNode.SelectSingleNode("Scorer").InnerText);
                    goal.Minute = int.Parse(scoredNode.SelectSingleNode("Minute").InnerText);
                    goal.Type = (GoalType)Enum.Parse(typeof(GoalType), scoredNode.SelectSingleNode("Type").InnerText);

                    //Add the goal to the home facts.
                    game.AwayFacts.GoalsScored.Add(goal);
                }

                //The goals conceded.
                foreach (XmlNode concededNode in xmlDocument.SelectNodes("/Game/AwayFacts/GoalsConceded/Goal"))
                {
                    //Create a goal.
                    Goal goal = new Goal();

                    //Parse the xml data.
                    goal.Scorer = GetPlayer(concededNode.SelectSingleNode("Scorer").InnerText);
                    goal.Minute = int.Parse(concededNode.SelectSingleNode("Minute").InnerText);
                    goal.Type = (GoalType)Enum.Parse(typeof(GoalType), concededNode.SelectSingleNode("Type").InnerText);

                    //Add the goal to the home facts.
                    game.AwayFacts.GoalsConceded.Add(goal);
                }

                //The bookings.
                foreach (XmlNode bookingNode in xmlDocument.SelectNodes("Bookings/Booking"))
                {
                    //Parse the xml data.
                    //game.AwayFacts.Bookings.Add((BookingType)Enum.Parse(typeof(BookingType), bookingNode.SelectSingleNode("Booking").InnerText));
                }
                #endregion

                //Return the game.
                return game;
            }
            catch { return null; }
        }
        /// <summary>
        /// Save a player to an xml file.
        /// </summary>
        /// <param name="player">The player to save.</param>
        public static void SaveSummary(Player player)
        {
            //Create the xml writer.
            XmlTextWriter textWriter = new XmlTextWriter("", null);
            //Set the formatting to use indent.
            textWriter.Formatting = Formatting.Indented;

            //Begin with the player.
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("Summary");

            //The last profile id.
            textWriter.WriteStartElement("LastProfileId");
            //textWriter.WriteValue(player.LastProfileId);
            textWriter.WriteEndElement();
            //The last team id.
            textWriter.WriteStartElement("LastTeamId");
            //textWriter.WriteValue(player.LastTeamId);
            textWriter.WriteEndElement();
            //The last game id.
            textWriter.WriteStartElement("LastGameId");
            //textWriter.WriteValue(player.LastGameId);
            textWriter.WriteEndElement();

            //End with the summary.
            textWriter.WriteEndElement();

            //End with the document.
            textWriter.WriteEndDocument();
            //Close the writer.
            textWriter.Close();
        }
        /// <summary>
        /// Get the path to a particular profile's file.
        /// </summary>
        /// <param name="id">The id of the profile.</param>
        /// <returns>The profile with the given id.</returns>
        public static string GetProfilePath(int id)
        {
            //Return the path.
            return @"Data\Profiles\P" + id.ToString() + ".xml";
        }
        /// <summary>
        /// Get the path to a particular team's file.
        /// </summary>
        /// <param name="id">The id of the team.</param>
        /// <returns>The team with the given id.</returns>
        public static string GetTeamPath(int id)
        {
            //Return the path.
            return @"Data\Teams\T" + id.ToString() + ".xml";
        }
        /// <summary>
        /// Get the path to a particular game's file.
        /// </summary>
        /// <param name="id">The id of the game.</param>
        /// <returns>The game with the given id.</returns>
        public static string GetGamePath(int id)
        {
            //Return the path.
            return @"Data\Games\G" + id.ToString() + ".xml";
        }
        /// <summary>
        /// Get the path to the summary's file.
        /// </summary>
        /// <returns>The summary's path.</returns>
        public static string GetSummaryPath()
        {
            return @"Data\Summary.xml";
        }
        /// <summary>
        /// Create a player instance from a jangled set of characters, ie. it's id.
        /// The id is a mix of the player's team's id and its own name. The denominator is a ':'.
        /// </summary>
        /// <param name="id">The id of the player.</param>
        /// <returns>The player corresponding to the given id.</returns>
        public static Player GetPlayer(string id)
        {
            //If the id is not valid.
            if (!Regex.IsMatch(id, @"^\d+:{1}\D+$")) { return null; }

            return new Player(id.Substring(id.IndexOf(':') + 1), LoadTeam(int.Parse(id.Substring(0, id.IndexOf(':')))));
        }
        /// <summary>
        /// Whether a list contains a specific player. The method tries to match the players' ids rather than reference.
        /// </summary>
        /// <param name="players">The list of players.</param>
        /// <param name="player">The player to try and find.</param>
        /// <returns>The player that matched.</returns>
        public static Player ContainsPlayer(List<Player> players, Player player)
        {
            //For all players in the list, check for a match.
            foreach (Player p in players) { if (player.Name.Equals(p.Name) && player.Team.Id.ToString().Equals(p.Team.Id.ToString())) { return p; } }

            //No player found.
            return null;
        }
    }
}
