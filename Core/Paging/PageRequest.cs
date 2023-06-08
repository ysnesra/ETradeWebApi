using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Paging
{
    public class PageRequest
    {
        public int Page { get; set; } = 0;    //Kaçıncı sayfa
        public int PageSize { get; set; } = 10;   //Bir sayfada kaç data 
    }
}
