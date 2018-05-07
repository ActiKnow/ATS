﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
  public  class QuestionOptionModel : BaseModel
    {
        [Key]
        public System.Guid Id { get; set; }
        public string KeyId { get; set; }
        public string Description { get; set; }
     
    }
}