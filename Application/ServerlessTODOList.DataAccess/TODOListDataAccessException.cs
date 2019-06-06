using System;
using System.Collections.Generic;
using System.Text;

namespace ServerlessTODOList.DataAccess
{
    public class TODOListDataAccessException : Exception
    {
        public TODOListDataAccessException(string message) : base(message) { }

        public TODOListDataAccessException(string message, Exception innerException) : base(message, innerException) { }
    }
}
