﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundle.Model.Entity
{
    public class UserInfo
    {
     public Guid Id { get; set; } //primar key
     public string Name { get; set; }
     public string PhoneNumber { get; set; }
     public string Email { get; set; }
     public DateTime? Createddate { get; set; }
     public DateTime? UpdatedDate { get; set; }
    }
}
