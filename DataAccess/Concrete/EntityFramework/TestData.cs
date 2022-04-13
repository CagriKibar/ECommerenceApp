using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public static class TestData
    {
        
            public static void Seed()
            {
                var context = new TezContext();

                if (context.Database.GetPendingMigrations().Count() == 0)
                {
                    if (context.Categories.Count() == 0)
                    {
                        context.Categories.AddRange(Categories);
                    }

                    if (context.Products.Count() == 0)
                    {
                        context.Products.AddRange(Products);
                        context.AddRange(ProductCategory);
                    }

                    context.SaveChanges();
                }
            }

            private static Category[] Categories = {
            new Category() { CategoryName="Telefon"},
            new Category() { CategoryName="Bilgisayar"},
            new Category() { CategoryName="Elektronik"}
        };

            private static Product[] Products =
            {
            new Product(){ ProductName="iPhone 13", Price=2000, ImageUrl="urun1.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ ProductName="Samsung S22 Ultra", Price=3000, ImageUrl="urun3.jpg", Description="<p>güzel telefon</p>"},
            new Product(){ ProductName="Macbook Pro", Price=4000, ImageUrl="urun2.jpg", Description="<p>güzel laptop</p>"},
            new Product(){ ProductName="Asus Monitör", Price=5000, ImageUrl="urun4.jpg", Description="<p>360 hz monitor</p>"},

        };


            private static ProductCategory[] ProductCategory =
            {
            new ProductCategory() { Product= Products[0],Category= Categories[0]},
            new ProductCategory() { Product= Products[0],Category= Categories[2]},
            new ProductCategory() { Product= Products[1],Category= Categories[0]},
            new ProductCategory() { Product= Products[1],Category= Categories[1]},

        };
        }
    }

