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
     
        public System.Guid TypeId { get; set; }
        public string Description { get; set; }      
        public int Value { get; set; }
        public int ParentKey { get; set; }
        public string ParentDescription { get; set; }
        public int? ParentValue { get; set; }
        public string StatusDescription { get; set; }
        public bool IsEditable { get; set; }
        public List<UserInfoModel> UserInfo { get; set; }
    }
}
