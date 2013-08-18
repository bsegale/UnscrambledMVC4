using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Unscrambled.Areas.Coordinator.ViewHelpers
{
    public class MyTextBoxHelper
    {
        public static MvcHtmlString CreateSlopeTextBox(string value, string slopeErrorMessage)
        {
            string textBoxHtmlString = "";
            MvcHtmlString textBoxMVCString;

            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "text";
            builder.Attributes["name"] = "Slope";
            builder.Attributes["id"] = "Slope";
            builder.Attributes["class"] = "radioCalcHandicap";
            builder.Attributes["value"] = value;



            textBoxMVCString = MvcHtmlString.Create(textBoxHtmlString);
            return textBoxMVCString;
        }
    }
}