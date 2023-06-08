using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Product
{
    public class CreatedProductDto : IDto
    {      
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }

        // Her ürün bir kullanıcıya ait olacak. Boşta ürün olmayacak
        public int UserId { get; set; }
    }
}
