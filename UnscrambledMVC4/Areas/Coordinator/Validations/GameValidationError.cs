using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnscrambledMVC4.Areas.Coordinator.Validations
{
    public class GameValidationError
    {
        public String Category { get; set; }
        public String ErrorMessage { get; set; }
        public int ActionID { get; set; }
    }
}