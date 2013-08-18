using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Models;
using Unscrambled.Areas.Coordinator.Functions;
using UnscrambledMVC4.Areas.Coordinator.Validations;
using UnscrambledMVC4.Infrastructure;
using System.Text.RegularExpressions;

namespace Unscrambled.Domain
{
    public class GameRepository
    {
        private UnscrambledDBEntities db = new UnscrambledDBEntities();

        public IEnumerable<Game> GetGames(int CoordinatorID)
        {
            using (db)
            {
                var games = db.Games.Include(g => g.Coordinator).Include(g => g.GameType).Include(g => g.Rounds).Include(g => g.Players).Include(g=>g.GameState).Where(g => g.CoordinatorID == 1);
                return games.ToList();
            }
        }

        public IEnumerable<Player> GetPlayers(int GameID)
        {
            using (db)
            {
                var players = db.Players.Where(p => p.GameID == GameID);
                return players.ToList();
            }
        }

        public List<Player> GetPlayersThatWillBeDeleted(int GameID, int numberOfPlayersToDelete)
        {
                
                IEnumerable<Player> players = db.Players.Where(p => p.GameID == GameID);
                List<Player> playersToDelete = new List<Player>();
                
                int numberOfPlayersToStart = players.Count();
                for (var i = numberOfPlayersToStart - numberOfPlayersToDelete; i < numberOfPlayersToStart; i++)
                {
                    playersToDelete.Add(players.ElementAt(i));
                }
                return playersToDelete.ToList();
        }

        public List<RoundFoursome> GetDeletableFoursomes(int roundId)
        {

            List<RoundFoursome> foursomes = db.RoundFoursomes.Include(f => f.FoursomePlayers).Where(f => f.RoundID == roundId).ToList();
            List<RoundFoursome> foursomesToDelete = new List<RoundFoursome>();

            foreach (var foursome in foursomes)
            {
                if (foursome.FoursomePlayers.Count() == 0)
                {
                    RoundFoursome roundFoursome = new RoundFoursome();
                    roundFoursome = foursome;
                    foursomesToDelete.Add(roundFoursome);
                }
            }

            return foursomesToDelete;
        }

        public List<Round> GetRoundsThatWillBeDeleted(int GameID, int numberOfRoundsToDelete)
        {

            IEnumerable<Round> rounds = db.Rounds.Where(p => p.GameID == GameID);
            List<Round> roundsToDelete = new List<Round>();

            int numberOfRoundsToStart = rounds.Count();
            for (var i = numberOfRoundsToStart - numberOfRoundsToDelete; i < numberOfRoundsToStart; i++)
            {
                roundsToDelete.Add(rounds.ElementAt(i));
            }
            return roundsToDelete.ToList();
        }

        public IEnumerable<RoundPlayerHandicap> GetRoundPlayerHandicapsByRoundID(int roundId)
        {

                var roundPlayerHandicaps = db.RoundPlayerHandicaps.Include(r => r.Player).Where(r => r.RoundID == roundId);
                return roundPlayerHandicaps.ToList();
        }

        public RoundPlayerHandicap GetRoundPlayerHandicapByID(int roundPlayerHandicapId)
        {

            RoundPlayerHandicap roundPlayerHandicap = db.RoundPlayerHandicaps.Include(r => r.Player).Where(r => r.ID == roundPlayerHandicapId).SingleOrDefault();
            return roundPlayerHandicap;
        }

        public RoundPlayerHandicap GetRoundPlayerHandicapByRoundIdAndPlayerId(int roundPlayerHandicapId, int playerId)
        {

            RoundPlayerHandicap roundPlayerHandicap = db.RoundPlayerHandicaps.Include(r => r.Player).Where(r => r.ID == roundPlayerHandicapId && r.PlayerID == playerId).SingleOrDefault();
            return roundPlayerHandicap;
        }

        public IEnumerable<Hole> GetHoles(int RoundID)
        {
            UnscrambledDBEntities _db = new UnscrambledDBEntities();
            var holes = _db.Holes.Where(h => h.RoundID == RoundID);
            return holes.ToList();
        }

        public Round GetRoundByID(int id)
        {
                var round = db.Rounds.Find(id);
                return round;

        }

        public Game GetGameByID(int id)
        {
            var game = db.Games.Find(id);
            return game;

        }

        public Round GetFirstRound(int id)
        {
            var round = db.Rounds.Where(r => r.GameID == id && r.RoundNumberInGame == 1).SingleOrDefault();

            return round;

        }

        public GameSetupState GetGameSetupState(int id)
        {
            var gameSetupState = db.GameSetupStates.Where(g => g.GameID == id).FirstOrDefault();
            return gameSetupState;

        }

        public String GetGameType(int id)
        {
            String gameType;
            Game game = db.Games.Include(g=>g.GameType).Where(g=>g.ID==id).SingleOrDefault();
            gameType = game.GameType.Name;
            return gameType;

        }

        public Round GetRoundAndHolesByID(int id)
        {
                var round = db.Rounds.Include(r => r.Holes).Where(r => r.ID == id).SingleOrDefault();
                return round;
        }

        public IEnumerable<Round> GetRoundsByGameID(int gameID)
        {
            var rounds = db.Rounds.Include(r => r.Holes).Where(r => r.GameID == gameID);
            return rounds.ToList();
        }


        public Game GetGameByRoundID(int roundId)
        {
            Round round = db.Rounds.Find(roundId);
            Game game = db.Games.Find(round.GameID);
            return game;
        }

        public GameLeaderboard GetGameLeaderboardByGameID(int gameId)
        {
            GameLeaderboard gameLeaderboard = db.GameLeaderboards.Where(g => g.GameID == gameId).SingleOrDefault();           
            return gameLeaderboard;
        }



        public Game GetGameRoundsAndPlayersByID(int id)
        {
            // Game game = db.Games.Find(id);
            // Game game = db.Games.Find(id);
            Game game = db.Games.Include(g => g.Coordinator).Include(g => g.GameType).Include(g => g.Rounds).Include(g => g.Players).Where(g => g.ID == id).SingleOrDefault();

            return game;
        }

        public Game GetGameAndPlayersByID(int id)
        {
                Game game = db.Games.Include(g => g.Players).Where(g => g.ID == id).SingleOrDefault();
                return game;      
        }

        public Game GetGameAndRoundsByID(int id)
        {
            // Game game = db.Games.Find(id);
            // Game game = db.Games.Find(id);
            Game game = db.Games.Include(g => g.Coordinator).Include(g => g.GameType).Include(g => g.Rounds).Where(g => g.ID == id).SingleOrDefault();

            return game;
        }


        public IEnumerable<GameType> GetAllGameTypes()
        {
            var gameTypes = db.GameTypes;
            return gameTypes;
        }

        // Creates the Game, adds placeholder for players and creates the rounds.
        public bool CreateGame(Game game)
        {
            int gameId;
            int roundId;
 
            // game.GameStateID = 1; // Set to Setup
            var opStatus = new OperationStatus { Status = true };

            using (db)
            {
                try
                {
                    db.Games.Add(game);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    if (ex1.InnerException.ToString().Contains("UNQ_Games_JoinCode"))
                    {
                        return false;
                    }
                    opStatus = OperationStatus.CreateFromException("Error creating game " + ex1.Message, ex1);
                }

                gameId = game.ID;

                GameSetupState gameSetupState = new GameSetupState();
                gameSetupState.GameID = gameId;
                gameSetupState.SetupStateGameDetails = "COMPLETE";
                try
                {
                    db.GameSetupStates.Add(gameSetupState);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }


                for (int p = 1; p <= game.NumberOfPlayersOrTeams; p++)
                {
                    Player player = new Player();
                    player.GameID = gameId;
                    player.Name = "Player" + p.ToString();
                    if (game.IsHandicapped == "N")
                    {
                        player.PlayerIndex = 0M;
                        //player.PlayerIndex == null ? 0M : player.PlayerIndex.Value;
                    }
                    db.Players.Add(player);
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex2)
                    {
                        return false;
                    }


                }

                // Add rounds to game

                for (int i = 1; i <= game.NumberOfRounds; i++)
                {
                    Round round = new Round();
                    round.GameID = gameId;
                    round.RoundNumberInGame = i;
                    round.HolesInRound = 18;
                    round.RoundStateID = 1;
                    db.Rounds.Add(round);

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex2)
                    {
                        return false;
                    }


                    roundId = round.ID;
                    for (int j = 1; j <= 18; j++)
                    {
                        Hole hole = new Hole();
                        hole.RoundID = roundId;
                        hole.HoleNumber = j;
                        db.Holes.Add(hole);
                    }

                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception ex3)
                    {
                        return false;
                    }
                }


                // Create RoundPlayerHandicaps
                var rounds = db.Rounds.Where(r => r.GameID == game.ID).ToList();
                var players = db.Players.Where(p => p.GameID == game.ID).ToList();
                foreach (var roundToUpdateHandicaps in rounds)
                {
                    foreach (var player in players)
                    {
                        RoundPlayerHandicap roundPlayerHandicap = new RoundPlayerHandicap();
                        roundPlayerHandicap.RoundID = roundToUpdateHandicaps.ID;
                        roundPlayerHandicap.PlayerID = player.ID;
                        roundPlayerHandicap.CalcHandicapFromIndex = game.CalcHandicapsFromIndex;
                        db.RoundPlayerHandicaps.Add(roundPlayerHandicap);
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception ex3)
                        {
                            return false;
                        }
                    }
                }
                        



                foreach (var roundToUpdate in rounds)
                {
                   
                    RoundFoursome foursome = AddFoursome(roundToUpdate);
                    if (foursome.ID > 0) // That means it was added in step prior
                    {
                        foreach (var playerInGame in players)
                        {
                            FoursomePlayer foursomePlayer = AddFoursomePlayer(foursome, playerInGame);
                            foursome = db.RoundFoursomes.Find(foursomePlayer.RoundFoursomeID);
                        }
                    }

                }

                foreach (var roundToUpdateSetupState in rounds)
                {
                    // Set Player Setup Status to INCOMPLETE
                    RoundSetupState roundSetupState = new RoundSetupState();
                    roundSetupState.RoundID = roundToUpdateSetupState.ID;
                    roundSetupState.State = "INCOMPLETE";

                    try
                    {
                        db.RoundSetupStates.Add(roundSetupState);
                        db.SaveChanges();
                    }
                    catch (Exception ex1)
                    {
                        return false;
                    }
                }


            }
               
            return true;
        }

        public RoundFoursome AddFoursome(Round round)
        {
                
                
                
                List<RoundFoursome> roundFoursomes = db.RoundFoursomes.Where(r => r.RoundID == round.ID).ToList();;
                int currentFoursomeCount = roundFoursomes.Count();

                int maxFoursomeNumber = roundFoursomes.Max(rf => rf.FoursomeNumberInRound);

                RoundFoursome foursome = new RoundFoursome();
                foursome.Name = "Group" + (maxFoursomeNumber + 1).ToString();
                foursome.RoundID = round.ID;
                foursome.FoursomeNumberInRound = currentFoursomeCount + 1;
                try
                {
                    db.RoundFoursomes.Add(foursome);
                    db.SaveChanges();          
                }
                catch (Exception ex1)
                {
                    return foursome;
                }

            return foursome;
        }

         public FoursomePlayer AddFoursomePlayer(RoundFoursome foursome, Player player)
        {
                int playersInFoursomeCounter = db.FoursomePlayers.Where(p => p.RoundFoursomeID == foursome.ID).Count();
                if (playersInFoursomeCounter > 3)
                {
                    Round round = db.Rounds.Find(foursome.RoundID);
                    foursome = AddFoursome(round);
                }
                
                FoursomePlayer foursomePlayer = new FoursomePlayer();
                foursomePlayer.RoundFoursomeID = foursome.ID;
                foursomePlayer.PlayerID = player.ID;
            
                try
                {
                    db.FoursomePlayers.Add(foursomePlayer);
                    db.SaveChanges();          
                }
                catch (Exception ex1)
                {
                    return foursomePlayer;
                }

            return foursomePlayer;
        }

        public bool EditGame(Game gameFromForm)
        {
            
            Game gameFromDB = db.Games.Find(gameFromForm.ID);
            Game copyOfDB = gameFromDB;

            int gameId = gameFromDB.ID;



            if (gameFromForm.NumberOfRounds > gameFromDB.NumberOfRounds)
            {

                int numberOfRoundsToAdd = gameFromForm.NumberOfRounds - gameFromDB.NumberOfRounds;
                for (int i = 1; i <= numberOfRoundsToAdd; i++)
                {
                    if (!AddRound(gameId))
                    {
                        return false;
                    }
                }

            }


            // *******************************************************************
            // If the game now has more players, add the players to the game and 
            // update Game Details


            if (gameFromForm.NumberOfPlayersOrTeams > gameFromDB.NumberOfPlayersOrTeams)
            {
                int numberOfPlayersToAdd = gameFromForm.NumberOfPlayersOrTeams - gameFromDB.NumberOfPlayersOrTeams;
                for (int i = 1; i <= numberOfPlayersToAdd; i++)
                {
                    Player player = new Player();
                    player.GameID = gameId;
                    player.Name = "";
                    if (gameFromForm.IsHandicapped == "N")
                    {
                        player.PlayerIndex = 0M;
                    }
                    // Add player through the AddPlayer method in this GameRepository
                    if (!AddPlayer(player))
                    {
                        return false;
                    }
                    
                }
            }
            // **************************************************************************
            // If the game has less players, delete the last players from the game
            else if (gameFromForm.NumberOfPlayersOrTeams < gameFromDB.NumberOfPlayersOrTeams)
            {
                int numberOfPlayersToDelete = gameFromForm.NumberOfPlayersOrTeams - gameFromDB.NumberOfPlayersOrTeams;
                int numberOfPlayersFromDB = gameFromDB.NumberOfPlayersOrTeams;
                IEnumerable<Player> players = db.Players.Where(g => g.GameID == gameFromDB.ID);

                for (int j = gameFromDB.NumberOfPlayersOrTeams; j > gameFromForm.NumberOfPlayersOrTeams; j--)
                {
                    if (!DeletePlayerFromGame(gameFromDB, players.ElementAt(j - 1)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool DeleteGameAndEverythingElse(int id)
        {

            String deleteHolesSQL = "";
            String deleteRoundsSQL = "";
            String deletePlayersSQL = "";
            String deleteGameSQL = "";
            String deletePlayerGameResultSQL = "";
            String deletePlayerRoundResultSQL = "";
            String deleteScorecardSQL = "";
            String deleteGameLeaderboardSQL = "";
            String deleteRoundLeaderboardSQL = "";

            try
            {
                // 1) Get the game
                Game game = GetGameRoundsAndPlayersByID(id);

                // 2) Delete the holes in each round
                foreach (var round in game.Rounds)
                {
                    if (game.Scorecards.Count > 0)
                    {
                        foreach (var scorecard in game.Scorecards)
                        {
                            deleteScorecardSQL = "DELETE FROM ScorecardHoles where ScorecardID = " + scorecard.ID;
                            db.Database.ExecuteSqlCommand(deleteScorecardSQL);
                        }
                        
                 
                        deleteScorecardSQL = "DELETE FROM Scorecards where GameID = " + game.ID;
                        db.Database.ExecuteSqlCommand(deleteScorecardSQL);
                    }

                    deleteHolesSQL = "DELETE FROM Holes where RoundID = " + round.ID;
                    db.Database.ExecuteSqlCommand(deleteHolesSQL);
                }



                GameLeaderboard gameLeaderboard = db.GameLeaderboards.Include(g => g.PlayerGameResults).Where(g => g.GameID == game.ID).SingleOrDefault();

                if (gameLeaderboard != null)
                {
                    foreach (var playerGameResult in gameLeaderboard.PlayerGameResults)
                    {
                        deletePlayerRoundResultSQL = "DELETE FROM PlayerRoundResults where PlayerGameResultID = " + playerGameResult.ID;
                        db.Database.ExecuteSqlCommand(deletePlayerRoundResultSQL);
                    }

                    deletePlayerGameResultSQL = "DELETE FROM PlayerGameResults where GameLeaderboardID = " + gameLeaderboard.ID;
                    db.Database.ExecuteSqlCommand(deletePlayerGameResultSQL);

                    deleteRoundLeaderboardSQL = "Delete FROM RoundLeaderboards where GameLeaderboardID = " + gameLeaderboard.ID;
                    db.Database.ExecuteSqlCommand(deleteRoundLeaderboardSQL);

                    deleteGameLeaderboardSQL = "DELETE FROM GameLeaderboards where GameID = " + game.ID;
                    db.Database.ExecuteSqlCommand(deleteGameLeaderboardSQL);
                }

                // 3) Delete the rounds in the game
                deleteRoundsSQL = "DELETE FROM Rounds where GameID = " + id;
                db.Database.ExecuteSqlCommand(deleteRoundsSQL);

                

                // 4) Delete the players in the game
                deletePlayersSQL = "DELETE FROM Players where GameID = " + id;
                db.Database.ExecuteSqlCommand(deletePlayersSQL);

                // 5) Delete the game
                deleteGameSQL = "DELETE FROM Games where ID = " + id;
                db.Database.ExecuteSqlCommand(deleteGameSQL);
            }

            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public int GetPlayerCount(int id)
        {
            return db.Players.Where(p => p.GameID == id).Count();
        }

        public IEnumerable<Player> GetPlayersByGameID(int gameId)
        {
            var players = db.Players.Where(p => p.GameID == gameId);
            return players;
        }

        public Player GetPlayerByID(int id)
        {
            Player player = db.Players
                        .Where(p => p.ID == id)
                        .FirstOrDefault();
            return player;
        }

        public RoundFoursome GetFoursomeByID(int id)
        {
            RoundFoursome roundFoursome = db.RoundFoursomes.Find(id);
            return roundFoursome;
        }

        public int GetSlope(int roundId)
        {
            Round round = db.Rounds.Find(roundId);
            
                        
            return round.ID;
        }

        public Round GetFirstRoundInGameByGameID(int gameId)
        {
            Round round = db.Rounds.Where(r => r.GameID == gameId && r.RoundNumberInGame == 1).SingleOrDefault();

            return round;
        }

        public bool UpdatePlayer(Player player)
        {
            try
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool UpdateRoundTo9Holes(int roundId)
        {
            Round round = db.Rounds.Find(roundId);
            round.HolesInRound = 9;
            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            if (!DeleteBackNine(roundId))
            {
                return false;
            }


            return true;
        }

        

        public bool UpdateRoundTo18Holes(int roundId)
        {
            Round round = db.Rounds.Include("Holes").Where(r => r.ID == roundId).SingleOrDefault();
            if (round.Holes.Count == 9)
            {
                round.HolesInRound = 18;
                try
                {
                    db.Entry(round).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }

                for (var i = 10; i <= 18; i++)
                {
                    Hole holeToAdd = new Hole();
                    holeToAdd.RoundID = roundId;
                    holeToAdd.HoleNumber = i;
                    try
                    {
                        db.Holes.Add(holeToAdd);
                        db.SaveChanges();
                    }
                    catch (Exception ex1)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateRoundSlope(int roundId, int? slope)
        {
            Round round = db.Rounds.Find(roundId);
            round.Slope = slope;
            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool UpdateRound(Round round)
        {
            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool SetGameToActive(Game game)
        {

            try
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool CalculateHandicapsFromSlopeAndIndex(int roundId)
        {
            Round round = db.Rounds.Find(roundId);
            CoordinatorLibrary cl = new CoordinatorLibrary();

            IEnumerable<RoundPlayerHandicap> roundPlayerHandicaps = db.RoundPlayerHandicaps.Include(r => r.Player).Where(r => r.RoundID == roundId).ToList();
            

            foreach (RoundPlayerHandicap roundPlayerHandicap in roundPlayerHandicaps)
            {
                if (roundPlayerHandicap.CalcHandicapFromIndex == "Y")
                {
                    roundPlayerHandicap.Handicap = cl.CalculateHandicap(round.Slope, roundPlayerHandicap);

                    try
                    {
                        db.Entry(roundPlayerHandicap).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool UpdateRoundPlayersAndHoles(Round round)
        {

            // 1) Update Round
            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            // 2) Update Handicaps in Player Table

            Game game = GetGameAndPlayersByID(round.GameID);

            CoordinatorLibrary cL = new CoordinatorLibrary();
        
            

            foreach (var hole in round.Holes)
            {
                try
                {
                    //db.Holes.Find(hole.ID);
                    db.Entry(hole).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            return true;
        }



        public bool UpdatePlayerHandicaps(int gameId)
        {

            // This method is used to recalculate player handicaps for each round in the game.
            // It take the gameId.  It loops through each player and calculates the player handicap and
            // updates the DB accordingly.

            
            Game game = GetGameRoundsAndPlayersByID(gameId);

            CoordinatorLibrary cL = new CoordinatorLibrary();

            

            return true;

           
        }

        public bool AddPlayer(Player player)
        {
         
            try
            {
                if (String.IsNullOrEmpty(player.Name))
                {
                    int playerCount = db.Players.Where(p => p.GameID == player.GameID).Count();
                    player.Name = "Player" + (playerCount+ 1).ToString();
                }
                db.Players.Add(player);
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }

            Game game = db.Games.Find(player.GameID);
            try
            {
                game.NumberOfPlayersOrTeams++;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }
            

            CoordinatorLibrary cl = new CoordinatorLibrary();


            var rounds = db.Rounds.Include(r => r.RoundPlayerHandicaps).Where(r => r.GameID == player.GameID).ToList();

            foreach (var round in rounds)
            {

                RoundPlayerHandicap rph = new RoundPlayerHandicap();


                rph.CalcHandicapFromIndex = "Y";
                rph.PlayerID = player.ID;
                rph.RoundID = round.ID;
                    
                db.RoundPlayerHandicaps.Add(rph);
                try
                {
                        
                        
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }

                // Add to Player to a Foursome

                IEnumerable<RoundFoursome> roundFoursomes = GetRoundFoursomesByRoundID(round.ID);
                Boolean existingFoursomeAvailable = false;
                FoursomePlayer foursomePlayerToAdd = new FoursomePlayer();
                foursomePlayerToAdd.PlayerID = player.ID;

                foreach (RoundFoursome roundFoursome in roundFoursomes)
                {
                    if (!existingFoursomeAvailable)
                    {
                        if (roundFoursome.FoursomePlayers.Count < 4)
                        {
                            existingFoursomeAvailable = true;
                            foursomePlayerToAdd.RoundFoursomeID = roundFoursome.ID;
                        }
                    }
                }
                if (!existingFoursomeAvailable)
                {
                    RoundFoursome addedRoundFoursome = AddFoursome(round);
                    foursomePlayerToAdd.RoundFoursomeID = addedRoundFoursome.ID;
                }

                db.FoursomePlayers.Add(foursomePlayerToAdd);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }
            }


            foreach (var round in rounds)
            {
                if (!CalculateHandicapsFromSlopeAndIndex(round.ID))
                {
                    return false;
                }
            }

        return true;
        }



        public bool AddRound(int gameId)
        {
            Round round = new Round();
            round.HolesInRound = 18;
            round.GameID = gameId;
            round.RoundStateID = 1;
            
            try
            {
                db.Rounds.Add(round);
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }

           int roundId = round.ID;
            for (int j = 1; j <= 18; j++)
            {
                Hole hole = new Hole();
                hole.RoundID = roundId;
                hole.HoleNumber = j;
                db.Holes.Add(hole);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex3)
            {
                return false;
            }
        


        // Create RoundPlayerHandicaps
        //var rounds = db.Rounds.Where(r => r.GameID == game.ID).ToList();
        Game game = db.Games.Find(round.GameID);
        game.NumberOfRounds++;

        try
        {
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
        }
        catch (Exception ex3)
        {
            return false;
        }

        var players = db.Players.Where(p => p.GameID == round.GameID).ToList();
        
            foreach (var player in players)
            {
                RoundPlayerHandicap roundPlayerHandicap = new RoundPlayerHandicap();
                roundPlayerHandicap.RoundID = round.ID;
                roundPlayerHandicap.PlayerID = player.ID;
                roundPlayerHandicap.CalcHandicapFromIndex = game.CalcHandicapsFromIndex;
                db.RoundPlayerHandicaps.Add(roundPlayerHandicap);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex3)
                {
                    return false;
                }
            }

            return true;
        }

            
        

      

        public bool UpdateHole(Hole hole)
        {

            try
            {
                db.Entry(hole).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool UpdateRoundPlayerHandicap(RoundPlayerHandicap roundPlayerHandicap)
        {

            try
            {
                db.Entry(roundPlayerHandicap).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool UpdateRoundFoursome(RoundFoursome roundFoursome)
        {
            try
            {
                db.Entry(roundFoursome).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }

        public bool UpdateGameCalcHandicapFlagtoN(int gameId)
        {
            Game game = db.Games.Find(gameId);
            game.CalcHandicapsFromIndex = "N";

            try
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            var rounds = db.Rounds.Include(r => r.RoundPlayerHandicaps).Where(r => r.GameID == game.ID).ToList();
            foreach (var round in rounds)
            {
                foreach (var roundPlayerHandicap in round.RoundPlayerHandicaps)
                {
                    roundPlayerHandicap.CalcHandicapFromIndex = "N";
                    try
                    {
                        db.Entry(roundPlayerHandicap).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool UpdateGameCalcHandicapFlagtoY(int gameId)
        {
            Game game = db.Games.Find(gameId);
            game.CalcHandicapsFromIndex = "Y";

            try
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            var rounds = db.Rounds.Include(r => r.RoundPlayerHandicaps).Where(r => r.GameID == game.ID).ToList();
            foreach (var round in rounds)
            {
                foreach (var roundPlayerHandicap in round.RoundPlayerHandicaps)
                {
                    roundPlayerHandicap.CalcHandicapFromIndex = "Y";
                    try
                    {
                        db.Entry(roundPlayerHandicap).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                }
            }

            return true;

        }




        public bool UpdateFoursomePlayerNumber(int foursomePlayerId, int RoundFoursomeID)
        {
            FoursomePlayer foursomePlayer = db.FoursomePlayers.Find(foursomePlayerId);
            foursomePlayer.RoundFoursomeID = RoundFoursomeID;
         
            
            try
            {
                db.Entry(foursomePlayer).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            
            return true;

        }


        public bool UpdateNumberOfPlayers(int gameID, int numberOfPlayersToAdd)
        {
            // Updating the NumberOfPlayers in the Game Table will happen when you call the AddPlayer method


            for (int p = 1; p <= numberOfPlayersToAdd; p++)
            {
                Player player = new Player();
                player.GameID = gameID;
                player.Name = "";
                if (!AddPlayer(player))
                {
                    return false;
                }
            }

            return true;

        }

        public bool DeletePlayerFromGame(Game game, Player player)
        {
            try
            {
                {
                    String deleteFoursomePlayerSQL = "DELETE FROM FoursomePlayers where PlayerID = " + player.ID;
                    db.Database.ExecuteSqlCommand(deleteFoursomePlayerSQL);
                    String deleteRoundPlayerHandicapSQL = "DELETE FROM RoundPlayerHandicaps where PlayerID = " + player.ID;
                    db.Database.ExecuteSqlCommand(deleteRoundPlayerHandicapSQL);
                    String deletePlayerSQL = "DELETE FROM Players where ID = " + player.ID;
                    db.Database.ExecuteSqlCommand(deletePlayerSQL);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            try
            {
                game.NumberOfPlayersOrTeams = game.NumberOfPlayersOrTeams - 1;
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }



        public bool DeletePlayersFromGame(IEnumerable<Player> playersToDelete)
        {
            Game game = db.Games.Find(playersToDelete.ElementAt(0).GameID);
            foreach (var player in playersToDelete)
            {
                if (!DeletePlayerFromGame(game, player))
                {
                    return false;
                }
            }
            return true;

        }

        public bool DeleteEmptyFoursomesFromRound(int roundId)
        {
            List<RoundFoursome> emptyFoursomes = GetDeletableFoursomes(roundId).ToList();

            foreach (var foursome in emptyFoursomes)
            {
                if (!DeleteFoursomeFromRound(foursome))
                {
                    return false;
                }
            }

            return true;
        }


        public bool DeleteFoursomeFromRound(RoundFoursome roundFoursome)
        {
            try
            {
                {
                    String deleteFoursomeSQL = "DELETE FROM RoundFoursomes where ID = " + roundFoursome.ID;
                    db.Database.ExecuteSqlCommand(deleteFoursomeSQL);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            List<RoundFoursome> roundFoursomes = db.RoundFoursomes.Where(rf => rf.RoundID == roundFoursome.RoundID).OrderBy(rf => rf.FoursomeNumberInRound).ToList();
            List<RoundFoursome> updatedFoursomeList = new List<RoundFoursome>();
            int numberOfFoursomes = roundFoursomes.Count();

            for (int i = 0; i<=numberOfFoursomes-1; i++)
            {
                RoundFoursome aRoundFoursome = roundFoursomes.Where(aR => aR.FoursomeNumberInRound == i+1).FirstOrDefault();
                if (aRoundFoursome != null)
                {
                    updatedFoursomeList.Add(aRoundFoursome);
                    roundFoursomes.RemoveAt(i);
                    i--;
                    numberOfFoursomes--;
                }
                else
                {
                    int min = roundFoursomes.Min(foursomeNumber => foursomeNumber.FoursomeNumberInRound);
                    aRoundFoursome = roundFoursomes.Where(aR => aR.FoursomeNumberInRound == min).FirstOrDefault();
                    aRoundFoursome.FoursomeNumberInRound = updatedFoursomeList.Count() + 1;
                    aRoundFoursome.Name = "Group" + aRoundFoursome.FoursomeNumberInRound.ToString();
                    updatedFoursomeList.Add(aRoundFoursome);
                    roundFoursomes.RemoveAt(i);
                    i--;
                    numberOfFoursomes--;
                }
            }

            foreach (RoundFoursome updatedFoursome in updatedFoursomeList)
            {
                try
                {
                    db.Entry(updatedFoursome).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;

        }

        // Delete the last 9 nine holes from the hole table and set the Round.HolesInRound to 9
        public bool DeleteBackNine(int roundID)
        {
            for (int i = 10; i <= 18; i++)
                try
                {
                
                    if (roundID != null)
                    {
                        String deleteHolesSQL = "DELETE FROM Holes where RoundID = " + roundID + " And HoleNumber = " + i;
                        db.Database.ExecuteSqlCommand(deleteHolesSQL);
                    }

                }
            catch (Exception ex)
            {
                return false;
            }

            Round round = db.Rounds.Find(roundID);
            round.HolesInRound = 9;

            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public bool AddBackNine(int roundID)
        {


            for (int j = 10; j <= 18; j++)
            {
                Hole hole = new Hole();
                hole.RoundID = roundID;
                hole.HoleNumber = j;
                db.Holes.Add(hole);
            }

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            Round round = db.Rounds.Find(roundID);
            round.HolesInRound = 18;

            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<GameValidationError> IsGameReady(Game inGame, Round round)
        {
            Game game = GetGameAndPlayersByID(inGame.ID);
            
            // GameValidationError gve = new GameValidationError();
            List<GameValidationError> errorList = new List<GameValidationError>();
            List<GameValidationWarning> warningList = new List<GameValidationWarning>();
            // ***********************************************************************
            // Check Players


            if (!ValidateNumberOfPlayers(game))
            {
                GameValidationError gve = new GameValidationError()
                {
                    Category = "SetPlayers",
                    ErrorMessage = "Number of players is wrong",
                    ActionID = game.ID,
                };
                errorList.Add(gve);
            }

            if (!ValidatePlayerIndexes(game))
            {
                GameValidationError gve = new GameValidationError()
                {
                    Category = "SetPlayers",
                    ErrorMessage = "Player indexes not correct.",
                    ActionID = game.ID,
                };
                errorList.Add(gve);
            }         
            

            if (!ValidatePlayerNames(game))
            {
                GameValidationError gve = new GameValidationError()
                {
                    Category = "SetPlayers",
                    ErrorMessage = "Not all player names are set.",
                    ActionID = game.ID,
                };
                errorList.Add(gve);
            }

            // Validate Round
            // *************************************************************************

            if (game.IsHandicapped == "Y" && game.CalcHandicapsFromIndex == "Y")
            {
                    if (!ValidateSlope(round))
                    {
                        GameValidationError gve = new GameValidationError()
                        {
                            Category = "SetRound",
                            ErrorMessage = "Slope invalid or not set",
                            ActionID = game.ID,
                        };
                        errorList.Add(gve);
                    }
            }
            if (game.IsHandicapped == "Y")
            {
                    if (!ValidateHolesHaveParAndHandicap(round))
                    {
                        GameValidationError gve = new GameValidationError()
                        {
                            Category = "SetRound",
                            ErrorMessage = "Hole handicaps are not set.",
                            ActionID = round.ID,
                        };
                        errorList.Add(gve);
                    }


                    if (!ValidateHolesHaveNoDuplicateHandicaps(round))
                    {
                        GameValidationError gve = new GameValidationError()
                        {
                            Category = "SetRound",
                            ErrorMessage = "Duplicate handicaps found for some holes.",
                            ActionID = round.ID, 
                        };
                        errorList.Add(gve);
                    }
            }
            
            if (game.IsHandicapped == "N")
            {
                    if (!ValidateHolesHavePar(round))
                    {
                         GameValidationError gve = new GameValidationError()
                        {
                            Category = "SetRound",
                            ErrorMessage = "Some holes missing a par.",
                            ActionID = round.ID, 
                        };
                        errorList.Add(gve);
                    }
            }
            

            if (!ValidateRoundTotals(round))
            {
                GameValidationError gve = new GameValidationError()
                {
                    Category = "SetRound",
                    ErrorMessage = "Round totals are not set.",
                    ActionID = game.ID,
                };
                errorList.Add(gve);
            }

            //*************************************************************************************
            // Validate RoundHandicaps

            if (game.CalcHandicapsFromIndex == "Y" && game.IsHandicapped == "Y")
            {
                if (!ValidateRoundHandicaps(game, round))
                {
                    GameValidationError gve = new GameValidationError()
                    {
                        Category = "SetPlayers",
                        ErrorMessage = "Round handicaps are not set.  Make sure player indexes are set.",
                        ActionID = round.ID,
                    };
                    if (!ValidateSlope(round))
                    {
                        gve.Category = "SetRound";
                        gve.ErrorMessage = "Round slope is not set.";
                        gve.ActionID = round.ID;
                    }

                    errorList.Add(gve);
                }
            }
            else if (game.CalcHandicapsFromIndex == "N")
            {
                if (!ValidateRoundHandicaps(game, round))
                {
                    GameValidationError gve = new GameValidationError()
                    {
                        Category = "SetHandicaps",
                        ErrorMessage = "Round handicaps are not set.",
                        ActionID = round.ID,
                    };
                    errorList.Add(gve);
                }
            }

            return errorList;
            
        }

        public List<GameValidationWarning> DoesGameHaveWarnings(Game inGame, Round round)
        {
            Game game = GetGameAndPlayersByID(inGame.ID);

            List<GameValidationWarning> warningList = new List<GameValidationWarning>();
            // ***********************************************************************
            // Check Players Ind


            if (game.CalcHandicapsFromIndex == "Y" && game.IsHandicapped == "Y")
            {
                if (!ValidateNoZeroIndexes(game))
                {
                    GameValidationWarning gvw = new GameValidationWarning()
                    {
                        Category = "SetPlayers",
                        WarningMessage = "Player indexes not correct.",
                        ActionID = game.ID,
                    };
                    warningList.Add(gvw);
                }
            }

            List<RoundFoursome> emptyFoursomes = GetDeletableFoursomes(round.ID);
            if (emptyFoursomes.Count() > 0)
            {
                GameValidationWarning gvw = new GameValidationWarning()
                {
                    Category = "SetFoursomes",
                    WarningMessage = "Empty foursomes will be deleted if you choose to start round.",
                    ActionID = round.ID,
                };
                warningList.Add(gvw);
         
            }

            if (game.IsHandicapped == "Y")
            {
                List<RoundPlayerHandicap> roundHandicaps = GetRoundPlayerHandicapsByRoundID(round.ID).ToList();
                foreach (var roundHandicap in roundHandicaps)
                {
                    if (roundHandicap.Handicap == 0)
                    {
                        
                        GameValidationWarning gvw = new GameValidationWarning()
                        {
                            Category = "SetHandicaps",
                            WarningMessage = roundHandicap.Player.Name + "has a zero handicap in a handicapped game.  Is that right?",
                            ActionID = round.ID,
                        };
                        if (game.CalcHandicapsFromIndex == "Y")
                        {
                            gvw.Category = "SetPlayers";
                        }
                        warningList.Add(gvw);
                    }              
                }
            }

            return warningList;
        }

        public bool CheckGame(int gameId)
        {
            UnscrambledDBEntities _db = new UnscrambledDBEntities();
            Game game = _db.Games.Find(gameId);
            return true;
        }




        public bool StartGame(Game game, Round round)
        {
            // Setup the Game Leaderboard
            
            GameLeaderboard gameLeaderboard = new GameLeaderboard();
            gameLeaderboard.CurrentRoundNumber = 1;
            gameLeaderboard.GameID = game.ID;
            gameLeaderboard.NumberOfPlayers = game.NumberOfPlayersOrTeams;
            gameLeaderboard.NumberOfRounds = game.NumberOfRounds;

            try
            {
                db.GameLeaderboards.Add(gameLeaderboard);
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }

            // Setup the PlayerGameResults

            List<Player> players = db.Players.Where(p => p.GameID == game.ID).ToList();

            foreach (var player in players)
            {
                PlayerGameResult pGR = new PlayerGameResult();
                pGR.PlayerID = player.ID;
                pGR.GameAdjustedPar = 0;
                pGR.GameAdjustedTotal = 0;
                pGR.GameLeaderboardID = gameLeaderboard.ID;
                pGR.GameNotAdjustedPar = 0;
                pGR.GameNotAdjustedTotal = 0;
                pGR.GameRank = 1;

                try
                {
                    db.PlayerGameResults.Add(pGR);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }
            }

            // Set the game state to In Progress
            game.GameStateID = 2;
            

            try
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }


        public bool StartRound(Game game, Round round)
        { 
            // Setup Round Leaderboard

            RoundLeaderboard roundLeaderboard = new RoundLeaderboard();
            GameLeaderboard gameLeaderboard = GetGameLeaderboardByGameID(game.ID);
            roundLeaderboard.GameID = gameLeaderboard.GameID;
            roundLeaderboard.RoundNumberInGame = round.RoundNumberInGame;
            roundLeaderboard.GameLeaderboardID = gameLeaderboard.ID;
 
            try
            {
                db.RoundLeaderboards.Add(roundLeaderboard);
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }

            // Setup the PlayerRoundResults

            List<Player> players = db.Players.Where(p => p.GameID == game.ID).ToList();
            

            foreach (var player in players)
            {
                PlayerRoundResult pRR = new PlayerRoundResult();
                pRR.PlayerID = player.ID;
                pRR.LastCompletedHole = 0;
                pRR.PlayerGameResultID = db.PlayerGameResults.Where(p => p.PlayerID == player.ID && p.GameLeaderboardID == gameLeaderboard.ID).SingleOrDefault().ID;
                pRR.RoundAdjustedPar = 0;
                pRR.RoundAdjustedTotal = 0;
                pRR.RoundLeaderBoardID = roundLeaderboard.ID;
                pRR.RoundNotAdjustedPar = 0;
                pRR.RoundNotAdjustedTotal = 0;
                pRR.RoundNumberInGame = round.RoundNumberInGame;
                pRR.RoundRank = 1;
                

                try
                {
                    db.PlayerRoundResults.Add(pRR);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }


                // Now setup the scorecards
                Scorecard scoreCard = new Scorecard();
                scoreCard.Back9Total = 0;
                scoreCard.FoursomeID = GetScorecardFoursomeIDForRound(player, round);
                scoreCard.Front9Total = 0;
                scoreCard.FullRoundTotal = 0;
                scoreCard.GameID = game.ID;
                scoreCard.PlayerGameResultID = pRR.PlayerGameResultID ;
                
                scoreCard.PlayerName = player.Name;
                scoreCard.RoundID = round.ID;
                scoreCard.Slope = round.Slope;
                scoreCard.Strokes = db.RoundPlayerHandicaps.Where(rph => rph.RoundID == round.ID && rph.PlayerID == player.ID).SingleOrDefault().Handicap;
                scoreCard.PlayerRoundResultID = pRR.ID;

                try
                {
                    db.Scorecards.Add(scoreCard);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }



                List<Hole> holes = db.Holes.Where(h=>h.RoundID == round.ID).ToList();
                foreach (Hole hole in holes)
                {
                    ScorecardHole scoreCardHole = new ScorecardHole();
                    scoreCardHole.HoleNumber = hole.HoleNumber;
                    scoreCardHole.Handicap = hole.Handicap.Value;
                    scoreCardHole.Par = hole.Par.Value;
                    scoreCardHole.ScorecardID = scoreCard.ID;
                    try
                    {
                        db.ScorecardHoles.Add(scoreCardHole);
                        db.SaveChanges();
                    }
                    catch (Exception ex1)
                    {
                        return false;
                    }
                }  
            }

            round.RoundStateID = 2;
            try
            {
                db.Entry(round).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }


            return true;
        }     
        private bool ValidateNumberOfPlayers(Game game)
        {
            if (game.NumberOfPlayersOrTeams != game.Players.Count())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidatePlayerNames(Game game)
        {
            foreach (var player in game.Players)
            {
                if (String.IsNullOrEmpty(player.Name))
                {
                    return false;
                }

                

            }

            return true;
    
        }

        private bool ValidatePlayerNamesNotDefault(Game game)
        {
            foreach (Player player in game.Players)
            {
                Match match = Regex.Match(player.Name, "Player([0-9])+", RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        return false;
                    }       
            }
            return true;
        }

        private bool ValidatePlayerIndexes(Game game)
        {
            foreach (var player in game.Players)
            {
                if (player.PlayerIndex == null)
                {
                    return false;
                }

                
            }

            return true;

        }

        private bool ValidateNoZeroIndexes(Game game)
        {
            foreach (var player in game.Players)
            {
                if (player.PlayerIndex == 0)
                {
                    return false;
                }
                
            }

            return true;

        }

      

        private bool ValidateRoundHandicaps(Game game, Round round) // Validates RoundHandicaps are set for one round
        {
            // List<RoundPlayerHandicap> roundPlayerHandicaps = db.RoundPlayerHandicaps.Include(r => r.Player).Where(r => r.RoundID == round.ID).ToList();
            foreach (var player in game.Players)
            {
                RoundPlayerHandicap roundPlayerHandicap = db.RoundPlayerHandicaps.Where(r => r.PlayerID == player.ID).SingleOrDefault();
                if (roundPlayerHandicap == null)
                {
                    return false;
                }

                if (game.IsHandicapped == "Y")
                {
                    if (roundPlayerHandicap.Handicap == null)
                    {
                        return false;
                    }
                    else if (roundPlayerHandicap.Handicap > 50 || roundPlayerHandicap.Handicap < -10)
                    {
                        return false;
                    }
                }
            }


            return true;

        }


        private bool ValidateRoundTotals(Round round)
        {
                if (round.Front9Total < 15 || round.Front9Total == null)
                {
                    return false;
                }
                if (round.Back9Total < 15 || round.Back9Total == null)
                {
                    return false;
                }
                if (round.FullRoundTotal < 30 || round.FullRoundTotal == null)
                {
                    return false;
                }
                return true;
        }

        private bool ValidateSlope(Round round)
        {
            if (round.Slope == null || round.Slope < 50 || round.Slope > 160)
            {
                return false;
            }
                
            return true;
        }

        private bool ValidateHolesHaveParAndHandicap(Round round)
        {
            
            IEnumerable<Hole> holes = GetHoles(round.ID);
            foreach (var hole in holes)
            {
                if (hole.Par == 0 || hole.Par == null)
                {
                    return false;
                }
                if (hole.Handicap == 0 || hole.Handicap == null)
                {
                    return false;
                }
            }
            return true;

        }

        private bool ValidateHolesHavePar(Round round)
        {
            
            IEnumerable<Hole> holes = GetHoles(round.ID);
            foreach (var hole in holes)
            {
                if (hole.Par == 0 || hole.Par == null)
                {
                    return false;
                }
            }

            return true;

        }
        
        public IEnumerable<RoundFoursome> GetFoursomes(int roundId)
        {
            var foursomes = db.RoundFoursomes.Include("FoursomePlayers").Include("FoursomePlayers.Player").Where(f => f.RoundID == roundId).OrderBy(f => f.FoursomeNumberInRound).ToList();
            return foursomes;
        }

        public int GetScorecardFoursomeIDForRound (Player player,Round round)
        {
           
            int foursomeID = db.FoursomePlayers.Include(fp=>fp.RoundFoursome).Where(rf=>rf.RoundFoursome.RoundID == round.ID && rf.PlayerID == player.ID).SingleOrDefault().RoundFoursomeID;
            // var foursomes = db.RoundFoursomes.Include("FoursomePlayers").Include("FoursomePlayers.Player").Where(f => f.RoundID == roundId).OrderBy(f => f.FoursomeNumberInRound).ToList();
            return foursomeID;

        }

        public IEnumerable<RoundFoursome> GetRoundFoursomesByRoundID(int roundId)
        {
            var foursomes = db.RoundFoursomes.Include("FoursomePlayers").Include("FoursomePlayers.Player").Where(f => f.RoundID == roundId).OrderBy(f => f.FoursomeNumberInRound).ToList();
            return foursomes;
        }

        private bool ValidateHolesHaveNoDuplicateHandicaps(Round round)
        {

                IEnumerable<Hole> holes = GetHoles(round.ID);
                foreach (var hole in holes)
                {
                    foreach (var innerHole in holes)
                    {
                        if (innerHole.HoleNumber != hole.HoleNumber)
                        {
                            if (innerHole.Handicap == hole.Handicap)
                            {
                                return false;
                            }
                        }
                    }
                }
            return true;

        }



            
    }
}