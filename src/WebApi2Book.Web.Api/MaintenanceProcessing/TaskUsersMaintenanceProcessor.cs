using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi2Book.Api.Models;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskUsersMaintenanceProcessor : ITaskUsersMaintenanceProcessor
    {
        private readonly IMapper _mapper;
        private readonly IUpdateTaskQueryProcessor _queryProcessor;

        public TaskUsersMaintenanceProcessor(IUpdateTaskQueryProcessor queryProcessor,
            Mapper mapper)
        {
            _queryProcessor = queryProcessor;
            _mapper = mapper;
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.AddTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUser(taskId, userId);
            return CreateTaskResponse(taskEntity);
        }

        public Task DeleteTaskUsers(long taskId)
        {
            var taskEntity = _queryProcessor.DeleteTaskUsers(taskId);
            return CreateTaskResponse(taskEntity);
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            var taskEntity = _queryProcessor.ReplaceTaskUsers(taskId, userIds);
            return CreateTaskResponse(taskEntity);
        }

        public virtual Task CreateTaskResponse(Data.Entities.Task taskEntity)
        {
            var task = _mapper.Map<Task>(taskEntity);
            return task;
        }
    }
}