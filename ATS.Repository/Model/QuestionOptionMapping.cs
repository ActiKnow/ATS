using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
   public class QuestionOptionMapping
    {
        [Key]
        public Guid Id { get; set; }
        public System.Guid QId { get; set; }
        public string OptionKeyId { get; set; }
        public string Answer { get; set; }

        [ForeignKey("QId")]
        public virtual QuestionBank QuestionBank { get; set; }

        
    }
}
