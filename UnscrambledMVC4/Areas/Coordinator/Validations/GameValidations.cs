using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Validations;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class GameValidations
    {
        public List<GameValidationError> isGameValid(Game game)
        {
            GameValidationError validationError = new GameValidationError();
            List<GameValidationError> errorList = new List<GameValidationError>();


            if (!isNameValid(game.Name))
            {
                validationError.Category = "Name";
                validationError.ErrorMessage = "Bad Game Name";
                errorList.Add(validationError);
            }

            return errorList;
        }

        public bool isNameValid(string playerName)
        {
            if (playerName == null)
            {
                return false;
            }

            if ((playerName is string) && string.IsNullOrEmpty((string)playerName))
            {
                return false;
            }

            if (playerName.Length < 2)
            {
                return false;
            }

            if (playerName.Length > 60)
            {
                return false;
            }

            return true;

        }

    }
}