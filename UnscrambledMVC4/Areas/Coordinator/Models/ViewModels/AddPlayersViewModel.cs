using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class AddPlayersViewModel
    {
        public Game Game { get; set; }
        public bool PageHasErrors { get; set; }
        public bool numberPlayersHasErrors { get; set; }
    }
}