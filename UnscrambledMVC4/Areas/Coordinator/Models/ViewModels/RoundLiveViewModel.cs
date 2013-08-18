using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class RoundLiveViewModel
    {
        public Game Game { get; set; }
        public Round Round { get; set; }
    }
}