using System;
using System.Collections.Generic;
using System.Text;

namespace Bundle.Model.Entity
{
    public class TransactionHistory
    {
     public Guid Id { get; set; }
     public Guid UserId { get; set; }
     public string TransactionDescription { get; set; }
     public DateTime Date { get; set; }
     public double Amount { get; set; }
     public string TypeOfTransaction { get; set; }
     public bool Issuccessful { get; set; }
    }
}
