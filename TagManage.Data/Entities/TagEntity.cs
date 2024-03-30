using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagManage.Data.Entities
{
    public class TagEntity : BaseEntity
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public bool IsRequired { get; set; }
        public bool IsModeratorOnly { get; set; }
        public bool HasSynonyms { get; set; }

    }
}
