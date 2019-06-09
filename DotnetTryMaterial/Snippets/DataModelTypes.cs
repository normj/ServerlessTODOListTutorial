using System;
using System.Collections.Generic;
using System.Text;

namespace Snippets
{
    #region data_model_classes
    public class TODOList
    {
        public string User { get; set; }

        public string ListId { get; set; }

        public string Name { get; set; }

        public List<TODOListItem> Items { get; set; }

        public bool Complete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }

    public class TODOListItem
    {
        public string Description { get; set; }

        public string AssignedEmail { get; set; }

        public bool Complete { get; set; }
    }
    #endregion

}
