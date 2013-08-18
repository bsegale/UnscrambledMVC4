using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Unscrambled.Areas.Coordinator.Models.ViewModels;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class SetRoundViewModel
    {
        public RoundViewModel RoundViewModel { get; set; }
        public bool PageHasErrors { get; set; }
        public IEnumerable<HoleViewModel> HoleViewModels { get; set; }
        public SelectList Handicap { get; set; }

        public List<SelectListItem> HoleHandicapList { get; set; }
        public List<SelectListItem> ParList { get; set; }
    }
}
