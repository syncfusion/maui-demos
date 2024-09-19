#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Inputs.Samples.NumericUpDown.NumericUpDownGettingStarted.Model
{
    public class ProductInfoRepository
    {
        #region Constructor

        public ProductInfoRepository()
        {

        }

        #endregion

        #region Properties

        internal ObservableCollection<ProductInfo> GetProductInfo()
        {
            var productInfo = new ObservableCollection<ProductInfo>();
            for (int i = 0; i < ProductNames.Length; i++)
            {
                var info = new ProductInfo()
                {
                    ProductName = ProductNames[i],
                    ProductDescription = ProductDescriptions[i],
                    ProductImage = ProductImages[i],
                    Price = ProductPrices[i],
                    Count = Counts[i],
                };
                productInfo.Add(info);
            }
            return productInfo;
        }

        #endregion

        #region ProductInfo

        readonly string[] ProductNames = new string[]
        {    
            "Cherry (Imported)",
            "Orange",           
            "Strawberry",
            "Peach",
            "Pineapple",
            "Raspberry",
            "Grapes",           
            "Blueberry"
        };
        readonly string[] ProductImages = new string[]
        {         
            "Cherry.jpg",
            "Orange.jpg",          
            "Strawberry.jpg",
            "Peach.jpg",
            "Pineapple.jpg",
            "Raspberry.jpg",
            "Grapes.jpg",
            "Blueberry.jpg"
        };
        readonly string[] ProductDescriptions = new string[]
        {
          "Savor the luscious and irresistible allure of imported cherries. Indulge in their vibrant colors, tantalizing sweetness, and delicate texture as you embark on a sensory journey like no other. Delight your tongue with their juicy burst of flavor. Order now and embark on a journey of pure indulgence with our premium imported cherries.",
          "The orange is a popular fruit known for its bright color, refreshing taste, and high nutritional value. It belongs to the citrus family and is scientifically called Citrus sinensis. Oranges are widely cultivated in many parts of the world and are consumed in various forms, including fresh, juiced, or used in cooking and baking.",
          "Elevate your culinary creations and indulge in our irresistible, premium strawberries. Perfect for adding a touch of elegance to your summer desserts and cocktails, or simply enjoyed as a refreshing snack, these red jewels of nature are a must-have for berry lovers. Order now and experience the sumptuous delight of our fresh and flavorful strawberries. Let their juicy sweetness captivate your senses and bring a burst of natural indulgence to your everyday moments.",
          "Delve into the mouthwatering world of peaches. Peaches are a true embodiment of summertime bliss, offering an exquisite sensory experience that will transport you to sun-kissed orchards and warm, lazy afternoons. Order now and indulge in the captivating taste of our premium peaches, bringing a slice of summer to your table.",
          "Pineapples have a distinctive appearance with a rough, spiky, and cylindrical outer skin. The skin is typically green or yellow when ripe, and the fruit's flesh is a vibrant yellow color. Pineapples have a sweet and tart taste, which can vary depending on the variety and level of ripeness. The fruit contains a combination of sugars, including fructose and glucose, which contribute to its sweetness.",
          "Raspberries are small berries with a deep red color. They are made up of numerous small drupelets, which are individual sections that make up the fruit. Raspberries can also be found in other colors such as black, purple, yellow, and gold. Raspberries have a sweet and slightly tangy taste with a hint of acidity. The flavor can vary depending on the variety and level of ripeness. Ripe raspberries are juicy and fragrant.",
          "Grapes are small, round or oval-shaped fruits that grow in clusters on woody vines of the genus Vitis. They are one of the world’s oldest cultivated fruits and have been enjoyed by humans for thousands of years. Grapes are a nutrient-dense fruit. They are a good source of vitamins C and K, as well as antioxidants and various minerals.",
          "Blueberries are small, round berries that belong to the genus Vaccinium. They are known for their deep blue or purple color and have a sweet and tangy flavor. Blueberries are native to North America but are now cultivated in many regions around the world. They are often regarded as a superfood due to their high nutritional content. They are low in calories but rich in vitamins C and K. Blueberries are also an excellent source of dietary fiber and antioxidants."
        };

        readonly double[] ProductPrices = new double[]
        {
            0.49,
            0.99,
            0.19,
            0.09,
            0.49,
            0.99,
            0.19,
            0.09,

        };

        public int[] Counts = new int[]
     {
            2,
            0,
            4,
            1,
            0,
            0,
            5,
            3,
     };
        #endregion
    }
}
