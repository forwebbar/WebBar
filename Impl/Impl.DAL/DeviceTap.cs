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
    
    public partial class DeviceTap
    {
        public int id { get; set; }
        public long idDevice { get; set; }
        public Nullable<int> idDrink { get; set; }
        public int TapCode { get; set; }
        public Nullable<int> idFutureDrink { get; set; }
        public Nullable<System.DateTimeOffset> FutureDrinkDate { get; set; }
    }
}