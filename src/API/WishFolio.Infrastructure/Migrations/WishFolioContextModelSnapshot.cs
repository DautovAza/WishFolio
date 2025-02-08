﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WishFolio.Infrastructure.Dal.Write;

#nullable disable

namespace WishFolio.Infrastructure.Migrations
{
    [DbContext(typeof(WishFolioContext))]
    partial class WishFolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.Friends.Friendship", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FriendId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "FriendId");

                    b.ToTable("Friendships", (string)null);
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.Notifications.Notification", b =>
                {
                    b.Property<Guid>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsRead")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("NotificationId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.WishListAgregate.WishList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<int>("Visibility")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Wishlists", (string)null);
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.WishListAgregate.WishlistItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("character varying(60)");

                    b.Property<int>("ReservationStatus")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ReservationUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WishListId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WishListId");

                    b.ToTable("WishlistItems", (string)null);
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.Friends.Friendship", b =>
                {
                    b.HasOne("WishFolio.Domain.Entities.UserAgregate.User", null)
                        .WithMany("Friends")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.Notifications.Notification", b =>
                {
                    b.HasOne("WishFolio.Domain.Entities.UserAgregate.User", null)
                        .WithMany("Notifications")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.User", b =>
                {
                    b.OwnsOne("WishFolio.Domain.Entities.UserAgregate.Profile.UserProfile", "Profile", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Age")
                                .HasColumnType("integer")
                                .HasColumnName("Age");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WishFolio.Domain.Entities.UserAgregate.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("Profile")
                        .IsRequired();
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.WishListAgregate.WishlistItem", b =>
                {
                    b.HasOne("WishFolio.Domain.Entities.WishListAgregate.WishList", null)
                        .WithMany("Items")
                        .HasForeignKey("WishListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("WishFolio.Domain.Entities.WishListAgregate.ValueObjects.WishItemLink", "Link", b1 =>
                        {
                            b1.Property<Guid>("WishlistItemId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Uri")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Uri");

                            b1.HasKey("WishlistItemId");

                            b1.ToTable("WishlistItems");

                            b1.WithOwner()
                                .HasForeignKey("WishlistItemId");
                        });

                    b.Navigation("Link");
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.UserAgregate.User", b =>
                {
                    b.Navigation("Friends");

                    b.Navigation("Notifications");
                });

            modelBuilder.Entity("WishFolio.Domain.Entities.WishListAgregate.WishList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
