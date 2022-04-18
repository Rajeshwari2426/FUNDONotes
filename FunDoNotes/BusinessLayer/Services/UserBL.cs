using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRl userRl;
        public UserBL(IUserRl userRl)
        {
            this.userRl = userRl;
        }
        public UserEntity Register(UserRegistration userRegistration)
        {
            try
            {
                return userRl.Register(userRegistration);

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
               var result=userRl.Login(userLogin);
                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }
                        
    }
}
