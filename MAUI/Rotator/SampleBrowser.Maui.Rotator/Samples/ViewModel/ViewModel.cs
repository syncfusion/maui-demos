#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.Rotator.ViewModel
{
    internal class ViewModel : BindableObject
    {
        public ViewModel()
        {
#if WINDOWS || MACCATALYST
            ImageCollection.Add(new Model("bird01.png"));
            ImageCollection.Add(new Model("bird02.png"));
            ImageCollection.Add(new Model("bird03.png"));
            ImageCollection.Add(new Model("bird04.png"));
            ImageCollection.Add(new Model("bird05.png"));
            ImageCollection.Add(new Model("bird06.png"));
            ImageCollection.Add(new Model("bird07.png"));
            ImageCollection.Add(new Model("bird08.png"));
            ImageCollection.Add(new Model("bird17.jpg"));
            ImageCollection.Add(new Model("bird18.jpg"));
            ImageCollection.Add(new Model("bird19.jpg"));
            ImageCollection.Add(new Model("bird20.jpg"));
            ImageCollection.Add(new Model("bird21.jpg"));
            ImageCollection.Add(new Model("bird22.jpg"));
            ImageCollection.Add(new Model("bird23.jpg"));
            ImageCollection.Add(new Model("bird24.jpg"));
            ImageCollection.Add(new Model("bird25.jpg"));
            ImageCollection.Add(new Model("bird26.jpg"));
            ImageCollection.Add(new Model("bird27.jpg"));
            ImageCollection.Add(new Model("bird28.jpg"));
            ImageCollection.Add(new Model("bird29.jpg"));
            ImageCollection.Add(new Model("bird30.jpg"));
            ImageCollection.Add(new Model("bird31.jpg"));
            ImageCollection.Add(new Model("bird32.jpg"));
            ImageCollection.Add(new Model("bird33.jpg"));
            ImageCollection.Add(new Model("bird34.jpg"));
            ImageCollection.Add(new Model("bird35.jpg"));
            ImageCollection.Add(new Model("bird36.jpg"));
#else
            ImageCollection.Add(new Model("bird09.png"));
            ImageCollection.Add(new Model("bird10.png"));
            ImageCollection.Add(new Model("bird11.png"));
            ImageCollection.Add(new Model("bird12.png"));
            ImageCollection.Add(new Model("bird13.png"));
            ImageCollection.Add(new Model("bird14.png"));
            ImageCollection.Add(new Model("bird15.png"));
            ImageCollection.Add(new Model("bird16.png"));
#endif
        }

        private List<Model> imageCollection = new List<Model>();

        public List<Model> ImageCollection
        {
            get { return imageCollection; }
            set { imageCollection = value; }
        }
    }

    public class Model
    {
        public string Image { get; set; }
        public Model(String image)
        {
            Image = image;
        }
    }

    public class StoryViewModel
    {
       
        private ObservableCollection<Model> storyCollection = new ObservableCollection<Model>();

        public ObservableCollection<Model> StoryCollection
        {
            get { return storyCollection; }
            set { storyCollection = value; }
        }

        public StoryViewModel()
        {            
            StoryCollection.Add(new Model("wonders01.png"));
            StoryCollection.Add(new Model("wonders02.png"));
            StoryCollection.Add(new Model("wonders03.png"));
            StoryCollection.Add(new Model("wonders04.png"));
        }
       
    }
}
