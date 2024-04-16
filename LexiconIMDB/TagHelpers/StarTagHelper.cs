using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;
using System.Text.Encodings.Web;

namespace LexiconIMDB.TagHelpers
{
    [HtmlTargetElement("star")]
    public class StarTagHelper : TagHelper
    {
        public float Rating { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";
            output.AddClass("star", HtmlEncoder.Default);

            var roundedRating = (int)Math.Round(Rating);

            var maxStars = 10; 
            var stars = roundedRating;
            var isHalfStar = roundedRating % 2 == 1;

            var commons = "https://upload.wikimedia.org/wikipedia/commons/";
            var star = commons + "e/e5/Full_Star_Yellow.svg";
            var half = commons + "d/d6/Half_Star_Yellow.svg";
            var empty = commons + "e/e7/Empty_Star.svg";

            var builder = new StringBuilder();

            int i = 0;
            while (i < stars)
            {
                builder.Append($"<img src='{star}' />");
                i++;
            }
            if (isHalfStar)
            {
                builder.Append($"<img src='{half}' />");
                i++;
            }

            while (i < maxStars)
            {
                builder.Append($"<img src='{empty}' />");
                i++;
            }


            //for (int i = 0; i< stars; i++)
            //{
            //    builder.Append($"<img src='{star}' />");
            //}
            //if (isHalfStar)
            //    builder.Append($"<img src='{half}' />");


            output.Content.SetHtmlContent(builder.ToString());
        }

    }
}
