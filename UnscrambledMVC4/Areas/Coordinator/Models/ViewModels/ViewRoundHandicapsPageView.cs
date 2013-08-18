﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class ViewRoundHandicapsPageView
    {
        public ViewRoundHandicapsViewModel viewRoundHandicapsVM { get; set; }
        public LeftNavViewModel leftNavVM { get; set; }


        public ViewRoundHandicapsPageView()
        {
            this.leftNavVM = new LeftNavViewModel();
            this.viewRoundHandicapsVM = new ViewRoundHandicapsViewModel();
        }
    }
}