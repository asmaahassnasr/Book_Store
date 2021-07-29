using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50,MinimumLength =10)]
        public string FullName { get; set; }

    }
}
