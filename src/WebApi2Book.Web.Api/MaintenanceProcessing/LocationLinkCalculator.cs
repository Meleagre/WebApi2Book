using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi2Book.Api.Models;
using WebApi2Book.Common;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class LocationLinkCalculator
    {
        public static Uri GetLocationLink(ILinkContaining linkContaining)
        {
            var locationLink = linkContaining.Links.FirstOrDefault(
                x => x.Rel == Constants.CommonLinkRelValues.Self);
            return locationLink == null ? null : new Uri(locationLink.Href);
        }
    }
}