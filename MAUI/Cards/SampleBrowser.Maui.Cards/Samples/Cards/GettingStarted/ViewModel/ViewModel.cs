#region Copyright Syncfusion Inc. 2001-2023.
// Copyright Syncfusion Inc. 2001-2023. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.Cards.SfCards
{
    using System.ComponentModel;

    public class CardDetails : INotifyPropertyChanged
    {
        private string cardName = string.Empty;
        private string cardNumber = string.Empty;
        private string cardHolderName = string.Empty;
        private string cardDueDate = string.Empty;
        private string cardDueAmount = string.Empty;
        private Color dueIndicatorColor = Colors.Transparent;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string CardName
        {
            get
            {
                return cardName;
            }
            set
            {
                cardName = value;
                RaisePropertyChanged("CardName");
            }
        }

        public string CardNumber
        {
            get
            {
                return cardNumber;
            }
            set
            {
                cardNumber = value;
                RaisePropertyChanged("CardNumber");
            }
        }

        public string CardHolderName
        {
            get
            {
                return cardHolderName;
            }
            set
            {
                cardHolderName = value;
                RaisePropertyChanged("CardHolderName");
            }
        }

        public string CardDueDate
        {
            get
            {
                return cardDueDate;
            }
            set
            {
                cardDueDate = value;
                RaisePropertyChanged("CardDueDate");
            }
        }

        public string CardDueAmount
        {
            get
            {
                return cardDueAmount;
            }
            set
            {
                cardDueAmount = value;
                RaisePropertyChanged("CardDueAmount");
            }
        }

        public Color DueIndicatorColor
        {
            get
            {
                return dueIndicatorColor;
            }
            set
            {
                dueIndicatorColor = value;
                RaisePropertyChanged("DueIndicatorColor");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    public class ViewModel : INotifyPropertyChanged
    {
        private CardDetails firstCardDetails;

        private CardDetails secondCardDetails;

        private CardDetails thirdCardDetails;

        private CardDetails fourthCardDetails;

        private CardDetails fifthCardDetails;

        private double cornerRadius = 7;

        private bool fadeOutOnSwiping = false;

        private bool swipeToDismiss = false;

        public ViewModel()
        {
            firstCardDetails = new CardDetails()
            {
                CardName = "Wells Fargo",
                CardNumber = "XXXX 4976",
                CardHolderName = "Williamson S",
                CardDueDate = $"Due on {DateTime.Now.AddDays(1).ToString("dd")} {DateTime.Now.AddDays(1).ToString("MMM")}",
                CardDueAmount = "$457",
                DueIndicatorColor = Colors.Red
            };

            secondCardDetails = new CardDetails()
            {
                CardName = "Chase Platinum",
                CardNumber = "XXXX 1863",
                CardHolderName = "Williamson S",
                CardDueDate = $"Due on {DateTime.Now.AddDays(5).ToString("dd")} {DateTime.Now.AddDays(5).ToString("MMM")}",
                CardDueAmount = "$300",
                DueIndicatorColor = Colors.Orange
            };

            thirdCardDetails = new CardDetails()
            {
                CardName = "KeyBank Business",
                CardNumber = "XXXX 0417",
                CardHolderName = "Williamson S",
                CardDueDate = $"Due on {DateTime.Now.AddDays(15).ToString("dd")} {DateTime.Now.AddDays(15).ToString("MMM")}",
                CardDueAmount = "$160",
                DueIndicatorColor = Colors.Gray
            };

            fourthCardDetails = new CardDetails()
            {
                CardName = "American Express",
                CardNumber = "XXXX 2810",
                CardHolderName = "Williamson S",
                CardDueDate = $"Due on {DateTime.Now.AddDays(5).ToString("dd")} {DateTime.Now.AddDays(5).ToString("MMM")}",
                CardDueAmount = "$320",
                DueIndicatorColor = Colors.Orange
            };

            fifthCardDetails = new CardDetails()
            {
                CardName = "Bank of America",
                CardNumber = "XXXX 0063",
                CardHolderName = "Williamson S",
                CardDueDate = $"Paid on {DateTime.Now.AddDays(-2).ToString("dd")} {DateTime.Now.AddDays(-2).ToString("MMM")}",
                CardDueAmount = "$250",
                DueIndicatorColor = Colors.Green
            };
        }

        public CardDetails FirstCardDetails
        {
            get
            {
                return firstCardDetails;
            }
            set
            {
                firstCardDetails = value;
                OnPropertyChanged("FirstCardDetails");
            }
        }

        public CardDetails SecondCardDetails
        {
            get
            {
                return secondCardDetails;
            }
            set
            {
                secondCardDetails = value;
                OnPropertyChanged("SecondCardDetails");
            }
        }

        public CardDetails ThirdCardDetails
        {
            get
            {
                return thirdCardDetails;
            }
            set
            {
                thirdCardDetails = value;
                OnPropertyChanged("ThirdCardDetails");
            }
        }

        public CardDetails FourthCardDetails
        {
            get
            {
                return fourthCardDetails;
            }
            set
            {
                fourthCardDetails = value;
                OnPropertyChanged("FourthCardDetails");
            }
        }

        public CardDetails FifthCardDetails
        {
            get
            {
                return fifthCardDetails;
            }
            set
            {
                fifthCardDetails = value;
                OnPropertyChanged("FifthCardDetails");
            }
        }

        public string IndicatorPosition { get; set; } = "Left";

        public string SwipeDirection { get; set; } = "Right";

        public double CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                cornerRadius = Math.Round(value);
                OnPropertyChanged("CornerRadius");
            }
        }

        public bool FadeOutOnSwiping
        {
            get
            {
                return fadeOutOnSwiping;
            }
            set
            {
                fadeOutOnSwiping = value;
                OnPropertyChanged("FadeOutOnSwiping");
            }
        }

        public bool SwipeToDismiss
        {
            get
            {
                return swipeToDismiss;
            }
            set
            {
                swipeToDismiss = value;
                OnPropertyChanged("SwipeToDismiss");
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
