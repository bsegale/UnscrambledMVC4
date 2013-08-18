using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class DeletePlayersViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Player> Players { get; set; }
    }
}