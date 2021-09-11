using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Store.Data.EF
{
    public class StoreDbContext : DbContext
    {
        public DbSet<BookDto> Books { get; set; }

        public DbSet<OrderDto> Orders { get; set; }

        public DbSet<OrderItemDto> OrderItems { get; set; }

        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildBooks(modelBuilder);
            BuildOrders(modelBuilder);
            BuildOrderItems(modelBuilder);
        }

        private void BuildOrderItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemDto>(action =>
            {
                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasOne(dto => dto.Order)
                      .WithMany(dto => dto.Items)
                      .IsRequired();
            });
        }

        private static void BuildOrders(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>(action =>
            {
                action.Property(dto => dto.CellPhone)
                      .HasMaxLength(20);

                action.Property(dto => dto.DeliveryUniqueCode)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryPrice)
                      .HasColumnType("money");

                action.Property(dto => dto.PaymentServiceName)
                      .HasMaxLength(40);

                action.Property(dto => dto.DeliveryParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);

                action.Property(dto => dto.PaymentParameters)
                      .HasConversion(
                          value => JsonConvert.SerializeObject(value),
                          value => JsonConvert.DeserializeObject<Dictionary<string, string>>(value))
                      .Metadata.SetValueComparer(DictionaryComparer);
            });
        }

        private static readonly ValueComparer DictionaryComparer =
            new ValueComparer<Dictionary<string, string>>(
                (dictionary1, dictionary2) => dictionary1.SequenceEqual(dictionary2),
                dictionary => dictionary.Aggregate(
                    0,
                    (a, p) => HashCode.Combine(HashCode.Combine(a, p.Key.GetHashCode()), p.Value.GetHashCode())
                )
            );

        private static void BuildBooks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookDto>(action =>
            {
                action.Property(dto => dto.Isbn)
                      .HasMaxLength(17)
                      .IsRequired();

                action.Property(dto => dto.Title)
                      .IsRequired();

                action.Property(dto => dto.Price)
                      .HasColumnType("money");

                action.HasData(
                    new BookDto
                    {
                        Id = 1,
                        Isbn = "ISBN0201038013",
                        Author = "D. Knuth",
                        Title = "Art Of Programming, Vol. 1",
                        Description = "This volume begins with basic programming concepts and techniques, then focuses more particularly on information structures-the representation of information inside a computer, the structural relationships between data elements and how to deal with them efficiently.",
                        Price = 7.19m,
                    },
                    new BookDto
                    {
                        Id = 2,
                        Isbn = "ISBN0201485672",
                        Author = "M. Fowler",
                        Title = "Refactoring",
                        Description = "As the application of object technology--particularly the Java programming language--has become commonplace, a new problem has emerged to confront the software development community.",
                        Price = 12.45m,
                    },
                    new BookDto
                    {
                        Id = 3,
                        Isbn = "ISBN1231231232",
                        Author = "A. Freeman",
                        Title = "Pro ASP.NET Core MVC 2",
                        Description = "Now in its 7th edition, the best selling book on MVC is updated for ASP.NET Core MVC 2. It contains detailed explanations of the Core MVC functionality which enables developers to produce leaner, cloud optimized and mobile-ready applications for the .NET platform. This book puts ASP.NET Core MVC into context and dives deep into the tools and techniques required to build modern, cloud optimized extensible web applications. All the new MVC features are described in detail and the author explains how best to apply them to both new and existing projects.The ASP.NET Core MVC Framework is the latest evolution of Microsoft’s ASP.NET web platform, built on a completely new foundation. It represents a fundamental change to how Microsoft constructs and deploys web frameworks and is free of the legacy of earlier technologies such as Web Forms. ASP.NET Core MVC provides a 'host agnostic' framework and a high-productivity programming model that promotes cleaner code architecture, test-driven development, and powerful extensibility.",
                        Price = 52.35m,
                    },
                    new BookDto
                    {
                        Id = 4,
                        Isbn = "ISBN1231231232",
                        Author = "S. Chacon, B. Straub",
                        Title = "Pro Git",
                        Description = "Pro Git (Second Edition) is your fully-updated guide to Git and its usage in the modern world. Git has come a long way since it was first developed by Linus Torvalds for Linux kernel development. It has taken the open source world by storm since its inception in 2005, and this book teaches you how to use it like a pro.",
                        Price = 56.99m,
                    }
                );
            });
        }
    }
}