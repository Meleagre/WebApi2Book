using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using System.Threading;
using System.Threading.Tasks;
using WebApi2Book.Common;
using TaskModel = WebApi2Book.Api.Models.Task;

namespace WebApi2Book.Web.Api.Security
{
    public class TaskDataSecurityMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public TaskDataSecurityMessageHandler(ILogManager logManager, IUserSession userSession)
        {
            _userSession = userSession;
            _log = logManager.GetLog(typeof(TaskDataSecurityMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (CanHandleResponse(response))
            {
                ApplySecurityToResponseData((ObjectContent)response.Content);
            }
            return response;
        }

        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;
            var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof(TaskModel);
            return canHandleResponse;
        }

        public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var removeSensitiveData = !_userSession.IsInRole(Constants.RoleNames.SeniorWorker);
            if (removeSensitiveData)
            {
                _log.DebugFormat("Applying security data masing for user {0}", _userSession.Username);
            }
            var task = responseObjectContent.Value as TaskModel;
            task.SetShouldSerializeAssignees(!removeSensitiveData);
        }
    }
}