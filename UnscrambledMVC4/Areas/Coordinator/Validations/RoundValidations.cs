using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Areas.Coordinator.Models;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Validations
{
    public class RoundValidations
    {

        public bool areHolesValid(Round round)
        {
            foreach (var hole in round.Holes)
            {
                foreach (var iHole in round.Holes)
                {
                    if (hole.Handicap == iHole.Handicap && hole.HoleNumber != iHole.HoleNumber)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool isSlopeValid(string slope)
        {
            if (String.IsNullOrEmpty(slope))
            {
                return false;
            }

            int value;
                if (Int32.TryParse(slope, out value))
                {
                    Console.WriteLine("Slope: " + value.ToString());
                    if (value < 55 || value > 155)
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

        public bool isParValid(string parString)
        {
            if (String.IsNullOrEmpty(parString))
            {
                return false;
            }

            int value;
            if (Int32.TryParse(parString, out value))
            {
                if (value < 2 || value > 6)
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

        public bool isHandicapValid(string handicapString, int numberOfHoles)
        {
            if (String.IsNullOrEmpty(handicapString))
            {
                return false;
            }

            int value;
            if (Int32.TryParse(handicapString, out value))
            {
                if (value < 1 || value > numberOfHoles)
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