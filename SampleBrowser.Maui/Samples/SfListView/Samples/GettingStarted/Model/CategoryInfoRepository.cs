using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.SfListView
{
    public class ShoppingCategoryInfoRepository
    {
        #region Constructor

        public ShoppingCategoryInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<ListViewShoppingCategoryInfo> GetCategoryInfo()
        {
            var categoryInfo = new ObservableCollection<ListViewShoppingCategoryInfo>();
            for (int i = 0; i < CategoryNames.Count(); i++)
            {
                var info = new ListViewShoppingCategoryInfo()
                {
                    CategoryName = CategoryNames[i],
                    CategoryDescription = CategoryDescriptions[i],
                    CategoryImage =  CategoryImages[i]
                };
                categoryInfo.Add(info);
            }
            return categoryInfo;
        }

        #endregion

        #region CategoryInfo

        string[] CategoryNames = new string[]
        {
            "Fashion",
            "Electronics",
            "Home & Kitchen",
            "Sports",
            "Kids",
            "Books",
            "Footware",
            "Mobile Phones & Accesories",
            "FlowerGiftCakes",
            "Watches",
            "Jewelery",
            "Food",
            "Perfumes",
            "Movies & Music",
            "Cameras & Accessories"
        };

        string[] CategoryImages = new string[]
        {
            "shopping.jpg",
            "electronics.jpg",
            "diningtable.jpg",
            "sports_health.jpg",
            "naughtyboy.jpg",
            "novels.jpg",
            "graycanvas.jpg",
            "mobile.jpg",
            "flowergiftcakes.jpg",
            "watches.jpg",
            "jewellery.jpg",
            "food.jpg",
            "perfumes.jpg",
            "brownguitar.jpg",
            "cameras.png"
        };

        string[] CategoryDescriptions = new string[]
        {
            "Latest fashion trends in online shopping for branded shoes, clothing, dresses, handbags, watches and more for men and women.",
            "Shop from a wide range of electronics like mobiles, laptops, tablets, cameras, TVs, LEDs, music systems, and much more.",
            "Purchase home and kitchen accessories like cookware, home cleaning, furniture, dining, etc.",
            "Buy sports equipment and accessories for badminton, cricket, football, swimming, , tennis, gym, volleyball, hockey, etc. at the lowest price.",
            "Shop for kids clothes, footwear, books, accessories, computer games and toys for boys, girls, and infants.",
            "Purchase books online from millions of book titles across various categories at low prices. Read books online and download as PDF.",
            "Buy footwear for men, women, and kids from a collection of formal shoes, slippers, casual shoes, sandals, and more at the best price.",
            "Buy branded mobile phones, SmartPhones, tablets, and mobile accessories like bluetooth, headsets, memorycards, charger, covers, etc",
            "Buy different flowers, gifts, and cakes online for birthdays, anniversariess, mother’s day,, etc",
            "Latest range of trendy branded, Digital & Analog watches, Digital Steel Watches, Digital LED Watches for men and women.",
            "Buy jewelry for men, women, and children from brands like Gitanjali, Tara, Orra, Sia Art Jewelry, Addons, Ayesha, Peora. etc.",
            "Shop from a wide range of best quality fruits, vegetables, health food, grocery, , , dry and frozen foods, etc.",
            "Choose the best perfumes from brands like Azzaro, Davidoff, CK, Axes, Good Morning, Hugo Boss, Jaguar, Calvin Klein, Antonio etc.",
            "Buy a wide variety of movies in different languages and music in different formats like audio CD, DVD, MP3, etc.",
            "Buy cameras and accessories at best prices. Choose cameras from popular brands like Nikon, Sony, Canon and more accessories."
        };

        #endregion
    }
}
