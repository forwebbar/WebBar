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
    
    public partial class DeviceDayTotal
    {
        public long id { get; set; }
        public long idDevice { get; set; }
        public int TapCode { get; set; }
        public long DayTotal { get; set; }
        public System.DateTimeOffset Ts { get; set; }
    }
}