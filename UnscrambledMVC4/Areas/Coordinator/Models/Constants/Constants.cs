using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Unscrambled.Areas.Coordinator.Models.Constants
{
    public class Constants
    {
            public const int MAX_NUMBER_OF_ROUNDS = 4;

            public int GetMaxNumberOfRounds()
            {
                return MAX_NUMBER_OF_ROUNDS;
            }
    }


}