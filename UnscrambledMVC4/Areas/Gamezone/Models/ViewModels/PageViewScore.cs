using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Gamezone.Models.ViewModels
{
    public class PageViewScore
    {
        // public GameLeaderboard GameLeaderboard { get; set; }
        public IEnumerable<PlayerOnScore> PlayersOnScore { get; set; }
    }
}