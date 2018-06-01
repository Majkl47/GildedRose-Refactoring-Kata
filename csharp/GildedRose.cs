using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        Dictionary<string, Action<Item>> availableItemsMapping;

        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
            availableItemsMapping = new Dictionary<string, Action<Item>>()
            {
                { "Sulfuras, Hand of Ragnaros", UpdateLegendaryItem },
                { "Aged Brie", UpdateAgedBrieItem },
                { "Backstage passes to a TAFKAL80ETC concert", UpdateBackstagePasses }
            };
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (availableItemsMapping.ContainsKey(Items[i].Name))
                {
                    availableItemsMapping[Items[i].Name](Items[i]);
                }
                else
                {
                    UpdateBasicItem(Items[i]);
                }
            }
        }

        private void UpdateBasicItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
            }

            DecreaseSellIn(item);

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality--;
            }
        }

        private void UpdateBackstagePasses(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;

                if (item.SellIn < 11 && item.Quality < 50)
                {
                    item.Quality++;
                }

                if (item.SellIn < 6 && item.Quality < 50)
                {
                    item.Quality++;
                }
            }

            DecreaseSellIn(item);

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private void UpdateAgedBrieItem(Item item)
        {
            DecreaseSellIn(item);

            if (item.Quality < 50)
            {
                item.Quality++;
            }

            if (item.SellIn < 0 && item.Quality < 50)
            {
                item.Quality++;
            }
        }

        private void DecreaseSellIn(Item item)
        {
            item.SellIn -= 1;
        }

        private void UpdateLegendaryItem(Item item)
        {
            // Legendary item currently stays in the same state during the update
        }
    }
}
