using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6_ORM_Dapper_Many_to_Many_.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public Gerne Genre { get; set; }
        public ICollection<Author> Authors { get; set; } = [];
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
