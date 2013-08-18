using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unscrambled.Areas.Coordinator.Models.ViewModels;

namespace Unscrambled.Areas.Coordinator.ViewHelpers
{
    public class MySelectHelper
    {
        public static MvcHtmlString CreateOptionsInSelectTag(List<SelectListItem> options, string selectedValue)
        {
            string SelectOptionsString = "";
            MvcHtmlString SelectOptionsMVCString;

            foreach (var option in options)
            {


                TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(option.Text)
                };
                if (option.Value != null)
                {
                    builder.Attributes["value"] = option.Value;
                }
                if (option.Value.ToLower() == selectedValue.ToLower())
                {
                    builder.Attributes["selected"] = "selected";
                }

                SelectOptionsString += builder.ToString(TagRenderMode.Normal);
            }


            SelectOptionsMVCString = MvcHtmlString.Create(SelectOptionsString);
            return SelectOptionsMVCString;
        }


        /*
        public static MvcHtmlString CreateOptionsInParSelectTag(List<SelectListItem> options, string selectedValue, int index)
        {
            string selectHtmlString = "";
            string selectOptionsHtmlString = "";
            MvcHtmlString SelectOptionsMVCString;
           
            TagBuilder inputPart = new TagBuilder("select");
            inputPart.Attributes["id"] = "ParForHole" + index.ToString();
            inputPart.Attributes["name"] = "ParForHole" + index.ToString();
            inputPart.Attributes["class"] = "HandicapDropDown";
            


                

            foreach (var option in options)
            {


                TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(option.Text)
                };
                if (option.Value != null)
                {
                    builder.Attributes["value"] = option.Value;
                }
                
                if (option.Value.ToLower() == selectedValue.ToLower())
                {
                    builder.Attributes["selected"] = "selected";
                }
                

                selectOptionsHtmlString += builder.ToString(TagRenderMode.Normal);
            }

            inputPart.InnerHtml = selectOptionsHtmlString;
            selectHtmlString += inputPart.ToString(TagRenderMode.Normal);


            SelectOptionsMVCString = MvcHtmlString.Create(selectHtmlString);
            return SelectOptionsMVCString;
        }

        */

        public static MvcHtmlString CreateOptionsInParSelectTag(HoleViewModel holeViewModel)
        {
            string selectHtmlString = "";
            string selectOptionsHtmlString = "";
            MvcHtmlString SelectOptionsMVCString;

            TagBuilder inputPart = new TagBuilder("select");
            inputPart.Attributes["id"] = "ParForHole" + holeViewModel.HoleNumber.ToString();
            inputPart.Attributes["name"] = "ParForHole" + holeViewModel.HoleNumber.ToString();
            inputPart.Attributes["class"] = "HandicapDropDown";
            if (holeViewModel.ParErrorMessage != "")
            {
                inputPart.Attributes["style"] = "color:red;border-color:red";
            }





            foreach (var option in holeViewModel.ParList)
            {
                TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(option.Text)
                };
                if (option.Value != null)
                {
                    builder.Attributes["value"] = option.Value;
                }

                if (option.Value == holeViewModel.Par.ToString())
                {
                    builder.Attributes["selected"] = "selected";
                }

               


                selectOptionsHtmlString += builder.ToString(TagRenderMode.Normal);
            }

            inputPart.InnerHtml = selectOptionsHtmlString;
            selectHtmlString += inputPart.ToString(TagRenderMode.Normal);


            SelectOptionsMVCString = MvcHtmlString.Create(selectHtmlString);
            return SelectOptionsMVCString;
        }




        public static MvcHtmlString CreateOptionsInHandicapSelectTag(HoleViewModel holeViewModel)
        {
            string selectHtmlString = "";
            string selectOptionsHtmlString = "";
            MvcHtmlString SelectOptionsMVCString;

            TagBuilder inputPart = new TagBuilder("select");
            inputPart.Attributes["id"] = "HandicapForHole" + holeViewModel.HoleNumber.ToString();
            inputPart.Attributes["name"] = "HandicapForHole" + holeViewModel.HoleNumber.ToString();
            inputPart.Attributes["class"] = "HandicapDropDown";
            if (holeViewModel.HandicapErrorMessage != "")
            {
                inputPart.Attributes["style"] = "color:red;border-color:red";
            }





            foreach (var option in holeViewModel.HoleHandicapList)
            {
                TagBuilder builder = new TagBuilder("option")
                {
                    InnerHtml = HttpUtility.HtmlEncode(option.Text)
                };
                if (option.Value != null)
                {
                    builder.Attributes["value"] = option.Value;
                }

                if (option.Value == holeViewModel.Handicap.ToString())
                {
                    builder.Attributes["selected"] = "selected";
                }

                


                selectOptionsHtmlString += builder.ToString(TagRenderMode.Normal);
            }

            inputPart.InnerHtml = selectOptionsHtmlString;
            selectHtmlString += inputPart.ToString(TagRenderMode.Normal);


            SelectOptionsMVCString = MvcHtmlString.Create(selectHtmlString);
            return SelectOptionsMVCString;
        }
    }
}