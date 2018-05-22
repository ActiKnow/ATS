﻿using ATS.Core.Global;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATS.Repository.Repo;

namespace ATS.Repository.Migrations
{
  public  class ATSDBInitializer : CreateDatabaseIfNotExists<ATSDBContext>
    {
        protected override void Seed(ATSDBContext context)
        {
            // Status Type
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Status", Value = (int)CommonType.STATUS, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Active", Value = (int)CommonType.ACTIVE, ParentKey = (int)CommonType.STATUS, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "InActive", Value = (int)CommonType.INACTIVE, ParentKey = (int)CommonType.STATUS, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Deleted", Value = (int)CommonType.DELETED, ParentKey = (int)CommonType.STATUS, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            //Master Type
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Role", Value = (int)CommonType.ROLE, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Level", Value = (int)CommonType.LEVEL, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Category", Value = (int)CommonType.CATEGORY, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Question", Value = (int)CommonType.QUESTION, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Test Type", Value = (int)CommonType.TESTTYPE, ParentKey = (int)CommonType.PARENT, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable=false });
            //Question Type
            //context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "DefaultQuestion", Value = (int)CommonType.DEFAULT, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = true,IsEditable=false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Objective", Value = (int)CommonType.OPTION, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "TrueFalse", Value = (int)CommonType.BOOL, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            context.TypeDef.Add(new Model.TypeDef { TypeId = Guid.NewGuid(), Description = "Subjective", Value = (int)CommonType.TEXT, ParentKey = (int)CommonType.QUESTION, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE, IsEditable = false });
            //Role Type
            Guid adminTypeId = Guid.NewGuid();
            int adminTypeValue= (int)CommonType.ADMIN;
            context.TypeDef.Add(new Model.TypeDef { TypeId = adminTypeId, Description = "Admin", Value = adminTypeValue, ParentKey = (int)CommonType.ROLE, CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE });
            //Insert Admin
            Guid adminId = Guid.NewGuid();
            context.UserInfo.Add(new Model.UserInfo {UserId = adminId, FName="Admin" , LName = "Admin", RoleTypeValue = adminTypeValue, Email="admin@admin.com", CreatedBy="Admin",CreatedDate=DateTime.Now , StatusId= (int)CommonType.ACTIVE });
            context.UserCredential.Add(new Model.UserCredential { UserId = adminId, CurrPassword="admin", EmailId= "admin@admin.com", Id= Guid.NewGuid(), CreatedBy = "Admin", CreatedDate = DateTime.Now, StatusId = (int)CommonType.ACTIVE });
        }

    }
}
