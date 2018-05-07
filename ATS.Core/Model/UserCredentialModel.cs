using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
  public  class UserCredentialModel : BaseModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }
        public Nullable<System.Guid> RoleId { get; set; }
        public string PrevPassword { get; set; }
        public string CurrPassword { get; set; }
        public virtual UserInfoModel UserInfo { get; set; }
        public string EmailId { get; set; }
    }
}
