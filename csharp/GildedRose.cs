using System;
using System.Collections.Generic;

namespace csharp
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == "Sulfuras, Hand of Ragnaros")
                {
                    UpdateLegendaryItem(Items[i]);
                    continue;
                }
                else if (Items[i].Name == "Aged Brie")
                {
                    UpdateAgedBrieItem(Items[i]);
                    continue;
                }
                else if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                {
                    UpdateBackstagePasses(Items[i]);
                    continue;
                }
                else
                {
                    UpdateBasicItem(Items[i]);
                    continue;
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
