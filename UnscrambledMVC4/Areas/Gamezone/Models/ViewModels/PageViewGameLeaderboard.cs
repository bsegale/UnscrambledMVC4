using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Gamezone.Models.ViewModels
{
    public class PageViewGameLeaderboard
    {
        public GameLeaderboard GameLeaderboard { get; set; }
        public IEnumerable<PlayerOnGameLeaderboard> PlayersOnLeaderboard { get; set; }
        // public IEnumerable<PlayerRoundResult> PlayerRoundResults { get; set; }

    }
}