using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Gamezone.Models.ViewModels
{
    public class PlayerOnGameLeaderboard
    {
        public String PlayerName { get; set; }
        public int RoundNumberInGame { get; set; }
        public int LastCompleteHoleInCurrentRound { get; set; }
        public int GameRank { get; set; }
        public int GameToPar { get; set; }
        public int GameTotal { get; set; }
        // public GameLeaderboard GameLeaderboard { get; set; }
        // public IEnumerable<PlayerGameResult> PlayerGameResults { get; set; }
        // public IEnumerable<PlayerRoundResult> PlayerRoundResults { get; set; }
    }
}