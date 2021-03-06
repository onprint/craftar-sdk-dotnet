﻿using System;
using System.IO;
using CraftAR.SDK.Dotnet.CraftARClasses;
using NUnit.Framework;

namespace CraftAR.SDK.Dotnet.IntegrationTests
{
    [TestFixture()]
    public class CraftARClientTests
    {
        private CraftARConfiguration Configuration
        {
            get
            {
                return new CraftARConfiguration()
                {
                    APIKey = "Your API Key here",
                    DefaultCollectionId = "Your default collection Id Here",
                    HostModify = @"https://my.craftar.net/api/v0",
                    HostSearch = @"https://search.craftar.net/v1"
                };
            }
        }

        [Test()]
        public void CreateItemTest()
        {
            var client = new CraftARClient(Configuration);
            var item = client.CreateItem(new ItemRequest()
            {
                name = "Test Create Item"
            });

            Assert.IsTrue(!String.IsNullOrWhiteSpace(item.uuid));

            client.DeleteItem(item.uuid);
        }

        [Test()]
        public void DeleteItemTest()
        {
            var client = new CraftARClient(Configuration);
            var item = client.CreateItem(new ItemRequest()
            {
                name = "Test Delete Item"
            });

            Assert.IsTrue(client.DeleteItem(item.uuid));
        }

        [Test()]
        public void CreateImageTest()
        {
            var client = new CraftARClient(Configuration);
            string imagePath = @"..\Debug\cloud.jpeg";
            var item = client.CreateItem(new ItemRequest()
            {
                name = "Test Create Image"
            });
            var image = client.CreateImage(new ImageRequest()
            {
                content = File.ReadAllBytes(imagePath),
                filename = "cloud.jpeg",
                itemId = item.uuid
            });
            
            Assert.IsTrue(!String.IsNullOrWhiteSpace(image.uuid));

            client.DeleteItem(item.uuid);
        }

        [Test()]
        public void DeleteImageTest()
        {
            var client = new CraftARClient(Configuration);
            string imagePath = @"..\Debug\water.png";
            var item = client.CreateItem(new ItemRequest()
            {
                name = "Test Delete Image"
            });
            var image = client.CreateImage(new ImageRequest()
            {
                content = File.ReadAllBytes(imagePath),
                filename = "water.png",
                itemId = item.uuid
            });
            
            Assert.IsTrue(client.DeleteImage(image.uuid));

            client.DeleteItem(item.uuid);
        }
    }
}
