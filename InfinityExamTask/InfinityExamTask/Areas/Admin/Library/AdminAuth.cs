using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.WebUI.Areas.Admin.Library
{
    public class AdminAuth : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filtercontext)
        {
            var SessionControl = filtercontext.HttpContext.Session.GetString("AdminSession");
            if (SessionControl == null)
                filtercontext.Result = new RedirectToActionResult("Index", "Account", new { area = "Admin" });
        }
    }
}
