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
        [Column("cnpj")]
        [StringLength(14)]
        public string Cnpj { get; set; }

        [InverseProperty("Company")]
        public virtual ICollection<Association> Associations { get; set; } = new List<Association>();
    }
}