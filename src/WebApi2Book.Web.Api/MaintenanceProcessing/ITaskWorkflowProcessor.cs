using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi2Book.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface ITaskWorkflowProcessor
    {
        Task StartTask(long taskId);
        Task CompleteTask(long taskId);
        Task ReactivateTask(long taskId);
    }
}