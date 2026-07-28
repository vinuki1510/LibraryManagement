using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.model
{
    public class Books
    {
        public int BooksID { get; set; }
        public String BooksName { get; set; }
        public int AuthorID { get; set; }
        public int CategoryID { get; set; }
        public Books() { }
        public Books(int booksID, string booksName, int authorID, int categoryID)
        {
            BooksID = booksID;
            BooksName = booksName; 
            AuthorID = authorID;
            CategoryID = categoryID;
        }
    }
}
