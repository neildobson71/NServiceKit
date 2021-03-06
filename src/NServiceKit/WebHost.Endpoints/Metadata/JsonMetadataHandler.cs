using System;
using System.Web.UI;
using NServiceKit.Common.Utils;
using NServiceKit.ServiceHost;
using NServiceKit.ServiceModel.Serialization;
using NServiceKit.Text;
using NServiceKit.WebHost.Endpoints.Support.Metadata.Controls;

namespace NServiceKit.WebHost.Endpoints.Metadata
{
    /// <summary>A JSON metadata handler.</summary>
    public class JsonMetadataHandler : BaseMetadataHandler
    {
        /// <summary>Gets the format to use.</summary>
        ///
        /// <value>The format.</value>
        public override Format Format { get { return Format.Json; } }

        /// <summary>Creates a message.</summary>
        ///
        /// <param name="dtoType">Type of the dto.</param>
        ///
        /// <returns>The new message.</returns>
		protected override string CreateMessage(Type dtoType)
        {
            var requestObj = ReflectionUtils.PopulateObject(dtoType.CreateInstance());
            return JsonDataContractSerializer.Instance.SerializeToString(requestObj);
        }

        /// <summary>Renders the operations.</summary>
        ///
        /// <param name="writer">  The writer.</param>
        /// <param name="httpReq"> The HTTP request.</param>
        /// <param name="metadata">The metadata.</param>
        protected override void RenderOperations(HtmlTextWriter writer, IHttpRequest httpReq, ServiceMetadata metadata)
        {
            var defaultPage = new OperationsControl
            {
				Title = EndpointHost.Config.ServiceName,
                OperationNames = metadata.GetOperationNamesForMetadata(httpReq, Format),
                MetadataOperationPageBodyHtml = EndpointHost.Config.MetadataOperationPageBodyHtml,
            };

            defaultPage.RenderControl(writer);
        }
    }
}