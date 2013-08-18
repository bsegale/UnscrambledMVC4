using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class LeftNavItem
    {
        public String itemText { get; set; }
        public String actionName { get; set; }
        public int ID { get; set; }
        public bool isCurrentView { get; set; }
    }
}