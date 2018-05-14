using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [Index(IsUnique = true)]
        public System.Guid TypeId { get; set; }
        public string Description { get; set; }    

        [Key]
        public int Value { get; set; }

        public int ParentKey { get; set; }
        public List<UserInfo> UserInfo { get; set; }
        public List<QuestionBank> QuestBank { get; set; }
    }
}
