using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDataBase_Correct.Taghelper
{
    public class TestTagHelper : TagHelper


    {

        public string Color { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.PreContent.SetHtmlContent("tessss <br>");
            output.TagName = "div";
            output.Attributes.Add("style", "background-color:"+Color);
            output.Content.SetContent("Eto Tag Helper");
            output.PostContent.SetHtmlContent(" sdasdas <br>");
        }


    }
}
