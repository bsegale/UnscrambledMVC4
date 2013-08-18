using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Unscrambled.Areas.Coordinator.ViewHelpers
{
    public class MyRadioHelper
    {
        public static MvcHtmlString CreateCalcHandicappedRadioHTML(string selectedValue)
        {
            string RadioHTMLString = "";
            MvcHtmlString RadioHTMLMVCString;

            


                TagBuilder builder = new TagBuilder("input");
                builder.Attributes["type"] = "radio";
                builder.Attributes["name"] = "AutoCalcHandicap";
                builder.Attributes["id"] = "AutoCalcHandicapYes";
                builder.Attributes["class"] = "radioCalcHandicap";
                builder.Attributes["onclick"] = "enable()";
                builder.Attributes["value"] = "Y";
                if (selectedValue == null)
                {
                    builder.Attributes["checked"] = "checked";
                }
                else if (selectedValue == "Y")
                {
                    builder.Attributes["checked"] = "checked";
                }

                TagBuilder labelBuilder = new TagBuilder("label")
                {
                    InnerHtml = HttpUtility.HtmlEncode("Yes")
                };
                labelBuilder.Attributes["for"] = "AutoCalcHandicapYes";
                labelBuilder.Attributes["class"] = "labelCalcHandicap";


                
                    
                

                TagBuilder builder2 = new TagBuilder("input");
                    builder2.Attributes["type"] = "radio";
                    builder2.Attributes["name"] = "AutoCalcHandicap";
                    builder2.Attributes["id"] = "AutoCalcHandicapNo";
                    builder2.Attributes["class"] = "radioCalcHandicap";
                    builder2.Attributes["onclick"] = "disable()";
                    builder2.Attributes["value"] = "N";
                    if (selectedValue == "N")
                    {
                        builder2.Attributes["checked"] = "checked";
                    }

                    TagBuilder labelBuilder2 = new TagBuilder("label")
                    {
                        InnerHtml = HttpUtility.HtmlEncode("No")
                    };
                    labelBuilder2.Attributes["for"] = "AutoCalcHandicapNo";
                    labelBuilder2.Attributes["class"] = "labelCalcHandicap";


                RadioHTMLString += builder.ToString(TagRenderMode.Normal);
                RadioHTMLString += labelBuilder.ToString(TagRenderMode.Normal);
                RadioHTMLString += builder2.ToString(TagRenderMode.Normal);
                RadioHTMLString += labelBuilder2.ToString(TagRenderMode.Normal);


                RadioHTMLMVCString = MvcHtmlString.Create(RadioHTMLString);
                return RadioHTMLMVCString;
        }


        public static MvcHtmlString CreateNumberOfHolesRadioHTML(int? numberOfHoles)
        {
            string RadioHTMLString = "";
            MvcHtmlString RadioHTMLMVCString;




            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "radio";
            builder.Attributes["name"] = "NumberOfHolesInRound";
            builder.Attributes["id"] = "NumberOfHolesInRound18";
            builder.Attributes["class"] = "radioCalcHandicap";
            builder.Attributes["onclick"] = "enable()";
            builder.Attributes["value"] = "18";
            if (numberOfHoles == null)
            {
                builder.Attributes["checked"] = "checked";
            }
            else if (numberOfHoles == 18)
            {
                builder.Attributes["checked"] = "checked";
            }

            TagBuilder labelBuilder = new TagBuilder("label")
            {
                InnerHtml = HttpUtility.HtmlEncode("18")
            };
            labelBuilder.Attributes["for"] = "NumberOfHolesInRound18";
            labelBuilder.Attributes["class"] = "labelCalcHandicap";






            TagBuilder builder2 = new TagBuilder("input");
            builder2.Attributes["type"] = "radio";
            builder2.Attributes["name"] = "NumberOfHolesInRound";
            builder2.Attributes["id"] = "NumberOfHolesInRound9";
            builder2.Attributes["class"] = "radioCalcHandicap";
            builder2.Attributes["onclick"] = "disable()";
            builder2.Attributes["value"] = "9";
            if (numberOfHoles == 9)
            {
                builder2.Attributes["checked"] = "checked";
            }

            TagBuilder labelBuilder2 = new TagBuilder("label")
            {
                InnerHtml = HttpUtility.HtmlEncode("9")
            };
            labelBuilder2.Attributes["for"] = "NumberOfHolesInRound";
            labelBuilder2.Attributes["class"] = "labelCalcHandicap";


            RadioHTMLString += builder.ToString(TagRenderMode.Normal);
            RadioHTMLString += labelBuilder.ToString(TagRenderMode.Normal);
            RadioHTMLString += builder2.ToString(TagRenderMode.Normal);
            RadioHTMLString += labelBuilder2.ToString(TagRenderMode.Normal);


            RadioHTMLMVCString = MvcHtmlString.Create(RadioHTMLString);
            return RadioHTMLMVCString;
        }

        public static MvcHtmlString CreateParRadioHTML(int? holePar)
        {
            string RadioHTMLString = "";
            MvcHtmlString RadioHTMLMVCString;




            TagBuilder builder = new TagBuilder("input");
            builder.Attributes["type"] = "radio";
            builder.Attributes["name"] = "HolePar";
            builder.Attributes["id"] = "HolePar3";
            builder.Attributes["class"] = "radioCalcHandicap";
            builder.Attributes["value"] = "3";
            if (holePar == 3)
            {
                builder.Attributes["checked"] = "checked";
            }
            TagBuilder labelBuilder = new TagBuilder("label")
            {
                InnerHtml = HttpUtility.HtmlEncode("3")
            };
            labelBuilder.Attributes["for"] = "HolePar";
            labelBuilder.Attributes["class"] = "labelCalcHandicap";


            TagBuilder builder2 = new TagBuilder("input");
            builder2.Attributes["type"] = "radio";
            builder2.Attributes["name"] = "HolePar";
            builder2.Attributes["id"] = "HolePar4";
            builder2.Attributes["class"] = "radioCalcHandicap";
            builder2.Attributes["value"] = "4";
            if (holePar == 4)
            {
                builder.Attributes["checked"] = "checked";
            }
            TagBuilder labelBuilder2 = new TagBuilder("label")
            {
                InnerHtml = HttpUtility.HtmlEncode("4")
            };
            labelBuilder2.Attributes["for"] = "HolePar";
            labelBuilder2.Attributes["class"] = "labelCalcHandicap";


            TagBuilder builder3 = new TagBuilder("input");
            builder3.Attributes["type"] = "radio";
            builder3.Attributes["name"] = "HolePar";
            builder3.Attributes["id"] = "HolePar5";
            builder3.Attributes["class"] = "radioCalcHandicap";
            builder3.Attributes["value"] = "5";
            if (holePar == 5)
            {
                builder.Attributes["checked"] = "checked";
            }
            TagBuilder labelBuilder3 = new TagBuilder("label")
            {
                InnerHtml = HttpUtility.HtmlEncode("5")
            };
            labelBuilder3.Attributes["for"] = "HolePar";
            labelBuilder3.Attributes["class"] = "labelCalcHandicap";



            RadioHTMLString += builder.ToString(TagRenderMode.Normal);
            RadioHTMLString += labelBuilder.ToString(TagRenderMode.Normal);
            RadioHTMLString += builder2.ToString(TagRenderMode.Normal);
            RadioHTMLString += labelBuilder2.ToString(TagRenderMode.Normal);
            RadioHTMLString += builder3.ToString(TagRenderMode.Normal);
            RadioHTMLString += labelBuilder3.ToString(TagRenderMode.Normal);

            RadioHTMLMVCString = MvcHtmlString.Create(RadioHTMLString);
            return RadioHTMLMVCString;
        }
    
    }
}