using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class EditGameDetailsConfirmationViewModel
    {
        public int GameID { get; set; }
        public Game EditedGameDetails { get; set; }
        public int NumberOfPlayersToStart {get; set; }
        public int NumberOfPlayersToDelete { get; set; }
        public int NumberOfPlayersAfterDelete { get; set; }
        public List<Player> ListOfPlayerToDelete { get; set; }
        public int NumberOfRoundsToStart { get; set; }
        public int NumberOfRoundsToDelete { get; set; }
        public int NumberOfRoundsAfterDelete { get; set; }
        public List<Round> ListOfRoundsToDelete { get; set; }
        
        public bool didIsHandicappedChange { get; set; }
    }
}