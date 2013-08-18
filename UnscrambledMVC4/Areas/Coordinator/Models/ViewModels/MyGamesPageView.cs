using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class MyGamesPageView
    {
        public MyGamesViewModel myGamesVM { get; set; }
        public CoordinatorLeftNavViewModel leftNavVM { get; set; }
    }
}