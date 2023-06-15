using System;
using System.Collections.Generic;
using System.Text;

namespace Bundle.Model.Entity
{
    public class UserBalance
    {
     public Guid Id { get; set; }   //primary key
     public Guid UserId { get; set; } //foreign key
     public double Balance { get; set; }
     public DateTime? CreatedDate { get; set; }
     public DateTime? UpadatedDate { get; set; }
    }
}
