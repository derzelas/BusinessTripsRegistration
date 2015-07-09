﻿using System;
using BusinessTrips.DAL.Model;

namespace BusinessTrips.DAL.Entity
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }
        public bool IsConfirmed { get; set; }

        public UserModel ToUserModel()
        {
            return new UserModel
            {
                Name = Name,
                Email = Email,
                IsConfirmed = IsConfirmed,
                Id = Id,
                Password = Password,
            };
        }
    }
}