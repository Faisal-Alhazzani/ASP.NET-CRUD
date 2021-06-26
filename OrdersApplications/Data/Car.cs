using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersApplications.Data
{
    public class Car
    {
        [Key]
        public string Id { get; set; }

        public string name { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public string Image { get; set; }

        public DateTime CreationTime { get; set; }


    }

    
}
