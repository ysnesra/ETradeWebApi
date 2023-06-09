using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class User: BaseEntity
    {
        public User()
        {
            Products= new HashSet<Product>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

