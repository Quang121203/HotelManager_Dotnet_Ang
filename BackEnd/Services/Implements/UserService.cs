﻿using BackEnd.Models.Domains;
using BackEnd.Models.DTOS;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace BackEnd.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public async Task<object> CreateUser(UserVM model)
        {
            var check = await this.userManager.FindByEmailAsync(model.Email);
            if (check != null)
            {
                return new
                {
                    EC = 1,
                    EM = "Email already exist",
                    DT = "",
                };
            }

            string defaultRole = "User";  // initial default role
            string defaultPassword = "Abc@123";

            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName
            };


            // create role if not exist:
            bool isRoleExist = await this.roleManager.RoleExistsAsync(defaultRole); // take in roleName(string)
            if (!isRoleExist)
            {
                var newRole = new IdentityRole(defaultRole);
                await this.roleManager.CreateAsync(newRole);

            }

            

            // Try to create the user
            var result = await this.userManager.CreateAsync(user, defaultPassword);
            await this.userManager.AddToRoleAsync(user, defaultRole);

     

            return new
            {
                EC = 0,
                EM = "User has been create",
                DT = result,
            };
        }

        public async Task<object> DeleteUser(string id)
        {
            var check = await this.userManager.FindByIdAsync(id);
            if (check != null)
            {
                await this.userManager.DeleteAsync(check);
                return new
                {
                    EC = 0,
                    EM = "User has been delete",
                    DT = ""
                };
            }

            return new
            {
                EC = 1,
                EM = "User not found",
                DT = ""
            };
        }

        public async Task<object> GetAllUser()
        {
            var users = await this.userManager.Users.OrderByDescending(u => u.DateJoined).ToListAsync();
            return new
            {
                EC = 0,
                EM = "",
                DT = users
            };

        }

        public async Task<object> GetUser(string id)
        {
            var user = await this.userManager.FindByIdAsync(id);
            return new
            {
                EC = 0,
                EM = "",
                DT = user
            };
        }

        public async Task<object> UpdateUser(UserVM model)
        {
            var user = await this.userManager.FindByIdAsync(model.ID);

            if (user == null)
            {
                return new
                {
                    EC = 1,
                    EM = "User not found",
                    DT = ""
                };
            }

            user.UserName = model.UserName;
            user.Email = model.Email;

           
            await this.userManager.UpdateAsync(user);

            return new
            {
                EC = 0,
                EM = "User has been update",
                DT = ""
            };

        }
    }
}