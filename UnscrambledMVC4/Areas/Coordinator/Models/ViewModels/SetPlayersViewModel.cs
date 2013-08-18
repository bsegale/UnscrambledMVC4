using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class SetPlayersViewModel
    {
        public Game Game { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public int playerCounter { get; set; }
        public List<KeyValuePair<string, bool>> playerIndexErrors { get; set; }
        public List<KeyValuePair<string, bool>> playerNameErrors { get; set; }
    }
}