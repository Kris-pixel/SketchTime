﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SketchTime
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SKETCH_TTIMEEntities : DbContext
    {
        public SKETCH_TTIMEEntities()
            : base("name=SKETCH_TTIMEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ANIMALS> ANIMALS { get; set; }
        public virtual DbSet<CONFIGURATION> CONFIGURATION { get; set; }
        public virtual DbSet<IMG_FILES> IMG_FILES { get; set; }
        public virtual DbSet<PARTS_OF_THE_BODY> PARTS_OF_THE_BODY { get; set; }
        public virtual DbSet<PEOPLE> PEOPLE { get; set; }
        public virtual DbSet<POINTS_OF_VIEW> POINTS_OF_VIEW { get; set; }
        public virtual DbSet<POSTURES> POSTURES { get; set; }
        public virtual DbSet<THINGS> THINGS { get; set; }
    }
}