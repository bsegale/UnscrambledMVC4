using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;
using UnscrambledMVC4.Areas.Coordinator.Validations;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class StartRoundConfirmationPageView
    {
        public List<GameValidationWarning> warningList { get; set; }
        public Game Game { get; set; }
        public Round Round { get; set; }
        public LeftNavViewModel leftNavViewModel { get; set; }
    }
}