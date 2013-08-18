using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class RoundViewModel
    {
        public int ID { get; set; }
        public int GameID { get; set; }
        public int RoundNumberInGame { get; set; }
        public Nullable<int> HolesInRound { get; set; }
        public Nullable<int> Front9Total { get; set; }
        public Nullable<int> Back9Total { get; set; }
        public Nullable<int> FullRoundTotal { get; set; }
        public Nullable<int> Slope { get; set; }
        public string SlopeErrorMessage { get; set; }
        public int RoundStateID { get; set; }

        public RoundViewModel()
        {
            SlopeErrorMessage = "";
        }

    }
}