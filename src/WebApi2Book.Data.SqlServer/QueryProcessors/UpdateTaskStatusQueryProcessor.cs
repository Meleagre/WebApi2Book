using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class UpdateTaskStatusQueryProcessor : IUpdateTaskStatusQueryProcessor
    {
        private readonly ISession _session;

        public UpdateTaskStatusQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            taskToUpdate.Status = _session.QueryOver<Status>()
                .Where(x => x.Name == statusName)
                .SingleOrDefault();
            _session.SaveOrUpdate(taskToUpdate);
        }
    }
}
