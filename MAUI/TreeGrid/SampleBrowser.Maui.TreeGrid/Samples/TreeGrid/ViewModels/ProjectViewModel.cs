using System.Collections.ObjectModel;

namespace SampleBrowser.Maui.TreeGrid
{
    public class ProjectViewModel
    {
        private Random _random = new Random(42);

        public ObservableCollection<ProjectInfo> Projects { get; set; }

        string[] priorities =
        {
            "High",
            "Medium",
            "Low"
        };

        private ObservableCollection<object>? treeGridSelectedItem;

        public ProjectViewModel()
        {
            Projects = GenerateProjects();
            this.TreeGridSelectedItems = new ObservableCollection<object>();
            this.TreeGridSelectedItems!.Add(this.Projects[0]);
            this.TreeGridSelectedItems!.Add(this.Projects[1]);
            this.TreeGridSelectedItems!.Add(this.Projects[3]);
        }

        private ObservableCollection<ProjectInfo> GenerateProjects()
        {
            var projects = new ObservableCollection<ProjectInfo>();

            string[] projectNames =
            {
                "E-Commerce Redesign",
                "Mobile Authentication",
                "Cloud Migration",
                "Dashboard Enhancement",
                "Analytics Engine",
                "API Gateway",
                "Database Optimization",
                "Component Library",
                "Payment Gateway",
                "User Onboarding",
                "Performance Monitoring",
                "Security Audit",
                "Data Backup",
                "3rd Party Integration",
                "Email Service",
                "Multi-language Support",
                "Admin Panel Overhaul",
                "Search Upgrade",
                "Mobile Responsive",
                "Testing Suite",
                "Dev Documentation",
                "Infrastructure Scaling",
                "ML Pipeline",
                "Dark Mode",
                "Accessibility",
                "Rate Limiting",
                "Cache Optimization",
                "Log Aggregation",
                "Disaster Recovery",
                "DevOps Pipeline"
            };

            string[] owners =
            {
            "Andrew Fuller",
            "Janet Leverling",
            "Nancy Davolio",
            "Margaret Peacock",
            "Steven Buchanan"
        };

            string[] statuses =
            {
            "Open",
            "In Progress",
            "Completed",
            "Testing"
        };


            for (int i = 0; i < projectNames.Length; i++)
            {
                // Randomize status
                string parentStatus = statuses[_random.Next(statuses.Length)];

                // Randomize start dates across the year
                int randomDays = _random.Next(0, 365);
                var startDate = new DateTime(2026, 1, 1).AddDays(randomDays);

                string parentPriority = priorities[_random.Next(priorities.Length)];

                var parent = new ProjectInfo
                {
                    Id = $"PRJ-{101 + i}",
                    Name = projectNames[i],
                    Owner = owners[_random.Next(owners.Length)],
                    Status = parentStatus,
                    Priority = parentPriority,
                    StartDate = startDate,
                    Progress = 0,
                    IsCompleted = false
                };

                int taskCount = _random.Next(2, 6);

                for (int j = 1; j <= taskCount; j++)
                {
                    // If parent is Completed, all children must be Completed
                    string taskStatus = parentStatus == "Completed"
                        ? "Completed"
                        : statuses[_random.Next(statuses.Length)];

                    int taskProgress = taskStatus switch
                    {
                        "Completed" => 100,
                        "In Progress" => _random.Next(40, 80),
                        "Testing" => _random.Next(75, 99),
                        "Open" => _random.Next(0, 25),
                        _ => _random.Next(0, 100)
                    };

                    string taskPriority;

                    switch (parentPriority)
                    {
                        case "High":
                            taskPriority = _random.Next(2) == 0
                                ? "High"
                                : "Medium";
                            break;

                        case "Medium":
                            taskPriority = _random.Next(2) == 0
                                ? "Medium"
                                : "Low";
                            break;

                        default: // Low
                            taskPriority = _random.Next(2) == 0
                                ? "Low"
                                : "Medium";
                            break;
                    }

                    var task = new ProjectInfo
                    {
                        Id = $"PRJ-{101 + i}",
                        Name = $"Phase {j}: {GetTaskName(j)}",
                        Owner = owners[_random.Next(owners.Length)],
                        Priority = taskPriority,
                        Status = taskStatus,
                        Progress = taskProgress,
                        IsCompleted = taskProgress == 100,
                        StartDate = startDate.AddDays(_random.Next(1, 30))
                    };

                    parent.Tasks.Add(task);
                }

                parent.Progress = (int)parent.Tasks.Average(t => t.Progress);
                parent.IsCompleted = parent.Progress == 100;

                // Parent Priority = Highest Child Priority
                int highestPriority = parent.Tasks.Max(t => GetPriorityRank(t.Priority));
                parent.Priority = GetPriorityFromRank(highestPriority);

                projects.Add(parent);
            }

            return projects;
        }

        private int GetPriorityRank(string? priority)
        {
            return priority switch
            {
                "High" => 3,
                "Medium" => 2,
                "Low" => 1,
                _ => 1
            };
        }

        private string GetPriorityFromRank(int rank)
        {
            return rank switch
            {
                3 => "High",
                2 => "Medium",
                _ => "Low"
            };
        }

        private string GetTaskName(int phase)
        {
            return phase switch
            {
                1 => "Requirements",
                2 => "Implementation",
                3 => "Testing & QA",
                4 => "Code Review",
                5 => "Deployment",
                _ => "Miscellaneous"
            };
        }

        public ObservableCollection<object>? TreeGridSelectedItems
        {
            get
            {
                return treeGridSelectedItem;
            }
            set
            {
                this.treeGridSelectedItem = value;
            }
        }
    }
}