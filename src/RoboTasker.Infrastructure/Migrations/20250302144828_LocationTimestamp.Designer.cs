﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RoboTasker.Infrastructure.Data;

#nullable disable

namespace RoboTasker.Infrastructure.Migrations
{
    [DbContext(typeof(RoboTaskerDbContext))]
    [Migration("20250302144828_LocationTimestamp")]
    partial class LocationTimestamp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "postgis");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.Capability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("capabilities", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.CapabilityGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("capability_groups", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.RobotCapability", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CapabilityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RobotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CapabilityId");

                    b.HasIndex("RobotId");

                    b.ToTable("RobotCapability");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Robot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastActivity")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("robots", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("robot_categories", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCommunication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RobotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RobotId")
                        .IsUnique();

                    b.ToTable("robot_communications", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCustomProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RobotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RobotId", "Name")
                        .IsUnique();

                    b.ToTable("robot_custom_properties", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Latitude")
                        .HasColumnType("double precision");

                    b.Property<double>("Longitude")
                        .HasColumnType("double precision");

                    b.Property<Guid>("RobotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("RobotId")
                        .IsUnique();

                    b.ToTable("RobotLocation");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("robot_category_properties", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotPropertyValue", b =>
                {
                    b.Property<Guid>("RobotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PropertyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("RobotId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("robot_properties_values", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("roles", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.Tenant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("tenants", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("TenantId");

                    b.ToTable("users_roles", (string)null);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Communications.HttpCommunication", b =>
                {
                    b.HasBaseType("RoboTasker.Domain.Robots.RobotCommunication");

                    b.Property<string>("ApiEndpoint")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HttpMethod")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Communications.MqttCommunication", b =>
                {
                    b.HasBaseType("RoboTasker.Domain.Robots.RobotCommunication");

                    b.Property<string>("MqttBrokerAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MqttBrokerPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MqttBrokerUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MqttTopic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Communications.TcpCommunication", b =>
                {
                    b.HasBaseType("RoboTasker.Domain.Robots.RobotCommunication");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Communications.WebSocketCommunication", b =>
                {
                    b.HasBaseType("RoboTasker.Domain.Robots.RobotCommunication");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboTasker.Domain.Tenants.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.Capability", b =>
                {
                    b.HasOne("RoboTasker.Domain.Capabilities.CapabilityGroup", "Group")
                        .WithMany("Capabilities")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.RobotCapability", b =>
                {
                    b.HasOne("RoboTasker.Domain.Capabilities.Capability", "Capability")
                        .WithMany("Robots")
                        .HasForeignKey("CapabilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboTasker.Domain.Robots.Robot", "Robot")
                        .WithMany("Capabilities")
                        .HasForeignKey("RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Capability");

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Robot", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.RobotCategory", "Category")
                        .WithMany("Robots")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCommunication", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.Robot", "Robot")
                        .WithOne("Communication")
                        .HasForeignKey("RoboTasker.Domain.Robots.RobotCommunication", "RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCustomProperty", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.Robot", "Robot")
                        .WithMany("CustomProperties")
                        .HasForeignKey("RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotLocation", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.Robot", "Robot")
                        .WithOne("Location")
                        .HasForeignKey("RoboTasker.Domain.Robots.RobotLocation", "RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotProperty", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.RobotCategory", "Category")
                        .WithMany("Properties")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotPropertyValue", b =>
                {
                    b.HasOne("RoboTasker.Domain.Robots.RobotProperty", "Property")
                        .WithMany("Values")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboTasker.Domain.Robots.Robot", "Robot")
                        .WithMany("Properties")
                        .HasForeignKey("RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Property");

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.Role", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.Tenant", "Tenant")
                        .WithMany("Roles")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.User", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.Tenant", "Tenant")
                        .WithMany("Users")
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.UserRole", b =>
                {
                    b.HasOne("RoboTasker.Domain.Tenants.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboTasker.Domain.Tenants.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RoboTasker.Domain.Tenants.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("Tenant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.Capability", b =>
                {
                    b.Navigation("Robots");
                });

            modelBuilder.Entity("RoboTasker.Domain.Capabilities.CapabilityGroup", b =>
                {
                    b.Navigation("Capabilities");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.Robot", b =>
                {
                    b.Navigation("Capabilities");

                    b.Navigation("Communication")
                        .IsRequired();

                    b.Navigation("CustomProperties");

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotCategory", b =>
                {
                    b.Navigation("Properties");

                    b.Navigation("Robots");
                });

            modelBuilder.Entity("RoboTasker.Domain.Robots.RobotProperty", b =>
                {
                    b.Navigation("Values");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.Tenant", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("RoboTasker.Domain.Tenants.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
