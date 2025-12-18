#region Copyright Syncfusion® Inc. 2001-2025.
// Copyright Syncfusion® Inc. 2001-2025. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
namespace SampleBrowser.Maui.LiquidGlass.SfCartesianChart
{
    public class Student
    {
        public string? Language { get; set; }   // English / Spanish / French
        public double Score { get; set; }
        public bool HasStarted { get; set; }
        public bool HasCompleted { get; set; }
        public bool IsCertified { get; set; }
        public string? Topic { get; set; }
        public string? Subtopic { get; set; }
    }

    public class ScoreData
    {
        public string Month { get; set; }
        public double Score { get; set; }
        public DateTime Date { get; set; }

        public ScoreData(string month, double score)
        {
            Month = month;
            Score = score;
        }

        public ScoreData(DateTime date, string month, double score)
        {
            Date = date;
            Month = month;
            Score = score;
        }
    }

    public class CurriculumNode
    {
        public string Language { get; set; }
        public string Topic { get; set; }
        public string Subtopic { get; set; }
        public double StudentCount { get; set; }

        public CurriculumNode(string language, string topic, string subtopic, double count)
        {
            Language = language;
            Topic = topic;
            Subtopic = subtopic;
            StudentCount = count;
        }
    }

    public class ProgressStage
    {
        public string Stage { get; set; }
        public int Count { get; set; }
        public ProgressStage(string stage, int count)
        {
            Stage = stage;
            Count = count;
        }
    }
}