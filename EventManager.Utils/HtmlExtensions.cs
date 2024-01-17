using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManager.Utils;

public static class HtmlExtensions
{
    public static IHtmlContent DisabledIf(this IHtmlHelper htmlHelper,
        bool condition)
        => new HtmlString(condition ? "disabled=\"disabled\"" : "");
}