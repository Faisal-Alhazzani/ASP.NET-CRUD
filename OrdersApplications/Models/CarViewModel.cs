using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrdersApplications.Models
{
    public class CarViewModel
    {
        public CarViewModel()
        {
            Guid guid = Guid.NewGuid();
            this.Id = guid.ToString().Substring(0, 5);
            this.CreationTime = DateTime.Now;
        }

        public string Id { get; set; }
        [Required]
        public string name { get; set; }

        public string Brand { get; set; }
        [Range(10, 2000)]
        public double Price { get; set; }
        public string Image { get; set; }

        public DateTime CreationTime { get; set; }

        public IFormFile Pic { get; set; }

        public string type { get; set; }

    }
}
