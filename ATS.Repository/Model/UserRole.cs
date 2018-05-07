using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ATS.Core.Model;

namespace ATS.Repository.Model
{
   public class UserRole :BaseModel
    {
        public UserRole()
        {
            this.UserCredentials = new List<UserCredential>();
            this.UserRoleMappings = new List<UserRoleMapping>();
        }
        [Key]
        public System.Guid RoleId { get; set; }
        public string Description { get; set; }
        public virtual List<UserCredential> UserCredentials { get; set; }
        public virtual List<UserRoleMapping> UserRoleMappings { get; set; }
    }
}
