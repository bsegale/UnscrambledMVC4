using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class DeleteFoursomesViewModel
    {
        public Game Game { get; set; }
        public List<RoundFoursome> deletableFoursomes { get; set; }
        public int RoundID { get; set; }
    }
}