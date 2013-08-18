using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Areas.Coordinator.Models;
using Unscrambled.Domain;


namespace Unscrambled.Areas.Coordinator.Validations
{
    public class RoundFoursomeValidations
    {

       public List<RoundFoursome> getEmptyFoursomes(int roundId)
        {
            GameRepository db = new GameRepository();

            List<RoundFoursome> roundFoursomes = db.GetDeletableFoursomes(roundId); // Deletable means it has no players in it.

            return roundFoursomes;

        }
     
    }
            
}