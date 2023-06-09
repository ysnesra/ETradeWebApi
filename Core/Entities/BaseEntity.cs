using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseEntity
    {
        //Id ; bütün classlarda ortak olduğu için base olarak burda tanımlandı. Oluşturduğumuz classlara BaseEntity clasından kalıtım verilebilir.
        public int Id { get; set; }

        public BaseEntity()
        {

        }

        //this() -> ortak şeyler gitsin base de çalışsın
        public BaseEntity(int id) : this()
        {
            Id = id;
        }
    }
}
