using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SampleBrowser.Maui.TreeGrid
{
    public class ProjectInfo
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Owner { get; set; }

        public string? Status { get; set; }

        public DateTime StartDate { get; set; }

        public int Progress { get; set; }

        public bool IsCompleted { get; set; }

        public string? Priority { get; set; }

        public ObservableCollection<ProjectInfo> Tasks { get; set; } = new();
    }
}
