﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class QuestionOptionMapping
    {
        [Key]
        public Guid Id { get; set; }
        public System.Guid QId { get; set; }
        public string OptionKeyId { get; set; }
        public string Answer { get; set; }
        public virtual QuestionBank QuestionBank { get; set; }
    }
}
