using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi2Book.Api.Models
{
    public class Task : ILinkContaining
    {
        private List<Link> _links;
        private bool _shouldSerializeAssignees;

        public long? TaskId { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Status Status { get; set; }
        public List<User> Assignees { get; set; }

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #region ShouldSerialize implementation

        // By convention ASP.NET Web API calls this methods using reflection to determine
        // if specifig public properties should be seralized. Here we control serialization
        // of Assignees property.

        public void SetShouldSerializeAssignees(bool shouldSerailize)
        {
            _shouldSerializeAssignees = shouldSerailize;
        }

        public bool ShouldSerializeAssignees()
        {
            return _shouldSerializeAssignees;
        }

        #endregion
    }
}
