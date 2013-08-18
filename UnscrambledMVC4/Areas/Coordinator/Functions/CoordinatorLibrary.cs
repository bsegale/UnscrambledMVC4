using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Models;
using Unscrambled.Areas.Coordinator.Models.Constants;
using Unscrambled.Areas.Coordinator.Models;
using System.Web.Mvc;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Functions
{
    public class CoordinatorLibrary
    {

        public String GenerateJoinCode()
        {
            UnscrambledDBEntities db = new UnscrambledDBEntities();

            String JoinCode;
            bool JoinCodeAlreadyExists;
            JoinCode = generateRandomString(6);

            for (int i = 1; i <= 5; i++)
            {
                JoinCodeAlreadyExists = db.Games.Where(g => g.JoinCode == JoinCode).Count() > 0;
                if (!JoinCodeAlreadyExists)
                {
                    break;
                }
                else
                {
                    JoinCode = generateRandomString(6);
                }
            }
            return JoinCode;
        }

        public List<LeftNavItem> GenerateLeftNavItems(Game game)
        {
            var items = new List<LeftNavItem>();

            if (game.ID == 0) // This means it is a new game
            {
                LeftNavItem gameDetailsItem = new LeftNavItem();
                gameDetailsItem.itemText = "Game Details";
                gameDetailsItem.actionName = "Create";
                gameDetailsItem.ID = 0;
                gameDetailsItem.isCurrentView = true;
                items.Add(gameDetailsItem);

                LeftNavItem playersItem = new LeftNavItem();
                playersItem.itemText = "Players";
                playersItem.actionName = "SetPlayers";
                playersItem.ID = 0;
                playersItem.isCurrentView = false;
                items.Add(playersItem);

                LeftNavItem roundItem = new LeftNavItem();
                roundItem.itemText = "Course";
                roundItem.actionName = "SetRound";
                roundItem.ID = 0;
                roundItem.isCurrentView = false;
                items.Add(roundItem);

                LeftNavItem handicapItem = new LeftNavItem();
                handicapItem.itemText = "Handicaps";
                handicapItem.actionName = "ViewPlayers";
                handicapItem.ID = 0;
                handicapItem.isCurrentView = false;
                items.Add(handicapItem);

                LeftNavItem foursomeItem = new LeftNavItem();
                foursomeItem.itemText = "Foursomes";
                foursomeItem.actionName = "SetFoursomes";
                foursomeItem.ID = 0;
                foursomeItem.isCurrentView = false;
                items.Add(foursomeItem);

                LeftNavItem startItem = new LeftNavItem();
                startItem.itemText = "Start Game";
                startItem.actionName = "SetRound";
                startItem.ID = 0;
                startItem.isCurrentView = false;
                items.Add(startItem);
                return items;
            }
            else
            {
                LeftNavItem gameDetailsItem = new LeftNavItem();
                gameDetailsItem.itemText = "Game Details";
                gameDetailsItem.actionName = "GameDetails";
                gameDetailsItem.ID = game.ID;
                gameDetailsItem.isCurrentView = false;
                items.Add(gameDetailsItem);

                LeftNavItem playersItem = new LeftNavItem();
                playersItem.itemText = "Players";
                playersItem.actionName = "SetPlayers";
                playersItem.ID = game.ID;
                playersItem.isCurrentView = true;
                items.Add(playersItem);

                UnscrambledDBEntities db = new UnscrambledDBEntities();
                int roundCounter = 1;
                var rounds = db.Rounds.Where(r => r.GameID == game.ID);
                foreach (var round in rounds)
                {
                    if (rounds.Count() == 1)
                    {
                        if (round.RoundStateID == 1)
                        {
                            LeftNavItem roundItem = new LeftNavItem();
                            roundItem.itemText = "Course";
                            roundItem.actionName = "SetRound";
                            roundItem.ID = round.ID;
                            roundItem.isCurrentView = false;
                            items.Add(roundItem);

                            LeftNavItem handicapItem = new LeftNavItem();
                            handicapItem.itemText = "Handicaps";
                            handicapItem.actionName = "ViewCalculatedHandicaps";
                            handicapItem.ID = round.ID;
                            handicapItem.isCurrentView = false;
                            items.Add(handicapItem);

                            LeftNavItem foursomeItem = new LeftNavItem();
                            foursomeItem.itemText = "Foursomes";
                            foursomeItem.actionName = "SetFoursomes";
                            foursomeItem.ID = round.ID;
                            foursomeItem.isCurrentView = false;
                            items.Add(foursomeItem);

                            LeftNavItem startItem = new LeftNavItem();
                            startItem.itemText = "Start Game";
                            startItem.actionName = "StartRound";
                            startItem.ID = round.ID;
                            startItem.isCurrentView = false;
                            items.Add(startItem);
                        }
                        else
                        {
                            LeftNavItem roundItem = new LeftNavItem();
                            roundItem.itemText = "Course";
                            roundItem.actionName = "ViewRound";
                            roundItem.ID = round.ID;
                            roundItem.isCurrentView = false;
                            items.Add(roundItem);

                            LeftNavItem handicapItem = new LeftNavItem();
                            handicapItem.itemText = "Handicaps";
                            handicapItem.actionName = "ViewHandicaps";
                            handicapItem.ID = round.ID;
                            handicapItem.isCurrentView = false;
                            items.Add(handicapItem);

                            LeftNavItem foursomeItem = new LeftNavItem();
                            foursomeItem.itemText = "Foursomes";
                            foursomeItem.actionName = "ViewFoursomes";
                            foursomeItem.ID = round.ID;
                            foursomeItem.isCurrentView = false;
                            items.Add(foursomeItem);

                            LeftNavItem startItem = new LeftNavItem();
                            startItem.itemText = "Round Live";
                            startItem.actionName = "RoundLive";
                            startItem.ID = round.ID;
                            startItem.isCurrentView = false;
                            items.Add(startItem);
                        }
                        return items;
                    }
                    else
                    {
                        LeftNavItem roundItem = new LeftNavItem();
                        roundItem.itemText = "Round " + roundCounter.ToString() + " Course";
                        roundItem.actionName = "SetRound";
                        roundItem.ID = round.ID;
                        roundItem.isCurrentView = false;
                        items.Add(roundItem);

                        LeftNavItem handicapItem = new LeftNavItem();
                        handicapItem.itemText = "Round " + roundCounter.ToString() + " Handicaps";
                        handicapItem.actionName = "ViewHandicaps";
                        handicapItem.ID = round.ID;
                        handicapItem.isCurrentView = false;
                        items.Add(handicapItem);

                        LeftNavItem foursomeItem = new LeftNavItem();
                        foursomeItem.itemText = "Round " + roundCounter.ToString() + " Foursomes";
                        foursomeItem.actionName = "SetFoursomes";
                        foursomeItem.ID = round.ID;
                        foursomeItem.isCurrentView = false;
                        items.Add(foursomeItem);

                        LeftNavItem startItem = new LeftNavItem();
                        if (round.RoundStateID == 1)
                        {
                            startItem.itemText = "Start Round " + roundCounter.ToString();
                            startItem.actionName = "StartRound";
                            startItem.ID = round.ID;
                            startItem.isCurrentView = false;
                            items.Add(startItem);
                        }
                        else
                        {
                            startItem.itemText = "Round " + roundCounter.ToString() + " Live";
                            startItem.actionName = "StartRound";
                            startItem.ID = round.ID;
                            startItem.isCurrentView = false;
                            items.Add(startItem);
                        }

                        roundCounter++;

                        
                    }
                }
                
            }

            return items;
                  

        }


        public List<LeftNavItem> GenerateCoordinatorLeftNavItems(int CoordinatorID)
        {
            var items = new List<LeftNavItem>();

            LeftNavItem myGamesItem = new LeftNavItem();
            myGamesItem.actionName = "MyGames";
            myGamesItem.ID = CoordinatorID;
            myGamesItem.itemText = "My Games";
            myGamesItem.isCurrentView = true;
            items.Add(myGamesItem);

            LeftNavItem myPlayersItem = new LeftNavItem();
            myPlayersItem.actionName = "MyPlayers";
            myPlayersItem.ID = CoordinatorID;
            myPlayersItem.itemText = "My Regulars Players";
            myPlayersItem.isCurrentView = false;
            items.Add(myPlayersItem);

            LeftNavItem myCoursesItem = new LeftNavItem();
            myCoursesItem.actionName = "MyCourses";
            myCoursesItem.ID = CoordinatorID;
            myCoursesItem.itemText = "Courses";
            myCoursesItem.isCurrentView = false;
            items.Add(myCoursesItem);

           
            return items;

        }

        public int CalculateHandicap(int? roundSlope, RoundPlayerHandicap roundPlayer)
        {
            UnscrambledDBEntities db = new UnscrambledDBEntities();
            if (roundPlayer.Player == null)
            {
                roundPlayer.Player = db.Players.Find(roundPlayer.PlayerID);
            }
            Decimal playerIndexDec = roundPlayer.Player.PlayerIndex == null ? 0M : roundPlayer.Player.PlayerIndex.Value;
            Decimal roundSlopeDec;

            if (roundSlope != null)
            {
                roundSlopeDec = Convert.ToDecimal(roundSlope);
            }
            else
            {
                roundSlopeDec = 0M;
            }


            Decimal playerHandicapDec = (playerIndexDec * roundSlopeDec) / 113;

            playerHandicapDec = Math.Round(playerHandicapDec, 0, MidpointRounding.AwayFromZero);

            return Convert.ToInt32(playerHandicapDec);
            
            

            
        }



        public int ExtractIntOrMake0(string intAsString)
        {
            int value;
            if (Int32.TryParse(intAsString, out value))
            {
                return value;
            }
            else
            {
                return 0;
            }
        }

        public List<SelectListItem> NumberOfRoundsSelectListItems(int selectedItem)
        {
            var items = new List<SelectListItem>();
            // Add the drop down item for something not selected
            Constants constants = new Constants();
            int numberOfRounds = constants.GetMaxNumberOfRounds();

            items.Add(new SelectListItem() { Text = "Choose", Value = "", Selected = false });
            for (int i = 1; i <= numberOfRounds; i++)
            {
                if (selectedItem == i)
                {
                    items.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
                }
            }

            return items;
        }

        public List<SelectListItem> ParSelectItems(int? selectedItem)
        {
            var selectItems = new List<SelectListItem>();
            // Add the drop down item for something not selected
            selectItems.Add(new SelectListItem() { Text = "--", Value = "", Selected = false });
            for (int i = 3; i <= 5; i++)
            {
                if (selectedItem == i)
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                }
                else
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
                }
            }
            return selectItems;
           
        }

        public List<SelectListItem> HandicapSelectItems18(int? selectedItem)
        {
            var selectItems = new List<SelectListItem>();
            // Add the drop down item for something not selected
            selectItems.Add(new SelectListItem() { Text = "--", Value = "", Selected = false });
            for (int i = 1; i <= 18; i++)
            {
                if (selectedItem == i)
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                }
                else
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
                }
            }
            return selectItems;

        }

        public List<SelectListItem> HandicapSelectItems9(int? selectedItem)
        {
            var selectItems = new List<SelectListItem>();
            // Add the drop down item for something not selected
            selectItems.Add(new SelectListItem() { Text = "--", Value = "", Selected = false });
            for (int i = 1; i <= 9; i++)
            {
                if (selectedItem == i)
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = true });
                }
                else
                {
                    selectItems.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString(), Selected = false });
                }
            }
            return selectItems;

        }

        public List<SelectListItem> FoursomeNumberSelectListItems(IEnumerable<RoundFoursome> foursomes)
        {
            var items = new List<SelectListItem>();
            int foursomeCounter = 1;
            // Add the drop down item for something not selected
            
            

            // items.Add(new SelectListItem() { Text = "TBD", Value = "", Selected = false });
            foreach (var foursome in foursomes)
            {
                items.Add(new SelectListItem() { Text = foursome.Name, Value = foursome.ID.ToString() });
                foursomeCounter++;
            }

            return items;
        }

        public List<SelectListItem> IsHandicappedSelectListItems(string selectedItem)
        {
            var items = new List<SelectListItem>();
            // Add the drop down item for something not selected

            items.Add(new SelectListItem() { Text = "Choose Yes/No", Value = "" });
            if (selectedItem == "Y")
            {
                items.Add(new SelectListItem() { Text = "Yes", Value = "Y", Selected = true });
            }
            else
            {
                items.Add(new SelectListItem() { Text = "Yes", Value = "Y", Selected = false });
            }

            if (selectedItem == "N")
            {
                items.Add(new SelectListItem() { Text = "No", Value = "N", Selected = true });
            }
            else
            {
                items.Add(new SelectListItem() { Text = "No", Value = "N", Selected = false });
            }
            return items;
        }

        public List<SelectListItem> GameTypesSelectListItems(int selectedItem)
        {
            GameRepository db = new GameRepository();

            var items = new List<SelectListItem>();
            // Add the drop down item for something not selected
            
            
            
            items.Add(new SelectListItem() { Text = "Choose", Value = "", Selected = false });

            var gameTypes = db.GetAllGameTypes();

            foreach (var gametype in gameTypes)
            {
                if (gametype.ID == selectedItem)
                {
                    items.Add(new SelectListItem() { Text = gametype.Name.ToString(), Value = gametype.ID.ToString(), Selected = true });
                }
                else
                {
                    items.Add(new SelectListItem() { Text = gametype.Name.ToString(), Value = gametype.ID.ToString(), Selected = false });
                }
            }

            return items;
        }


        private String generateRandomString(int length)
        {
            Random random = new Random();
            String randomString = "";
            int randNumber;

            //Loop ‘length’ times to generate a random number or character
            for (int i = 0; i < length; i++)
            {
                if (random.Next(1, 3) == 1)
                    randNumber = random.Next(97, 123); //char {a-z}
                else
                    randNumber = random.Next(48, 58); //int {0-9}

                //append random char or digit to random string
                randomString = randomString + (char)randNumber;
            }
            //return the random string
            return randomString;
        }



    }



        



  
}