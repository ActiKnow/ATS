using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
   public class UserInfo : BaseModel
    {
        public UserInfo()
        {
            this.TestAssignments = new List<TestAssignment>();
            this.UserCredentials = new List<UserCredential>();
            this.UserTestHistories = new List<UserTestHistory>();
        }
        [Key]
        public System.Guid UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public decimal Mobile { get; set; }
        public string Email { get; set; }
        public System.Guid RoleTypeId { get; set; }        
        public virtual List<TestAssignment> TestAssignments { get; set; }
        public virtual TypeDef TypeDef { get; set; }
        public virtual List<UserCredential> UserCredentials { get; set; }
        public virtual List<UserTestHistory> UserTestHistories { get; set; }
    }
}
