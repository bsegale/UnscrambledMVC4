using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class DeleteFoursomeConfirmationViewModel
    {
        public int RoundID { get; set; }
        public int currentNumberOfFoursomes { get; set; }
        public int newNumberOfFoursomes { get; set; }
        public Game Game { get; set; }
        public Boolean canDelete { get; set; }
        public List<RoundFoursome> deletableFoursomes { get; set; }
    }
}