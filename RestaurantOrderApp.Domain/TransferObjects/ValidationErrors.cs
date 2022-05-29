using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantOrderApp.Domain.TransferObjects
{
    public class ValidationErrors
    {
        public ValidationErrors(string field, string message)
        {
            this.field = field;
            this.message = message;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string field { get; set; }
        public string message { get; set; }
    }
}
