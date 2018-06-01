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
                { ItemsDefinitions.Sulfuras, UpdateLegendaryItem },
                { ItemsDefinitions.AgedBrie, UpdateAgedBrieItem },
                { ItemsDefinitions.BackstgPass, UpdateBackstagePasses },
                { ItemsDefinitions.ConjuredItem, UpdateConjuredItem }
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

        #region Specific updates

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

            item.Quality = AdjustQualityToUpperLimit(item.Quality);
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

            item.Quality = AdjustQualityToUpperLimit(item.Quality);

            item.SellIn--;

            if (item.SellIn < 0)
            {
                item.Quality = 0;
            }
        }

        private void UpdateConjuredItem(Item item)
        {
            DecreaseItemBy(item, 2);
        }
        
        private void UpdateBasicItem(Item item)
        {
            DecreaseItemBy(item, 1);
        }

        #endregion

        #region Adjustments

        private void DecreaseItemBy(Item item, int count)
        {
            item.Quality -= count;

            item.SellIn -= 1;

            if (item.SellIn < 0)
            {
                item.Quality -= count;
            }

            item.Quality = AdjustQualityToLowerLimit(item.Quality);
        }

        private int AdjustQualityToLowerLimit(int quality)
        {
            return quality < 0 ? 0 : quality;
        }

        private int AdjustQualityToUpperLimit(int quality)
        {
            return quality > 50 ? 50 : quality;
        }

        #endregion
    }
}
