using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unscrambled.Domain;

namespace Unscrambled.Areas.Coordinator.Models.ViewModels
{
    public class AddFoursomeConfirmationViewModel
    {
        public int RoundID { get; set; }
        public int currentNumberOfFoursomes { get; set; }
        public int newNumberOfFoursomes { get; set; }
        public Game Game { get; set; }
    }
}