using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs.Product
{
    public class DeletedProductDto : IDto
    {
        public int ProductId { get; set; }      
    }
}
