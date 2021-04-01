using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebDataBase_Correct.Taghelper
{
    [HtmlTargetElement("table",Attributes="categories")]
    public class CategoryTable:TagHelper
    {
        public IEnumerable<WebDataBase_Correct.DB.Category> categories { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";
            output.Attributes.Add("border", "1");
            foreach (WebDataBase_Correct.DB.Category item in categories)
            {
                output.Content.AppendHtml("<tr> <td>" + item.Name + "</td> </tr>");
            } 
            

        }

    }
}
