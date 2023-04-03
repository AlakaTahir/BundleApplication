using System;
using System.Collections.Generic;
using System.Text;

namespace Bundle.Project.Entity
{
    public class UserBalance
    {
     public Guid Id { get; set; }   
     public Guid UserId { get; set; }
     public double Balance { get; set; }
     public DateTime? CreatedDate { get; set; }
     public DateTime? UpadatedDate { get; set; }
    }
}
