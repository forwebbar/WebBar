//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
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
