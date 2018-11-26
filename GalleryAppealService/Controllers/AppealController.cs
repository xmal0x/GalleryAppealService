using GalleryAppealService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Web.Http;


namespace GalleryAppealService.Controllers
{
    public class AppealController : ApiController
    {
        string crmUri = ConfigurationManager.AppSettings["organizationServiceUri"];
        string crmUser = ConfigurationManager.AppSettings["organizationServiceUsername"];
        string crmPass = ConfigurationManager.AppSettings["organizationServicePass"];

        [HttpPost]
        public string CreateAppeal(AppealEntity appealEntity)
        {
            var crmGuid = string.Empty;

            try
            {
                var crmHelper = new CrmHelper(crmUri, crmUser, crmPass);
                crmGuid = crmHelper.CreateAppeal(appealEntity);
            }
            catch (Exception e)
            {
                return JsonConvert.SerializeObject(new { code = "500", message="error", details=e.Message });
            }

            return JsonConvert.SerializeObject(new { code="200", message="ok", guid=crmGuid}) ;
        }
    }
}