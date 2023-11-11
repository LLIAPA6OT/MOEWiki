﻿// <auto-generated />
using System;
using MOEWiki.DBMySQL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MOEWiki.DBMySQL.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20231205175602_add-guil-skill2")]
    partial class addguilskill2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.AutoParse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AdditionalString1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Mode")
                        .HasColumnType("int");

                    b.Property<string>("ParseResult")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ParseState")
                        .HasColumnType("int");

                    b.Property<string>("RecognitionResult")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AutoParses");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Call", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Calls");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Synonyms")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Synonyms");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.GuildSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaxLevel")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<int>("Skill")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GuildSkills");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.GuildSkillEffect", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GuildSkillId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Mask")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("GuildSkillEffects");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.GuildSkillRelation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GuildSkillId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PrevGuildSkillId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GuildSkillRelations");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.GuildSkillRequire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CopperCoins")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("GuildActivity")
                        .HasColumnType("int");

                    b.Property<int>("GuildLevel")
                        .HasColumnType("int");

                    b.Property<int>("GuildSkillId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("PrevLevel")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GuildSkillRequires");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("PropertiesLastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecipesLastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ResearchId")
                        .HasColumnType("int");

                    b.Property<int>("SubcategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .HasFilter("[IsDelete] = 0");

                    b.HasIndex("ResearchId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessLvl")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HTMLValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyType")
                        .HasColumnType("int");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PropertyId");

                    b.ToTable("ItemProperties");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemPropertyWithNonActual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessLvl")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("HTMLValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("ItemPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ItemPropertiesWithNonActual");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemRecipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsStepByStep")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RecipeItemId")
                        .HasColumnType("int");

                    b.Property<string>("RecipeItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemRecipes");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemRecipeWithNonActual", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("ItemRecipeId")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("RecipeItemId")
                        .HasColumnType("int");

                    b.Property<string>("RecipeItemName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("ItemRecipesWithNonActual");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MapMarker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Coalition")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Enemies")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LinkName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("MapId")
                        .HasColumnType("int");

                    b.Property<int?>("MarkerGroupId")
                        .HasColumnType("int");

                    b.Property<int>("MarkerType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MarkerGroupId");

                    b.ToTable("MapMarkers");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MapMarkerToMarkerGroupRel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("MapMarkerId")
                        .HasColumnType("int");

                    b.Property<int>("MarkerGroupId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialInfo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MapMarkerId");

                    b.HasIndex("MarkerGroupId");

                    b.ToTable("MapMarkerToMarkerGroupRels");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MarkerGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("MarkerGroups");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Perk", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ComprehensionPoint")
                        .HasColumnType("int");

                    b.Property<int>("CopperCoins")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("PreviousId")
                        .HasColumnType("int");

                    b.Property<string>("PreviousName")
                        .HasColumnType("longtext");

                    b.Property<int>("RequiredItemId")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<int>("Skill")
                        .HasColumnType("int");

                    b.Property<int>("SkillLevel")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Perks");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccessLvl")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsDependsByQuality")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsHide")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("PropertyType")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Research", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CopperCoins")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GuildActivity")
                        .HasColumnType("int");

                    b.Property<int>("GuildLevel")
                        .HasColumnType("int");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("PreviousId")
                        .HasColumnType("int");

                    b.Property<string>("PreviousName")
                        .HasColumnType("longtext");

                    b.Property<int>("RecipePoint")
                        .HasColumnType("int");

                    b.Property<int?>("RequiredItemCount")
                        .HasColumnType("int");

                    b.Property<int?>("RequiredItemId")
                        .HasColumnType("int");

                    b.Property<string>("RequiredItemName")
                        .HasColumnType("longtext");

                    b.Property<int>("ResearchBranch")
                        .HasColumnType("int");

                    b.Property<int>("Row")
                        .HasColumnType("int");

                    b.Property<int>("SortId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.ToTable("Researches");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Synonyms")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("Synonyms");

                    b.ToTable("Subcategories");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Item", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.Research", "Research")
                        .WithMany("Items")
                        .HasForeignKey("ResearchId");

                    b.HasOne("MOEWiki.DBMySQL.Models.Subcategory", "Subcategory")
                        .WithMany("Items")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Research");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemProperty", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.Item", "Item")
                        .WithMany("ItemProperties")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MOEWiki.DBMySQL.Models.Property", "Property")
                        .WithMany("ItemProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Property");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.ItemRecipe", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.Item", "Item")
                        .WithMany("ItemRecipes")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MapMarker", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.MarkerGroup", null)
                        .WithMany("MapMarkers")
                        .HasForeignKey("MarkerGroupId");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MapMarkerToMarkerGroupRel", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.MapMarker", "MapMarker")
                        .WithMany()
                        .HasForeignKey("MapMarkerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MOEWiki.DBMySQL.Models.MarkerGroup", "MarkerGroup")
                        .WithMany()
                        .HasForeignKey("MarkerGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MapMarker");

                    b.Navigation("MarkerGroup");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Subcategory", b =>
                {
                    b.HasOne("MOEWiki.DBMySQL.Models.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Category", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Item", b =>
                {
                    b.Navigation("ItemProperties");

                    b.Navigation("ItemRecipes");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.MarkerGroup", b =>
                {
                    b.Navigation("MapMarkers");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Property", b =>
                {
                    b.Navigation("ItemProperties");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Research", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("MOEWiki.DBMySQL.Models.Subcategory", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
