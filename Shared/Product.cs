using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace Mysql_blazor2.Shared
{
    public class Product
    {
        
        public int Id { get; set; }
        [Required]
        public string  nome { get; set; }
        [Required]
        public string descricao { get; set; }
        [Required]
        public Decimal preco { get; set; }
    }
}