#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
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
        private string fourthTransaction = string.Empty;
        private string firstTransactionType = string.Empty;
        private string secondTransactionType = string.Empty;
        private string thirdTransactionType = string.Empty;
        private string fourthTransactionType = string.Empty;
        private string firstTransactionAmount = string.Empty;
        private string secondTransactionAmount = string.Empty;
        private string thirdTransactionAmount = string.Empty;
        private string fourthTransactionAmount = string.Empty;
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

        public string FourthTransaction
        {
            get
            {
                return fourthTransaction;
            }
            set
            {
                fourthTransaction = value;
                RaisePropertyChanged("FourthTransaction");
            }
        }

        public string FirstTransactionType
        {
            get
            {
                return firstTransactionType;
            }
            set
            {
                firstTransactionType = value;
                RaisePropertyChanged("FirstTransactionDate");
            }
        }

        public string SecondTransactionType
        {
            get
            {
                return secondTransactionType;
            }
            set
            {
                secondTransactionType = value;
                RaisePropertyChanged("SecondTransactionDate");
            }
        }

        public string ThirdTransactionType
        {
            get
            {
                return thirdTransactionType;
            }
            set
            {
                thirdTransactionType = value;
                RaisePropertyChanged("ThirdTransactionDate");
            }
        }

        public string FourthTransactionType
        {
            get
            {
                return fourthTransactionType;
            }
            set
            {
                fourthTransactionType = value;
                RaisePropertyChanged("FourthTransactionDate");
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

        public string FourthTransactionAmount
        {
            get
            {
                return fourthTransactionAmount;
            }
            set
            {
                fourthTransactionAmount = value;
                RaisePropertyChanged("FourthTransactionAmount");
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
                FirstTransaction = "Andrie",
                FirstTransactionType = "Food order",
                FirstTransactionAmount = "+ $100",
                SecondTransaction = "Wade Warren",
                SecondTransactionType = "Cashback",
                SecondTransactionAmount = "+ $20",
                ThirdTransaction = "Jane Cooper",
                ThirdTransactionType = "Shopping",
                ThirdTransactionAmount = "+ $100",
                FourthTransaction = "Guy Hawkins",
                FourthTransactionType = "Subscription",
                FourthTransactionAmount = "+ $100",
                DueAmount = "$300"
            };

            this.secondCardTransaction = new TransactionDetails()
            {
                FirstTransaction = "Andrie",
                FirstTransactionType = "Shopping",
                FirstTransactionAmount = "+ $180",
                SecondTransaction = "Wade Warren",
                SecondTransactionType = "Cashback",
                SecondTransactionAmount = "+ $40",
                ThirdTransaction = "Jane Cooper",
                ThirdTransactionType = "Rent",
                ThirdTransactionAmount = "+ $200",
                FourthTransaction = "Guy Hawkins",
                FourthTransactionType = "Cashback",
                FourthTransactionAmount = "+ $50",
                DueAmount = "$380"
            };

            this.thirdCardTransaction = new TransactionDetails()
            {
                FirstTransaction = "Andrie",
                FirstTransactionType = "Entertainment",
                FirstTransactionAmount = "+ $120",
                SecondTransaction = "Wade Warren",
                SecondTransactionType = "Food order",
                SecondTransactionAmount = "+ $140",
                ThirdTransaction = "Jane Cooper",
                ThirdTransactionType = "Cashback",
                ThirdTransactionAmount = "+ $30",
                FourthTransaction = "Guy Hawkins",
                FourthTransactionType = "Shopping",
                FourthTransactionAmount = "+ $100",
                DueAmount = "$360"
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
