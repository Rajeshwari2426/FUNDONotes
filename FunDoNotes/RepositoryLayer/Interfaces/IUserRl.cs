using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
   public interface IUserRl
    {
        public UserEntity Register(UserRegistration userRegistration);
        public string Login(UserLogin userLogin);
    }
}
