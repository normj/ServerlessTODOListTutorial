using System;
using System.Collections.Generic;
using System.Text;

namespace ServerlessTODOList.Common
{
    public class TODOListItem
    {
        public string Description { get; set; }

        public string AssignedEmail { get; set; }

        public bool Complete { get; set; }

    }
}
