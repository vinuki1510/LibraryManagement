using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.model
{
    public class Member
    {
        public int MemberID { get; set; }
        public String MemberName { get; set; }
        public Member() { }
        public Member(int memberID, string memberName)
        {
            MemberID = memberID;
            MemberName = memberName;
        }
    }
}
