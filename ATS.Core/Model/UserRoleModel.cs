using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ATS.Core.Model
{
   public class UserRoleModel :BaseModel
    {
        public UserRoleModel()
        {
            this.UserCredentials = new List<UserCredentialModel>();
            this.UserRoleMappings = new List<UserRoleMapModel>();
        }
        [Key]
        public System.Guid RoleId { get; set; }
        public string Description { get; set; }
        public virtual List<UserCredentialModel> UserCredentials { get; set; }
        public virtual List<UserRoleMapModel> UserRoleMappings { get; set; }
    }
}
