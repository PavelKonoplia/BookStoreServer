﻿using BookStore.DAL;
using BookStore.Entity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;

namespace BookStore.BLL
{
    public class IdentityRoleManager: RoleManager<CustomRole, long>, IDisposable
    {
        public IdentityRoleManager(IRoleStore<CustomRole, long>  store)
           : base(store)
        { }
    }
}
