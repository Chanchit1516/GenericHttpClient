using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceTest.API.DTOs
{
    public class BaseDTO
    {
        public BaseDTO()
        {
            CreatedDateTime = DateTime.UtcNow;
        }
        public DateTime CreatedDateTime { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDateTime { get; set; }
    }
}
