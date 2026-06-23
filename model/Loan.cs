using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Model 
{
    public class Loan
    {
        public int LoanID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Loan(int loanID, int memberID, int bookID, DateTime loanDate, DateTime returnDate)
        {
            LoanID = loanID;
            MemberID = memberID;
            BookID = bookID;
            LoanDate = loanDate;
            ReturnDate = returnDate;
        }
    } 

}
