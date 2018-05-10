using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class UserInfoModel : BaseModel
    {
        public UserInfoModel()
        {
            this.TestAssignments = new List<TestAssignmentModel>();
            this.UserCredentials = new List<UserCredentialModel>();
            this.UserTestHistories = new List<UserTestHistoryModel>();
        }
        [Key]
        public System.Guid UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public decimal Mobile { get; set; }
        public string Email { get; set; }
        public System.Guid RoleTypeId { get; set; }
        public string RoleDescription { get; set; }
        public int RoleValue { get; set; }
        public System.Guid? UserTypeId { get; set; }
        public string UserTypeDescription { get; set; }
        public string UserTypeValue { get; set; }
        public virtual List<TestAssignmentModel> TestAssignments { get; set; }
        public virtual TypeDefModel TypeDef { get; set; }
        public virtual List<UserCredentialModel> UserCredentials { get; set; }
        public virtual List<UserTestHistoryModel> UserTestHistories { get; set; }
    }
}
