using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.Models
{
    public class Cliente
    {

        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
