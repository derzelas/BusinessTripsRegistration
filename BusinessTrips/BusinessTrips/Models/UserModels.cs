﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BusinessTrips.Models
{
    public class UserModels
    {

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}