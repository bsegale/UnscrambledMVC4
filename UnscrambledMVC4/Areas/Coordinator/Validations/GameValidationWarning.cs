using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnscrambledMVC4.Areas.Coordinator.Validations
{
    public class GameValidationWarning
    {
        public String Category { get; set; }
        public String WarningMessage { get; set; }
        public int ActionID { get; set; }
    }
}