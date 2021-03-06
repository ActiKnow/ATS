﻿using ATS.Core.Global;
using ATS.Repository.DAO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Repository.Migrations
{
  public  class ATSDBInitializer : CreateDatabaseIfNotExists<ATSDBContext>
    {
        protected override void Seed(ATSDBContext context)
        {
           
            //Master Type
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Role", Value = (int)CommonType.ROLE, ParentKey = 0, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Level", Value = (int)CommonType.LEVEL, ParentKey = 0, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Category", Value = (int)CommonType.CATEGORY, ParentKey = 0, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Question", Value = (int)CommonType.QUESTION, ParentKey = 0, CreatedBy = "Admin", CreatedDate = DateTime.Now });

            //Question Type
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "DefaultQuestion", Value = (int)CommonType.DEFAULT, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Objective", Value = (int)CommonType.OPTION, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "TrueFalse", Value = (int)CommonType.BOOL, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Subjective", Value = (int)CommonType.TEXT, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            //Role Type
            Guid adminTypeId = Guid.NewGuid();
            context.TypeDef.Add(new Model.TypeDef { TypeId = adminTypeId, Description = "Admin", Value = (int)CommonType.ADMIN , ParentKey = (int)CommonType.ROLE, CreatedBy = "Admin", CreatedDate = DateTime.Now });
            //Insert Admin
            Guid adminId = Guid.NewGuid();
            context.UserInfo.Add(new Model.UserInfo {UserId = adminId, FName="Admin" , LName = "Admin", RoleTypeId = adminTypeId,Email="admin@admin.com", CreatedBy="Admin",CreatedDate=DateTime.Now , StatusId=true});
            context.UserCredential.Add(new Model.UserCredential { UserId = adminId, CurrPassword="admin", EmailId= "admin@admin.com", Id= Guid.NewGuid(), CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = true });
        }

    }
}
