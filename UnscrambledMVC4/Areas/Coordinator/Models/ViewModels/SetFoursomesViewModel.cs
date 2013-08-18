using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class SetFoursomesViewModel
    {
        public IEnumerable<RoundFoursome> Foursomes { get; set; }
        public int NumberOfPlayersOrTeams { get; set; }
        public int RoundID { get; set; }
        public int GameID { get; set; }
        public List<SelectListItem> FoursomeNumberSelectListItems { get; set; }
        public List<RoundFoursome> emptyFoursomes { get; set; }
    }
}