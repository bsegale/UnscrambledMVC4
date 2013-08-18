using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class SetPlayerHandicapViewModel
    {
        public Game Game { get; set; }
        public RoundPlayerViewModel RoundPlayerViewModel { get; set; }
    }


    
}