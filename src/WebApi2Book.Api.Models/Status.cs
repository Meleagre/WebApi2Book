using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi2Book.Api.Models
{
    public class Status
    {
        public Int64 StatusId { get; set; }
        public string Name { get; set; }
        public int Ordinal { get; set; }
    }
}
