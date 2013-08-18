//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Unscrambled.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class PlayerRoundResult
    {
        public PlayerRoundResult()
        {
            this.Scorecards = new HashSet<Scorecard>();
        }
    
        public int ID { get; set; }
        public int RoundLeaderBoardID { get; set; }
        public int PlayerID { get; set; }
        public Nullable<int> RoundRank { get; set; }
        public Nullable<int> RoundAdjustedPar { get; set; }
        public Nullable<int> RoundAdjustedTotal { get; set; }
        public Nullable<int> RoundNotAdjustedPar { get; set; }
        public Nullable<int> RoundNotAdjustedTotal { get; set; }
        public int ScorecardID { get; set; }
        public int PlayerGameResultID { get; set; }
        public Nullable<int> LastCompletedHole { get; set; }
        public Nullable<int> RoundNumberInGame { get; set; }
    
        public virtual PlayerGameResult PlayerGameResult { get; set; }
        public virtual Player Player { get; set; }
        public virtual RoundLeaderboard RoundLeaderboard { get; set; }
        public virtual ICollection<Scorecard> Scorecards { get; set; }
    }
}
