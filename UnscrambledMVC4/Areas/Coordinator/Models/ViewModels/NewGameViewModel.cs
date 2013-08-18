using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class NewGameViewModel
    {
        public Game Game { get; set; }
        // public List<SelectListItem> GameTypeSelectList { get; set; }
        public SelectList GameTypeID { get; set; }
        public int SelectedItem { get; set; }
    }
}