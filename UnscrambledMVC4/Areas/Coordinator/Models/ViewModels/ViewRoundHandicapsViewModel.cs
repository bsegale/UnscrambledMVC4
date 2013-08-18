using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class ViewRoundHandicapsViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<RoundPlayerHandicap> RoundPlayersViewModels { get; set; }
        public int playerCounter { get; set; }
    }
}