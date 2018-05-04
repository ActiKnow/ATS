using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
  public  class TypeDef :BaseModel
    {
        public TypeDef()
        {
            this.UserInfoes = new List<UserInfo>();
        }
        [Key]
        public System.Guid TypeId { get; set; }
        public string Description { get; set; }
        public string TypeKey { get; set; }
        public string TypeValue { get; set; }
        public string ParentTypeId { get; set; }
        public virtual List<UserInfo> UserInfoes { get; set; }
    }
}
