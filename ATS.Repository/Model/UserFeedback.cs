using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
   public class UserFeedback : BaseModel
    {       
        [Key]
        public Guid Id { get; set; }
        public string Feedback { get; set; }        
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserInfo UserInfo { get; set; }
        
        public decimal Reating { get; set; }
        public bool ReadStatus { get; set; } = false;
    }
}
