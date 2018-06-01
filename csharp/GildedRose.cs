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

                if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        Items[i].Quality = Items[i].Quality - 1;
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                Items[i].SellIn = Items[i].SellIn - 1;

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (Items[i].Quality > 0)
                        {
                            Items[i].Quality = Items[i].Quality - 1;
                        }
                    }
                    else
                    {
                        Items[i].Quality = Items[i].Quality - Items[i].Quality;
                    }
                }
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
