#region Copyright Syncfusion Inc. 2001-2024.
// Copyright Syncfusion Inc. 2001-2024. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SampleBrowser.Maui.Scheduler.SfScheduler
{
    /// <summary>
    /// The calendar type View Model.
    /// </summary>
    public class CalendarTypeViewModel : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// Notes.
        /// </summary>
        private List<string> notes;

        /// <summary>
        /// colors.
        /// </summary>
        private List<Brush> colors;

        /// <summary>
        /// Events.
        /// </summary>
        private ObservableCollection<Meeting>? events;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CalendarTypeViewModel" /> class.
        /// </summary>
        public CalendarTypeViewModel()
        {
            this.colors = new List<Brush>();
            this.notes = new List<string>();
            this.events = new ObservableCollection<Meeting>();

            this.EnglishSubjects = new List<string>();
            this.HebrewSubjects = new List<string>();
            this.HijriSubjects = new List<string>();
            this.JapaneseSubjects = new List<string>();
            this.KoreanSubjects = new List<string>();
            this.PersianSubjects = new List<string>();
            this.TaiwanSubjects = new List<string>();
            this.ThaiBudhhishtSubjects = new List<string>();
            this.UmAlQuaraSubjects = new List<string>();

            this.AddEnglishSubjects();
            this.AddHebrewSubjects();
            this.AddHijriSubjects();
            this.AddJapaneseSubjects();
            this.AddKoreanSubjects();
            this.AddPersianSubjects();
            this.AddTaiwanSubjects();
            this.AddThaiBudhhishtSubjects();
            this.AddUmAlQuaraSubjects();

            this.CreateColors();
            this.CreateNotes();
            this.GetAppointments(this.HijriSubjects);
            this.DisplayDate = DateTime.Now.Date.AddHours(8).AddMinutes(50);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the schedule display date.
        /// </summary>
        public DateTime DisplayDate { get; set; }

        /// <summary>
        /// Gets or sets appointments
        /// </summary>
        public ObservableCollection<Meeting>? Events
        {
            get
            {
                return this.events;
            }
            set
            {
                this.events = value;
                this.RaiseOnPropertyChanged("Events");
            }
        }

        /// <summary>
        /// List of japanese subjects
        /// </summary>
        internal List<string> JapaneseSubjects { get; set; }

        /// <summary>
        /// english subjects
        /// </summary>
        internal List<string> EnglishSubjects { get; set; }

        /// <summary>
        /// List of french subjects
        /// </summary>
        internal List<string> HebrewSubjects { get; set; }

        /// <summary>
        /// hijri subjects
        /// </summary>
        internal List<string> HijriSubjects { get; set; }

        /// <summary>
        /// List of korean subjects
        /// </summary>
        internal List<string> KoreanSubjects { get; set; }

        /// <summary>
        /// List of persian subjects
        /// </summary>
        internal List<string> PersianSubjects { get; set; }

        /// <summary>
        /// List of taiwan subjects
        /// </summary>
        internal List<string> TaiwanSubjects { get; set; }

        /// <summary>
        /// List of thaiBudhhisht subjects
        /// </summary>
        internal List<string> ThaiBudhhishtSubjects { get; set; }

        /// <summary>
        /// List of umAlQuara subjects
        /// </summary>
        internal List<string> UmAlQuaraSubjects { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Method to create the notes.
        /// </summary>
        private void CreateNotes()
        {
            if (this.notes == null)
            {
                return;
            }

            this.notes.AddRange(new List<string>()
            {
                "Consulting firm laws with business advisers",
                "Execute Project Scope",
                "Project Scope & Deliverables",
                "Executive summary",
                "Try to reduce the risks",
                "Encourages the integration of mind, body, and spirit",
                "Execute Project Scope",
                "Project Scope & Deliverables",
                "Executive summary",
                "Try to reduce the risk"
            });
        }

        /// <summary>
        /// Method to create the color list.
        /// </summary>
        private void CreateColors()
        {
            if (this.colors == null)
            {
                return;
            }

            this.colors.AddRange(new List<Brush>()
            {
                new SolidColorBrush(Color.FromArgb("#FF8B1FA9")),
                new SolidColorBrush(Color.FromArgb("#FFD20100")),
                new SolidColorBrush(Color.FromArgb("#FFFC571D")),
                new SolidColorBrush(Color.FromArgb("#FF36B37B")),
                new SolidColorBrush(Color.FromArgb("#FF3D4FB5")),
                new SolidColorBrush(Color.FromArgb("#FFE47C73")),
                new SolidColorBrush(Color.FromArgb("#FF636363")),
                new SolidColorBrush(Color.FromArgb("#FF85461E")),
                new SolidColorBrush(Color.FromArgb("#FF0F8644")),
                new SolidColorBrush(Color.FromArgb("#FF01A1EF"))
            });
        }

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }


        /// <summary>
        /// Method to initialize the appointments.
        /// </summary>
        internal void GetAppointments(List<string> subjectCollection)
        {
            var meetings = new ObservableCollection<Meeting>();
            Random randomTime = new();
            List<Point> randomTimeCollection = this.GettingTimeRanges();
            DateTime date;
            DateTime dateFrom = DateTime.Now.AddDays(-50);
            DateTime dateTo = DateTime.Now.AddDays(50);

            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 1; additionalAppointmentIndex++)
                {
                    var meeting = new Meeting();
                    int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = subjectCollection[randomTime.Next(9)];
                    meeting.Background = this.colors[randomTime.Next(10)];
                    meeting.IsAllDay = false;
                    meeting.Notes = this.notes[randomTime.Next(10)];
                    meeting.StartTimeZone = TimeZoneInfo.Local;
                    meeting.EndTimeZone = TimeZoneInfo.Local;
                    meetings.Add(meeting);
                }
            }

            if (meetings != null && this.Events != null)
            {
                this.Events = meetings;
            }
        }

        #endregion

        #region Language String

        /// <summary>
        /// List of Japanese subjects.
        /// </summary>
        private void AddJapaneseSubjects()
        {
            this.JapaneseSubjects.AddRange(new List<string>()
            {
                "会議に行く",
                "総会",
                "会議",
                "議論ドラフト法",
                "監査",
                "顧客会議",
                "スクラム",
                "対象会議",
                "総会",
                "ステータスの更新",
                "プロジェクト計画"
            });
        }

        /// <summary>
        /// List of English appointments.
        /// </summary>
        private void AddEnglishSubjects()
        {
            this.EnglishSubjects.AddRange(new List<string>()
            {
                "GoToMeeting",
                "Business Meeting",
                "Conference",
                "Project Status Discussion",
                "Auditing",
                "Client Meeting",
                "Generate Report",
                "Target Meeting",
                "General Meeting",
                "Pay House Rent",
                "Car Service"
            });
        }

        /// <summary>
        /// List of Hebrew subjects.
        /// </summary>
        private void AddHebrewSubjects()
        {
            this.HebrewSubjects.AddRange(new List<string>()
            {
                "לך לפגישה",
                "פגישת עסקים",
                "וְעִידָה",
                "דיון סטטוס הפרויקט",
                "ביקורת",
                "פגישת לקוח",
                "להפיק דוח",
                "פגישת יעד",
                "פגישה כללית",
                "שלם שכר דירה",
                "שירות מכוניות"
            });
        }

        /// <summary>
        /// List of Hijri subjects.
        /// </summary>
        private void AddHijriSubjects()
        {
            this.HijriSubjects.AddRange(new List<string>()
            {
                "اذهب للأجتماع",
                "اجتماع عمل",
                "مؤتمر",
                "مناقشة حالة المشروع",
                "تدقيق",
                "اجتماع العميل",
                "انشاء تقرير",
                "الهدف الاجتماع",
                "اجتماع عام",
                "دفع إيجار المنزل",
                "خدمة السيارات"
            });
        }

        /// <summary>
        /// List of Korean subjects.
        /// </summary>
        private void AddKoreanSubjects()
        {
            this.KoreanSubjects.AddRange(new List<string>()
            {
                "지원하다",
                "계획",
                "회의",
                "집세 지불",
                "감사",
                "스크럼",
                "풀어 주다",
                "목표 회의",
                "총회",
                "집세 지불",
                "컨설팅"
            });
        }

        /// <summary>
        /// List of Persian subjects.
        /// </summary>
        private void AddPersianSubjects()
        {
            this.PersianSubjects.AddRange(new List<string>()
            {
                "رفتن به جلسه",
                "نشست کسب و کار",
                "کنفرانس",
                "بحث وضعیت پروژه",
                "حسابرسی",
                "جلسه مشتری",
                "ایجاد گزارش",
                "جلسه هدف",
                "مجمع عمومی",
                "پرداخت اجاره خانه",
                "خدمات خودرو"
            });
        }

        /// <summary>
        /// List of Taiwan subjects.
        /// </summary>
        private void AddTaiwanSubjects()
        {
            this.TaiwanSubjects.AddRange(new List<string>()
            {
                "轉到會議",
                "商務會議",
                "會議",
                "項目狀態討論",
                "審計",
                "客戶會議",
                "客戶會議",
                "目標會議",
                "股東大會",
                "支付房屋租金",
                "汽車服務"
            });
        }

        /// <summary>
        /// List of ThaiBudhhisht subjects.
        /// </summary>
        private void AddThaiBudhhishtSubjects()
        {
            this.ThaiBudhhishtSubjects.AddRange(new List<string>()
            {
                "การประชุม",
                "ประชุมธุรกิจ",
                "การประชุม",
                "สถานะโครงการอภิปราย",
                "สอบบัญชี",
                "ประชุมลูกค้า",
                "สร้างรายงาน",
                "เป้าหมายการประชุม",
                "ประชุมใหญ่",
                "จ่ายค่าเช่าบ้าน",
                "บริการรถ"
            });
        }

        /// <summary>
        /// List of UmAlQuara subjects
        /// </summary>
        private void AddUmAlQuaraSubjects()
        {
            this.UmAlQuaraSubjects.AddRange(new List<string>()
            {
                "اذهب للأجتماع",
                "اجتماع عمل",
                "مؤتمر",
                "مناقشة حالة المشروع",
                "تدقيق",
                "اجتماع العميل",
                "انشاء تقرير",
                "الهدف الاجتماع",
                "اجتماع عام",
                "دفع إيجار المنزل",
                "خدمة السيارات"
            });
        }

        #endregion 

        #region Property Changed Event

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Invoke method when property changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
