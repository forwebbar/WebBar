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
    
    public partial class Price
    {
        public int id { get; set; }
        public int idDrink { get; set; }
        public int Val { get; set; }
        public System.DateTimeOffset StartTs { get; set; }
        public Nullable<System.DateTimeOffset> EndTs { get; set; }
        public Nullable<int> idMarket { get; set; }
    }
}