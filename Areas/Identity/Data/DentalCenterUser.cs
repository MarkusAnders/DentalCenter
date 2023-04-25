﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DentalCenter.Areas.Identity.Data;

// Add profile data for application users by adding properties to the DentalCenterUser class
public class DentalCenterUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Patronymic { get; set; }
}

