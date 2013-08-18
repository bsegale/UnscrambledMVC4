using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Validations;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class GameStartValidations
    {
        public List<GameValidationError> isGameReadytoStart(Game game)
        {
            GameValidations gv = new GameValidations();
            GameValidationError validationError = new GameValidationError();
            List<GameValidationError> errorList = new List<GameValidationError>();

            // Check game name
            if (!gv.isNameValid(game.Name))
            {
                validationError.Category = "Name";
                validationError.ErrorMessage = "Bad Game Name";
                errorList.Add(validationError);
            }

            // Check player indexes are complete for handicapped game

            return errorList;
        }

        

    }
}