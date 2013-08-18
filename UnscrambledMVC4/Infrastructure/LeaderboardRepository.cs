using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Models;
using UnscrambledMVC4.Infrastructure;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Gamezone.Models.ViewModels;



namespace UnscrambledMVC4.Infrastructure
{
    public class LeaderboardRepository
    {

        private UnscrambledDBEntities db = new UnscrambledDBEntities();
        GameRepository gameRepository = new GameRepository();


        public bool GenerateLeaderboards(int gameId)
        {

            // **** Populate Gameleaderboard **************************************
            Game game = gameRepository.GetGameRoundsAndPlayersByID(gameId);
            GameLeaderboard gl = new GameLeaderboard();
            gl.GameID = game.ID;
            gl.NumberOfPlayers = game.NumberOfPlayersOrTeams;
            gl.NumberOfRounds = game.NumberOfRounds;
            gl.CurrentRoundNumber = 1;

            try
            {
                db.GameLeaderboards.Add(gl);
                db.SaveChanges();
            }
            catch (Exception ex1)
            {
                return false;
            }


            // ****** Populate PlayerGameResults **************************************


            foreach (var player in game.Players)
            {
                PlayerGameResult pgr = new PlayerGameResult();
                pgr.PlayerID = player.ID;
                pgr.GameLeaderboardID = gl.ID;
                pgr.GameAdjustedPar = 0;
                pgr.GameAdjustedTotal = 0;
                pgr.GameNotAdjustedPar = 0;
                pgr.GameNotAdjustedTotal = 0;
                pgr.GameRank = 1;


                try
                {
                    db.PlayerGameResults.Add(pgr);
                    db.SaveChanges();
                }
                catch (Exception ex1)
                {
                    return false;
                }

                int roundCount = 1;
                foreach (var round in game.Rounds)
                {
                    RoundLeaderboard rL = new RoundLeaderboard();
                    rL.GameID = game.ID;
                    rL.GameLeaderboardID = gl.ID;
                    rL.RoundNumberInGame = roundCount;

                    try
                    {
                        db.RoundLeaderboards.Add(rL);
                        db.SaveChanges();
                    }
                    catch (Exception ex1)
                    {
                        return false;
                    }

                    PlayerRoundResult prr = new PlayerRoundResult();
                    prr.PlayerID = player.ID;
                    prr.RoundLeaderBoardID = rL.ID;
                    prr.PlayerGameResultID = pgr.ID;
                    prr.RoundAdjustedPar = 0;
                    prr.RoundAdjustedTotal = 0;
                    prr.RoundNotAdjustedPar = 0;
                    prr.RoundNotAdjustedTotal = 0;
                    prr.RoundRank = 1;
                    prr.LastCompletedHole = 1;
                    prr.RoundNumberInGame = rL.RoundNumberInGame;

                    try
                    {
                        db.PlayerRoundResults.Add(prr);
                        db.SaveChanges();
                    }
                    catch (Exception ex1)
                    {
                        return false;
                    }


                    // Create Foursomes
                    /*
                    var foursomes = db.Foursomes.Include(f => f.Scorecards).Where(f => f.RoundID == round.ID).ToList();
                    int foursomeCounter = foursomes.Count();

                    if (foursomeCounter == 0)
                    {
                        Foursome foursomeToAdd = new Foursome();
                        foursomeToAdd.Name = "Group1";
                        foursomeToAdd.RoundID = round.ID;
                        foursomeToAdd.FoursomeNumberInRound = 1;
                        try
                        {
                            db.Foursomes.Add(foursomeToAdd);
                            db.SaveChanges();
                            foursomes = db.Foursomes.Where(f => f.RoundID == round.ID).ToList();
                        }
                        catch (Exception ex1)
                        {
                            return false;
                        }
                    }

                    bool wasAdded = false;
                    int scorecardCounter = 0;

                    foreach (var foursome in foursomes)
                    {
                        if (foursome.Scorecards != null)
                        {
                            scorecardCounter = foursome.Scorecards.Count();
                        }
                        else
                        {
                            scorecardCounter = 0;
                        }

                        if (scorecardCounter < 4 && wasAdded == false)
                        {
                            Scorecard scorecard = new Scorecard();
                            scorecard.RoundID = round.ID;
                            scorecard.GameID = game.ID;
                            scorecard.FoursomeID = foursome.ID;
                            scorecard.PlayerName = player.Name;
                            scorecard.PlayerGameResultID = pgr.ID;
                            scorecard.PlayerRoundResultID = prr.ID;
                            scorecard.Back9Total = 0;
                            scorecard.Front9Total = 0;
                            scorecard.FullRoundTotal = 0;
                            scorecard.Slope = gameRepository.GetSlope(round.ID);
                            scorecard.Strokes = gameRepository.GetPlayerStrokesforRound(player.GameID, player.ID, roundCount);

                            try
                            {
                                db.Scorecards.Add(scorecard);
                                db.SaveChanges();
                                wasAdded = true;
                            }
                            catch (Exception ex1)
                            {
                                return false;
                            }

                            IEnumerable<Hole> holes = gameRepository.GetHoles(round.ID);

                            foreach (var hole in holes)
                            {
                                ScorecardHole sh = new ScorecardHole();
                                sh.HoleNumber = hole.HoleNumber;
                                sh.Par = hole.Par.Value;
                                sh.Handicap = hole.Handicap.Value;
                                sh.ScorecardID = scorecard.ID;
                                try
                                {
                                    db.ScorecardHoles.Add(sh);
                                    db.SaveChanges();
                                }
                                catch (Exception ex1)
                                {
                                    return false;
                                }
                            }
                            foursomeCounter++;
                        }

                        if (wasAdded == false)
                        {
                            Foursome foursomeToAdd = new Foursome();
                            foursomeCounter++;
                            foursomeToAdd.Name = "Group" + foursomeCounter.ToString();
                            foursomeToAdd.RoundID = round.ID;
                            foursomeToAdd.FoursomeNumberInRound = foursomeCounter;
                            try
                            {
                                db.Foursomes.Add(foursomeToAdd);
                                db.SaveChanges();

                            }
                            catch (Exception ex1)
                            {
                                return false;
                            }

                            Scorecard scorecard = new Scorecard();
                            scorecard.RoundID = round.ID;
                            scorecard.GameID = game.ID;
                            scorecard.FoursomeID = foursomeToAdd.ID;
                            scorecard.PlayerName = player.Name;
                            scorecard.PlayerGameResultID = pgr.ID;
                            scorecard.PlayerRoundResultID = prr.ID;
                            scorecard.Back9Total = 0;
                            scorecard.Front9Total = 0;
                            scorecard.FullRoundTotal = 0;
                            scorecard.Strokes = gameRepository.GetPlayerStrokesforRound(player.GameID, player.ID, roundCount);

                            try
                            {
                                db.Scorecards.Add(scorecard);
                                db.SaveChanges();
                                wasAdded = true;
                            }
                            catch (Exception ex1)
                            {
                                return false;
                            }

                            IEnumerable<Hole> holes = gameRepository.GetHoles(round.ID);

                            foreach (var hole in holes)
                            {
                                ScorecardHole sh = new ScorecardHole();
                                sh.HoleNumber = hole.HoleNumber;
                                sh.Par = hole.Par.Value;
                                sh.Handicap = hole.Handicap.Value;
                                sh.ScorecardID = scorecard.ID;
                                try
                                {
                                    db.ScorecardHoles.Add(sh);
                                    db.SaveChanges();
                                }
                                catch (Exception ex1)
                                {
                                    return false;
                                }
                            }

                        }

                    }





                    roundCount++;
                 */
                }


            }



            return true;


        }

        public GameLeaderboard GetLeaderboard(int gameId)
        {
            // GameLeaderboard gameLeaderboard = db.GameLeaderboards.Find(gameId);
            GameLeaderboard gameLeaderboard = db.GameLeaderboards.Include(g => g.Game).Where(g => g.GameID == gameId).SingleOrDefault();
            return gameLeaderboard;
        }

        public IEnumerable<PlayerGameResult> GetPlayerGameResults(int gameLeaderboardId)
        {
            // GameLeaderboard gameLeaderboard = db.GameLeaderboards.Find(gameId);
            var playerGameResults = db.PlayerGameResults.Include(p => p.Player).Include(p => p.PlayerRoundResults).Where(p => p.GameLeaderboardID == gameLeaderboardId);
            return playerGameResults.ToList();
        }

        public IEnumerable<PlayerOnGameLeaderboard> GetPlayersOnGameLeaderboard(int gameLeaderboardId, int roundNum)
        {
            // GameLeaderboard gameLeaderboard = db.GameLeaderboards.Find(gameId);
            List<PlayerOnGameLeaderboard> list = new List<PlayerOnGameLeaderboard>();
            var playerGameResults = db.PlayerGameResults.Include(p => p.Player).Include(p => p.PlayerRoundResults).Where(p => p.GameLeaderboardID == gameLeaderboardId);

            foreach (var playerGameResult in playerGameResults)
            {
                foreach (var playerRoundResult in playerGameResult.PlayerRoundResults)
                {
                    if (playerRoundResult.RoundNumberInGame == roundNum)
                    {
                        PlayerOnGameLeaderboard pogl = new PlayerOnGameLeaderboard();
                        pogl.GameRank = playerGameResult.GameRank != null ? playerGameResult.GameRank.Value : 1;
                        pogl.GameToPar = playerGameResult.GameAdjustedPar != null ? playerGameResult.GameAdjustedPar.Value : 0;
                        pogl.GameTotal = playerGameResult.GameAdjustedTotal != null ? playerGameResult.GameAdjustedTotal.Value : 0;
                        pogl.PlayerName = playerGameResult.Player.Name;
                        pogl.RoundNumberInGame = playerRoundResult.RoundNumberInGame.Value;
                        pogl.LastCompleteHoleInCurrentRound = playerRoundResult.LastCompletedHole.Value;
                        list.Add(pogl);
                    }
                }
            }



            return list;
        }


        public IEnumerable<PlayerOnScore> GetScorecardHoleForPairing(int gameId, int roundId, int foursomeId)
        {
            List<PlayerOnScore> playersOnScore = new List<PlayerOnScore>();
            return playersOnScore.ToList();
        }

        /*
        public IEnumerable<Foursome> GetFoursomes(int roundId)
        {
            var foursomes = db.Foursomes.Include(f => f.Scorecards).Where(f => f.RoundID == roundId).ToList();
            return foursomes;
        }
         * 
         */

    }
}