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
                { "Backstage passes to a TAFKAL80ETC concert", UpdateBackstagePasses },
                { "Conjured Mana Cake", UpdateConjuredItem }
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

        private void UpdateLegendaryItem(Item item)
        {
            // Legendary item currently stays in the same state during the update
        }

        private void UpdateAgedBrieItem(Item item)
        {
            item.SellIn--;

            item.Quality++;

            if (item.SellIn < 0)
            {
                item.Quality++;
            }

            item.Quality = AdjustQualityToLimit(item.Quality);
        }
                
        private void UpdateBackstagePasses(Item item)
        {
            item.Quality++;

            if (item.SellIn < 11)
            {
                item.Quality++;
            }

            if (item.SellIn < 6)
            {
                item.Quality++;
            }

            item.Quality = AdjustQualityToLimit(item.Quality);

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private int AdjustQualityToLimit(int quality)
        {
            return quality > 50 ? 50 : quality;
        }

        private void UpdateConjuredItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality -= 2;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality -= 2;
            }
        }

        private void UpdateBasicItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality--;
            }

            item.SellIn -= 1;

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality--;
            }
        }
    }
}
