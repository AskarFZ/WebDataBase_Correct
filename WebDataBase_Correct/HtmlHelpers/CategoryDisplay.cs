using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDataBase_Correct.HtmlHelpers
{
   static public class CategoryDisplay
    {

        static public HtmlString SimpleHelper(this IHtmlHelper helper)
        {
            string str = "<div style ='width:150px ; height:150px ;background-color:black;' > " +
                " Eto Html helper " +
                "</div>";
            return new HtmlString(str);
        }
        static public HtmlString ShowListHelper(this IHtmlHelper helper,List<string> sList)
        {

            string str = "<ol>";
            foreach (string item in sList)
            {
                str += "<li>"+item+"</li>";
            }

            str += "</ol>";

            return new HtmlString(str);

        }

        static public HtmlString ShowListHelper(this IHtmlHelper helper, IEnumerable<WebDataBase_Correct.DB.Category> sList)
        {

            string str = "<ol>";
            foreach (WebDataBase_Correct.DB.Category item in sList)
            {
                str += "<li>" + item.Name + "</li>";
            }

            str += "</ol>";

            return new HtmlString(str);

        }

        static public HtmlString ShowListHelper(this IHtmlHelper helper, IEnumerable<WebDataBase_Correct.DB.Category> sList, string markerType)
        {

            string str = "<ul type='"+markerType+"'>";  
            foreach (WebDataBase_Correct.DB.Category item in sList)
            {
                str += "<li>" + item.Name + "</li>";
            }

            str += "</ul>";

            return new HtmlString(str);

        }




    }
}
