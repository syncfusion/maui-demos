#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.Shapes;

namespace SampleBrowser.Maui.LiquidGlass.SfCartesianChart
{
    public class ChartViewModel
    {
        private ObservableCollection<Student> Students { get; set; }
        private readonly Random _rnd = new Random();
        private static readonly string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private static readonly string[] Languages = { "English", "Spanish", "French" };
        private static readonly string[] Topics = { "Grammar", "Vocabulary", "Pronunciation" };
        private static readonly string[] Subtopics = { "Basics", "Intermediate", "Advanced" };

        public Geometry? IconPathData1 { get; set; } = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString("M13.099005,15.831011L13.173005,15.831011C15.976006,15.831011,17.523007,18.208001,18.004008,20.915989L18.249007,22.827981 18.249007,26.000967 9.1250038,26.000967 0,26.000967 0,22.827981 0.24499989,20.915989C0.71100044,18.293001 2.1810007,15.98701 4.8200026,15.845011 5.9080029,16.964006 7.3580031,17.654004 8.9530039,17.654004 10.555004,17.654004 12.010005,16.958006 13.099005,15.831011z M18.827027,10.309859L18.901032,10.309859C19.990021,11.436821 21.445036,12.132871 23.047025,12.132871 24.642024,12.132871 26.092005,11.442802 27.180017,10.323775 29.819021,10.465866 31.289021,12.771792 31.755025,15.394858L32.000018,17.30686 32.000018,20.479858 22.875028,20.479858 19.467011,20.479858C18.815034,17.104833 16.860019,14.86983 14.226019,14.401809 14.883031,12.131772 16.37302,10.309859 18.827027,10.309859z M8.7649922,5.5209818C11.349993,5.5209818 13.445994,7.9249854 13.445994,10.889991 13.445994,13.854995 11.565994,15.953998 8.9809923,15.953998 6.3959913,15.953998 4.2999907,13.550994 4.2999907,10.58499 4.2999907,7.6199849 6.1799908,5.5209818 8.7649922,5.5209818z M23.234998,0C25.819999,0 27.7,2.0990028 27.7,5.0640078 27.7,8.0300126 25.603999,10.433016 23.018998,10.433016 20.433997,10.433016 18.553996,8.334013 18.553996,5.3690081 18.553996,2.4040036 20.649997,0 23.234998,0z");
        public Geometry? IconPathData2 { get; set; } = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString("M23.542969,22.45802C22.734985,25.87001 21.407959,28.077012 20.090942,29.487016 23.763977,28.375016 26.799988,25.806014 28.536987,22.45802z M10.493958,22.45802C12.218994,27.951005 15.189941,29.660022 16.054993,30.047015 16.948975,29.756 20.10199,28.311998 21.610962,22.45802z M3.4629517,22.45802C5.2749634,25.951003 8.506958,28.585008 12.393982,29.612018 11.026978,28.191026 9.5439453,25.941024 8.5379639,22.45802z M24.159973,12.441993C24.270996,13.596016 24.336975,14.824014 24.302979,16.19999 24.263,17.827007 24.119995,19.268993 23.911987,20.579999L29.339966,20.579999C29.835999,19.143993 30.117981,17.611003 30.117981,16.009011 30.117981,14.775003 29.941956,13.583992 29.640991,12.441993z M9.4679565,12.441993C9.2939453,13.785012 9.2469482,15.264016 9.407959,16.921001 9.539978,18.275005 9.7419434,19.483012 9.9879761,20.579999L22.006958,20.579999C22.225952,19.280009 22.379944,17.827007 22.420959,16.152993 22.454956,14.790017 22.403992,13.560005 22.289001,12.441993z M2.3589478,12.441993C2.05896,13.583992 1.8829956,14.775003 1.8829956,16.009011 1.8829956,17.611003 2.164978,19.143993 2.6599731,20.579999L8.0739746,20.579999C7.848999,19.513011 7.6569824,18.375011 7.532959,17.104015 7.3669434,15.397989 7.4069824,13.855995 7.5769653,12.441993z M20.221985,2.5690017C21.727966,4.1199972,23.221985,6.617008,23.91095,10.564001L29.017944,10.564001C27.416992,6.7680094,24.195984,3.8139975,20.221985,2.5690017z M12.529968,2.3720102C8.2149658,3.4669819,4.6819458,6.5329931,2.9819946,10.564001L7.901001,10.564001C8.7919922,6.6609838,10.78595,4.0180075,12.529968,2.3720102z M16.095947,1.9920053C14.978943,2.6090102,11.182983,5.0700099,9.80896,10.564001L22.013977,10.564001C20.78595,4.1120016,17.180969,2.372987,16.095947,1.9920053z M15.891968,0L16.129944,0.053009032C24.891968,0.12298569 32,7.2519877 32,16.009011 32,24.770004 24.886963,31.900012 16.121948,31.966998L15.828979,31.964008C7.0869751,31.871021 0,24.753005 0,16.009011 0,7.2869913 7.0529785,0.18499745 15.765991,0.057983398z");
        public Geometry? IconPathData3 { get; set; } = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString("M5.3000031,24.599984L5.3000031,31.699994 0,31.699994 0,30.00001z M9.6000061,19.4L11.700012,22.599984 11.700012,31.8 6.9000092,31.8 6.9000092,22.599984z M18.600006,18.799994L18.600006,31.699994 13.800003,31.699994 13.800003,23.599984z M25.5,11.899999L25.5,31.699994 20.700012,31.699994 20.700012,17.199988z M32,0L27.800003,10.599979 25.900009,8.5000027 13.800003,21.19999 9.8000031,17.199988 0,27.299998 0,22.500008 9.8000031,12.399999 13.700012,16.4 23.300003,6.2999901 21.200012,4.2999896z");
        public Geometry? IconPathData4 { get; set; } = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString("M22.899994,10.699997L25.099991,13.199997 13.5,23.399994 7,15.899994 9.5,13.699997 13.799988,18.699997z M16,3.5999908C9.1999969,3.5999908 3.5999908,9.1999969 3.5999908,16 3.5999908,18.699997 4.3999939,21.099991 5.8999939,23.199997L6,23.300003 6.0999908,23.399994C6.5,23.899994,6.7999878,24.300003,7.2999878,24.699997L7.5,24.899994 7.6999969,24.899994 7.6999969,25 7.7999878,25.099991C10,27.099991 12.899994,28.300003 16.099991,28.300003 22.899994,28.300003 28.5,22.800003 28.5,15.899994 28.399994,9.1999969 22.799988,3.5999908 16,3.5999908z M16,0C24.799988,0 32,7.1999969 32,16 32,24.800003 24.799988,32 16,32 7.1999969,32 0,24.800003 0,16 0,7.1999969 7.1999969,0 16,0z");
        public Geometry? IconPathData5 { get; set; } = (Geometry?)new PathGeometryConverter().ConvertFromInvariantString("M19.359018,15.650012C19.396017,15.665012,23.146002,17.212999,26.854986,16.332006L27.317984,18.276991C26.324987,18.512989 25.339993,18.605988 24.408997,18.605988 21.25301,18.605988 18.71702,17.548996 18.580021,17.491997z M11.697002,15.650012L12.477004,17.491997C12.340004,17.548996 9.8049974,18.605988 6.6489882,18.605988 5.7169857,18.605988 4.7319832,18.513988 3.7389803,18.276991L4.2019815,16.332006C7.906992,17.212999,11.660002,15.665012,11.697002,15.650012z M19.359018,11.650012C19.396017,11.665012,23.146002,13.211999,26.854986,12.332006L27.317984,14.276991C26.324987,14.512989 25.339993,14.605988 24.408997,14.605988 21.25301,14.605988 18.71702,13.548997 18.580021,13.491997z M11.697002,11.650012L12.477004,13.491997C12.340004,13.548997 9.8049974,14.605988 6.6489882,14.605988 5.7169857,14.605988 4.7319832,14.513988 3.7389803,14.276991L4.2019815,12.332006C7.906992,13.211999,11.660002,11.665012,11.697002,11.650012z M19.359018,7.650012C19.396017,7.6650119,23.146002,9.2119989,26.854986,8.3320065L27.317984,10.276991C26.324987,10.512989 25.339993,10.605988 24.408997,10.605988 21.25301,10.605988 18.71702,9.5489969 18.580021,9.4919968z M11.697002,7.650012L12.477004,9.4919968C12.340004,9.5489969 9.8049974,10.605988 6.6489882,10.605988 5.7169857,10.605988 4.7319832,10.513988 3.7389803,10.276991L4.2019815,8.3320065C7.906992,9.2119989,11.660002,7.6650119,11.697002,7.650012z M14.999994,2.791018L14.993829,2.7938824C12.465567,3.9590192,7.1699924,5.8726907,2,4.4349637L2,20.836847C3.6468868,21.792995,9.5094147,24.682388,14.869985,20.98365L14.999994,20.891203z M16.999994,2.7909093L16.999994,20.891201 17.129986,20.98365C22.489674,24.682388,28.353172,21.792995,30,20.836847L30,4.4349637C24.833469,5.8726907,19.535452,3.9590192,17.006395,2.7938824z M15.999023,0L16.506042,0.29699469C16.582031,0.34100088,24.177002,4.7209725,30.626038,2.1129904L32,1.5599856 32,21.926834 31.559021,22.223829C31.476013,22.280836 23.312012,27.667788 16,22.632822 13.677002,24.232817 11.265991,24.77981 9.0560303,24.77981 4.3140259,24.77981 0.49804688,22.262831 0.44104004,22.223829L0,21.926834 0,1.5599856 1.3740234,2.1129904C7.842041,4.7259774,15.416992,0.34301501,15.493042,0.29699469z");

        public int TotalStudents { get; private set; }
        public double AverageScore { get; private set; }
        public int LanguagesOffered { get; private set; }
        public double CompletionRate { get; private set; }


        public ObservableCollection<ScoreData> EnglishScores { get; private set; }
        public ObservableCollection<ScoreData> SpanishScores { get; private set; }
        public ObservableCollection<ScoreData> FrenchScores { get; private set; }


        public ObservableCollection<CurriculumNode> CurriculumBreakdown { get; private set; }
        public ObservableCollection<ProgressStage> StudentProgress { get; private set; }

        public List<Brush> SunburstPalette { get; set; }

        public List<Brush> FunnelPalette { get; set; }

        public ChartViewModel()
        {
            EnglishScores = GenerateLinearScores(65, 85);
            SpanishScores = GenerateLinearScores(55, 75);
            FrenchScores = GenerateLinearScores(50, 70);

            Students = GenerateStudentsAlignedWithLine(count: 180);

            TotalStudents = Students.Count;
            LanguagesOffered = Languages.Length;
            AverageScore = Math.Round((
                                        EnglishScores.Average(s => s.Score) +
                                        SpanishScores.Average(s => s.Score) +
                                        FrenchScores.Average(s => s.Score)) / 3.0, 1);
            CompletionRate = Math.Round(Students.Count(s => s.IsCertified) * 100.0 / Students.Count, 1);

            CurriculumBreakdown = AggregateCurriculumFromStudents();

            StudentProgress = new ObservableCollection<ProgressStage>
            {
                new ProgressStage("Certified",  Students.Count(s => s.IsCertified)),
                new ProgressStage("Completed",  Students.Count(s => s.HasCompleted)),
                new ProgressStage("Started",    Students.Count(s => s.HasStarted)),
                new ProgressStage("Enrolled",   Students.Count),
            };

            SunburstPalette = new List<Brush>
            {
                new SolidColorBrush(Color.FromArgb("#FF8D28")),
                new SolidColorBrush(Color.FromArgb("#0088FF")),
                new SolidColorBrush(Color.FromArgb("#CB30E0"))
            };

            FunnelPalette = new List<Brush>
            {
                new SolidColorBrush(Color.FromArgb("#FF8D28")),
                new SolidColorBrush(Color.FromArgb("#CB30E0")),
                new SolidColorBrush(Color.FromArgb("#34C759")),
                new SolidColorBrush(Color.FromArgb("#0088FF"))
            };
        }

        private ObservableCollection<ScoreData> GenerateLinearScores(double start, double end)
        {
            int count = Months.Length;
            double step = (end - start) / (count - 1);

            var list = new List<ScoreData>(count);

            for (int i = 0; i < count; i++)
            {
                double baseScore = start + (step * i);
                double variation = Random.Shared.Next(-2, 3); 
                double value = Math.Clamp(baseScore + variation, 0, 100);

                DateTime date = new DateTime(2025, i + 1, 1);

                list.Add(new ScoreData(date, Months[i], value));
            }

            return new ObservableCollection<ScoreData>(list);
        }

        private double GetLineScore(string language, string month)
        {
            ScoreData? point = language switch
            {
                "English" => EnglishScores.FirstOrDefault(s => s.Month == month),
                "Spanish" => SpanishScores.FirstOrDefault(s => s.Month == month),
                "French" => FrenchScores.FirstOrDefault(s => s.Month == month),
                _ => null
            };
            return point?.Score ?? 0;
        }

        private ObservableCollection<Student> GenerateStudentsAlignedWithLine(int count)
        {
            var students = new ObservableCollection<Student>();

            for (int i = 1; i <= count; i++)
            {
                string lang = Languages[_rnd.Next(Languages.Length)];
                string month = Months[_rnd.Next(Months.Length)];

                // Score centered around line-chart value for this language/month
                double lineValue = GetLineScore(lang, month);
                double noise = _rnd.Next(-3, 4); // [-3, +3]
                double score = Math.Clamp(lineValue + noise, 0, 100);

                // Study hours with language-specific bias for realism
                int baseHours = _rnd.Next(12, 48);
                int langBias = lang == "English" ? 5 : (lang == "French" ? -2 : 0);
                int studyHrs = Math.Max(5, baseHours + langBias);

                // Progress flags consistent with funnel (no backflow)
                bool started = _rnd.NextDouble() >= 0.02; // all enrolled have started in this demo
                bool completed = _rnd.NextDouble() < (studyHrs >= 24 ? 0.72 : 0.55);
                bool certified = completed && _rnd.NextDouble() <
                                 (lang == "English" ? 0.86 :
                                  lang == "Spanish" ? 0.82 : 0.78);

                // Assign exactly one topic & subtopic per student
                string topic = Topics[_rnd.Next(Topics.Length)];
                string subtopic = Subtopics[_rnd.Next(Subtopics.Length)];

                students.Add(new Student
                {
                    Language = lang,
                    Score = score,
                    HasStarted = started,
                    HasCompleted = completed,
                    IsCertified = certified,
                    Topic = topic,
                    Subtopic = subtopic
                });
            }

            return students;
        }

        private ObservableCollection<CurriculumNode> AggregateCurriculumFromStudents()
        {
            var nodes = new ObservableCollection<CurriculumNode>();

            var groups = Students
                .GroupBy(s => new { s.Language, s.Topic, s.Subtopic })
                .OrderBy(g => g.Key.Language)
                .ThenBy(g => g.Key.Topic)
                .ThenBy(g => g.Key.Subtopic);

            foreach (var g in groups)
            {
                nodes.Add(new CurriculumNode(
                    g.Key.Language ?? "Unknown",
                    g.Key.Topic ?? "Unknown",
                    g.Key.Subtopic ?? "Unknown",
                    g.Count()
                ));
            }
            return nodes;
        }
    }
}