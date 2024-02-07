#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Dispatching;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.DataGrid
{
    public class StockViewModel : INotifyPropertyChanged, IDisposable
    {
        #region Members

        private ObservableCollection<Stock> data;
        private IDispatcherTimer? timer;
        private Random r = new Random(123345345);
        private int noOfUpdates = 500;
        private List<string> stockSymbols = new List<string>();
        private string[] accounts = new string[]
        {
            "American Funds",
            "College Savings",
            "Day Trading",
            "Mountain Range",
            "Fidelity Funds",
            "Mortages",
            "Housing Loans"
        };

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the RenderingDynamicDataViewModel class. 
        /// </summary>
        public StockViewModel()
        {
            this.data = new ObservableCollection<Stock>();
            this.AddRows(200);
            this.ResetRefreshFrequency(2500);
        }

        #endregion

        /// <summary>
        /// Represents the method that will handle the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged"></see> event raised when a property is changed on a component
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        /// <summary>
        /// Gets the stocks.
        /// </summary>
        /// <value>The stocks.</value>
        public ObservableCollection<Stock> Stocks
        {
            get { return this.data; }
        }

        /// <summary>
        /// Gets or sets the value of SelectedItem notifies user when value gets changed
        /// </summary>
        public object SelectedItem
        {
            get
            {
                return this.noOfUpdates;
            }

            set
            {
                this.noOfUpdates = 2;
                this.RaisePropertyChanged("SelectedItem");
            }
        }

        /// <summary>
        /// Gets the value of ComboCollection
        /// </summary>
        public List<int> ComboCollection
        {
            get { return new List<int> { 500, 5000, 50000, 500000 }; }
        }

        #region Timer and updating code

        /// <summary>
        /// Starts the timer.
        /// </summary>
        public void StartTimer()
        {
            timer = Dispatcher.GetForCurrentThread()!.CreateTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Start();
            timer.Tick += Timer_Elapsed;
        }

        private void Timer_Elapsed(object? sender, EventArgs e)
        {
            this.Timer_Tick();
        }

        /// <summary>
        /// Used to reset the refresh frequency
        /// </summary>
        /// <param name="changesPerTick">integer type parameter changesPerTick</param>
        public void ResetRefreshFrequency(int changesPerTick)
        {
            this.noOfUpdates = changesPerTick;
            this.StartTimer();
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        private void Timer_Tick()
        {
            int startTime = DateTime.Now.Millisecond;
            this.noOfUpdates = 100;
            this.ChangeRows(this.noOfUpdates);
        }

        /// <summary>
        /// Adds the rows.
        /// </summary>
        /// <param name="count">The given count.</param>
        private void AddRows(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                var newRec = new Stock();
                newRec.Symbol = this.ChangeSymbol();
                newRec.Account = this.ChangeAccount(i);
                newRec.Open = Math.Round(this.r.NextDouble() * 30, 2);
                newRec.LastTrade = Math.Round(1 + (this.r.NextDouble() * 50));
                double d = this.r.NextDouble();
                if (d < .5)
                {
                    newRec.StockChange = string.Format(" {0:N2}", d);
                }
                else
                {
                    newRec.StockChange = string.Format("-{0:N2}", d);
                }

                newRec.PreviousClose = Math.Round(this.r.NextDouble() * 30, 2);
                newRec.Volume = this.r.Next();
                this.data.Add(newRec);
            }
        }

        /// <summary>
        /// Changes the symbol.
        /// </summary>
        /// <returns>returns builder value</returns>
        private string ChangeSymbol()
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;

            do
            {
                builder = new StringBuilder();
                for (int i = 0; i < 4; i++)
                {
                    ch = Convert.ToChar(Convert.ToInt32(Math.Floor((26 * random.NextDouble()) + 65)));
                    builder.Append(ch);
                }
            }
            while (this.stockSymbols.Contains(builder.ToString()));
            this.stockSymbols.Add(builder.ToString());
            return builder.ToString();
        }

        /// <summary>
        /// Changes the account.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>returns the get calculated value</returns>
        private string ChangeAccount(int index)
        {
            return this.accounts[index % this.accounts.Length];
        }

        /// <summary>
        /// Changes the rows.
        /// </summary>
        /// <param name="count">The count.</param>
        private void ChangeRows(int count)
        {
            if (this.data.Count < count)
            {
                count = this.data.Count;
            }

            for (int i = 0; i < count; ++i)
            {
                int recNo = this.r.Next(this.data.Count);
                Stock recRow = this.data[recNo];

                this.data[recNo].LastTrade = Math.Round(1 + (this.r.NextDouble() * 50));

                double d = this.r.NextDouble();
                if (d < .5)
                {
                    this.data[recNo].StockChange = string.Format(" {0:N2}", d);
                }
                else
                {
                    this.data[recNo].StockChange = string.Format("-{0:N2}", d);
                }

                this.data[recNo].Open = Math.Round(this.r.NextDouble() * 50, 2);
                this.data[recNo].PreviousClose = Math.Round(this.r.NextDouble() * 30, 2);
                this.data[recNo].Volume = this.r.Next();
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        /// <summary>
        /// Triggers when Items Collections Changed.
        /// </summary>
        /// <param name="name">string type of name</param>
        private void RaisePropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer!.Tick -= Timer_Elapsed;
                timer.Stop();
                timer = null;
            }
        }

        #endregion
    }
}
