//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdministracionMinisterio.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ministro
    {
        public int Id { get; set; }
        public int IdFuncionario { get; set; }
        public System.DateTime FechaDeIngreso { get; set; }
        public System.DateTime FechaDeEgreso { get; set; }
        public Nullable<int> MinistroAnterior { get; set; }
    
        public virtual Funcionario Funcionario { get; set; }
    }
}