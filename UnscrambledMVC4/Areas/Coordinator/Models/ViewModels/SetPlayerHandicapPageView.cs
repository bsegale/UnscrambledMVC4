using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class SetPlayerHandicapPageView
    {
        public SetPlayerHandicapViewModel setPlayerHandicapVM { get; set; }
        public LeftNavViewModel leftNavVM { get; set; }

        public SetPlayerHandicapPageView()
        {
            this.leftNavVM = new LeftNavViewModel();
            this.setPlayerHandicapVM = new SetPlayerHandicapViewModel();
        }
    }
}