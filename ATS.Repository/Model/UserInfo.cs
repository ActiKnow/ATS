using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public int RoleTypeValue { get; set; }        
        public  List<TestAssignment> TestAssignments { get; set; }

        [ForeignKey("RoleTypeValue")]
        public  TypeDef TypeDef { get; set; }
        public  List<UserCredential> UserCredentials { get; set; }
        public  List<UserTestHistory> UserTestHistories { get; set; }
    }
}
