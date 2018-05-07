using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
  public  class QuestionOption : BaseModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public string KeyId { get; set; }
        public string Description { get; set; }
     
    }
}
