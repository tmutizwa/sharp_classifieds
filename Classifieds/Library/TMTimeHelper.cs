using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Classifieds.Library
{
    public static class TMTimeHelper
    {
        public static MvcHtmlString ToWords(this HtmlHelper htmlHelper, DateTime? sdateTime)
        {
            //convert our nullable sdatetime to no-nullable
            var dateTime = sdateTime ?? DateTime.Now;
            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (ts.Seconds > 0)
            {
                if (delta < 60)
                {
                    return ts.Seconds == 1 ? MvcHtmlString.Create("one second ago") : MvcHtmlString.Create(ts.Seconds + " seconds ago");
                }
                if (delta < 120)
                {
                    return MvcHtmlString.Create("a minute ago");
                }
                if (delta < 2700) // 45 * 60
                {
                    return MvcHtmlString.Create(ts.Minutes + " minutes ago");
                }
                if (delta < 5400) // 90 * 60
                {
                    return MvcHtmlString.Create("an hour ago");
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return MvcHtmlString.Create(ts.Hours + " hours ago");
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return MvcHtmlString.Create("yesterday");
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return MvcHtmlString.Create(ts.Days + " days ago");
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? MvcHtmlString.Create("one month ago") : MvcHtmlString.Create(months + " months ago");
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? MvcHtmlString.Create("one year ago") : MvcHtmlString.Create(years + " years ago");
            }
            else
            {

                if (delta < 60)
                {
                    return ts.Seconds == 1 ? MvcHtmlString.Create("in one second") : MvcHtmlString.Create("in "+ts.Seconds+" seconds");
                }
                if (delta < 120)
                {
                    return MvcHtmlString.Create("in one minute");
                }
                if (delta < 2700) // 45 * 60
                {
                    return MvcHtmlString.Create("in "+(ts.Minutes * -1)+" minutes");
                }
                if (delta < 5400) // 90 * 60
                {
                    return MvcHtmlString.Create("in one hour");
                }
                if (delta < 86400) // 24 * 60 * 60
                {
                    return MvcHtmlString.Create("in "+ts.Hours*-1+" hours");
                }
                if (delta < 172800) // 48 * 60 * 60
                {
                    return MvcHtmlString.Create("tomorrow");
                }
                if (delta < 2592000) // 30 * 24 * 60 * 60
                {
                    return MvcHtmlString.Create("in "+ts.Days*-1+" days");
                }
                if (delta < 31104000) // 12 * 30 * 24 * 60 * 60
                {
                    int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                    return months <= 1 ? MvcHtmlString.Create("in one month") : MvcHtmlString.Create("in "+months*-1+" months");
                }
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? MvcHtmlString.Create("in one year") : MvcHtmlString.Create("in "+years*-1+" years");
            }

            
        }
    }
}