//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DPBSv11.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TB_CATEGORIA
    {
        public int COD_CATEGORIA { get; set; }
        [Display(Name = "Categoria")]
        public string CATEGORIA { get; set; }
        [Display(Name = "Servi�o")]
        public Nullable<int> COD_SERVICO { get; set; }

        public virtual TB_SERVICO TB_SERVICO { get; set; }
    }
}
