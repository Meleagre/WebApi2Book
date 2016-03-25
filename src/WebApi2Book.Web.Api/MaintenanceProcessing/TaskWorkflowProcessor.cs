using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi2Book.Api.Models;
using WebApi2Book.Common;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class TaskWorkflowProcessor : ITaskWorkflowProcessor
    {
        private readonly IMapper _mapper;
        private readonly ITaskByIdQueryProcessor _taskByIdQueryProcessor;
        private readonly IDateTime _dateTime;
        private readonly IUpdateTaskStatusQueryProcessor _updateTaskStatusQueryProcessor;

        public TaskWorkflowProcessor(ITaskByIdQueryProcessor taskByIdQueryProcessor,
            IUpdateTaskStatusQueryProcessor updateTask, IMapper mapper, IDateTime dateTime)
        {
            _mapper = mapper;
            _updateTaskStatusQueryProcessor = updateTask;
            _taskByIdQueryProcessor = taskByIdQueryProcessor;
            _dateTime = dateTime;
        }

        public Task StartTask(long taskId)
        {
            var taskEntity = _taskByIdQueryProcessor.GetTask(taskId);
            if (taskEntity == null)
                throw new RootObjectNotFoundException("Task not found");

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "Not Started")
            {
                throw new BusinessRuleViolationException(
                    "Incorrect task status. Expected status of 'Not Started'.");
            }

            taskEntity.StartDate = _dateTime.UtcNow;
            _updateTaskStatusQueryProcessor.UpdateTaskStatus(taskEntity, "In Progress");

            var task = _mapper.Map<Task>(taskEntity);
            return task;
        }

        public Task CompleteTask(long taskId)
        {
            var taskEntity = _taskByIdQueryProcessor.GetTask(taskId);
            if (taskEntity == null)
                throw new RootObjectNotFoundException("Task not found");

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "In Progress")
            {
                throw new BusinessRuleViolationException(
                    "Incorrect task status. Expected status of 'In Progress'.");
            }

            taskEntity.CompletedDate = _dateTime.UtcNow;
            _updateTaskStatusQueryProcessor.UpdateTaskStatus(taskEntity, "Completed");

            var task = _mapper.Map<Task>(taskEntity);
            return task;
        }

        public Task ReactivateTask(long taskId)
        {
            var taskEntity = _taskByIdQueryProcessor.GetTask(taskId);
            if (taskEntity == null)
                throw new RootObjectNotFoundException("Task not found");

            // Simulate some workflow logic...
            if (taskEntity.Status.Name != "Completed")
            {
                throw new BusinessRuleViolationException(
                    "Incorrect task status. Expected status of 'Completed'.");
            }

            taskEntity.CompletedDate = null;
            _updateTaskStatusQueryProcessor.UpdateTaskStatus(taskEntity, "In Progress");

            var task = _mapper.Map<Task>(taskEntity);
            return task;
        }
    }
}