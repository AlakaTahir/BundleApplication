using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bundle.Model.ViewModel
{
    public class BaseResponseViewModel
    {
     public bool Status { get; set; }
     public string Message { get; set; }
     public string Note { get; set; }
    }
}
