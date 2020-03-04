using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.TagHelpers
{
    public class EmailTagHelper : TagHelper
    {

        private const string EmailDomain = "fiap.com";

        public string MailTo { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";

            var address = $"{MailTo.ToLower()}@{EmailDomain}";

            output.Attributes.SetAttribute("href", $"mailto:{address}");
            output.Content.SetHtmlContent($"<div> teste  {address}</div>");
            //output.Content.SetContent(address);

            //base.Process(context, output);

        }

    }


}
