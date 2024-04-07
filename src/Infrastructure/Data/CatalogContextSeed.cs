using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Infrastructure.Data;

public class CatalogContextSeed
{
    public static async Task SeedAsync(CatalogContext catalogContext,
        ILogger logger,
        int retry = 0)
    {
        var retryForAvailability = retry;
        try
        {
            if (catalogContext.Database.IsSqlServer())
            {
                catalogContext.Database.Migrate();
            }

            if (!await catalogContext.CatalogBrands.AnyAsync())
            {
                await catalogContext.CatalogBrands.AddRangeAsync(
                    GetPreconfiguredCatalogBrands());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogTypes.AnyAsync())
            {
                await catalogContext.CatalogTypes.AddRangeAsync(
                    GetPreconfiguredCatalogTypes());

                await catalogContext.SaveChangesAsync();
            }

            if (!await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.CatalogItems.AddRangeAsync(
                    GetPreconfiguredItems());

                await catalogContext.SaveChangesAsync();
            }
            if(!await catalogContext.OrderItems.AnyAsync() && await catalogContext.CatalogItems.AnyAsync())
            {
                await catalogContext.OrderItems.AddRangeAsync(
                    GetPreconfiguredOrderItems(catalogContext.CatalogItems.ToList()));

                await catalogContext.SaveChangesAsync();
            }
            if (!await catalogContext.Orders.AnyAsync() && await catalogContext.OrderItems.AnyAsync())
            {
                await catalogContext.Orders.AddRangeAsync(
                    GetPreconfiguredOrders(catalogContext.OrderItems.ToList()));

                await catalogContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailability >= 10) throw;

            retryForAvailability++;
            
            logger.LogError(ex.Message);
            await SeedAsync(catalogContext, logger, retryForAvailability);
            throw;
        }
    }

    static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
    {
        return new List<CatalogBrand>
            {
                new("Azure"),
                new(".NET"),
                new("Visual Studio"),
                new("SQL Server"),
                new("Other")
            };
    }

    static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
    {
        return new List<CatalogType>
            {
                new("Mug"),
                new("T-Shirt"),
                new("Sheet"),
                new("USB Memory Stick")
            };
    }

    static IEnumerable<CatalogItem> GetPreconfiguredItems()
    {
        return new List<CatalogItem>
            {
                new(2,2, ".NET Bot Black Sweatshirt", ".NET Bot Black Sweatshirt", 19.5M,  "http://catalogbaseurltobereplaced/images/products/1.png"),
                new(1,2, ".NET Black & White Mug", ".NET Black & White Mug", 8.50M, "http://catalogbaseurltobereplaced/images/products/2.png"),
                new(2,5, "Prism White T-Shirt", "Prism White T-Shirt", 12,  "http://catalogbaseurltobereplaced/images/products/3.png"),
                new(2,2, ".NET Foundation Sweatshirt", ".NET Foundation Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/4.png"),
                new(3,5, "Roslyn Red Sheet", "Roslyn Red Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/5.png"),
                new(2,2, ".NET Blue Sweatshirt", ".NET Blue Sweatshirt", 12, "http://catalogbaseurltobereplaced/images/products/6.png"),
                new(2,5, "Roslyn Red T-Shirt", "Roslyn Red T-Shirt",  12, "http://catalogbaseurltobereplaced/images/products/7.png"),
                new(2,5, "Kudu Purple Sweatshirt", "Kudu Purple Sweatshirt", 8.5M, "http://catalogbaseurltobereplaced/images/products/8.png"),
                new(1,5, "Cup<T> White Mug", "Cup<T> White Mug", 12, "http://catalogbaseurltobereplaced/images/products/9.png"),
                new(3,2, ".NET Foundation Sheet", ".NET Foundation Sheet", 12, "http://catalogbaseurltobereplaced/images/products/10.png"),
                new(3,2, "Cup<T> Sheet", "Cup<T> Sheet", 8.5M, "http://catalogbaseurltobereplaced/images/products/11.png"),
                new(2,5, "Prism White TShirt", "Prism White TShirt", 12, "http://catalogbaseurltobereplaced/images/products/12.png")
            };
    }

    static IEnumerable<OrderItem> GetPreconfiguredOrderItems(List<CatalogItem> catalogItems)
    {
        return new List<OrderItem>
        {
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[0].Id,
                catalogItems[0].Name,
                catalogItems[0].PictureUri),
                catalogItems[0].Price,
                2),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[1].Id,
                catalogItems[1].Name,
                catalogItems[1].PictureUri),
                catalogItems[1].Price,
                1),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[2].Id,
                catalogItems[2].Name,
                catalogItems[2].PictureUri),
                catalogItems[2].Price,
                2),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[3].Id,
                catalogItems[3].Name,
                catalogItems[3].PictureUri),
                catalogItems[3].Price,
                1),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[4].Id,
                catalogItems[4].Name,
                catalogItems[4].PictureUri),
                catalogItems[4].Price,
                2),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[5].Id,
                catalogItems[5].Name,
                catalogItems[5].PictureUri),
                catalogItems[5].Price,
                1),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[6].Id,
                catalogItems[6].Name,
                catalogItems[6].PictureUri),
                catalogItems[6].Price,
                2),
            new OrderItem(
                itemOrdered: new CatalogItemOrdered(
                catalogItems[7].Id,
                catalogItems[7].Name,
                catalogItems[7].PictureUri),
                catalogItems[7].Price,
                1)

        };
    }

    static IEnumerable<Order> GetPreconfiguredOrders(List<OrderItem> orderItems)
    {
        return new List<Order>
            {
                new (
                    buyerId: "1", 
                    shipToAddress: new Address("Street1", "City1", "State1", "Country1", "123"),
                    items: orderItems.GetRange(0, 2)
                    ),
                new (
                    buyerId: "2",
                    shipToAddress: new Address("Street1", "City1", "State1", "Country1", "123"),
                    items: orderItems.GetRange(2, 2)
                    ),
                new (
                    buyerId: "3",
                    shipToAddress: new Address("Street1", "City1", "State1", "Country1", "123"),
                    items: orderItems.GetRange(4, 2)
                    ),
                new (
                    buyerId: "4",
                    shipToAddress: new Address("Street1", "City1", "State1", "Country1", "123"),
                    items: orderItems.GetRange(6, 2)
                    )
            };
    }
}
