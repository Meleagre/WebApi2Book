using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi2Book.Api.Models;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class TaskByIdInquiryProcessor : ITaskByIdInquiryProcessor
    {
        private readonly IMapper _mapper;
        private readonly ITaskByIdQueryProcessor _queryProcessor;

        public TaskByIdInquiryProcessor(ITaskByIdQueryProcessor queryProcessor, IMapper mapper)
        {
            _queryProcessor = queryProcessor;
            _mapper = mapper;
        }

        public Task GetTask(long taskId)
        {
            var taskEntity = _queryProcessor.GetTask(taskId);
            if (taskEntity == null)
                throw new RootObjectNotFoundException("Task not found");

            var task = _mapper.Map<Task>(taskEntity);
            return task;
        }
    }
}