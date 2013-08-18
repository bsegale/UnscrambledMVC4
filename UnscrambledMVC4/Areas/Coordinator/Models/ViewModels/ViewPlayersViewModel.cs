using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class ViewPlayersViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public Round Round { get; set; }
    }
}
