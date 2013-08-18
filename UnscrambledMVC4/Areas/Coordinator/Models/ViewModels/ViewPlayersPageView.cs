using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class ViewPlayersPageView
    {
        public ViewPlayersViewModel viewPlayerVM { get; set; }
        public LeftNavViewModel leftNavVM { get; set; }
    }
}