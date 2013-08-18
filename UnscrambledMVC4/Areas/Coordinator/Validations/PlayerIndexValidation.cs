using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class PlayerIndexValidation
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationStatus ValidatePlayerName(string playerName)
        {
            ValidationStatus valerie = new ValidationStatus();

            if (playerName == null)
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Bad Player Name";
                return valerie;
            }

            if ((playerName is string) && string.IsNullOrEmpty((string)playerName))
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Bad Player Name";
                return valerie;
            }

            if (playerName.Length < 2)
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Player name has to be atleast 2 characters";
                return valerie;
            }

            if (playerName.Length > 60)
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Player name has to be less than 60 characters";
                return valerie;
            }

            valerie.Result = true;
            return valerie;

        }

        public ValidationStatus ValidatePlayerIndex(string playerIndex)
        {
            ValidationStatus valerie = new ValidationStatus();
            decimal number;

            if (playerIndex == null)
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Please Enter Player Index";
                return valerie;
            }

            if ((playerIndex is string) && string.IsNullOrEmpty((string)playerIndex))
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Please Enter Player Index";
                return valerie;
            }

            if (!Decimal.TryParse(playerIndex, out number))
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Bad index format";
                return valerie;
            }

            if (number < -10 | number > 40)
            {
                valerie.Result = false;
                valerie.ErrorDescription = "Index out of range";
                return valerie;
            }

        

            valerie.Result = true;
            return valerie;

        }

       

    }
}