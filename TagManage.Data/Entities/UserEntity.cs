using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagManage.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
