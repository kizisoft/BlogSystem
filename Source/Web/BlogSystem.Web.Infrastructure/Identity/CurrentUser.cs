﻿namespace BlogSystem.Web.Infrastructure.Identity
 {
     using System.Security.Principal;

     using BlogSystem.Data;
     using BlogSystem.Data.Models;

     using Microsoft.AspNet.Identity;

     public class CurrentUser : ICurrentUser
     {
         private readonly ApplicationDbContext currentDbContext;
         private readonly IIdentity currentIdentity;

         private ApplicationUser user;

         public CurrentUser(IIdentity identity, ApplicationDbContext context)
         {
             this.currentDbContext = context;
             this.currentIdentity = identity;
         }

         public ApplicationUser Get()
         {
             return this.user ?? (this.user = this.currentDbContext.Users.Find(this.currentIdentity.GetUserId()));
         }
     }
 }