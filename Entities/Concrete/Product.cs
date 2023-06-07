using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}