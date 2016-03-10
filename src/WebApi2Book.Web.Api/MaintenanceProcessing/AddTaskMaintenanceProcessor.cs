using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using WebApi2Book.Api.Models;
using WebApi2Book.Common;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
        private readonly IMapper _mapper;
        private readonly IAddTaskQueryProcessor _queryProcessor;

        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor, IMapper mapper)
        {
            _queryProcessor = queryProcessor;
            _mapper = mapper;
        }

        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _mapper.Map<Data.Entities.Task>(newTask);
            _queryProcessor.AddTask(taskEntity);
            var task = _mapper.Map<Task>(taskEntity);

            // TODO: Implement link service
            task.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost:58300/api/v1/tasks/" + task.TaskId,
                Rel = Constants.CommonLinkRelValues.Self
            });
            return task;
        }
    }
}