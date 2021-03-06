﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssociationWebApp.Models
{
    public partial class Association
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("associatedId")]
        [Display(Name = "Associado")]
        public int AssociatedId { get; set; }
        [Column("companyId")]
        [Display(Name = "Empresa")]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(AssociatedId))]
        [InverseProperty("Associations")]
        public virtual Associated Associated { get; set; }
        [ForeignKey(nameof(CompanyId))]
        [InverseProperty("Associations")]
        public virtual Company Company { get; set; }
    }
}