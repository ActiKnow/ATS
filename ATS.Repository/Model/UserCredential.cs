using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
  public  class UserCredential : BaseModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public System.Guid UserId { get; set; }        
        public string PrevPassword { get; set; }
        public string CurrPassword { get; set; }

        [ForeignKey("UserId")]
        public UserInfo UserInfo { get; set; }
        public string EmailId { get; set; }
    }
}
