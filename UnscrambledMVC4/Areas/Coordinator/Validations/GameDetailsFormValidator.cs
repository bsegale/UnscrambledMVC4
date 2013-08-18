using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;
using Unscrambled.Areas.Coordinator.Functions;

namespace UnscrambledMVC4.Areas.Coordinator.Validations
{
    public class GameDetailsFormValidator
    {
        public bool hasErrors { get; set; }
        public GameDetailsViewModel gameDetailsViewModel { get; set; }

        public bool isFormValid(Game inGame, string isHandicappedStr)
        {
            gameDetailsViewModel = new GameDetailsViewModel();

            if (!isGameNameValid(inGame.Name))
            {
                hasErrors = true;
                gameDetailsViewModel.gameNameHasErrors = true;
            }

            if (!isNumberPlayersValid(inGame.NumberOfPlayersOrTeams.ToString()))
            {
                hasErrors = true;
                gameDetailsViewModel.numberPlayersHasErrors = true;
            }

            if (!isGameTypeValid(inGame.GameTypeID.ToString()))
            {
                hasErrors = true;
                gameDetailsViewModel.gameTypeHasErrors = true;
            }

            if (!isHandicappedValid(isHandicappedStr))
            {
                hasErrors = true;
                gameDetailsViewModel.isHandicappedHasErrors = true;
            }


            if (hasErrors == true)
            {
                CoordinatorLibrary lib = new CoordinatorLibrary();
                gameDetailsViewModel.game = new Game();
                gameDetailsViewModel.game.Name = inGame.Name;
                gameDetailsViewModel.game.NumberOfPlayersOrTeams = inGame.NumberOfPlayersOrTeams;
                gameDetailsViewModel.game.GameTypeID = inGame.GameTypeID;


                // Begin Set the selected Game Type
                var selectListItems = lib.GameTypesSelectListItems(99);

                foreach (var selectItem in selectListItems)
                {
                    if (selectItem.Value == inGame.GameTypeID.ToString())
                    {
                        selectItem.Selected = true;
                    }
                }
                gameDetailsViewModel.GameTypeSelectItems = selectListItems;

                // End Set the selected Game Type

                // Begin Set the selected IsHandicapped Item
                selectListItems = lib.IsHandicappedSelectListItems("");
                foreach (var selectItem in selectListItems)
                {
                    if (selectItem.Value == inGame.IsHandicapped.ToString())
                    {
                        selectItem.Selected = true;
                    }
                }
                gameDetailsViewModel.IsHandicappedSelectItems = selectListItems;
                // End Set the selected IsHandicapped Item


                // Begin Set the selected IsHandicapped Item
                selectListItems = lib.NumberOfRoundsSelectListItems(99);
                foreach (var selectItem in selectListItems)
                {
                    if (selectItem.Value == inGame.NumberOfRounds.ToString())
                    {
                        selectItem.Selected = true;
                    }
                }
                gameDetailsViewModel.numberOfRoundsSelectItems = selectListItems;

                return false;
            }

            return true;
        }

        public bool isGameIDValid(string gameIDString)
        {
            if (gameIDString == null)
            {
                return false;
            }

            if ((gameIDString is string) && string.IsNullOrEmpty((string)gameIDString))
            {
                return false;
            }

            try
            {
                int number = Int32.Parse(gameIDString);
                if (number < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public bool isPlayerFoursomeValid(string playerFoursomeString)
        {
            if (playerFoursomeString == null)
            {
                return false;
            }

            if ((playerFoursomeString is string) && string.IsNullOrEmpty((string)playerFoursomeString))
            {
                return false;
            }

            try
            {
                int number = Int32.Parse(playerFoursomeString);
                if (number < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }

        }

        public bool isGameNameValid(string gameName)
        {
            if (gameName == null)
            {
                return false;
            }

            if ((gameName is string) && string.IsNullOrEmpty((string)gameName))
            {
                return false;
            }

            if (gameName.Length < 2)
            {
                return false;
            }

            if (gameName.Length > 60)
            {
                return false;
            }

            return true;

        }

           
        public bool isNumberPlayersValid (string numberOfPlayers)
        {
            
            decimal number;

            if (numberOfPlayers == null)
            {
                return false;
            }

            if ((numberOfPlayers is string) && string.IsNullOrEmpty((string)numberOfPlayers))
            {
                return false;
            }

            if (!Decimal.TryParse(numberOfPlayers, out number))
            {
                return false;
            }

            if (number < 1 | number > 120)
            {
                return false;
            }

            return true;

        }


        public bool isGameTypeValid(string gameTypeID)
        {

            decimal number;

            if (gameTypeID == null)
            {
                return false;
            }

            if ((gameTypeID is string) && string.IsNullOrEmpty((string)gameTypeID))
            {
                return false;
            }

            if (!Decimal.TryParse(gameTypeID, out number))
            {
                return false;
            }

            if (number < 1 | number > 4)
            {
                return false;
            }

            return true;

        }

        public bool isNumberOfRoundsValid(string numberOfRounds)
        {

            int number;

            if (numberOfRounds == null)
            {
                return false;
            }

            if ((numberOfRounds is string) && string.IsNullOrEmpty((string)numberOfRounds))
            {
                return false;
            }

            if (!Int32.TryParse(numberOfRounds, out number))
            {
                return false;
            }


            if (number < 1 | number > 4)
            {
                return false;
            }

            return true;

        }
        public bool isHandicappedValid(string handicapStr)
        {

            if (handicapStr == null)
            {
                return false;
            }

            if ((handicapStr is string) && string.IsNullOrEmpty((string)handicapStr))
            {
                return false;
            }

            // bool handicapBool = Convert.ToBoolean(handicapStr);
            if (handicapStr == "Y" || handicapStr == "N")
            {
                return true;
            }
            else
            {
                return false;
            }

            



        }
    }
}