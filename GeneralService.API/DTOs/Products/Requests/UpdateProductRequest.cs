using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneralService.API.DTOs.Products.Requests
{
    public class UpdateProductRequest : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
