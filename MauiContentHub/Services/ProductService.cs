using MauiContentHub.Models;
using MauiContentHub.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiContentHub.Services
{
    public static class ProductService
    {
        public static async Task<List<HomeProduct>> GetHomes()
        {
            List<HomeProduct> homes = new List<HomeProduct>();

            var entities = await ReadEntity.GetEntitiesByDefinition("M.PCM.Product");
            foreach (var entity in entities.Items)
            {
                homes.Add(new HomeProduct()
                {
                    //ProductName = entity.GetPropertyValue<String>("ProductName"),
                    Address = entity.GetPropertyValue<String>("Address"),
                    SqFt = entity.GetPropertyValue<Decimal>("SqFt"),
                    //ProductNumber = entity.GetPropertyValue<String>("ProductNumber"),
                    //City = entity.GetPropertyValue<String>("City"),
                    NoOfBedrooms = entity.GetPropertyValue<Int32>("NoOfBedrooms"),
                    NoOfBathrooms = entity.GetPropertyValue<String>("NoOfBathrooms"),
                    //BuildYear = entity.GetPropertyValue<Int32>("BuildYear"),
                    //LotSize = entity.GetPropertyValue<Decimal>("LotSize"),
                    //SftPrice = entity.GetPropertyValue<Decimal>("SftPrice"),
                    //Construction = entity.GetPropertyValue<String>("Construction"),
                    //Roof = entity.GetPropertyValue<String>("Roof"),
                    //Exterior = entity.GetPropertyValue<String>("Exterior"),
                    //Stories = entity.GetPropertyValue<Int32>("Stories"),
                    //DiningRoom = entity.GetPropertyValue<String>("DiningRoom"),
                    //FamilyRoom = entity.GetPropertyValue<String>("FamilyRoom"),
                    //Garage = entity.GetPropertyValue<Int32>("Garage"),
                    Price = entity.GetPropertyValue<Decimal>("Price"),
                    MasterImageURL = await ReadEntity.GetAssetURLByEntityId(entity.GetRelation("PCMProductToMasterAsset")?.Properties?.Keys?.First()),
                    ProductFamily = await ReadEntity.GetProductFamilyNameById(entity.GetRelation("PCMProductFamilyToProduct")?.Properties?.Keys?.First())
                });
            }

            return homes;
        }
    }
}
