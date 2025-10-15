#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace SampleBrowser.Maui.ListView.SfListView
{
    public class ListViewItemReorderingViewModel : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<SongInfo>? songInfo;

        #endregion

        #region Properties

        public ObservableCollection<SongInfo>? SongInfo
        {
            get { return songInfo; }
            set { this.songInfo = value; OnPropertyChanged("SongInfo"); }
        }

        #endregion

        #region Constructor

        public ListViewItemReorderingViewModel()
        {
            SongInfo = new ObservableCollection<SongInfo>();
            for (int i = 0; i < SongTitles.Length; i++)
            {
                var info = new SongInfo()
                {
                    SongTitle = SongTitles[i],
                    SingerName = SingerNames[i],
                    TrackImage = TrackImages[i],
                };
                SongInfo.Add(info);
            }
        }

        #endregion

        #region Collections

        string[] SongTitles = new string[]
        {
            "Driver’s license",
            "Wellerman",
            "The Business",
            "Montero (Call Me By Your Name)",
            "Save your tears",
            "Levitating",
            "Friday",
            "The Joker and the Queen",
            "Without You",
            "Good 4 U",
            "Blinding Lights",
            "Head & Heart",
            "Bed",
            "Don't Play",
            "Latest Trends",
            "Peaches",
            "Little bit of Love",
            "Goosebumps",
            "Your Love ( 9 PM)",
            "Paradise",
            "Kiss me more",
            "Get out my head",
            "Calling my phone",
            "Let's go home together",
            "Sweet Melody",
            "Heat Waves",
            "Astronaut in the Ocean",
            "Mood",
            "Streets",
            "Anyone",
            "My head & my heart",
            "Déja Vu",
            "Someone you loved",
            "34+35",
            "Dance Monkey",
            "Afterglow",
            "Watermelon sugar",
            "Whoopty",
            "You broke me first",
            "Rapstar",
        };

        string[] SingerNames = new string[]
        {
            "Olivia Rodrigo",
            "Nathan Evans",
            "Tiesto",
            "Lil Nas X",
            "Weeknd",
            "Dua Lipa",
            "Riton/Nightcrawlers",
            "Ed Sheeran",
            "The Kid Laroi",
            "Olivia Rodrigo",
            "Weeknd",
            "Joel Corry ft Mnek",
            "Joel Corry ft Mnek",
            "Ann-Marie/Ksi/Digital Farm",
            "A1 & J1",
            "Justin Bieber/Caesar/Giveon",
            "Tom Grennan",
            "Travis Scott & HVME",
            "ATB/Topic/A7S",
            "Meduza ft Dermot Kennedy",
            "Doja Cat ft SZA",
            "Shane Codd",
            "Lil Tjay & 6LACK",
            "Ella Henderson & Tom Grennan",
            "Little Mix",
            "Glass Animals",
            "Masked Wolf",
            "24kGoldn ft Iann Dior",
            "Doja Cat",
            "Justin Bieber",
            "Ava Max",
            "Olivia Rodrigo",
            "Lewis Capaldi",
            "Ariana Grande",
            "Tones & I",
            "Ed Sheeran",
            "Harry Styles",
            "CJ",
            "Tate McRae",
            "Polo G",
        };

        string[] TrackImages = new string[]
        {
            "driverslicense.png",
            "wellerman.png",
            "thebusiness.png",
            "montero.png",
            "saveyourtears.png",
            "levitating.png",
            "friday.png",
            "thejokerandthequeen.png",
            "withoutyou.png",
            "good4u.png",
            "blindinglights.png",
            "headheart.png",
            "bed.png",
            "don'tplay.png",
            "latesttrends.png",
            "peaches.png",
            "littlebitoflove.png",
            "goosebumps.png",
            "yourlove.png",
            "paradise.png",
            "kissmemore.png",
            "getoutmyhead.png",
            "callingmyphone.png",
            "let'sgohometogether.png",
            "sweetmelody.png",
            "heatwaves.png",
            "atronautintheocaen.png",
            "mood.png",
            "streets.png",
            "anyone.png",
            "myheadmyheart.png",
            "dejavu.png",
            "someoneyouloved.png",
            "3435.png",
            "dancemonkey.png",
            "afterglow.png",
            "watermelonsugar.png",
            "whoopty.png",
            "youbrokemefirst.png",
            "rapstar.png",
        };

        #endregion

        #region Interface Member

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
