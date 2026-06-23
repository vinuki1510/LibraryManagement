using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Model
{
    public class Author
    {
        public int AuthorID { get; set; }
        public String AuthorName { get; set; }
     
        public Author(int authorID, string inauthorName)
        {
            AuthorID = authorID;
            AuthorName = inauthorName;
        }
    }
   
}
