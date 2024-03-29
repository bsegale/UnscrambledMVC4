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
    
    public partial class Game
    {
        public Game()
        {
            this.GameLeaderboards = new HashSet<GameLeaderboard>();
            this.GameSetupStates = new HashSet<GameSetupState>();
            this.Players = new HashSet<Player>();
            this.RoundLeaderboards = new HashSet<RoundLeaderboard>();
            this.Rounds = new HashSet<Round>();
            this.Scorecards = new HashSet<Scorecard>();
        }
    
        public int ID { get; set; }
        public int CoordinatorID { get; set; }
        public string JoinCode { get; set; }
        public string Name { get; set; }
        public int GameTypeID { get; set; }
        public int NumberOfRounds { get; set; }
        public int NumberOfPlayersOrTeams { get; set; }
        public int GameStateID { get; set; }
        public string IsHandicapped { get; set; }
        public string CalcHandicapsFromIndex { get; set; }
    
        public virtual Coordinator Coordinator { get; set; }
        public virtual ICollection<GameLeaderboard> GameLeaderboards { get; set; }
        public virtual ICollection<GameSetupState> GameSetupStates { get; set; }
        public virtual GameState GameState { get; set; }
        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<RoundLeaderboard> RoundLeaderboards { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<Scorecard> Scorecards { get; set; }
        public virtual GameType GameType { get; set; }
    }
}
