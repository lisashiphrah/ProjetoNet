﻿using Eventos.IO.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventos.IO.Site.Models
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;
        
        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public Guid GetUserId()
        {
            return IsAuthenticaded() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.NewGuid();
        }

        public bool IsAuthenticaded()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }
    }
}
