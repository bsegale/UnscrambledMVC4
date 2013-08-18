using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Gamezone.Models.ViewModels
{
    public class PlayerOnScore
    {
        public String PlayerName { get; set; }
        public ScorecardHole Hole { get; set; }
    }
}