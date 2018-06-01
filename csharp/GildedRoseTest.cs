using NUnit.Framework;
using System.Collections.Generic;

namespace csharp
{
    [TestFixture]
    public class GildedRoseTest
    {
        private const string sulfuras = "Sulfuras, Hand of Ragnaros";

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
        public void Sulfuras_UpdateQuality_QualityRemains()
        {
            int initialQuality = 80;
            items.Add(new Item { Name = sulfuras, SellIn = 0, Quality = initialQuality });
            GildedRose app = new GildedRose(items);

            app.UpdateQuality();

            Assert.That(items[0].Quality, Is.EqualTo(initialQuality));
        }

        [Test]
        [TestCase(0)]
        [TestCase(15)]
        [TestCase(-5)]
        public void Sulfuras_UpdateQuality_SellInDoesNotDecreased(int initSellIn)
        {
            items.Add(new Item { Name = sulfuras, SellIn = initSellIn, Quality = 80 });
            GildedRose app = new GildedRose(items);

            app.UpdateQuality();

            Assert.That(items[0].SellIn, Is.EqualTo(initSellIn));
        }
    }
}
