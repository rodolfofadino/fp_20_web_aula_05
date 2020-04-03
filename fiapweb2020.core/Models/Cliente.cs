using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace fiapweb2020.core.Models
{

    //[Table("NomeDaTabelaDeClientes")]
    public class Cliente
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Cep { get; set; }
        [Required]
        public string Nome { get; set; }
        public int Idade { get; set; }
        
        //public Status Status { get; set; }
    }

    //public class Status
    //{
    //    [Required]
    //    public string Descricao { get; set; }
    //}
}
