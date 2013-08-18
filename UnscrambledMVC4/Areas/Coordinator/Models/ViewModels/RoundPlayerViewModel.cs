using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class RoundPlayerViewModel
    {
        public int ID { get; set; }
        public int RoundID { get; set; }
        public int PlayerID { get; set; }
        public string PlayerName { get; set; }
        public Nullable<int> Handicap { get; set; }
        public string HandicapErrorMessage { get; set; }


        public RoundPlayerViewModel()
        {
            HandicapErrorMessage = "";
        }
    }
}