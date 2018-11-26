using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Web;

namespace GalleryAppealService.Models
{
    public class CrmHelper
    {
        private string crmUri = string.Empty; 
        private string crmUser = string.Empty;
        private string crmPass = string.Empty;
        IOrganizationService organizationService;

        public CrmHelper(string crmUri, string crmUser, string crmPass)
        {
            this.crmUri = crmUri;
            this.crmUser = crmUser;
            this.crmPass = crmPass;

            ClientCredentials clientCredentials = new ClientCredentials();
            clientCredentials.UserName.UserName = this.crmUser;
            clientCredentials.UserName.Password = this.crmPass;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            organizationService = (IOrganizationService)new OrganizationServiceProxy(new Uri(this.crmUri),
                null, clientCredentials, null);
        }

        public string CreateAppeal(AppealEntity appealEntity)
        {
            Entity crmAppeal = new Entity("incident");
            crmAppeal["title"] = appealEntity.Name;
            QueryExpression queryExpression = new QueryExpression("account");
            queryExpression.ColumnSet = new ColumnSet("name");
            var customerEntityRef = organizationService.RetrieveMultiple(queryExpression).Entities.FirstOrDefault().ToEntityReference();
            crmAppeal["customerid"] = customerEntityRef;
            
            crmAppeal["new_web_contact"] = appealEntity.Name;
            crmAppeal["new_web_email"] = appealEntity.Email;
            crmAppeal["new_web_subject_appeals"] = appealEntity.AppealType;
            crmAppeal["new_web_description"] = appealEntity.Question;
            
            var appeal = organizationService.Create(crmAppeal);
            return appeal.ToString();
        }

    }
}