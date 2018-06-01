using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private const string sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string basicItem = "Elixir of the Mongoose";

        private IList<Item> items;

        [SetUp]
        public void Setup()
        {
            items = new List<Item>();
        }


        [Test]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedRose app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.AreEqual("foo", Items[0].Name);
        }

        [Test]
        [TestCase(20)]
        [TestCase(2)]
        [TestCase(0)]
        [TestCase(-5)]
        public void Sulfuras_UpdateQuality_QualityRemains(int initSellIn)
        {
            int initialQuality = 80;
            items.Add(new Item { Name = sulfuras, SellIn = initSellIn, Quality = initialQuality });
            GildedRose app = new GildedRose(items);

            app.UpdateQuality();

            Assert.That(items[0].Quality, Is.EqualTo(initialQuality));
        }

        [Test]
        [TestCase(0)]
        [TestCase(15)]
        [TestCase(-5)]
        public void Sulfuras_UpdateQuality_SellInNotDecreased(int initSellIn)
        {
            items.Add(new Item { Name = sulfuras, SellIn = initSellIn, Quality = 80 });
            GildedRose app = new GildedRose(items);

            app.UpdateQuality();

            Assert.That(items[0].SellIn, Is.EqualTo(initSellIn));
        }

        [Test]
        [TestCase(0)]
        [TestCase(15)]
        [TestCase(-5)]
        public void BasicItem_UpdateQuality_SellInDecreasesCorrectly(int initSellIn)
        {
            items.Add(new Item { Name = basicItem, SellIn = initSellIn, Quality = 20 });
            GildedRose app = new GildedRose(items);

            app.UpdateQuality();

            Assert.That(items[0].SellIn, Is.EqualTo(initSellIn - 1));
        }

        [Test]
        [TestCase(10, 15)]
        [TestCase(1, 11)]
        [TestCase(-5, 10)]
        public void BasicItem_UpdateQuality_QualityDecreasesCorrectly(int initSellIn, int expectedQuality)
        {
            int initQuality = 20;
            items.Add(new Item { Name = basicItem, SellIn = initSellIn, Quality = initQuality });
            GildedRose app = new GildedRose(items);

            for (int i = 0; i < 5; i++)
            {
                app.UpdateQuality();
            }

            Assert.That(items[0].Quality, Is.EqualTo(expectedQuality));
        }
    }
}
