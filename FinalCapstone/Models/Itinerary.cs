using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Models
{
    public class Itinerary
    {
        public IList<ItineraryItem> Items { get; } = new List<ItineraryItem>();

        public void AddToItinerary(Landmark a)
        {
            ItineraryItem itineraryItem = Items.FirstOrDefault(i => i.Landmark.Code == a.Code);

            if (itineraryItem == null)
            {
                itineraryItem = new ItineraryItem()
                {
                    Landmark = a,
                };
                Items.Add(itineraryItem);
            }
        }

        public void RemoveOne(Landmark landmark)
        {
            var item = Items.FirstOrDefault(i => i.Landmark.Code == landmark.Code);

            if (item == null)
            {
                return;
            }
            else
            {
                Items.Remove(item);
                return;
            }
        }
    }
}

