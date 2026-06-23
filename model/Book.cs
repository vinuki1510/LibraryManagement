using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Model
{
    public class Books
    {
        public int BooksID { get; set; }
        public String BooksName { get; set; }
        public Books() { }
        public Books(int booksID, string booksName)
        {
            BooksID = booksID;
            BooksName = booksName;
        }
    }

}

