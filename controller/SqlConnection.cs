using System.Data;

namespace LibraryManagement.controller
{
    internal class SqlConnection
    {
        public SqlConnection(string connectionString)
        {
        }

        public ConnectionState State { get; internal set; }

        internal void Close()
        {
            throw new NotImplementedException();
        }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}