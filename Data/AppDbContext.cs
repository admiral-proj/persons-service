using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsWebApi.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LastNameEntity>().HasIndex(e => e.LastName).IsUnique();             // Ставим уникальный индекс на фамилии
            modelBuilder.Entity<FirstNameEntity>().HasIndex(e => e.FirstName).IsUnique();           // Ставим уникальный индекс на имена
            modelBuilder.Entity<PatronymicEntity>().HasIndex(e => e.Patronymic).IsUnique();         // Ставим уникальный индекс на отчества
            modelBuilder.Entity<DateOfBirthEntity>().HasIndex(e => e.DateOfBirth).IsUnique();       // Ставим уникальный индекс на даты рождения

            modelBuilder.Entity<PersonalDataEntity>().HasIndex(e => new { e.LastNameId,             // Ставим уникальный индекс на установочные данные
                e.FirstNameId, e.PatronymicId, e.DateOfBirthId });

            modelBuilder.Entity<PersonEntity>().HasIndex(e => e.PersonGuid).IsUnique();             // Ставим уникальный индекс на лица

            modelBuilder.Entity<Person_2_PersonalData>().HasIndex(e => new { e.PersonId,            // Ставим уникальный индекс на связи с установочными данными
                e.PersonalDataId }).IsUnique();                                                     // (конкретное лицо может быть связано с конкретными установочными данными только один раз)

            modelBuilder.Entity<Parent_2_Child>().HasCheckConstraint("ValidParent2Child",           // Добавляем ограничение на связи Родитель-Ребёнок
                "ParentId <> ChildId");                                                             // (Лицо не может быть связано с самим собой)

            modelBuilder.Entity<Parent_2_Child>().HasIndex(e => new { e.ParentId, e.ChildId })      // Ставим уникальный индекс на связи Родитель-Ребёнок
                .IsUnique();                                                                        // (Лицо не может быть связано с одним родителем или одним ребёнком более одного раза)

            modelBuilder.Entity<PersonsView>(pc => { pc.HasNoKey(); pc.ToView("PersonsView"); });

            //base.OnModelCreating(modelBuilder);
        }

        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<LastNameEntity> LastNames { get; set; }

        public DbSet<FirstNameEntity> FirstNames { get; set; }

        public DbSet<PatronymicEntity> Patronymics { get; set; }

        public DbSet<DateOfBirthEntity> DateOfBirth { get; set; }

        public DbSet<PersonalDataEntity> PersonalData { get; set; }

        public DbSet<PersonEntity> Persons { get; set; }

        public DbSet<Person_2_PersonalData> Person2PersonalData { get; set; }

        public DbSet<Parent_2_Child> Parent2Child { get; set; }

        public DbSet<PersonsView> Persons_View { get; set; }
    }
}
