using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class UserRoleMapping : BaseModel
    {
        [Key]
        public System.Guid ID { get; set; }
        public System.Guid USERID { get; set; }
        public System.Guid ROLEID { get; set; }
        public virtual UserInfo UserInfo { get; set; }
        public virtual UserRole UserRole { get; set; }
    }
}
