﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Mvc.Server.Core;
using Mvc.Server.Exceptions;
using MvcServer.Entities;

namespace Mvc.Server.Infrastructure.Mvc
{
    public abstract class BaseController : Controller
    {
        protected UserManager<ApplicationUser> UserManager { get; }

        protected BaseController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        private async Task<ApplicationUser> GetCurrentUser()
        {
            var userId = User.FindFirst(ApplicationConstants.UserIdClaim).Value;
            var user = (await UserManager.FindByIdAsync(userId));
            if (user == null)
            {
                throw new BadRequestException("No user found");
            }

            return user;
        }

        protected async Task<ApplicationUser> CurrentUser() => await GetCurrentUser();
    }
}