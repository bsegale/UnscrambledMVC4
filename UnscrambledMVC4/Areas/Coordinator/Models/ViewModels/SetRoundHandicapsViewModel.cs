using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class SetRoundHandicapsViewModel
    {
        public Game Game { get; set; }
        public int RoundID { get; set; }
        public IEnumerable<RoundPlayerViewModel> RoundPlayersViewModels { get; set; }
        public int playerCounter { get; set; }
    }
}