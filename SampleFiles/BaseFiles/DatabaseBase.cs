using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class FILL_IN_TITLE
    {
        [ForeignKey("ParentElement")]
        [Key, Column(Order = 0)]
        public int ParentElementId { get; set; }
        [Key, Column(Order = 1)]
        public FILL_IN_ENUM_TITLE Value { get; set; }

        public virtual ParentElement ParentElement { get; set; }
    }
}
