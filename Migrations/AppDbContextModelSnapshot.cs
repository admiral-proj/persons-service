﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonsWebApi.Data;

namespace PersonsWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PersonsWebApi.Data.DateOfBirthEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("DateOfBirth")
                        .IsUnique();

                    b.ToTable("DateOfBirth");
                });

            modelBuilder.Entity("PersonsWebApi.Data.FirstNameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("FirstName")
                        .IsUnique();

                    b.ToTable("FirstNames");
                });

            modelBuilder.Entity("PersonsWebApi.Data.LastNameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("LastName")
                        .IsUnique();

                    b.ToTable("LastNames");
                });

            modelBuilder.Entity("PersonsWebApi.Data.Parent_2_Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ChildId")
                        .HasColumnType("int");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId", "ChildId")
                        .IsUnique();

                    b.ToTable("Parent2Child");

                    b.HasCheckConstraint("ValidParent2Child", "ParentId <> ChildId");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PatronymicEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Patronymic")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("Patronymic")
                        .IsUnique()
                        .HasFilter("[Patronymic] IS NOT NULL");

                    b.ToTable("Patronymics");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("PersonGuid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PersonGuid")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PersonsWebApi.Data.Person_2_PersonalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("PersonalDataId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDataId");

                    b.HasIndex("PersonId", "PersonalDataId")
                        .IsUnique();

                    b.ToTable("Person2PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonalDataEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DateOfBirthId")
                        .HasColumnType("int");

                    b.Property<int>("FirstNameId")
                        .HasColumnType("int");

                    b.Property<int>("LastNameId")
                        .HasColumnType("int");

                    b.Property<int>("PatronymicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DateOfBirthId");

                    b.HasIndex("FirstNameId");

                    b.HasIndex("PatronymicId");

                    b.HasIndex("LastNameId", "FirstNameId", "PatronymicId", "DateOfBirthId");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonsView", b =>
                {
                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonGuid")
                        .HasColumnType("nvarchar(max)");

                    b.ToView("PersonsView");
                });

            modelBuilder.Entity("PersonsWebApi.Data.Person_2_PersonalData", b =>
                {
                    b.HasOne("PersonsWebApi.Data.PersonEntity", "Person")
                        .WithMany("Person_2_PersonalData")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonsWebApi.Data.PersonalDataEntity", "PersonalData")
                        .WithMany("Person2PersonalData")
                        .HasForeignKey("PersonalDataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonalDataEntity", b =>
                {
                    b.HasOne("PersonsWebApi.Data.DateOfBirthEntity", "DateOfBirth")
                        .WithMany("PersonalData")
                        .HasForeignKey("DateOfBirthId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonsWebApi.Data.FirstNameEntity", "FirstName")
                        .WithMany("PersonalData")
                        .HasForeignKey("FirstNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonsWebApi.Data.LastNameEntity", "LastName")
                        .WithMany("PersonalData")
                        .HasForeignKey("LastNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PersonsWebApi.Data.PatronymicEntity", "Patronymic")
                        .WithMany("PersonalData")
                        .HasForeignKey("PatronymicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DateOfBirth");

                    b.Navigation("FirstName");

                    b.Navigation("LastName");

                    b.Navigation("Patronymic");
                });

            modelBuilder.Entity("PersonsWebApi.Data.DateOfBirthEntity", b =>
                {
                    b.Navigation("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.FirstNameEntity", b =>
                {
                    b.Navigation("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.LastNameEntity", b =>
                {
                    b.Navigation("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PatronymicEntity", b =>
                {
                    b.Navigation("PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonEntity", b =>
                {
                    b.Navigation("Person_2_PersonalData");
                });

            modelBuilder.Entity("PersonsWebApi.Data.PersonalDataEntity", b =>
                {
                    b.Navigation("Person2PersonalData");
                });
#pragma warning restore 612, 618
        }
    }
}
