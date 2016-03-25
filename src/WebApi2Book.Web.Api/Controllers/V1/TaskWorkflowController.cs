using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApi2Book.Api.Models;
using WebApi2Book.Common;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
    public class TaskWorkflowController : ApiController
    {
        private readonly ITaskWorkflowProcessor _taskWorkFlowProcessor;

        public TaskWorkflowController(ITaskWorkflowProcessor taskWorkflowProcessor)
        {
            _taskWorkFlowProcessor = taskWorkflowProcessor;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/activations", Name = "StartTaskRoute")]
        public Task StartTask(long taskId)
        {
            var task = _taskWorkFlowProcessor.StartTask(taskId);
            return task;
        }

        [HttpPost]
        [Route("tasks/{taskId:long}/completions", Name = "CompleteTaskRoute")]
        public Task CompleteTask(long taskId)
        {
            var task = _taskWorkFlowProcessor.CompleteTask(taskId);
            return task;
        }

        [HttpPost]
        [UserAudit]
        [Route("tasks/{taskId:long}/reactivations", Name = "ReactivateTaskRoute")]
        public Task ReactivateTask(long taskId)
        {
            var task = _taskWorkFlowProcessor.ReactivateTask(taskId);
            return task;
        }
    }
}