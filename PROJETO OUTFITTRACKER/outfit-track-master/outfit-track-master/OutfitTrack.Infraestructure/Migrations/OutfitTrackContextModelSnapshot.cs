﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OutfitTrack.Infraestructure;

#nullable disable

namespace OutfitTrack.Infraestructure.Migrations
{
    [DbContext(typeof(OutfitTrackContext))]
    partial class OutfitTrackContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Customer", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime?>("BirthDate")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_nascimento");

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_alteracao");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("nome_cidade");

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("complemento");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("VARCHAR(11)")
                        .HasColumnName("cpf")
                        .IsFixedLength();

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_cadastro");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("VARCHAR(256)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("primeiro_nome");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("sobrenome");

                    b.Property<string>("MobilePhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("VARCHAR(13)")
                        .HasColumnName("numero_celular")
                        .IsFixedLength();

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("bairro");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("numero")
                        .IsFixedLength();

                    b.Property<string>("PostalCode")
                        .HasColumnType("longtext");

                    b.Property<string>("Rg")
                        .HasMaxLength(9)
                        .HasColumnType("VARCHAR(9)")
                        .HasColumnName("rg");

                    b.Property<int>("StateAbbreviation")
                        .HasColumnType("INT")
                        .HasColumnName("sigla_estado");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("endereco");

                    b.HasKey("Id");

                    b.ToTable("cliente", (string)null);
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Order", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTime?>("ClosingDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_encerramento");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_cadastro");

                    b.Property<long?>("CustomerId")
                        .IsRequired()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id_cliente");

                    b.Property<long?>("Number")
                        .IsRequired()
                        .HasColumnType("BIGINT")
                        .HasColumnName("numero");

                    b.Property<string>("Observation")
                        .IsRequired()
                        .HasColumnType("VARCHAR(150)")
                        .HasColumnName("observacao");

                    b.Property<int>("Status")
                        .HasColumnType("INT")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("pedido", (string)null);
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.OrderItem", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_cadastro");

                    b.Property<int?>("Item")
                        .IsRequired()
                        .HasColumnType("INT")
                        .HasColumnName("quantidade");

                    b.Property<long?>("OrderId")
                        .IsRequired()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id_pedido");

                    b.Property<long?>("ProductId")
                        .IsRequired()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id_produto");

                    b.Property<int>("Status")
                        .HasColumnType("INT")
                        .HasColumnName("status");

                    b.Property<string>("Variation")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("variação");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("pedido_item", (string)null);
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Product", b =>
                {
                    b.Property<long?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("BIGINT")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long?>("Id"));

                    b.Property<string>("Brand")
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("marca");

                    b.Property<string>("Category")
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("categoria");

                    b.Property<DateTime?>("ChangeDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_alteracao");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("codigo");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("DATETIME")
                        .HasColumnName("data_cadastro");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("descricao");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("DECIMAL(10,2)")
                        .HasColumnName("preco");

                    b.HasKey("Id");

                    b.ToTable("produto", (string)null);
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Order", b =>
                {
                    b.HasOne("OutfitTrack.Domain.Entities.Customer", "Customer")
                        .WithMany("ListOrder")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("OutfitTrack.Domain.Entities.Order", "Order")
                        .WithMany("ListOrderItem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OutfitTrack.Domain.Entities.Product", "Product")
                        .WithMany("ListOrderItem")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Customer", b =>
                {
                    b.Navigation("ListOrder");
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Order", b =>
                {
                    b.Navigation("ListOrderItem");
                });

            modelBuilder.Entity("OutfitTrack.Domain.Entities.Product", b =>
                {
                    b.Navigation("ListOrderItem");
                });
#pragma warning restore 612, 618
        }
    }
}
