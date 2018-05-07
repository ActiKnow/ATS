using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class UserRoleMapModel : BaseModel
    {
        [Key]
        public System.Guid ID { get; set; }
        public System.Guid USERID { get; set; }
        public System.Guid ROLEID { get; set; }
        public virtual UserInfoModel UserInfo { get; set; }
        public virtual UserRoleModel UserRole { get; set; }
    }
}
