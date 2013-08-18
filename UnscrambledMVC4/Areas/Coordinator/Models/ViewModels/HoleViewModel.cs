using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class HoleViewModel
    {
        public int ID { get; set; }
        public int RoundID { get; set; }
        public int HoleNumber { get; set; }
        public Nullable<int> Par { get; set; }
        public string ParErrorMessage { get; set; }
        public Nullable<int> Handicap { get; set; }
        public string HandicapErrorMessage { get; set; }
        public List<SelectListItem> HoleHandicapList { get; set; }
        public List<SelectListItem> ParList { get; set; }

        public HoleViewModel()
        {
            ParErrorMessage = "";
            HandicapErrorMessage = "";
        }

    }
}