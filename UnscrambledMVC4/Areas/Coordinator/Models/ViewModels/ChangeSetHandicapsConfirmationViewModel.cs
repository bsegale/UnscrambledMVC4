﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Domain;

namespace UnscrambledMVC4.Areas.Coordinator.Models.ViewModels
{
    public class ChangeSetHandicapsConfirmationViewModel
    {
        public Game game { get; set; }
        public int RoundID { get; set; }
    }
}