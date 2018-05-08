﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
  public  class TypeDef :BaseModel
    {
        public TypeDef()
        {
            this.UserInfo = new List<UserInfo>();
        }
        [Key]
        public System.Guid TypeId { get; set; }
        public string Description { get; set; }      
        public string Value { get; set; }
        public Nullable<System.Guid> ParentKey { get; set; }
        public virtual List<UserInfo> UserInfo { get; set; }
    }
}
