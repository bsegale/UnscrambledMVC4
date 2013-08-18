using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Areas.Coordinator.Models;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class RoundPlayerValidations
    {

       public bool isHandicapValid(string handicap)
        {
            if (String.IsNullOrEmpty(handicap))
            {
                return false;
            }

            int value;
                if (Int32.TryParse(handicap, out value))
                {

                    if (value < -10 || value > 50)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }


        }
     
    }
            
}