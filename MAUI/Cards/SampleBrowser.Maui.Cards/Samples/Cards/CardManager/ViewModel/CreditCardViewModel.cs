#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.ComponentModel;

namespace SampleBrowser.Maui.Cards.SfCards
{
    public class TransactionDetails : INotifyPropertyChanged
    {
        private string firstTransaction = string.Empty;
        private string secondTransaction = string.Empty;
        private string thirdTransaction = string.Empty;
        private string firstTransactionDate = string.Empty;
        private string secondTransactionDate = string.Empty;
        private string thirdTransactionDate = string.Empty;
        private string firstTransactionAmount = string.Empty;
        private string secondTransactionAmount = string.Empty;
        private string thirdTransactionAmount = string.Empty;
        private string dueAmount = string.Empty;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string FirstTransaction
        {
            get
            {
                return firstTransaction;
            }
            set
            {
                firstTransaction = value;
                RaisePropertyChanged("FirstTransaction");
            }
        }

        public string SecondTransaction
        {
            get
            {
                return secondTransaction;
            }
            set
            {
                secondTransaction = value;
                RaisePropertyChanged("SecondTransaction");
            }
        }

        public string ThirdTransaction
        {
            get
            {
                return thirdTransaction;
            }
            set
            {
                thirdTransaction = value;
                RaisePropertyChanged("ThirdTransaction");
            }
        }

        public string FirstTransactionDate
        {
            get
            {
                return firstTransactionDate;
            }
            set
            {
                firstTransactionDate = value;
                RaisePropertyChanged("FirstTransactionDate");
            }
        }

        public string SecondTransactionDate
        {
            get
            {
                return secondTransactionDate;
            }
            set
            {
                secondTransactionDate = value;
                RaisePropertyChanged("SecondTransactionDate");
            }
        }

        public string ThirdTransactionDate
        {
            get
            {
                return thirdTransactionDate;
            }
            set
            {
                thirdTransactionDate = value;
                RaisePropertyChanged("ThirdTransactionDate");
            }
        }

        public string FirstTransactionAmount
        {
            get
            {
                return firstTransactionAmount;
            }
            set
            {
                firstTransactionAmount = value;
                RaisePropertyChanged("FirstTransactionAmount");
            }
        }

        public string SecondTransactionAmount
        {
            get
            {
                return secondTransactionAmount;
            }
            set
            {
                secondTransactionAmount = value;
                RaisePropertyChanged("SecondTransactionAmount");
            }
        }

        public string ThirdTransactionAmount
        {
            get
            {
                return thirdTransactionAmount;
            }
            set
            {
                thirdTransactionAmount = value;
                RaisePropertyChanged("ThirdTransactionAmount");
            }
        }

        public string DueAmount
        {
            get
            {
                return dueAmount;
            }
            set
            {
                dueAmount = value;
                RaisePropertyChanged("DueAmount");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class CreditCardViewModel : INotifyPropertyChanged
    {

        private TransactionDetails firstCardTransaction;

        private TransactionDetails secondCardTransaction;

        private TransactionDetails thirdCardTransaction;

        public TransactionDetails FirstCardTransaction
        {
            get
            {
                return firstCardTransaction;
            }
            set
            {
                firstCardTransaction = value;
                RaisePropertyChanged("FirstCardTransaction");
            }
        }

        public TransactionDetails SecondCardTransaction
        {
            get
            {
                return secondCardTransaction;
            }
            set
            {
                secondCardTransaction = value;
                RaisePropertyChanged("SecondCardTransaction");
            }
        }

        public TransactionDetails ThirdCardTransaction
        {
            get
            {
                return thirdCardTransaction;
            }
            set
            {
                thirdCardTransaction = value;
                RaisePropertyChanged("ThirdCardTransaction");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public CreditCardViewModel()
        {
            this.firstCardTransaction = new TransactionDetails()
            {
                FirstTransaction = "Shopping",
                FirstTransactionDate = $"{DateTime.Now.AddDays(-5).ToString("dd")} {DateTime.Now.AddDays(-5).ToString("MMMM")}, 10:10 AM",
                FirstTransactionAmount = "$140",
                SecondTransaction = "Food",
                SecondTransactionDate = $"{DateTime.Now.AddDays(-10).ToString("dd")} {DateTime.Now.AddDays(-10).ToString("MMMM")}, 5:06 PM",
                SecondTransactionAmount = "$100",
                ThirdTransaction = "Entertainment",
                ThirdTransactionDate = $"{DateTime.Now.AddDays(-15).ToString("dd")} {DateTime.Now.AddDays(-15).ToString("MMMM")}, 8:30 AM",
                ThirdTransactionAmount = "$80",
                DueAmount = "$320"
            };

            this.secondCardTransaction = new TransactionDetails()
            {
                FirstTransaction = "Travel",
                FirstTransactionDate = $"{DateTime.Now.AddDays(-3).ToString("dd")} {DateTime.Now.AddDays(-3).ToString("MMMM")}, 2:10 AM",
                FirstTransactionAmount = "$120",
                SecondTransaction = "Shopping",
                SecondTransactionDate = $"{DateTime.Now.AddDays(-6).ToString("dd")} {DateTime.Now.AddDays(-6).ToString("MMMM")}, 7:06 PM",
                SecondTransactionAmount = "$50",
                ThirdTransaction = "Rent",
                ThirdTransactionDate = $"{DateTime.Now.AddDays(-9).ToString("dd")} {DateTime.Now.AddDays(-9).ToString("MMMM")}, 3:30 PM",
                ThirdTransactionAmount = "$200",
                DueAmount = "$370"
            };

            this.thirdCardTransaction = new TransactionDetails()
            {
                FirstTransaction = "Shopping",
                FirstTransactionDate = $"{DateTime.Now.AddDays(-2).ToString("dd")} {DateTime.Now.AddDays(-2).ToString("MMMM")}, 10:10 AM",
                FirstTransactionAmount = "$100",
                SecondTransaction = "Food",
                SecondTransactionDate = $"{DateTime.Now.AddDays(-4).ToString("dd")} {DateTime.Now.AddDays(-4).ToString("MMMM")}, 5:06 PM",
                SecondTransactionAmount = "$80",
                ThirdTransaction = "Entertaiment",
                ThirdTransactionDate = $"{DateTime.Now.AddDays(-7).ToString("dd")} {DateTime.Now.AddDays(-7).ToString("MMMM")}, 8:30 AM",
                ThirdTransactionAmount = "$80",
                DueAmount = "$260"
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
