using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plum.Model.Model
{
   public class PlumResult
    {
        public PlumResult()
        {
            IsChange = true;
        }
        public bool IsChange { get; set; }
        public string Message { get; set; }
        public object ReturnValue { get; set; }

    }
}
