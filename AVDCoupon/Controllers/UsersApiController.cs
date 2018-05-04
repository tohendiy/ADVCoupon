﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using Microsoft.AspNetCore.Identity;
using AVDCoupon.Models;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/users")]
    public class UsersApiController : Controller
    {
        private UserManager<ApplicationUser> _userManager;

        public UsersApiController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ApplicationUser> GetUserData(string id)
        {
            var user = await _userManager.Users.Where(item => item.Id == id).FirstOrDefaultAsync();
            return user;
        }

        // POST api/values
        [HttpPost]
        public async Task<bool> Post([FromBody]ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            var user = await _userManager.Users.Where(item => item.Id == id).FirstOrDefaultAsync();
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }


    }
}
