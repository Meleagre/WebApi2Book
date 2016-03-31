using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi2Book.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface ITaskUsersMaintenanceProcessor
    {
        Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds);
        Task DeleteTaskUsers(long taskId);
        Task AddTaskUser(long taskId, long userId);
        Task DeleteTaskUser(long taskId, long userId);
    }
}
