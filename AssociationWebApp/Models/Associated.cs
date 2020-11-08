using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssociationWebApp.Models
{
    public partial class Associated
    {
        public Associated()
        {
            Associations = new HashSet<Association>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome obrigatório!")]
        [Display(Name = "Nome")]
        [Column("name")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O tamanho do nome tem que ser maior que 3 caracteres!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "CPF obrigatório!")]
        [Display(Name = "CPF")]
        [Column("cpf")]
        [StringLength(11)]
        public string Cpf { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Column("birthDate", TypeName = "datetime")]
        public DateTime? BirthDate { get; set; }

        [InverseProperty("Associated")]
        public virtual ICollection<Association> Associations { get; set; } = new List<Association>();
    }
}