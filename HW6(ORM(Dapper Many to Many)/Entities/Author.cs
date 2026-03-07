using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6_ORM_Dapper_Many_to_Many_.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        public ICollection<Book> Books { get; set; } = [];
        public override string ToString()
        {
            return FullName;
        }
    }
}
