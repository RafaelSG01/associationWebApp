using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssociationWebApp.Models
{
    public partial class Company
    {
        public Company()
        {
            Associations = new HashSet<Association>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome obrigatório!")]
        [Display(Name = "Nome")]
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "CNPJ obrigatório!")]
        [Display(Name = "CNPJ")]
        [RegularExpression(@"/^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$/", ErrorMessage = "O código postal deverá estar no formato 00000000 ou 00000-000")]
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Association> Associations { get; set; } = new List<Association>();
    }
}