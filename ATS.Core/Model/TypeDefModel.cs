using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
  public  class TypeDefModel :BaseModel
    {
        public TypeDefModel()
        {
            this.UserInfo = new List<UserInfoModel>();
        }
        [Key]
        public System.Guid TypeId { get; set; }
        public string Description { get; set; }      
        public string Value { get; set; }
        public Nullable<System.Guid> ParentKey { get; set; }
        public string ParentDescription { get; set; }
        public string ParentValue { get; set; }
        public string StatusDescription { get; set; }
        public virtual List<UserInfoModel> UserInfo { get; set; }
    }
}
