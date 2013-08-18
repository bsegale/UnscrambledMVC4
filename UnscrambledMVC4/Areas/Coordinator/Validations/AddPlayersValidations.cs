using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class AddPlayersValidations
    {

        public bool isNumberOfPlayersValid(string numberOfPlayersString)
        {
            if (numberOfPlayersString == null)
            {
                return false;
            }

            if ((numberOfPlayersString is string) && string.IsNullOrEmpty((string)numberOfPlayersString))
            {
                return false;
            }

            try
            {
                int number = Int32.Parse(numberOfPlayersString);
                if (number < 1 || number > 120)
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


    }
}