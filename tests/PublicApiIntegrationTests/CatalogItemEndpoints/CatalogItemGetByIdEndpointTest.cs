﻿using Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.eShopWeb.ApplicationCore.Extensions;
using System.Net;
using System.Threading.Tasks;

namespace PublicApiIntegrationTests.CatalogItemEndpoints
{
    [TestClass]
    public class CatalogItemGetByIdEndpointTest
    {
        [TestMethod]
        public async Task ReturnsItemGivenValidId()
        {
            var response = await ProgramTest.NewClient.GetAsync("api/catalog-items/5");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = stringResponse.FromJson<GetByIdCatalogItemResponse>();

            Assert.AreEqual(5, model.CatalogItem.Id);
            Assert.AreEqual("Roslyn Red Sheet", model.CatalogItem.Name);
        }

        [TestMethod]
        public async Task ReturnsNotFoundGivenInvalidId()
        {
            var response = await ProgramTest.NewClient.GetAsync("api/catalog-items/0");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
