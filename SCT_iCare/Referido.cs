//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SCT_iCare
{
    using System;
    using System.Collections.Generic;
    
    public partial class Referido
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Referido()
        {
            this.PagosGestores = new HashSet<PagosGestores>();
        }
    
        public int idReferido { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public Nullable<int> Saldo { get; set; }
        public string PrecioNormal { get; set; }
        public string PrecioAereo { get; set; }
        public string PrecioALT { get; set; }
        public string PrecioALTIS { get; set; }
        public string HistorialPrecios { get; set; }
        public string PrecioNormalconIVA { get; set; }
        public string Deuda { get; set; }
        public string Efectivo { get; set; }
        public string Orden { get; set; }
        public string Meta { get; set; }
        public string PrecioAereoPista { get; set; }
        public string PrecioAereoPistaconIVA { get; set; }
        public string PrecioAereosinIVA { get; set; }
        public string Residual { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PagosGestores> PagosGestores { get; set; }
    }
}
