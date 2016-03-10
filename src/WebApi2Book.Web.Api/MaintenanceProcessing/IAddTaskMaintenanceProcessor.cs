using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi2Book.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public interface IAddTaskMaintenanceProcessor
    {
        Task AddTask(NewTask newTask);
    }
}
