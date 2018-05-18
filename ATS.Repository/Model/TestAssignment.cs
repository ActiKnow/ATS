﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [DefaultValue(0)]
        public decimal MarksObtained { get; set; } = 0;

        [ForeignKey("TestBankId")]
        public TestBank TestBank { get; set; }

        [ForeignKey("UserId")]
        public UserInfo UserInfo { get; set; }
    }
}
