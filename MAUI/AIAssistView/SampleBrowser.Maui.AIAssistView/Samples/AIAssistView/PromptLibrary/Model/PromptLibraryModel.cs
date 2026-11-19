using System.Collections.ObjectModel;
using System.ComponentModel;
using Syncfusion.Maui.AIAssistView;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    /// <summary>
    /// Model class for managing prompt library data
    /// </summary>
    public class PromptLibraryInfoRepository : INotifyPropertyChanged
    {
        #region Fields

        private ObservableCollection<PromptItem> promptItemsInfo;

        #endregion

        #region Constructor

        public PromptLibraryInfoRepository()
        {
            this.promptItemsInfo = new ObservableCollection<PromptItem>();
            this.InitializePromptLibrary();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the collection of prompt items
        /// </summary>
        public ObservableCollection<PromptItem> PromptLibraryInfo
        {
            get => this.promptItemsInfo;
            set
            {
                if (this.promptItemsInfo != value)
                {
                    this.promptItemsInfo = value;
                    RaisePropertyChanged(nameof(PromptLibraryInfo));
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the prompt library with all categories and prompts
        /// </summary>
        private void InitializePromptLibrary()
        {
            // Add all prompts from different categories
            AddAnalyzePrompts();
            AddAskPrompts();
            AddAssistPrompts();
            AddCodePrompts();
            AddCreatePrompts();
            AddEditPrompts();
            AddLearnPrompts();
            AddOptimizePrompts();
            AddLearningPrompts();
            AddCareerPrompts();
            AddPlanningPrompts();
            AddPromptImprovementPrompts();
            AddGoalsPrompts();
        }

        /// <summary>
        /// Adds analyze-related prompts
        /// </summary>
        private void AddAnalyzePrompts()
        {
            PromptItem[] analyzePrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Data Insights Extractor",
                    PromptContent = "As a Data Insights Extractor, analyze the provided dataset and extract key insights. Identify trends, anomalies, and patterns. Summarize findings in business-friendly language. Recommend actions based on the analysis.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Surfaces meaningful insights and patterns from raw data."
                },
                new PromptItem()
                {
                    Title = "Sentiment Analyzer",
                    PromptContent = "As a Sentiment Analyzer, perform sentiment analysis on the provided customer reviews. Classify each review as positive, neutral, or negative. Highlight recurring themes and emotional drivers. Provide an overall sentiment summary.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Evaluates emotional tone and themes in text feedback."
                },
                new PromptItem()
                {
                    Title = "Competitor Research",
                    PromptContent = "As a Competitor Research analyst, analyze the top three competitors in the given market segment. Compare their products, pricing, positioning, and strengths. Identify gaps and opportunities for our offering.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Breaks down competitor strategies and market positioning."
                },
                new PromptItem()
                {
                    Title = "User Behavior Analyst",
                    PromptContent = "As a User Behavior Analyst, analyze user behavior data from the application. Identify common usage patterns and drop-off points. Highlight features that drive engagement. Suggest UX improvements based on findings.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Interprets user interaction data to guide product decisions."
                },
                new PromptItem()
                {
                    Title = "Code Complexity Reviewer",
                    PromptContent = "As a Code Complexity Reviewer, analyze the provided code for complexity, readability, and maintainability. Identify long methods, deep nesting, and duplicated logic. Suggest refactoring opportunities with rationale.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Evaluates code quality and surfaces refactoring opportunities."
                },
                new PromptItem()
                {
                    Title = "Sales Performance Analyzer",
                    PromptContent = "As a Sales Performance Analyzer, analyze quarterly sales performance across regions and product lines. Highlight top performers and underperforming segments. Identify contributing factors and seasonal patterns. Recommend corrective actions.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Diagnoses sales trends and uncovers growth opportunities."
                },
                new PromptItem()
                {
                    Title = "Log File Investigator",
                    PromptContent = "As a Log File Investigator, analyze the provided log file for errors, warnings, and unusual patterns. Group related events and trace root causes. Summarize incidents with timestamps and impact assessment.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Detects issues and traces root causes from log output."
                },
                new PromptItem()
                {
                    Title = "Survey Response Analyzer",
                    PromptContent = "As a Survey Response Analyzer, analyze responses from a customer satisfaction survey. Group answers by theme and sentiment. Quantify satisfaction levels across key dimensions. Highlight actionable feedback.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Transforms survey responses into structured insights."
                },
                new PromptItem()
                {
                    Title = "Market Trend Analyzer",
                    PromptContent = "As a Market Trend Analyzer, analyze current market trends in the specified industry. Identify emerging technologies, shifting customer expectations, and disruptive forces. Project likely developments over the next 12 months.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Provides forward-looking market intelligence and trend analysis."
                },
                new PromptItem()
                {
                    Title = "Resume Gap Analyzer",
                    PromptContent = "As a Resume Gap Analyzer, analyze the provided resume against a target job description. Identify matching skills, missing qualifications, and experience gaps. Recommend specific improvements to strengthen the application.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Compares candidate profiles with role requirements."
                },
                new PromptItem()
                {
                    Title = "Security Posture Analyzer",
                    PromptContent = "As a Security Posture Analyzer, analyze the security posture of the described system. Identify potential vulnerabilities, missing controls, and compliance gaps. Prioritize findings by severity and recommend mitigations.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Reviews security controls and surfaces vulnerabilities."
                },
                new PromptItem()
                {
                    Title = "Feedback Theme Analyzer",
                    PromptContent = "As a Feedback Theme Analyzer, analyze collected user feedback across support tickets, reviews, and surveys. Group comments into recurring themes. Quantify frequency and sentiment for each theme. Recommend product or service improvements.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Clusters feedback into themes for prioritized action."
                },
                new PromptItem()
                {
                    Title = "Process Bottleneck Analyzer",
                    PromptContent = "As a Process Bottleneck Analyzer, analyze the described business process to identify bottlenecks, redundancies, and delays. Map each step with estimated cycle time. Recommend streamlining opportunities to improve throughput.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Pinpoints inefficiencies in operational workflows."
                },
                new PromptItem()
                {
                    Title = "Test Coverage Analyzer",
                    PromptContent = "As a Test Coverage Analyzer, analyze the test suite for the given codebase. Identify untested modules, missing edge cases, and brittle tests. Recommend additional tests to improve coverage and reliability.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Evaluates test coverage and gaps in quality assurance."
                },
                new PromptItem()
                {
                    Title = "Campaign ROI Analyzer",
                    PromptContent = "As a Campaign ROI Analyzer, analyze the performance of recent marketing campaigns. Compare spend against conversions, revenue, and engagement. Identify the highest and lowest performing channels. Recommend budget reallocation.",
                    Section = "Prompt Topics",
                    Topic = "Analyze",
                    Description = "Measures marketing effectiveness and guides spend decisions."
                }
            };

            foreach (var prompt in analyzePrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds ask-related prompts
        /// </summary>
        private void AddAskPrompts()
        {
            PromptItem[] askPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Concept Explainer",
                    PromptContent = "As a Concept Explainer, explain the requested concept in simple terms suitable for a beginner. Use relatable analogies and concrete examples. Avoid jargon unless clearly defined. End with a short summary.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Answers 'what is' questions with clear, beginner-friendly explanations."
                },
                new PromptItem()
                {
                    Title = "Definition Lookup",
                    PromptContent = "As a Definition Lookup, provide a precise definition of the requested term. Include its origin, common usage, and any related variations. Add example sentences to illustrate correct usage.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Returns accurate definitions with context and examples."
                },
                new PromptItem()
                {
                    Title = "How-To Guide",
                    PromptContent = "As a How-To Guide, provide a step-by-step guide for the requested task. List prerequisites, tools, and expected outcomes. Include common pitfalls and how to avoid them. End with verification steps.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Walks through procedures with clear sequential instructions."
                },
                new PromptItem()
                {
                    Title = "Comparison Q&A",
                    PromptContent = "As a Comparison Q&A assistant, compare the two specified options across key dimensions such as cost, performance, ease of use, and fit for the described scenario. Recommend the better option with reasoning.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Answers 'which should I choose' questions with side-by-side comparisons."
                },
                new PromptItem()
                {
                    Title = "Best Practice Advisor",
                    PromptContent = "As a Best Practice Advisor, what are the current best practices for the requested activity in the given context? List industry-standard recommendations. Explain the rationale behind each practice. Highlight common anti-patterns to avoid.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Surfaces recommended practices and the reasoning behind them."
                },
                new PromptItem()
                {
                    Title = "Pros and Cons",
                    PromptContent = "As a Pros and Cons advisor, list the pros and cons of the proposed decision or technology. Cover short-term and long-term implications. Discuss trade-offs relevant to the stated team size and context. Recommend a default choice with conditions.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Balances advantages and disadvantages to support decision-making."
                },
                new PromptItem()
                {
                    Title = "FAQ Builder",
                    PromptContent = "As a FAQ Builder, generate a FAQ for the requested topic. Anticipate the most common questions a beginner would ask. Provide concise, accurate answers for each. Organize by logical groupings.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Anticipates and answers common questions on a topic."
                },
                new PromptItem()
                {
                    Title = "Recommendation Engine",
                    PromptContent = "As a Recommendation Engine, recommend the best resource, tool, or option for the stated user profile or need. List top three choices with pros, cons, and ideal use cases. Suggest a single best pick with justification.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Suggests tailored options based on stated requirements."
                },
                new PromptItem()
                {
                    Title = "Root Cause Investigator",
                    PromptContent = "As a Root Cause Investigator, help investigate the root cause of the described issue. Ask clarifying questions if needed. Suggest diagnostic steps and likely causes ranked by probability. Recommend corrective actions.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Guides systematic investigation of problems."
                },
                new PromptItem()
                {
                    Title = "Glossary Builder",
                    PromptContent = "As a Glossary Builder, build a glossary of key terms related to the requested domain. For each term, provide a one-line definition and a short example. Order entries alphabetically for easy reference.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Compiles domain terminology with concise definitions."
                },
                new PromptItem()
                {
                    Title = "Yes/No Decision Helper",
                    PromptContent = "As a Yes/No Decision Helper, should we proceed with the proposed action? Analyze the situation, list supporting and opposing factors, and provide a clear yes/no recommendation with confidence level and key assumptions.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Delivers structured binary recommendations with rationale."
                },
                new PromptItem()
                {
                    Title = "Quick Facts Retriever",
                    PromptContent = "As a Quick Facts Retriever, provide a quick reference of key facts about the requested subject. Cover history, current state, notable milestones, and authoritative sources. Keep the response factual and neutral.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Returns factual reference information on demand."
                },
                new PromptItem()
                {
                    Title = "Tool Selector",
                    PromptContent = "As a Tool Selector, which tool, library, or framework is best suited for the stated need? Compare the top three options. Highlight integration, learning curve, and community support. Recommend a default.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Matches tooling options to specific use cases."
                },
                new PromptItem()
                {
                    Title = "When to Use Guide",
                    PromptContent = "As a When to Use Guide, when should the specified approach or technology be used? Describe the ideal scenarios, the problems it solves, and the cases where it is not recommended. Include example triggers for choosing it.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Clarifies appropriate use cases and decision triggers."
                },
                new PromptItem()
                {
                    Title = "Acronym Decoder",
                    PromptContent = "As an Acronym Decoder, decode the requested acronym. Provide the full form, common variations, and a brief explanation. Add example sentences showing how the term is used in context.",
                    Section = "Prompt Topics",
                    Topic = "Ask",
                    Description = "Expands abbreviations with definitions and usage examples."
                }
            };

            foreach (var prompt in askPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds assist-related prompts
        /// </summary>
        private void AddAssistPrompts()
        {
            PromptItem[] assistPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Meeting Notes Assistant",
                    PromptContent = "As a Meeting Notes Assistant, convert the provided meeting transcript into structured notes. Capture key discussion points, decisions made, and action items with owners. Highlight open questions and follow-ups for the next meeting.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Turns raw meeting transcripts into actionable notes."
                },
                new PromptItem()
                {
                    Title = "Email Reply Helper",
                    PromptContent = "As an Email Reply Helper, draft a professional reply to the provided email. Match the sender's tone and intent. Address each point raised. End with a clear next step or call to action.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Composes context-aware email responses."
                },
                new PromptItem()
                {
                    Title = "Customer Support Responder",
                    PromptContent = "As a Customer Support Responder, help respond to the following customer support ticket. Acknowledge the issue with empathy. Provide a clear explanation and step-by-step resolution. Offer escalation paths if needed.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Drafts empathetic and accurate support replies."
                },
                new PromptItem()
                {
                    Title = "Schedule Coordinator",
                    PromptContent = "As a Schedule Coordinator, assist with scheduling a meeting across the following participants and time zones. Find overlapping availability within business hours. Suggest two to three time slots and a calendar invite draft.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Identifies meeting times across time zones."
                },
                new PromptItem()
                {
                    Title = "Travel Planner",
                    PromptContent = "As a Travel Planner, help plan a trip covering flights, accommodations, and a day-by-day itinerary. Suggest options for the specified origin, destination, and trip duration. Include estimated costs and booking tips.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Builds end-to-end travel plans and itineraries."
                },
                new PromptItem()
                {
                    Title = "Research Assistant",
                    PromptContent = "As a Research Assistant, assist with research on the requested topic. Gather key facts, recent developments, and authoritative sources. Summarize findings and cite references. Highlight areas where more research is needed.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Gathers and summarizes information from multiple sources."
                },
                new PromptItem()
                {
                    Title = "Interview Question Bank",
                    PromptContent = "As an Interview Question Bank builder, help build an interview question bank for the specified role. Include behavioral, technical, and situational questions. For each question, add what to listen for in a strong answer.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Curates role-specific interview questions with evaluation criteria."
                },
                new PromptItem()
                {
                    Title = "Project Status Writer",
                    PromptContent = "As a Project Status Writer, help write a project status update. Summarize progress made this week, blockers encountered, and plans for next week. Keep the tone factual and concise for stakeholder consumption.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Produces concise stakeholder-ready status reports."
                },
                new PromptItem()
                {
                    Title = "Decision Framework Builder",
                    PromptContent = "As a Decision Framework Builder, help decide between the two specified options. Build a decision matrix covering cost, risk, impact, and effort. Score each option and recommend a path with rationale.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Structures decisions with weighted criteria and scoring."
                },
                new PromptItem()
                {
                    Title = "Onboarding Guide Creator",
                    PromptContent = "As an Onboarding Guide Creator, create an onboarding guide for a new hire joining the team. Cover tools access, first-week schedule, key contacts, and expected outcomes. Include a 30-60-90 day success plan tailored to the stated role.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Produces structured onboarding plans for new hires."
                },
                new PromptItem()
                {
                    Title = "Event Logistics Assistant",
                    PromptContent = "As an Event Logistics Assistant, help coordinate logistics for the specified event type and attendee count. Outline venue, catering, A/V, and run-of-show needs. Create a checklist with deadlines and owners.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Builds end-to-end event logistics checklists."
                },
                new PromptItem()
                {
                    Title = "Brainstorming Partner",
                    PromptContent = "As a Brainstorming Partner, act as a brainstorming partner on the requested topic. Generate a wide range of ideas without judging. Group ideas into themes. Recommend the top three to explore further with rationale.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Co-creates and organizes ideas during brainstorming sessions."
                },
                new PromptItem()
                {
                    Title = "Presentation Outline Builder",
                    PromptContent = "As a Presentation Outline Builder, help build an outline for a presentation of the specified duration on the requested topic. Define the core message, audience, and key takeaways. Structure slides with a logical flow and suggested visuals.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Creates structured presentation outlines with visual cues."
                },
                new PromptItem()
                {
                    Title = "Conflict Mediator",
                    PromptContent = "As a Conflict Mediator, help mediate the following workplace conflict. Summarize each party's perspective neutrally. Identify underlying interests and shared goals. Suggest a constructive path forward.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Facilitates structured resolution of interpersonal issues."
                },
                new PromptItem()
                {
                    Title = "Personal Finance Advisor",
                    PromptContent = "As a Personal Finance Advisor, provide general guidance on the following personal finance question. Explain relevant concepts, typical approaches, and trade-offs. Recommend next steps and remind to consult a qualified professional for specific advice.",
                    Section = "Prompt Topics",
                    Topic = "Assist",
                    Description = "Explains personal finance concepts and considerations."
                }
            };

            foreach (var prompt in assistPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds code-related prompts
        /// </summary>
        private void AddCodePrompts()
        {
            PromptItem[] codePrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Function Generator",
                    PromptContent = "As a Function Generator, write a well-structured function in the specified language and name that implements the requested behavior. Include parameter validation, return value documentation, and example usage. Add inline comments for non-obvious logic.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Generates well-documented functions from a behavior spec."
                },
                new PromptItem()
                {
                    Title = "Unit Test Author",
                    PromptContent = "As a Unit Test Author, write unit tests for the provided code in the specified language. Cover happy paths, edge cases, and error conditions. Use a readable arrange-act-assert structure. Add test names that describe behavior.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Produces comprehensive unit tests for the given code."
                },
                new PromptItem()
                {
                    Title = "Bug Fix Helper",
                    PromptContent = "As a Bug Fix Helper, help debug the provided code in the specified language that exhibits the stated symptom. Walk through the logic, identify the likely root cause, and propose a fix. Explain why the bug occurred and how the fix prevents it.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Diagnoses bugs and provides targeted fixes."
                },
                new PromptItem()
                {
                    Title = "Code Translator",
                    PromptContent = "As a Code Translator, translate the provided code from the source language to the target language. Preserve behavior, naming clarity, and structure. Add comments highlighting idiomatic differences between the two languages.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Converts code between programming languages."
                },
                new PromptItem()
                {
                    Title = "API Endpoint Scaffolder",
                    PromptContent = "As an API Endpoint Scaffolder, scaffold a RESTful API endpoint for the specified resource in the chosen framework. Include route definition, request and response models, validation, and error handling. Add OpenAPI documentation comments.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Generates REST endpoint scaffolding with validation and docs."
                },
                new PromptItem()
                {
                    Title = "SQL Query Builder",
                    PromptContent = "As a SQL Query Builder, write a SQL query that satisfies the stated description. Use proper joins, aliases, and filtering. Optimize for readability and performance. Add comments explaining each major clause.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Composes clear, optimized SQL queries from intent."
                },
                new PromptItem()
                {
                    Title = "Regular Expression Builder",
                    PromptContent = "As a Regular Expression Builder, write a regular expression that matches the stated pattern. Explain each part of the expression. Provide test cases including valid and invalid inputs. Note any caveats or greedy matching concerns.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Builds and explains regular expressions with test cases."
                },
                new PromptItem()
                {
                    Title = "Code Commenter",
                    PromptContent = "As a Code Commenter, add clear comments and XML doc comments to the provided code in the specified language. Document each public method, its parameters, return value, and exceptions. Improve inline comments where logic is non-obvious.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Improves code documentation with structured comments."
                },
                new PromptItem()
                {
                    Title = "Class Skeleton Creator",
                    PromptContent = "As a Class Skeleton Creator, create a class skeleton for the specified name that represents the given concept. Include fields, constructor, properties, and method signatures. Add XML doc comments and follow SOLID principles.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Generates well-structured class skeletons from a concept."
                },
                new PromptItem()
                {
                    Title = "Error Handler Pattern",
                    PromptContent = "As an Error Handler Pattern guide, show how to implement consistent error handling in the specified language for the stated scenario. Include custom exception types, logging, and user-friendly error messages. Add example calls and tests.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Implements robust error handling patterns with examples."
                },
                new PromptItem()
                {
                    Title = "Algorithm Implementer",
                    PromptContent = "As an Algorithm Implementer, implement the specified algorithm in the chosen language. Include a brief explanation of the approach, complexity analysis, and a small test demonstrating the result.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Implements algorithms with explanations and tests."
                },
                new PromptItem()
                {
                    Title = "Mock Data Generator",
                    PromptContent = "As a Mock Data Generator, generate realistic mock data for the specified entity. Include a variety of values that exercise edge cases. Provide the data in JSON, CSV, and a code snippet for direct use in tests.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Produces realistic mock data sets for testing."
                },
                new PromptItem()
                {
                    Title = "CI Pipeline Script",
                    PromptContent = "As a CI Pipeline Script writer, write a CI pipeline configuration for the specified technology that builds, tests, and deploys the project. Include caching, parallel jobs where helpful, and clear stage names. Add comments explaining each step.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Creates CI pipeline configurations for common platforms."
                },
                new PromptItem()
                {
                    Title = "Design Pattern Example",
                    PromptContent = "As a Design Pattern Example, demonstrate the specified design pattern in the chosen language. Provide a concise, real-world example. Explain when to use the pattern and common pitfalls. Keep the code focused and well-commented.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Illustrates design patterns with practical examples."
                },
                new PromptItem()
                {
                    Title = "Code Comment Cleanup",
                    PromptContent = "As a Code Comment Cleanup tool, review the following code and clean up its comments. Remove redundant or outdated comments. Improve clarity where the code is non-obvious. Add brief comments explaining the 'why' for important decisions.",
                    Section = "Prompt Topics",
                    Topic = "Code",
                    Description = "Refines code comments for clarity and usefulness."
                }
            };

            foreach (var prompt in codePrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds create-related prompts
        /// </summary>
        private void AddCreatePrompts()
        {
            PromptItem[] createPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Blog Post Writer",
                    PromptContent = "As a Blog Post Writer, write a blog post of the specified length on the requested topic. Use a clear introduction, well-structured body with subheadings, and a concise conclusion. Include practical examples and a call to action.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Composes structured blog posts on any topic."
                },
                new PromptItem()
                {
                    Title = "Social Media Campaign",
                    PromptContent = "As a Social Media Campaign strategist, create a social media campaign on the specified platform for the stated product or goal. Define objectives, audience, content pillars, and a posting cadence. Include sample post copy for each pillar.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Designs multi-post social media campaigns."
                },
                new PromptItem()
                {
                    Title = "Tutorial Builder",
                    PromptContent = "As a Tutorial Builder, create a step-by-step tutorial on the requested topic for the stated audience. Break the process into clear, ordered steps. Include prerequisites, expected outcomes, and troubleshooting tips. End with next steps to deepen learning.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Produces structured tutorials with clear steps and tips."
                },
                new PromptItem()
                {
                    Title = "Product Story Narrator",
                    PromptContent = "As a Product Story Narrator, write a compelling product story for the specified product. Highlight the problem it solves, the user it serves, and the transformation it enables. Use a narrative arc with relatable examples and a memorable tagline.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Crafts compelling product narratives and taglines."
                },
                new PromptItem()
                {
                    Title = "Lesson Plan Designer",
                    PromptContent = "As a Lesson Plan Designer, create a lesson plan of the specified duration on the requested subject for the stated grade level. Define learning objectives, materials, activities, and assessments. Include differentiation tips for varied learner needs.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Builds structured lesson plans with assessments."
                },
                new PromptItem()
                {
                    Title = "Marketing Email Drafter",
                    PromptContent = "As a Marketing Email Drafter, draft a marketing email announcing the stated news or offer. Include a compelling subject line, preview text, focused body, and a clear call to action. Match the brand voice and keep paragraphs short.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Composes marketing emails with strong CTAs."
                },
                new PromptItem()
                {
                    Title = "Brand Voice Guide",
                    PromptContent = "As a Brand Voice Guide, create a brand voice guide for the specified brand. Define tone, vocabulary, do's and don'ts, and example phrases. Show how the voice adapts across channels like web, social, and support.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Defines brand voice with practical examples."
                },
                new PromptItem()
                {
                    Title = "Workshop Agenda Creator",
                    PromptContent = "As a Workshop Agenda Creator, create a workshop agenda of the specified duration on the requested topic. Set clear objectives, list activities with time allocations, and specify facilitator notes. Include an icebreaker, hands-on exercise, and wrap-up.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Plans interactive workshop agendas with timings."
                },
                new PromptItem()
                {
                    Title = "Course Curriculum Designer",
                    PromptContent = "As a Course Curriculum Designer, design a multi-module course curriculum on the requested topic for the stated audience. Define learning outcomes, module breakdown, and capstone project. Estimate time per module and suggest assessments.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Structures end-to-end course curricula."
                },
                new PromptItem()
                {
                    Title = "Speech Writer",
                    PromptContent = "As a Speech Writer, write a speech of the specified duration on the requested topic for the stated audience. Open with a hook, present a clear message with supporting points, and close with a memorable call to action. Match the speaker's tone.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Drafts speeches tailored to audience and tone."
                },
                new PromptItem()
                {
                    Title = "Storyboard Creator",
                    PromptContent = "As a Storyboard Creator, create a storyboard for a video of the specified duration on the requested topic. Outline scenes with visuals, narration, on-screen text, and transitions. Indicate pacing and emotional beats across the sequence.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Builds video storyboards with scene-by-scene detail."
                },
                new PromptItem()
                {
                    Title = "Newsletter Author",
                    PromptContent = "As a Newsletter Author, write a weekly newsletter issue for the specified audience on the stated theme. Include a short intro, three to five curated items with summaries, and a closing call to action. Keep the tone friendly and concise.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Drafts engaging newsletter issues."
                },
                new PromptItem()
                {
                    Title = "User Persona Builder",
                    PromptContent = "As a User Persona Builder, create a detailed user persona for the specified product. Include demographics, goals, frustrations, preferred channels, and a short narrative. Suggest how the persona influences product decisions.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Develops research-based user personas."
                },
                new PromptItem()
                {
                    Title = "Landing Page Copy",
                    PromptContent = "As a Landing Page Copy writer, write landing page copy for the specified product or feature. Include a hero headline, sub-headline, three benefit blocks, social proof, and a primary call to action. Keep the tone benefit-focused and clear.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Composes conversion-focused landing page copy."
                },
                new PromptItem()
                {
                    Title = "Quiz Generator",
                    PromptContent = "As a Quiz Generator, create a quiz with the specified number of questions on the requested topic for the stated audience. Mix question types such as multiple choice, true/false, and short answer. Provide an answer key with brief explanations.",
                    Section = "Prompt Topics",
                    Topic = "Create",
                    Description = "Generates quizzes with answer keys and explanations."
                }
            };

            foreach (var prompt in createPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds edit-related prompts
        /// </summary>
        private void AddEditPrompts()
        {
            PromptItem[] editPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Copy Editor",
                    PromptContent = "As a Copy Editor, edit the following text for grammar, spelling, and punctuation. Improve clarity and flow. Preserve the original voice and intent. Return the cleaned version and a brief list of key changes.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Polishes text for grammar, clarity, and flow."
                },
                new PromptItem()
                {
                    Title = "Tone Rewriter",
                    PromptContent = "As a Tone Rewriter, rewrite the following text in the requested tone. Keep the core message and key facts intact. Adjust word choice, sentence length, and structure to match the requested tone.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Adjusts the tone of text while preserving meaning."
                },
                new PromptItem()
                {
                    Title = "Concise Summarizer",
                    PromptContent = "As a Concise Summarizer, shorten the following text to roughly the target length in words. Preserve the main idea and any critical details. Remove redundancy and tangential information.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Condenses text while preserving essential meaning."
                },
                new PromptItem()
                {
                    Title = "Expander for Detail",
                    PromptContent = "As an Expander for Detail, expand the following text with additional detail, examples, and explanation. Keep the original structure but add depth where helpful. Maintain a consistent voice throughout.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Adds depth, examples, and detail to existing text."
                },
                new PromptItem()
                {
                    Title = "Style Guide Enforcer",
                    PromptContent = "As a Style Guide Enforcer, edit the following text to follow the specified style guide conventions. Adjust capitalization, punctuation, terminology, and formatting. Highlight non-trivial changes with brief notes.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Applies style guide rules to existing text."
                },
                new PromptItem()
                {
                    Title = "Plain Language Converter",
                    PromptContent = "As a Plain Language Converter, rewrite the following text in plain language for a general audience. Replace jargon with everyday words. Break long sentences into shorter ones. Preserve the original meaning.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Translates complex text into accessible plain language."
                },
                new PromptItem()
                {
                    Title = "Translation Helper",
                    PromptContent = "As a Translation Helper, translate the following text from the source language to the target language. Preserve tone, formatting, and key terminology. Flag any phrases where direct translation is ambiguous.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Translates text while preserving tone and intent."
                },
                new PromptItem()
                {
                    Title = "Email Polisher",
                    PromptContent = "As an Email Polisher, polish the following email draft. Make the opening friendlier, tighten the body, and strengthen the call to action. Keep it concise and ensure the tone matches the recipient and context.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Refines email drafts for clarity and impact."
                },
                new PromptItem()
                {
                    Title = "Headline Optimizer",
                    PromptContent = "As a Headline Optimizer, suggest the specified number of improved headline options for the following article or section. Make them clear, specific, and engaging. Explain the trade-offs of each option briefly.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Generates stronger headline options with rationale."
                },
                new PromptItem()
                {
                    Title = "Inconsistency Finder",
                    PromptContent = "As an Inconsistency Finder, review the following document for inconsistencies in terminology, formatting, capitalization, and numbers. List each issue with its location and a suggested fix.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Identifies inconsistencies in style and terminology."
                },
                new PromptItem()
                {
                    Title = "Audience Re-Targeter",
                    PromptContent = "As an Audience Re-Targeter, rewrite the following text for the specified new audience. Adjust vocabulary, examples, references, and assumed knowledge. Preserve the core message and any non-negotiable facts.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Re-targets existing content for a different audience."
                },
                new PromptItem()
                {
                    Title = "Bullet Point Converter",
                    PromptContent = "As a Bullet Point Converter, convert the following paragraph into a clear bulleted list. Use parallel structure across items. Group related points under subheadings if needed. Preserve all key information.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Reformats prose into structured bullet points."
                },
                new PromptItem()
                {
                    Title = "Active Voice Pass",
                    PromptContent = "As an Active Voice Pass editor, rewrite the following text to use active voice where possible. Replace passive constructions with clear actor-action statements. Maintain the original meaning and tone.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Converts passive voice to active for stronger writing."
                },
                new PromptItem()
                {
                    Title = "Inclusive Language Reviewer",
                    PromptContent = "As an Inclusive Language Reviewer, review the following text for inclusive and respectful language. Flag terms that may be exclusionary or biased. Suggest neutral alternatives with brief explanations for each change.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Flags and replaces non-inclusive language."
                },
                new PromptItem()
                {
                    Title = "Title and Meta Tuner",
                    PromptContent = "As a Title and Meta Tuner, improve the title and meta description for the following webpage or article. Make them SEO-friendly, compelling, and within recommended character limits. Provide two to three variants.",
                    Section = "Prompt Topics",
                    Topic = "Edit",
                    Description = "Optimizes page titles and meta descriptions for SEO."
                }
            };

            foreach (var prompt in editPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds learn-related prompts
        /// </summary>
        private void AddLearnPrompts()
        {
            PromptItem[] learnPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Topic Explainer",
                    PromptContent = "As a Topic Explainer, teach the requested topic as if to a beginner. Break it into a logical sequence of concepts. Explain each concept with an analogy and a small example. End with a quick self-check question.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Teaches a topic from scratch with analogies and examples."
                },
                new PromptItem()
                {
                    Title = "Concept Deep Dive",
                    PromptContent = "As a Concept Deep Dive guide, take a deep dive into the requested concept. Cover the underlying principles, common variations, and trade-offs. Use diagrams-in-words where helpful. Recommend what to study next.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Provides in-depth explanations of specific concepts."
                },
                new PromptItem()
                {
                    Title = "Practice Problem Generator",
                    PromptContent = "As a Practice Problem Generator, generate the specified number of practice problems on the requested topic ranging from easy to hard. For each problem, provide the expected difficulty, key skills tested, and a full solution with explanation.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Creates graded practice problems with solutions."
                },
                new PromptItem()
                {
                    Title = "Learning Roadmap",
                    PromptContent = "As a Learning Roadmap, build a learning roadmap for the specified skill or topic over the stated timeframe. Break the journey into phases. List resources, projects, and milestones. Include checkpoints to assess progress.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Plans structured learning journeys with milestones."
                },
                new PromptItem()
                {
                    Title = "Flashcard Maker",
                    PromptContent = "As a Flashcard Maker, create a set of flashcards on the requested topic. For each card, provide a concise question on the front and a clear answer on the back. Group cards by sub-topic and difficulty.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Builds study flashcards with questions and answers."
                },
                new PromptItem()
                {
                    Title = "Cheat Sheet Builder",
                    PromptContent = "As a Cheat Sheet Builder, build a one-page cheat sheet on the requested topic. Include the most important commands, formulas, definitions, and shortcuts. Organize for quick reference with clear visual hierarchy.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Condenses a topic into a quick-reference cheat sheet."
                },
                new PromptItem()
                {
                    Title = "ELI5 Explainer",
                    PromptContent = "As an ELI5 Explainer, explain the requested topic like the audience is five years old. Use simple words, a relatable story, and a fun analogy. Avoid technical terms unless absolutely necessary. Keep it short and engaging.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Simplifies complex topics for the youngest audience."
                },
                new PromptItem()
                {
                    Title = "Socratic Tutor",
                    PromptContent = "As a Socratic Tutor, help learn the requested topic using the Socratic method. Ask one question at a time to surface what is already known. Adapt the next question based on the answer. Reveal the answer only when asked.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Teaches through guided Socratic questioning."
                },
                new PromptItem()
                {
                    Title = "Resource Recommender",
                    PromptContent = "As a Resource Recommender, recommend the best resources to learn the requested topic. Include a mix of books, courses, videos, and hands-on projects. For each recommendation, note the skill level and time commitment.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Curates high-quality learning resources by level."
                },
                new PromptItem()
                {
                    Title = "Knowledge Check Quiz",
                    PromptContent = "As a Knowledge Check Quiz, quiz on the requested topic with the specified number of questions of increasing difficulty. Provide immediate feedback on each answer, including a brief explanation. End with a summary of areas to review.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Administers quizzes with feedback and review pointers."
                },
                new PromptItem()
                {
                    Title = "Analogy Generator",
                    PromptContent = "As an Analogy Generator, help understand the requested concept by providing three different analogies. Choose analogies from everyday life, sports, and nature. Explain how each analogy maps to the concept.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Generates multiple analogies to explain a concept."
                },
                new PromptItem()
                {
                    Title = "Concept Comparison",
                    PromptContent = "As a Concept Comparison, compare and contrast the two specified concepts. Highlight key similarities and differences. Use a side-by-side table and short explanations. Suggest when to choose one over the other.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Teaches through side-by-side concept comparisons."
                },
                new PromptItem()
                {
                    Title = "Skill Gap Diagnoser",
                    PromptContent = "As a Skill Gap Diagnoser, help identify skill gaps in the specified area. Ask a series of targeted questions. Based on the answers, summarize strengths, gaps, and a focused plan to close the gaps.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Diagnoses skill gaps and recommends next steps."
                },
                new PromptItem()
                {
                    Title = "Mnemonic Creator",
                    PromptContent = "As a Mnemonic Creator, create memorable mnemonics to help remember the specified list of items or steps. Use vivid imagery, rhymes, or acronyms where helpful. Test each mnemonic with a quick recall exercise.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Designs mnemonics to aid memorization."
                },
                new PromptItem()
                {
                    Title = "Hands-On Project Coach",
                    PromptContent = "As a Hands-On Project Coach, guide through building a hands-on project to learn the requested topic. Define the project scope, break it into ordered tasks, and explain key concepts as they arise. Suggest checkpoints and extensions.",
                    Section = "Prompt Topics",
                    Topic = "Learn",
                    Description = "Coaches project-based learning from start to finish."
                }
            };

            foreach (var prompt in learnPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds optimize-related prompts
        /// </summary>
        private void AddOptimizePrompts()
        {
            PromptItem[] optimizePrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Performance Profiler",
                    PromptContent = "As a Performance Profiler, review the following code in the specified language for performance. Identify hot spots, unnecessary allocations, and inefficient algorithms. Suggest specific optimizations with before-and-after examples.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Diagnoses and fixes performance bottlenecks in code."
                },
                new PromptItem()
                {
                    Title = "Database Query Tuner",
                    PromptContent = "As a Database Query Tuner, optimize the following SQL query for performance. Analyze the execution plan, suggest indexes, and rewrite the query where helpful. Explain the expected impact of each change.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Tunes SQL queries and recommends indexes."
                },
                new PromptItem()
                {
                    Title = "Cost Reduction Advisor",
                    PromptContent = "As a Cost Reduction Advisor, analyze the described cloud architecture for cost optimization. Identify underutilized resources, oversized instances, and storage waste. Recommend concrete actions with estimated savings.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Reduces cloud spend through targeted optimizations."
                },
                new PromptItem()
                {
                    Title = "Conversion Rate Optimizer",
                    PromptContent = "As a Conversion Rate Optimizer, audit the following landing page for conversion. Evaluate headline, value proposition, social proof, and call to action. Suggest specific changes expected to improve conversion rate.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Improves landing pages for higher conversion."
                },
                new PromptItem()
                {
                    Title = "SEO Enhancer",
                    PromptContent = "As an SEO Enhancer, optimize the following webpage for search engines. Suggest improvements to title, meta, headings, content, internal links, and structured data. Prioritize by expected impact.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Boosts on-page SEO with prioritized improvements."
                },
                new PromptItem()
                {
                    Title = "Email Subject Optimizer",
                    PromptContent = "As an Email Subject Optimizer, suggest the specified number of optimized subject line options for the following email. Aim for higher open rates. Briefly explain the rationale for each, including length, urgency, and personalization.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Generates higher-impact email subject lines."
                },
                new PromptItem()
                {
                    Title = "Workflow Streamliner",
                    PromptContent = "As a Workflow Streamliner, review the described workflow for inefficiencies. Identify handoff delays, redundant approvals, and manual steps. Recommend a streamlined flow with automation opportunities.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Streamlines operational workflows for speed."
                },
                new PromptItem()
                {
                    Title = "Bundle Size Reducer",
                    PromptContent = "As a Bundle Size Reducer, analyze the following web or mobile application bundle. Identify the largest dependencies, unused code, and lazy-loading opportunities. Recommend concrete size-reduction strategies.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Reduces application bundle size and load time."
                },
                new PromptItem()
                {
                    Title = "Image Optimizer Guide",
                    PromptContent = "As an Image Optimizer Guide, recommend image optimization techniques for the following use case. Cover format selection, compression, responsive sizing, and lazy loading. Provide target quality and size budgets.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Improves image performance and quality trade-offs."
                },
                new PromptItem()
                {
                    Title = "Ad Copy Optimizer",
                    PromptContent = "As an Ad Copy Optimizer, optimize the following ad copy for higher click-through and conversion. Test variations of headline, description, and call to action. Recommend a winner and explain why.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Improves ad copy through structured variations."
                },
                new PromptItem()
                {
                    Title = "CI Pipeline Optimizer",
                    PromptContent = "As a CI Pipeline Optimizer, review the following CI pipeline configuration for speed and reliability. Identify slow steps, redundant jobs, and flaky tests. Recommend caching, parallelization, and structural improvements.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Speeds up and stabilizes CI pipelines."
                },
                new PromptItem()
                {
                    Title = "Memory Leak Investigator",
                    PromptContent = "As a Memory Leak Investigator, help investigate a suspected memory leak in the following application written in the specified language. Suggest diagnostic steps, profiling tools, and likely causes. Recommend a fix and a regression test.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Diagnoses and resolves memory leaks."
                },
                new PromptItem()
                {
                    Title = "Cache Strategy Designer",
                    PromptContent = "As a Cache Strategy Designer, design a caching strategy for the specified system or feature. Choose appropriate cache layers, eviction policies, and invalidation approaches. Address consistency and cold-start concerns.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Designs effective caching strategies for systems."
                },
                new PromptItem()
                {
                    Title = "UX Flow Optimizer",
                    PromptContent = "As a UX Flow Optimizer, review the described user flow for friction. Identify unnecessary steps, confusing labels, and decision points. Recommend a simplified flow with rationale and success metrics.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Simplifies user flows to reduce friction."
                },
                new PromptItem()
                {
                    Title = "Build Time Reducer",
                    PromptContent = "As a Build Time Reducer, suggest build-time reductions via dependency optimization, incremental builds, parallelization, caching, and impact estimates.",
                    Section = "Prompt Topics",
                    Topic = "Optimize",
                    Description = "Reduces project build and compile times."
                }
            };

            foreach (var prompt in optimizePrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds learning-related prompts
        /// </summary>
        private void AddLearningPrompts()
        {
            PromptItem[] learningPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "AI Learning Advisor",
                    PromptContent = "Act as an AI learning advisor and create a personalized learning path for machine learning. Recommend topics based on skill level and learning goals. Include learning resources, projects, and practice exercises. Provide milestones to track progress effectively.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Builds a structured machine learning curriculum based on goals."
                },
                new PromptItem()
                {
                    Title = "Skill Assessment Coach",
                    PromptContent = "Assess my proficiency in C# and recommend improvements. Identify strengths, weaknesses, and knowledge gaps. Suggest learning resources and practical exercises. Create a plan to improve technical proficiency.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Identifies strengths and weaknesses in technical skills."
                },
                new PromptItem()
                {
                    Title = "Certification Planner",
                    PromptContent = "Create an Azure Developer certification study plan with modules, labs, resources, practice tests, and milestones.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Generates preparation plans for professional certifications."
                },
                new PromptItem()
                {
                    Title = "Study Schedule Builder",
                    PromptContent = "Generate a 30-day study schedule for MAUI development. Organize daily learning objectives and practice activities. Include project-based exercises to reinforce concepts. Track progress using weekly milestones and goals.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Creates daily learning plans with milestones."
                },
                new PromptItem()
                {
                    Title = "Knowledge Gap Analyzer",
                    PromptContent = "Analyze gaps in my software architecture knowledge. Identify missing concepts, patterns, and best practices. Recommend topics and resources to strengthen understanding. Provide a prioritized learning plan for improvement.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Evaluates missing concepts and suggests study resources."
                },
                new PromptItem()
                {
                    Title = "Language Learning Path",
                    PromptContent = "Create a personalized learning path for learning Spanish. Include speaking, listening, reading, and writing skills. Recommend apps, courses, and practice methods. Set realistic proficiency milestones.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Plans comprehensive language acquisition programs."
                },
                new PromptItem()
                {
                    Title = "Technical Mentoring Guide",
                    PromptContent = "Develop a mentoring plan for junior developers in the team. Identify key skills to develop. Create project assignments and code review strategies. Establish feedback and growth tracking methods.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Structures mentoring relationships for skill development."
                },
                new PromptItem()
                {
                    Title = "Online Course Recommender",
                    PromptContent = "Recommend online courses for learning cloud architecture. Filter by skill level, time commitment, and learning style. Include course reviews, cost, and certification options.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Curates online learning resources and courses."
                },
                new PromptItem()
                {
                    Title = "Hands-On Project Designer",
                    PromptContent = "Design practical projects for learning React development. Create project briefs with learning objectives. Include starter code, step-by-step tasks, and extension challenges.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Creates project-based learning experiences."
                },
                new PromptItem()
                {
                    Title = "Concept Explanation Master",
                    PromptContent = "Explain complex concepts in software design patterns. Break down abstract ideas into relatable analogies. Provide code examples and real-world applications. Build mental models.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Simplifies technical concepts for learning."
                },
                new PromptItem()
                {
                    Title = "Documentation Study Guide",
                    PromptContent = "Create a study guide for Microsoft API documentation. Highlight key concepts, examples, and common patterns. Include practice exercises and review questions.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Transforms technical documentation into study materials."
                },
                new PromptItem()
                {
                    Title = "Skill Transfer Program",
                    PromptContent = "Design a skill transfer program from waterfall to agile methodology. Include training modules, workshops, and practical exercises. Create adoption timeline and resistance management strategies.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Plans organizational skill transformation programs."
                },
                new PromptItem()
                {
                    Title = "Code Review Learning",
                    PromptContent = "Create a code review learning plan to improve code quality understanding. Include what to look for in reviews, common issues, and best practices. Provide examples and exercises.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Develops code review expertise systematically."
                },
                new PromptItem()
                {
                    Title = "Architecture Pattern Study",
                    PromptContent = "Create a comprehensive study plan for enterprise architecture patterns. Include pattern types, use cases, and implementation strategies. Provide design exercises and project applications.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Structures learning of software architecture patterns."
                },
                new PromptItem()
                {
                    Title = "Best Practices Collection",
                    PromptContent = "Compile best practices from industry standards for API design. Include security, performance, versioning, and documentation standards. Create checklist for implementation.",
                    Section = "Agent Prompts",
                    Topic = "Learning",
                    Description = "Collects and organizes industry best practices."
                }
            };

            foreach (var prompt in learningPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds career-related prompts
        /// </summary>
        private void AddCareerPrompts()
        {
            PromptItem[] careerPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Career Coach",
                    PromptContent = "Developer-to-Solution Architect roadmap: skills, experience, certifications, learning resources, and milestones.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Provides actionable steps toward targeted career roles."
                },
                new PromptItem()
                {
                    Title = "Resume Reviewer",
                    PromptContent = "Review my resume and suggest improvements. Identify strengths, weaknesses, and missing information. Recommend changes to increase interview opportunities. Provide an improved version of key resume sections.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Identifies opportunities to strengthen resumes."
                },
                new PromptItem()
                {
                    Title = "Interview Preparation Expert",
                    PromptContent = "Generate common .NET architect interview questions. Include technical, design, and leadership topics. Provide sample answers with explanations. Suggest preparation tips for interview success.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Creates realistic interview scenarios."
                },
                new PromptItem()
                {
                    Title = "Promotion Strategy Advisor",
                    PromptContent = "Create a promotion strategy for my current role. Identify skills, achievements, and responsibilities to demonstrate. Recommend ways to increase visibility and business impact. Provide an actionable plan with measurable milestones.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Helps users prepare for career advancement."
                },
                new PromptItem()
                {
                    Title = "Personal Branding Coach",
                    PromptContent = "Help create a strong LinkedIn professional profile. Optimize the headline, summary, and experience sections. Highlight achievements, skills, and professional strengths. Recommend strategies to improve online visibility and networking.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Improves online professional presence."
                },
                new PromptItem()
                {
                    Title = "Salary Negotiation Advisor",
                    PromptContent = "Prepare for salary negotiation for a new position. Provide market rate research for the role and location. Suggest negotiation tactics and talking points. Recommend benefits to negotiate beyond salary.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Guides salary negotiation preparation."
                },
                new PromptItem()
                {
                    Title = "Job Search Strategy Planner",
                    PromptContent = "Create a job search strategy for transitioning to senior management. Identify target companies and roles. Recommend networking strategies and interview preparation. Set timeline and success metrics.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Plans systematic job search approaches."
                },
                new PromptItem()
                {
                    Title = "Leadership Development Coach",
                    PromptContent = "Develop leadership skills for future manager role. Identify key competencies and development areas. Recommend mentoring, training, and practical experiences. Create 6-month development plan.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Structures leadership skill development."
                },
                new PromptItem()
                {
                    Title = "Portfolio Builder Advisor",
                    PromptContent = "Create a professional portfolio showcasing technical work. Include project descriptions, contributions, and outcomes. Recommend portfolio platforms and presentation strategies.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Guides creation of professional portfolios."
                },
                new PromptItem()
                {
                    Title = "Industry Transition Guide",
                    PromptContent = "Plan career transition from finance to technology sector. Identify transferable skills and skill gaps. Recommend roles that leverage existing expertise. Create transition timeline.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Facilitates career transitions between industries."
                },
                new PromptItem()
                {
                    Title = "Speaking Opportunity Finder",
                    PromptContent = "Identify speaking opportunities to build thought leadership. Research conferences, webinars, and podcasts in specialization. Create speaker pitch and presentation ideas.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Identifies platforms for professional visibility."
                },
                new PromptItem()
                {
                    Title = "Networking Strategy Developer",
                    PromptContent = "Develop networking strategy for expanding professional connections. Identify industry events, groups, and online communities. Create outreach plan and conversation starters.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Plans strategic professional networking."
                },
                new PromptItem()
                {
                    Title = "Freelancing Guide",
                    PromptContent = "Transition to freelancing from full-time employment. Identify marketable skills and service offerings. Recommend platforms and pricing strategies. Create business plan.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Guides freelancing career transitions."
                },
                new PromptItem()
                {
                    Title = "Performance Review Prep",
                    PromptContent = "Prepare for annual performance review with manager. Document achievements, goals met, and challenges. Create talking points highlighting contributions. Prepare discussion on growth opportunities.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Prepares for performance discussions."
                },
                new PromptItem()
                {
                    Title = "Side Project Strategist",
                    PromptContent = "Develop side project strategy aligned with career goals. Identify project ideas that build marketable skills. Create execution plan with time management. Define learning objectives.",
                    Section = "Agent Prompts",
                    Topic = "Career",
                    Description = "Plans career-building side projects."
                }
            };

            foreach (var prompt in careerPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds planning-related prompts
        /// </summary>
        private void AddPlanningPrompts()
        {
            PromptItem[] planningPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Plan My Day",
                    PromptContent = "Plan my day around meetings and development work. Balance focus time, collaboration, and personal productivity. Prioritize important tasks and deadlines. Create a realistic schedule with time allocations.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Creates balanced schedules for productivity."
                },
                new PromptItem()
                {
                    Title = "Weekly Goal Planner",
                    PromptContent = "Generate goals for the upcoming work week. Prioritize tasks based on importance and urgency. Break larger objectives into manageable actions. Include measurable outcomes for tracking progress.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Breaks large objectives into weekly goals."
                },
                new PromptItem()
                {
                    Title = "Sprint Organizer",
                    PromptContent = "Plan tasks for a two-week agile sprint. Organize work based on priorities and team capacity. Identify dependencies, risks, and deliverables. Provide a structured sprint backlog and timeline.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Organizes agile development tasks efficiently."
                },
                new PromptItem()
                {
                    Title = "Focus Block Scheduler",
                    PromptContent = "Create focus sessions for deep-work activities. Minimize interruptions and context switching. Schedule dedicated blocks for high-priority tasks. Recommend breaks to maintain productivity and energy.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Schedules uninterrupted work periods."
                },
                new PromptItem()
                {
                    Title = "Project Roadmap Creator",
                    PromptContent = "Create a roadmap for a mobile application project. Define key phases, milestones, and deliverables. Identify dependencies, risks, and success criteria. Provide a timeline from planning to deployment.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Generates project milestones and deliverables."
                },
                new PromptItem()
                {
                    Title = "Quarterly Planning Strategist",
                    PromptContent = "Create quarterly planning for product development team. Set OKRs, prioritize initiatives, and allocate resources. Identify risks and mitigation strategies. Create communication plan.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Plans organizational quarterly objectives."
                },
                new PromptItem()
                {
                    Title = "Meeting Agenda Organizer",
                    PromptContent = "Create structured agendas for weekly team meetings. Include discussion topics, time allocations, and expected outcomes. Assign discussion leads and prepare discussion questions.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Organizes productive meetings."
                },
                new PromptItem()
                {
                    Title = "Vacation Planning Assistant",
                    PromptContent = "Plan a two-week vacation covering travel, accommodation, and activities. Create day-by-day itinerary balancing activities and rest. Estimate budget and identify booking priorities.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Creates comprehensive travel plans."
                },
                new PromptItem()
                {
                    Title = "Personal Project Timeline",
                    PromptContent = "Create realistic timeline for completing home renovation project. Break project into phases with dependencies. Estimate time for each phase and identify resource needs. Plan contingencies.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Plans personal projects systematically."
                },
                new PromptItem()
                {
                    Title = "Event Planning Coordinator",
                    PromptContent = "Plan a corporate conference for 500 attendees. Create event timeline, budget, and logistics plan. Identify vendors, schedule, and contingencies. Plan marketing and registration strategy.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Coordinates large event planning."
                },
                new PromptItem()
                {
                    Title = "Product Launch Planner",
                    PromptContent = "Plan software product launch across multiple markets. Create pre-launch, launch, and post-launch timelines. Coordinate marketing, sales, and support preparation. Identify key milestones and success metrics.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Plans go-to-market strategies."
                },
                new PromptItem()
                {
                    Title = "Training Program Scheduler",
                    PromptContent = "Schedule employee training program for the year. Identify training topics, timing, and delivery methods. Coordinate trainer availability and venue scheduling. Create tracking and assessment plan.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Organizes corporate training programs."
                },
                new PromptItem()
                {
                    Title = "Risk Management Planner",
                    PromptContent = "Create risk management plan for project implementation. Identify potential risks, likelihood, and impact. Develop mitigation strategies and contingency plans. Create monitoring and response procedures.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Plans risk identification and mitigation."
                },
                new PromptItem()
                {
                    Title = "Resource Allocation Advisor",
                    PromptContent = "Plan resource allocation across multiple concurrent projects. Match team members to projects based on skills. Balance workload and identify resource conflicts. Create resource utilization plan.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Optimizes team resource planning."
                },
                new PromptItem()
                {
                    Title = "Budget Planning Specialist",
                    PromptContent = "Create annual budget plan for engineering department. Estimate costs for salaries, tools, training, and equipment. Align with business goals and growth plans. Justify budget allocations.",
                    Section = "Agent Prompts",
                    Topic = "Planning",
                    Description = "Develops comprehensive budget plans."
                }
            };

            foreach (var prompt in planningPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds prompt improvement-related prompts
        /// </summary>
        private void AddPromptImprovementPrompts()
        {
            PromptItem[] promptImprovePrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Prompt Coach",
                    PromptContent = "Improve this AI prompt for better results. Identify unclear or missing information. Suggest improvements for clarity and context. Provide an optimized version of the prompt.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Enhances clarity and prompt effectiveness."
                },
                new PromptItem()
                {
                    Title = "Prompt Optimizer",
                    PromptContent = "Rewrite this prompt using best prompting practices. Improve structure, specificity, and instructions. Reduce ambiguity while preserving the original intent. Provide the final optimized prompt.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Applies advanced prompt engineering techniques."
                },
                new PromptItem()
                {
                    Title = "Prompt Quality Evaluator",
                    PromptContent = "Evaluate prompt quality and score it. Analyze clarity, completeness, and effectiveness. Identify strengths and areas for improvement. Provide recommendations along with a score.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Reviews prompt strengths and weaknesses."
                },
                new PromptItem()
                {
                    Title = "Prompt Simplifier",
                    PromptContent = "Simplify a complex prompt without losing intent. Remove unnecessary wording and complexity. Retain all important requirements and context. Provide a shorter and clearer version.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Makes prompts easier to understand and use."
                },
                new PromptItem()
                {
                    Title = "Prompt Expansion Assistant",
                    PromptContent = "Expand this prompt with context and examples. Add missing details that improve response quality. Include sample inputs, outputs, or use cases. Return an enriched and comprehensive prompt.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Adds details and context to improve responses."
                },
                new PromptItem()
                {
                    Title = "Few-Shot Example Creator",
                    PromptContent = "Create few-shot examples for a prompt about code review. Include sample inputs and desired outputs. Help AI understand expected response format and quality level.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Develops example-based prompt guidance."
                },
                new PromptItem()
                {
                    Title = "Context Setter",
                    PromptContent = "Add necessary context to a prompt for better AI responses. Include background information and relevant constraints. Establish tone and style expectations. Improve response accuracy.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Provides context for improved AI understanding."
                },
                new PromptItem()
                {
                    Title = "Instruction Clarity Reviewer",
                    PromptContent = "Review instructions in prompts for clarity and completeness. Identify ambiguous phrases and missing steps. Restructure for logical flow. Ensure instructions are actionable.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Improves instruction clarity and completeness."
                },
                new PromptItem()
                {
                    Title = "Chain-of-Thought Enhancer",
                    PromptContent = "Enhance a prompt with chain-of-thought reasoning. Break down complex tasks into logical steps. Guide AI through reasoning process. Improve response quality and transparency.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Structures reasoning processes in prompts."
                },
                new PromptItem()
                {
                    Title = "Output Format Specifier",
                    PromptContent = "Add specific output format requirements to a prompt. Define desired structure, language, and style. Include format examples. Ensure consistent response formatting.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Specifies expected output structure."
                },
                new PromptItem()
                {
                    Title = "Constraint Adder",
                    PromptContent = "Add constraints and limitations to refocus a prompt. Define scope boundaries. Specify what to exclude or avoid. Reduce off-topic responses.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Adds boundaries to prompt scope."
                },
                new PromptItem()
                {
                    Title = "Tone Adjuster",
                    PromptContent = "Adjust tone and style in a prompt for different audiences. Modify formality level, language complexity, and perspective. Create variations for different contexts.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Adapts prompt tone for different audiences."
                },
                new PromptItem()
                {
                    Title = "Prompt Testing Assistant",
                    PromptContent = "Create test cases for evaluating prompt effectiveness. Develop diverse input examples. Define success criteria and evaluation metrics. Plan testing strategy.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Develops prompt testing strategies."
                },
                new PromptItem()
                {
                    Title = "Bias Detector",
                    PromptContent = "Analyze prompt for potential biases and fairness issues. Identify loaded language or assumptions. Suggest neutral alternatives. Ensure inclusive phrasing.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Identifies and removes bias from prompts."
                },
                new PromptItem()
                {
                    Title = "Version Controller",
                    PromptContent = "Create and manage versions of evolving prompts. Track changes and improvements over time. Document what worked and what didn't. Maintain prompt version history.",
                    Section = "Agent Prompts",
                    Topic = "Prompt Improvement",
                    Description = "Manages prompt iteration and versioning."
                }
            };

            foreach (var prompt in promptImprovePrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        /// <summary>
        /// Adds goals-related prompts
        /// </summary>
        private void AddGoalsPrompts()
        {
            PromptItem[] goalsPrompts = new[]
            {
                new PromptItem()
                {
                    Title = "Viva Goals Assistant",
                    PromptContent = "Create quarterly OKRs for a development team. Define measurable objectives and key results. Align goals with business and technical priorities. Include success criteria and expected outcomes.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Builds measurable objectives and key results."
                },
                new PromptItem()
                {
                    Title = "Team Goal Planner",
                    PromptContent = "Generate team goals for the next quarter. Focus on productivity, quality, and collaboration. Ensure goals are realistic and measurable. Organize them with priorities and milestones.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Creates prioritized team goals for the quarter."
                },
                new PromptItem()
                {
                    Title = "Personal Goal Tracker",
                    PromptContent = "Define personal growth goals for six months. Identify key areas for development and improvement. Create measurable milestones and success criteria. Suggest a plan to track progress consistently.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Helps structure personal development goals."
                },
                new PromptItem()
                {
                    Title = "KPI Advisor",
                    PromptContent = "Suggest KPIs for customer satisfaction initiatives. Recommend meaningful and measurable metrics. Explain how each KPI supports business objectives. Include methods for tracking and reporting results.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Recommends measurable indicators for success."
                },
                new PromptItem()
                {
                    Title = "Goal Alignment Checker",
                    PromptContent = "Review whether team goals align with business objectives. Identify gaps, conflicts, or missing priorities. Recommend improvements to strengthen alignment. Provide a summary of findings and next actions.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Verifies alignment between goals and strategy."
                },
                new PromptItem()
                {
                    Title = "SMART Goal Converter",
                    PromptContent = "Convert vague goals into SMART goals. Make objectives specific, measurable, achievable, relevant, and time-bound. Clarify success criteria and measurement methods.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Transforms goals into SMART format."
                },
                new PromptItem()
                {
                    Title = "Goal Dependency Mapper",
                    PromptContent = "Identify dependencies between different goals. Create dependency diagram showing relationships. Sequence goal achievement appropriately. Identify potential conflicts.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Maps dependencies between objectives."
                },
                new PromptItem()
                {
                    Title = "Progress Tracker Designer",
                    PromptContent = "Create tracking system for monitoring goal progress. Define metrics and reporting frequency. Design dashboards for visibility. Establish review cadence and adjustment process.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Designs systems for monitoring goal progress."
                },
                new PromptItem()
                {
                    Title = "Goal Communication Plan",
                    PromptContent = "Create communication plan for company-wide goals. Draft messaging for different audiences. Plan cascading goal communication. Ensure alignment and engagement.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Prepares messaging for goal rollout."
                },
                new PromptItem()
                {
                    Title = "Stretch Goal Challenger",
                    PromptContent = "Develop stretch goals pushing beyond comfort zone. Balance ambition with achievability. Create development plan for stretch goals. Identify learning opportunities.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Creates ambitious but achievable objectives."
                },
                new PromptItem()
                {
                    Title = "Goal Failure Analysis",
                    PromptContent = "Analyze why previous goals were not achieved. Identify root causes and contributing factors. Recommend improvements to goal setting process. Apply lessons to future goals.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Analyzes missed objectives to improve planning."
                },
                new PromptItem()
                {
                    Title = "Team Motivation Motivator",
                    PromptContent = "Create motivation strategies around team goals. Link individual work to goal achievement. Celebrate milestone progress. Maintain team engagement.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Designs incentives and recognition plans."
                },
                new PromptItem()
                {
                    Title = "Annual Review Preparer",
                    PromptContent = "Prepare for annual review focusing on goal achievement. Document goal progress and outcomes. Prepare examples of impact. Plan discussion points with manager.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Prepares evidence of goal accomplishments."
                },
                new PromptItem()
                {
                    Title = "Cross-Team Goal Synchronizer",
                    PromptContent = "Synchronize goals across dependent teams. Identify collaboration points and handoffs. Resolve conflicts and dependencies. Create coordination plan.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Aligns objectives across teams."
                },
                new PromptItem()
                {
                    Title = "Goal Retrospective Facilitator",
                    PromptContent = "Create retrospective process for completed goals. Analyze what worked and what didn't. Capture lessons learned. Share best practices across organization.",
                    Section = "Agent Prompts",
                    Topic = "Goals",
                    Description = "Facilitates learning from goal outcomes."
                }
            };

            foreach (var prompt in goalsPrompts)
            {
                this.promptItemsInfo.Add(prompt);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
