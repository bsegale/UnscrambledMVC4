using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class DeletePlayersConfirmationViewModel
    {
        public IEnumerable<Player> PlayersToDelete { get; set; }
        public Game Game { get; set; }
    }
}