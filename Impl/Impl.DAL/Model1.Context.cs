﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BeerControlEntities : DbContext
    {
        public BeerControlEntities()
            : base("name=BeerControlEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Device> Device { get; set; }
        public virtual DbSet<DeviceDayTotal> DeviceDayTotal { get; set; }
        public virtual DbSet<DeviceTap> DeviceTap { get; set; }
        public virtual DbSet<DeviceVurVals> DeviceVurVals { get; set; }
        public virtual DbSet<Drink> Drink { get; set; }
        public virtual DbSet<Fill> Fill { get; set; }
        public virtual DbSet<Market> Market { get; set; }
        public virtual DbSet<MarketDrink> MarketDrink { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Producer> Producer { get; set; }
        public virtual DbSet<Sell> Sell { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
