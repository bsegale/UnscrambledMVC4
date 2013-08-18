using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class PlayerValidations
    {

        public bool isNameValid(string playerName)
        {
            if (playerName == null)
            {
                return false;
            }

            if (playerName == "")
            {
                return true;
            }


            if (playerName.Length < 2 && playerName.Length > 0)
            {
                return false;
            }

            if (playerName.Length > 60)
            {
                return false;
            }

            return true;

        }

        public bool isIndexValid(string playerIndex)
        {
            if (playerIndex != null)
            {
                decimal value;
                if (Decimal.TryParse(playerIndex, out value))
                {
                    Console.WriteLine("PlayerIndex: " + value.ToString());
                    if (Decimal.Compare(value, (decimal)-10.0) < 0 || decimal.Compare(value, (decimal)50.0) > 0)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;

        }
    }
}