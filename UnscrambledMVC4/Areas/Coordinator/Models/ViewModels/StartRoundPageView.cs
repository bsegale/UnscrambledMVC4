using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UnscrambledMVC4.Areas.Coordinator.Validations;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class StartRoundPageView
    {
        public List<GameValidationError> errorList { get; set; }
        public int gameId { get; set; }
        public LeftNavViewModel leftNavViewModel { get; set; }
    }
}