//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Impl.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Fill
    {
        public long id { get; set; }
        public long idDevice { get; set; }
        public int TapCode { get; set; }
        public int Volume { get; set; }
        public System.DateTimeOffset Ts { get; set; }
        public int OperationCode { get; set; }
        public long Total { get; set; }
    }
}
