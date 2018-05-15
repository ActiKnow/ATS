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
            this.UserTestHistories = new List<UserTestHistoryModel>();
            this.UserCredentials = new UserCredentialModel();
        }

        [Key]
        public System.Guid UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public decimal Mobile { get; set; }
        public string Email { get; set; }
        public int RoleTypeValue { get; set; }
        public string RoleDescription { get; set; }
        public  List<TestAssignmentModel> TestAssignments { get; set; }
        public  TypeDefModel TypeDef { get; set; }
        public UserCredentialModel UserCredentials { get; set; }
        public  List<UserTestHistoryModel> UserTestHistories { get; set; }
        public string CurrPassword { get; set; }
    }
}
