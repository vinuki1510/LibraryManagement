using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.model
{
    internal class Category
    {
        public int CategoryID { get; set; }
        public String CategoryName { get; set; }

        public Category() { }
        public Category(int categoryID, string categoryName)
        {
            CategoryID = categoryID;
            CategoryName = categoryName;
        }
    }
}
