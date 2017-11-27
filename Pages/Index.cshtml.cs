using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using httpclient_messagehandler_example.Services;
using System.Text.RegularExpressions;

namespace httpclient_messagehandler_example.Pages
{
    public class IndexModel : PageModel
    {
        private FancyService _service;
        public IndexModel(FancyService service)
        {
            _service = service;
        }
        
        public async Task OnGet()
        {
            var fancyThing = await _service.GetSomethingFancy();            
            ViewData["FancyThing"] = SyntaxHighlightJson(fancyThing);
        }

        /// <summary>
        /// Format JSON so that it can be pretty printed in an HTML <pre> tag
        /// http://joelabrahamsson.com/syntax-highlighting-json-with-c/
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public string SyntaxHighlightJson(string original)
        {
            return Regex.Replace(
                original,
                @"(¤(\\u[a-zA-Z0-9]{4}|\\[^u]|[^\\¤])*¤(\s*:)?|\b(true|false|null)\b|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?)".Replace('¤', '"'),
                match => {
                var cls = "number";
                if (Regex.IsMatch(match.Value, @"^¤".Replace('¤', '"'))) {
                    if (Regex.IsMatch(match.Value, ":$")) {
                    cls = "key";
                    } else {
                    cls = "string";
                    }
                } else if (Regex.IsMatch(match.Value, "true|false")) {
                    cls = "boolean";
                } else if (Regex.IsMatch(match.Value, "null")) {
                    cls = "null";
                }
                return "<span class=\"" + cls + "\">" + match + "</span>";
                });
        }
    }
}
