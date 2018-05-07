using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
   public class TestAssignment : BaseModel
    {
        [Key]
        public System.Guid ID { get; set; }
        public System.Guid UserId { get; set; }
        public System.Guid TestBankId { get; set; }
        public virtual TestBank TestBank { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
