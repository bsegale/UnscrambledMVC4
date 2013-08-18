using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class LeftNavViewModel
    {
        public List<LeftNavItem> leftNavItems { get; set; }
    }
}