using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using Augustus.CRM.Entities;

[assembly: ProxyTypesAssemblyAttribute()]

namespace Augustus.CRM
{
    public class BaseOrganization : IDisposable
    {
        protected OrganizationServiceContext context;
        private IOrganizationService service;

        public BaseOrganization(Uri crmUrl, string accessToken)
        {
            var uri = new Uri(crmUrl, "/XRMServices/2011/Organization.svc/web");

            service = new OrganizationWebProxyClient(uri, useStrongTypes: true)
            {
                HeaderToken = accessToken,
                SdkClientVersion = "7.0",
            };
            
            context = new OrganizationServiceContext(service);
        }

        public BaseOrganization(string connectionString)
        {
            CrmServiceClient crmSvc = new CrmServiceClient(connectionString);

            if (!crmSvc.IsReady)
            {
                throw new Exception(crmSvc.LastCrmError, crmSvc.LastCrmException);
            }

            if (crmSvc.OrganizationWebProxyClient == null)
            {
                service = crmSvc.OrganizationServiceProxy;
            }
            else
            {
                service = crmSvc.OrganizationWebProxyClient;
            }

            context = new OrganizationServiceContext(service);
        }

        public IEnumerable<Status> InvoiceStatus
        {
            get
            {
                return GetOptionSet(Invoice.EntityLogicalName, "statuscode");
            }
        }
        
        private IEnumerable<Status> GetOptionSet(string entityLogicalName, string attributeLogicalName)
        {
            string optionLabel = String.Empty;

            RetrieveAttributeRequest attributeRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entityLogicalName,
                LogicalName = attributeLogicalName,
                RetrieveAsIfPublished = true
            };

            RetrieveAttributeResponse attributeResponse = (RetrieveAttributeResponse)service.Execute(attributeRequest);
            AttributeMetadata attrMetadata = (AttributeMetadata)attributeResponse.AttributeMetadata;
            StatusAttributeMetadata statusMetadata = (StatusAttributeMetadata)attrMetadata;

            return (from o in statusMetadata.OptionSet.Options
                    select new Status
                    {
                        Value = o.Value.Value,
                        Label = o.Label.UserLocalizedLabel.Label
                    });
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
