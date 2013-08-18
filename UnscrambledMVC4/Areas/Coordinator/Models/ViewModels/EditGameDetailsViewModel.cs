using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class EditGameDetailsViewModel
    {
        public Game game { get; set; }
        public bool gameNameHasErrors { get; set; }
        public string gameNameErrorMessage { get; set; }
        public bool numberPlayersHasErrors { get; set; }
        public string numberOfPlayersErrorMessage { get; set; }
        public bool gameTypeHasErrors { get; set; }
        public string gameTypeErrorMessage { get; set; }
        public bool isHandicappedHasErrors { get; set; }
        public string isHandicappedErrorMessage { get; set; }
        public bool numberOfRoundsHasErrors { get; set; }
        public string numberOfRoundsErrorMessage { get; set; }

        public List<SelectListItem> GameTypeSelectItems { get; set; }
        public List<SelectListItem> IsHandicappedSelectItems { get; set; }
        public List<SelectListItem> numberOfRoundsSelectItems { get; set; }
        // public int SelectedItem { get; set; }

    }
}