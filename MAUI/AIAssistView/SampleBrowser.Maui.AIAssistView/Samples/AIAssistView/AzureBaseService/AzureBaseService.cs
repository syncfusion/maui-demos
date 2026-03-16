#region Copyright Syncfusion® Inc. 2001-2026.
// Copyright Syncfusion® Inc. 2001-2026. All rights reserved.
// Use of this code is subject to the terms of our license.
// A copy of the current license can be obtained at any time by e-mailing
// licensing@syncfusion.com. Any infringement will be prosecuted under
// applicable laws. 
#endregion
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.AI;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SampleBrowser.Maui.AIAssistView.SfAIAssistView
{
    public abstract class AzureBaseService
    {
        #region Fields

        /// <summary>
        /// The EndPoint
        /// </summary>
        internal const string endpoint = "YOUR_END_POINT_NAME";

        /// <summary>
        /// The Deployment name
        /// </summary>
        internal const string deploymentName = "DEPLOYMENT_NAME";

        /// <summary>
        /// The Image Deployment name
        /// </summary>
        internal const string imageDeploymentName = "IMAGE_DEPOLYMENT_NAME";

        /// <summary>
        /// The API key
        /// </summary>
        internal const string key = "API_KEY";

        /// <summary>
        /// The kernal
        /// </summary>
        private IChatClient? client;

        /// <summary>
        /// The credential valid field
        /// </summary>
        private static bool isCredentialValid = false;

        /// <summary>
        /// The already credential validated field
        /// </summary>
        private static bool isAlreadyValidated = false;

        /// <summary>
        /// The chat histroy
        /// </summary>
        private string? chatHistory;

        /// <summary>
        /// The uri result field
        /// </summary>
        //private Uri? uriResult;

        #endregion

        public AzureBaseService()
        {
            ValidateCredential();
        }

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating the chat history object
        /// </summary>
        public string? ChatHistory
        {
            get
            {
                return chatHistory;
            }
            set
            {
                chatHistory = value;
            }
        }

        /// <summary>
        /// Gets or Set a value indicating whether an credentials are valid or not.
        /// Returns <c>true</c> if the credentials are valid; otherwise, <c>false</c>.
        /// </summary>
        public static bool IsCredentialValid
        {
            get
            {
                return isCredentialValid;
            }
            set
            {
                isCredentialValid = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the kernal object
        /// </summary>
        public IChatClient? Client
        {
            get
            {
                return client;
            }
            set
            {
                client = value;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validate Azure Credentials
        /// </summary>
        private async void ValidateCredential()
        {
            #region Azure OpenAI
            // Use below method for Azure Open AI
            this.GetAzureOpenAIKernal();
            #endregion

            #endregion

            if (isAlreadyValidated)
            {
                return;
            }

            try
            {
                if (Client != null)
                {
                    await Client.GetResponseAsync("Hello, Test Check");
                }
            }
            catch (Exception)
            {
                // Handle any exceptions that indicate the credentials or endpoint are invalid.               
                //ShowAlertAsync();
                return;
            }

            IsCredentialValid = true;
            isAlreadyValidated = true;
        }

        #region Azure OpenAI
        /// <summary>
        /// To get the Azure open ai kernal method
        /// </summary>
        private void GetAzureOpenAIKernal()
        {
            try
            {
                var client = new AzureOpenAIClient(new Uri(endpoint), new AzureKeyCredential(key)).GetChatClient(deploymentName).AsIChatClient();
                this.client = client;
            }
            catch (Exception)
            {
                ShowAlertAsync();
            }
        }

        /// <summary>
        /// Show Alert Popup
        /// </summary>
        private async void ShowAlertAsync()
        {
            var page = Application.Current?.Windows[0].Page;
            if (page != null && !IsCredentialValid)
            {
                isAlreadyValidated = true;
                await page.DisplayAlertAsync("Alert", "The Azure API key or endpoint is missing or incorrect. Please verify your credentials. You can also continue with the offline data.", "OK");
            }
        }

        #endregion
    }
    public class AzureAIService : AzureBaseService
    {
        public AzureAIService()
        {
            InitializeClient();
        }

        public void InitializeClient()
        {
            if (IsCredentialValid && Client != null)
            {
                ChatHistory = ChatHistory + "You are a helpful, intelligent and conversational assistant that can assit with a wide variety of topics.";
            }
        }

        internal async Task<string> GetResultsFromAI(string userPrompt, string userAIPrompt)
        {
            if (IsCredentialValid && Client !=null)
            {
                try
                {
                    ChatHistory = ChatHistory + userAIPrompt;
                    var chatresponse = await Client.GetResponseAsync(userPrompt);
                    return chatresponse.ToString();
                }
                catch
                {
                    var response = this.GetSolutionToPrompt(userPrompt);
                    return response;
                }
            }
            else
            {
                var response = this.GetSolutionToPrompt(userPrompt);
                return response;
            }

        }

        internal async Task<string> GetResultsFromAIForMarkdown(string userPrompt, string userAIPrompt)
        {
            if (IsCredentialValid && Client != null)
            {
                try
                {
                    ChatHistory = ChatHistory + userAIPrompt;
                    var chatresponse = await Client.GetResponseAsync(userPrompt);
                    return chatresponse.ToString();
                }
                catch
                {
                    var response = this.GetSolutionToPromptForMarkDown(userPrompt);
                    return response;
                }
            }
            else
            {
                var response = this.GetSolutionToPromptForMarkDown(userPrompt);
                return response;
            }

        }

        #region Offline Data generation
        internal string GetSolutionToPromptForMarkDown(string prompt)
        {
            if (prompt != null)
            {
                for (int i = 0; i < promptRequestForMarkdown.Count(); i++)
                {
                    if (promptRequestForMarkdown[i].ToLower().Contains(prompt.ToLower()))
                    {
                        return promptResponseHtmlForMarkdown[i];
                    }
                }
            }
            return "Please connect to your preferred AI service for real-time queries.";
        }

        internal string GetSolutionToPrompt(string prompt)
        {
            if (!string.IsNullOrWhiteSpace(prompt))
            {
                for (int i = 0; i < promptRequest.Count(); i++)
                {
                    string cleaned = Regex.Replace(prompt, @"[^\w\s]", "");
                    string pattern = $@"\b{Regex.Escape(cleaned)}\b";
                    var isMatch = Regex.IsMatch(promptRequest[i], pattern, RegexOptions.IgnoreCase);

                    if (isMatch)
                    {
                        return promptResponseHtml[i];
                    }
                }
            }
            return "Please connect to your preferred AI service for real-time queries.";
        }

        #endregion

        #region Prompts
        private string[] promptRequest = new string[]
        {
            "Characteristics of Ownership",
            "brainstorming",
            "Types of Listening",
            "resilience",
            "initiative",
            "responsibility",
            "accountability",
            "different perspective",
            "more ideas",
            "active listening",
            "passive listening",
            "compare online and offline marketing strategies",
            "why should i set achievable goals at work?",
            "write a joke that my coworkers would find funny",
            "why do people fly in their dreams?",
            "How to Improve Code Quality",
            "Best Practices for API Design",
            "Cloud Migration Strategy",
            "Database Optimization Tips",
            "Security Considerations",
            "Performance Tuning",
            "Testing Strategies",
            "DevOps Fundamentals",
            "Microservices Architecture",
            "Containerization with Docker",
            "What is time management",
            "How to improve productivity",
            "What are communication skills",
            "How to build a strong team",
            "What is project management",
            "How to handle stress",
            "What is effective problem solving",
            "How to make better decisions",
            "What is work-life balance",
            "How to achieve professional growth",
        };

        private string[] promptResponseHtml = new string[]
        {
            "<b>Characteristics of Ownership</b><ol><li>&nbsp;Ownership is about taking initiative.</li><li>&nbsp;It’s an understanding that taking action is your responsibility, not someone else’s.</li><li>&nbsp;It is the fundamental principle that you, as an individual, are accountable for the delivery of an outcome, even though there may be others who have a role to play.</li></ol>",
            "<b>Brainstorming</b><br>When a group of people sit and discuss a problem and try to derive the solution, this is termed brainstorming. Brainstorming helps team members, pool ideas and bounce them off each other, narrowing them down and refining them into a plan. It's useful for gaining different perspectives and more ideas.",
            "<b>Types of Listening</b><br>For a good communication, it is not only enough to convey the information efficiently, but it also needs to include good listening skill. Common types of Listening are Active listening and Passive listening.",
            "<b>Resilience</b><br>Resilience is the ability to bounce back from setbacks, adapt to difficult situations, and keep going despite adversity. It involves having the strength to confront challenges, learn from failures, and continue to grow without becoming overly discouraged. Essentially, resilience is about enduring tough times and emerging stronger.",
            "<b>Initiative</b><br>Taking the first step to start something or to bring new ideas and actions into motion. It means proactively beginning a task or project<ol><li><b>&nbsp;Definition:</b> Taking the first step to start a project, task, or initiative without waiting for direction or instruction.</li><li><b>&nbsp;Importance:</b> Demonstrates proactive behavior and leadership qualities. It shows an individual’s willingness to take charge and bring ideas to life.</li><li><b>&nbsp;Example:</b> An employee noticing a gap in the company's process and developing a plan to address it without being asked.</li></ol>",
            "<b>Responsibility</b><br>Being reliable and dependable in managing tasks or duties. It involves making decisions and completing tasks that one is assigned or has taken on.<ol><li><b>&nbsp;Definition:</b> Ability to be trusted with the duties and tasks associated with a particular role or project.</li><li><b>&nbsp;Importance:</b> Ensures that tasks are assigned and managed efficiently. Responsible individuals are committed to fulfilling their roles and meeting expectations.</li><li><b>&nbsp;Example:</b> A project manager overseeing the progress of a project, ensuring deadlines are met and resources are utilized effectively.</li></ol>",
            "<b>Accountability</b><br>Being answerable for the outcomes of your actions and decisions. It means acknowledging and accepting the consequences, whether they are successes or failures.<ol><li><b>&nbsp;Definition:</b> Being answerable for the outcomes of one's actions and decisions. This involves acknowledging both successes and failures.</li><li><b>&nbsp;Importance:</b> Encourages trust and transparency within a team or organization. It fosters a culture of continuous improvement and ethical behavior.</li><li><b>&nbsp;Example:</b> A team leader accepting responsibility for a project's failure, analyzing what went wrong, and developing strategies to avoid similar issues in the future.</li></ol>",
            "<b>Different Perspective</b><br>Each person has a different way of approaching a problem. It might differ with the way they see it, due to their unique understanding, way of thinking or experience. While brainstorming, participants can show each other different angles of a problem, giving everyone a better view of it.",
            "<b>More Ideas</b><br>When a group brainstorms, they can come up with a lot of ideas for a solution that don't stick to the usual approach. There are better chances of coming up with an innovative solution, as well as plan Bs and Cs. Sometimes, groups can try integrating the positives of each solution and can thus derive an effective solution for that problem.",
             "<b>Active Listening</b><br>Active listening is a two-way communication. The listener puts in active effort to understand the information conveyed by the speaker and react to it. ",
             "<b>Passive Listening</b><br>In passive listening, a person just tries to know what the speaker is trying to say. It is just like one way communication.",
            "<b>Compare online and offline marketing strategies</b><br>Sure, let’s compare online and offline marketing strategies across several key aspects:<br><br><b>Reach and Audience</b><br><br><b>Online Marketing:</b><ol><li> &nbsp;<b>Global Reach: </b>Can target a worldwide audience.</li><li>&nbsp;<b>Specific Targeting:</b> Allows precise targeting based on demographics, interests, and behaviors.</li></ol><br><b>Offline Marketing:</b><ol><li>&nbsp;<b>Local Reach:</b> Often more effective for local or regional audiences.</li><li>&nbsp;<b>Broad Targeting:</b> Reaches a wide audience but with less precision.</li></ol>",
            "<b>Why should I set achievable goals at work?</b><br>Setting achievable goals at work is a great way to stay motivated and productive.",
            "<b>Write a joke that my coworkers would find funny</b><br>Sure, here’s a lighthearted office joke for your coworkers:<br>Why did the scarecrow become an excellent employee?<br>Because he was outstanding in his field! 🌾😄<br>Hope that brings some smiles to the office! If you need more jokes or anything else, just let me know.",
            "<b>Why do people fly in their dreams?</b><br>Flying in dreams is a fascinating and common experience that often symbolizes various aspects of our subconscious mind. People may fly in their dreams for various symbolic and psychological reasons.",
            "<b>How to Improve Code Quality:</b><br>1. Follow SOLID principles (Single Responsibility, Open/Closed, Liskov Substitution, Interface Segregation, Dependency Inversion) to create maintainable and scalable code.<br>2. Write comprehensive unit tests with high coverage to catch bugs early and facilitate refactoring with confidence.<br>3. Conduct regular code reviews to share knowledge, identify issues, and maintain consistent coding standards across the team.<br>4. Use static analysis tools like SonarQube or StyleCop to automatically detect code smells, security vulnerabilities, and potential bugs.",
            "<b>Best Practices for API Design:</b><br>1. Use RESTful principles with clear resource models and proper HTTP methods (GET for retrieval, POST for creation, PUT for updates, DELETE for removal).<br>2. Implement API versioning (URL-based or header-based) to maintain backward compatibility while introducing new features.<br>3. Provide comprehensive API documentation using tools like Swagger/OpenAPI with examples, error codes, and authentication details.<br>4. Design consistent error responses, implement rate limiting, and use proper status codes to communicate API behavior clearly.",
            "<b>Cloud Migration Strategy:</b><br>1. Assess current infrastructure by documenting applications, dependencies, performance metrics, and security requirements in detail.<br>2. Choose appropriate cloud services (IaaS, PaaS, SaaS) based on workload requirements, cost analysis, and organizational expertise.<br>3. Create a phased migration plan starting with non-critical systems, conducting thorough testing at each stage, and maintaining rollback procedures.<br>4. Monitor post-migration performance, optimize resource allocation, and continuously review costs to ensure ROI.",
            "<b>Database Optimization Tips:</b><br>1. Implement proper indexing on frequently queried columns, but avoid over-indexing which can slow down write operations.<br>2. Optimize queries by using EXPLAIN plans, eliminating N+1 queries, and using appropriate JOIN strategies for your database system.<br>3. Implement connection pooling to reuse database connections efficiently and reduce overhead from establishing new connections.<br>4. Use caching strategies (Redis, Memcached) for frequently accessed data, and consider database partitioning for very large datasets to improve query performance.",
            "<b>Security Considerations:</b><br>1. Implement robust authentication mechanisms (OAuth 2.0, JWT) and authorization frameworks (RBAC, ABAC) to control access to sensitive resources.<br>2. Encrypt sensitive data both in transit (TLS/SSL) and at rest using industry-standard encryption algorithms like AES-256.<br>3. Validate all user inputs on the server-side to prevent injection attacks, XSS, and other common vulnerabilities.<br>4. Conduct regular security audits, penetration testing, and stay updated with security patches to protect against emerging threats.",
            "<b>Performance Tuning:</b><br>1. Profile your code using tools like dotTrace or profilers to identify bottlenecks and resource-intensive operations accurately.<br>2. Optimize algorithms by choosing appropriate data structures, reducing time complexity, and avoiding unnecessary computations.<br>3. Implement caching layers (distributed caches) and asynchronous processing to handle high loads and improve response times.<br>4. Use load balancing techniques and horizontal scaling to distribute traffic across multiple servers and ensure system reliability.",
            "<b>Testing Strategies:</b><br>1. Unit tests verify individual components in isolation, ensuring each function behaves correctly with various inputs and edge cases.<br>2. Integration tests validate interactions between multiple components, ensuring they work together as expected within the system.<br>3. End-to-end tests simulate real user scenarios to verify complete workflows function properly from UI to database.<br>4. Performance and security tests ensure the application meets non-functional requirements and is protected against common vulnerabilities.",
            "<b>DevOps Fundamentals:</b><br>1. Implement Continuous Integration to automatically build, test, and validate code changes, catching issues early in the development cycle.<br>2. Enable Continuous Deployment to automate the release process, reducing manual errors and enabling faster delivery of features.<br>3. Automate infrastructure provisioning using Infrastructure-as-Code tools like Terraform to ensure consistent, reproducible environments.<br>4. Implement comprehensive monitoring and logging to track application health, performance metrics, and quickly identify and resolve issues.",
            "<b>Microservices Architecture:</b><br>1. Break monolithic applications into smaller, independent services that can be developed, deployed, and scaled independently.<br>2. Use APIs for inter-service communication to maintain loose coupling and enable services to evolve independently.<br>3. Implement service discovery mechanisms to dynamically locate services, handle load balancing, and manage service registration and deregistration.<br>4. Design for failure with circuit breakers, timeouts, and retry mechanisms to ensure system resilience when services fail.",
            "<b>Containerization with Docker:</b><br>1. Package applications with all dependencies into lightweight, portable containers ensuring consistency across development, testing, and production environments.<br>2. Create optimized Dockerfiles using multi-stage builds, minimal base images, and proper layer caching to reduce image size and build time.<br>3. Use orchestration tools like Kubernetes to automate deployment, scaling, and management of containers across clusters.<br>4. Implement container security best practices including scanning for vulnerabilities, using private registries, and running containers with minimal privileges.",
            "<b>What is time management:</b><br>Time management is the practice of organizing and planning how to divide your time between specific activities to work more effectively. It involves prioritizing tasks, setting goals, and utilizing time efficiently to maximize productivity and minimize stress.<br><br><b>Key Principles:</b><br>• <b>Prioritization:</b> Identify most important tasks first using priority matrices or ranking systems.<br>• <b>Planning:</b> Create schedules and break large projects into smaller, manageable tasks.<br>• <b>Elimination:</b> Remove time-wasting activities and unnecessary meetings or distractions.<br>• <b>Delegation:</b> Assign tasks to appropriate team members to free up your time for critical work.<br>• <b>Review:</b> Regularly evaluate how you spend time and adjust strategies accordingly.",
            "<b>How to improve productivity:</b><br>Improving productivity means accomplishing more meaningful work in less time while maintaining quality and reducing unnecessary effort.<br><br><b>Effective Strategies:</b><br>1. <b>Set Clear Goals:</b> Define specific, measurable objectives for each day and week.<br>2. <b>Eliminate Distractions:</b> Turn off notifications, create focused work environments, use productivity apps.<br>3. <b>Use Time Blocking:</b> Allocate specific time slots for different types of tasks or projects.<br>4. <b>Take Regular Breaks:</b> Short breaks improve focus and prevent burnout; try Pomodoro technique (25 min work, 5 min break).<br>5. <b>Batch Similar Tasks:</b> Group similar activities together to minimize context switching.<br>6. <b>Organize Workspace:</b> Clean desk and organized files reduce time spent searching for items.<br>7. <b>Track Progress:</b> Monitor completed tasks to maintain motivation and identify bottlenecks.",
            "<b>What are communication skills:</b><br>Communication skills are the abilities to convey information clearly and listen effectively to others. Strong communication is fundamental for collaboration, relationship building, and professional success across all industries.<br><br><b>Essential Components:</b><br>• <b>Verbal Communication:</b> Speaking clearly, adjusting tone and pace, using appropriate vocabulary.<br>• <b>Non-Verbal Communication:</b> Body language, eye contact, facial expressions, gestures that reinforce or clarify messages.<br>• <b>Active Listening:</b> Full attention to speaker, asking clarifying questions, understanding intent beyond words.<br>• <b>Written Communication:</b> Clear emails, reports, and documentation that convey information professionally.<br>• <b>Emotional Intelligence:</b> Understanding emotions in yourself and others, responding appropriately.",
            "<b>How to build a strong team:</b><br>Building a strong team requires selecting skilled individuals, fostering collaboration, and creating an environment where everyone works towards common goals.<br><br><b>Key Steps:</b><br>1. <b>Hire the Right People:</b> Look for technical skills, cultural fit, and collaborative attitude during recruitment.<br>2. <b>Define Clear Roles:</b> Ensure everyone understands their responsibilities and how they contribute to team goals.<br>3. <b>Foster Trust:</b> Lead by example, be transparent, follow through on commitments, show genuine care for team members.<br>4. <b>Encourage Collaboration:</b> Create opportunities for teamwork, knowledge sharing, and cross-functional projects.<br>5. <b>Provide Support:</b> Offer resources, training, and mentorship to help team members grow professionally.<br>6. <b>Celebrate Wins:</b> Recognize achievements and contributions to maintain morale and motivation.<br>7. <b>Handle Conflicts:</b> Address issues promptly and constructively to maintain team cohesion.",
            "<b>What is project management:</b><br>Project management is the discipline of planning, organizing, executing, and monitoring work to achieve specific objectives within defined constraints of time, budget, and resources.<br><br><b>Core Phases:</b><br>• <b>Initiation:</b> Define project goals, scope, and identify stakeholders.<br>• <b>Planning:</b> Develop detailed plans, timelines, resource allocation, and risk management strategies.<br>• <b>Execution:</b> Carry out planned activities, coordinate team efforts, manage resources.<br>• <b>Monitoring:</b> Track progress, manage changes, control risks, and maintain communication.<br>• <b>Closure:</b> Complete deliverables, document lessons learned, formally close project.<br><br><b>Popular Methodologies:</b> Waterfall, Agile, Scrum, Kanban, Critical Path Method (CPM).",
            "<b>How to handle stress:</b><br>Stress management involves recognizing stress triggers and implementing techniques to reduce negative impacts on your physical and mental well-being.<br><br><b>Practical Techniques:</b><br>1. <b>Identify Triggers:</b> Recognize what situations cause stress in your work and personal life.<br>2. <b>Physical Exercise:</b> Regular activity reduces stress hormones and improves mood; aim for 30 minutes daily.<br>3. <b>Mindfulness:</b> Practice meditation, deep breathing, or yoga to calm your mind and body.<br>4. <b>Time Management:</b> Proper planning and prioritization prevent last-minute pressure and overwhelm.<br>5. <b>Social Support:</b> Share concerns with trusted colleagues, friends, or family members.<br>6. <b>Work Boundaries:</b> Establish limits on work hours, respond to emails during specific times only.<br>7. <b>Professional Help:</b> Consult counselors or therapists if stress becomes overwhelming or persistent.",
            "<b>What is effective problem solving:</b><br>Effective problem solving is a systematic approach to identifying issues, analyzing root causes, and implementing practical solutions that resolve problems sustainably.<br><br><b>Problem-Solving Steps:</b><br>• <b>Define the Problem:</b> Clearly articulate what the issue is, including symptoms and impact.<br>• <b>Analyze Root Causes:</b> Use techniques like Five Whys or Fishbone diagram to identify underlying reasons.<br>• <b>Generate Solutions:</b> Brainstorm multiple approaches without judgment initially.<br>• <b>Evaluate Options:</b> Assess feasibility, cost, time, and potential outcomes for each solution.<br>• <b>Implement Solution:</b> Execute the chosen approach with clear action steps and responsibilities.<br>• <b>Monitor Results:</b> Track effectiveness and adjust if needed.<br>• <b>Document Lessons:</b> Record what worked to apply learning to future problems.",
            "<b>How to make better decisions:</b><br>Better decision making involves gathering relevant information, considering multiple perspectives, and choosing options aligned with your values and long-term goals.<br><br><b>Decision-Making Framework:</b><br>1. <b>Define Objectives:</b> Clarify what outcome you want to achieve with the decision.<br>2. <b>Gather Information:</b> Collect relevant facts, data, and expert opinions before deciding.<br>3. <b>Identify Options:</b> Generate multiple alternatives rather than jumping to first solution.<br>4. <b>Evaluate Trade-offs:</b> Analyze pros and cons of each option, consider short-term and long-term impacts.<br>5. <b>Consider Values:</b> Ensure decision aligns with your principles and organizational values.<br>6. <b>Seek Input:</b> Consult with stakeholders and trusted advisors for additional perspective.<br>7. <b>Trust Your Judgment:</b> After thorough analysis, make confident decision and commit to it.",
            "<b>What is work-life balance:</b><br>Work-life balance is the equilibrium between time and energy devoted to career responsibilities and personal life activities, relationships, and self-care. It reduces burnout and increases overall well-being.<br><br><b>Why It Matters:</b><br>• <b>Health Benefits:</b> Reduces stress, improves physical and mental health, prevents burnout.<br>• <b>Better Performance:</b> Well-rested employees are more focused, creative, and productive at work.<br>• <b>Stronger Relationships:</b> Time with family and friends strengthens personal connections.<br>• <b>Personal Growth:</b> Pursuing hobbies and interests outside work provides fulfillment.<br>• <b>Longevity:</b> Balanced lifestyle contributes to longer, happier life and career sustainability.",
            "<b>How to achieve professional growth:</b><br>Professional growth involves continuously developing skills, expanding knowledge, taking on challenging responsibilities, and advancing your career through deliberate actions and learning.<br><br><b>Growth Strategies:</b><br>1. <b>Set Career Goals:</b> Define where you want to be in 1, 3, and 5 years with specific milestones.<br>2. <b>Continuous Learning:</b> Take courses, certifications, read industry publications, attend conferences.<br>3. <b>Seek Mentorship:</b> Find experienced professionals to guide your development and provide insights.<br>4. <b>Take on Challenges:</b> Volunteer for projects outside comfort zone to develop new skills.<br>5. <b>Build Network:</b> Connect with peers, industry leaders, and potential collaborators regularly.<br>6. <b>Request Feedback:</b> Actively seek constructive criticism to identify improvement areas.<br>7. <b>Reflect Regularly:</b> Review progress, celebrate wins, adjust strategies as needed to stay on track."
        };

        internal string[] promptRequestForMarkdown = new string[]
        {
           "what features are included in ai assistview?",
           "how can i customize the appearance of ai assistview?",
           "what is markdown, and how is it used?",
           "what customization options are available in ai assistview?",
           "what are the benefits of using markdown?",

          // TODO : Need to include the html content into the markdown viewer.
          // "prime number checking c# example?",
          // "generate fibonacci sequence c# example?",
          // "what are the benefits of using markdown?",
          // "generate ideas for a new web products that might be popular in 2025?",
          // "how do i set daily goals in my work day?",
          // "steps to publish a e-book with marketing strategy",
          // "i like the ai health assistant idea!",
          // "how can we ensure privacy with this assistant?",
          // "how do i prioritize tasks effectively?",
          // "what tools or apps can help me prioritize tasks?",
          // "how do i create an eye-catching e-book cover?",
          // "what are common mistakes to avoid in e-book covers?",
        };

        internal string[] promptResponseHtmlForMarkdown = new string[]
        {
           "The AI AssistView component offers several powerful features that enhance user interaction:\n\n- **Customizable Views:** Allows for custom templates to be used for banners, prompts, responses, and suggestions.\n- **Streaming Responses:** Provides real-time feedback as data is processed, enhancing immediacy and user engagement.\n- **Interactive Toolbar:** Easily integrates custom toolbar items, such as buttons and inputs, enhancing functionality.\n- **Seamless Integration:** Connects with AI services to provide advanced processing and intelligent responses.",
           "Customizing AI AssistView enables you to tailor the look and functionality to suit specific needs:\n\n- **Template Customization:** Modify templates for prompts, responses, and more.\n- **CSS Styling:** Apply custom styles to align with your application’s visual theme.\n- **Theme Support:** Utilize built-in themes or create your own for consistent styling.\n- **Banner and Toolbar Configurations:** Adjust content and tools for personalized UI experiences.",
           "Markdown is a lightweight markup language for formatting plain text that can be converted to HTML and other formats.\n\n- **Headers:** Use `#` through `######` for H1–H6.\n- **Emphasis:** Use `*italic*`, `**bold**`, and `inline code`.\n- **Lists:** Use `-` for unordered lists and `1.` for ordered lists; indent to nest.\n- **Code Blocks:** Fence code with triple backticks and optionally specify a language (e.g., ```csharp).\n- **Blockquotes:** Start a line with `>` to create a quote.\n\nMarkdown’s simplicity and readability make it ideal for writing documentation and notes.",
           "AI AssistView provides extensive customization options for tailored user interactions:\n\n- **Templates:** Use `<PromptItemTemplate>`, `<ResponseItemTemplate>`, etc., for customized display.\n- **Custom Toolbar Items:** Add buttons and other controls via `AssistViewToolbar`.\n- **Dynamic Content:** Update content based on user actions or external events.\n- **Styling:** Full CSS support for styling individual components and layouts.",
           "Markdown provides several benefits, especially in documentation and writing:\n\n- **Ease of Use:** Its syntax is simple and quick to learn, increasing productivity.\n- **Readability:** Plain text format ensures content is easy to read and edit.\n- **Flexibility:** Can be converted to HTML, PDF, and other formats easily.\n- **Collaboration:** Widely supported in various tools for collaborative writing and documentation.",
           
           // TODO : Need to include the html content into the markdown viewer.
           //"<pre><code class=\"csharp language-csharp\">using System;\n\nclass Program\n{\n    static bool IsPrime(int n)\n    {\n        if (n <= 1) return false;\n        for (int i = 2; i <= Math.Sqrt(n); i++)\n            if (n % i == 0) return false;\n        return true;\n    }\n\n    static void Main()\n    {\n        Console.WriteLine(IsPrime(11)); // True\n    }\n}\n</code></pre>",
           //"<pre><code class=\"csharp language-csharp\">using System;\n\nclass Program\n{\n    static void Fibonacci(int n)\n    {\n        int a = 0, b = 1, c;\n        for (int i = 0; i < n; i++)\n        {\n            Console.Write(a + \" \");\n            c = a + b;\n            a = b;\n            b = c;\n        }\n    }\n\n    static void Main()\n    {\n        Fibonacci(10); // 0 1 1 2 3 5 8 13 21 34\n    }\n}\n</code></pre>",
           //"<p>Here are some web product ideas for 2025:</p> <ul><li><strong>AI-Powered Health Assistant:</strong> A platform offering personalized health advice and virtual consultations.</li> <li><strong>VR Shopping Mall:</strong> An immersive experience for exploring and purchasing items virtually.</li> <li><strong>Sustainable Living Hub:</strong> A resource for adopting eco-friendly lifestyles with community support.</li> <li><strong>Remote Work Suite:</strong> A tool for remote teams with project management, collaboration, and productivity features.</li></ul> <p>Which one stands out to you?</p>",
           //"<p>To stay focused and productive, try these steps for setting daily goals:</p> <ol><li><strong>Identify Priorities:</strong> List the most important tasks based on deadlines or significance.</li> <li><strong>Break Down Tasks:</strong> Split larger tasks into smaller, manageable steps.</li> <li><strong>Set SMART Goals:</strong> Make sure goals are Specific, Measurable, Achievable, Relevant, and Time-bound.</li> <li><strong>Time Blocking:</strong> Allocate specific times for each task to stay organized and on track.</li></ol> <p>Would you like more tips on any of these steps?</p>",
           //"<p>To publish an e-book, follow the steps below:</p> <ol><li><strong>Write and format your e-book:</strong> Ensure your content is well-organized, edited, and formatted for digital reading.</li> <li><strong>Choose a publishing platform:</strong> Platforms like Amazon Kindle Direct Publishing (KDP) or Smashwords can help you publish and distribute your e-book.</li> <li><strong>Develop a marketing strategy:</strong> Utilize social media, email newsletters, and book promotion sites to create buzz and reach your target audience.</li> <li><strong>Launch and promote:</strong> Schedule a launch date, gather reviews, and continue promoting through various channels to maintain momentum and drive sales.</li></ol> <p>Do you have a specific topic in mind for your e-book?</p>",
           //"<p>Great choice! An AI-powered personal health assistant could revolutionize how we manage our health. It could provide personalized health advice, track fitness goals, and even offer virtual consultations with healthcare professionals.</p> <p>Would you like to explore more features for this idea or discuss how it could be developed?</p>",
           //"<p>Ensuring user privacy with an AI-powered health assistant involves several key strategies:</p> <ol><li><strong>Data Encryption:</strong> Protect user data with strong encryption to prevent unauthorized access.</li> <li><strong>User Consent:</strong> Clearly inform users about data collection and obtain their explicit consent.</li> <li><strong>Anonymization:</strong> Use techniques to anonymize personal data, reducing identification risks.</li> <li><strong>Regular Audits:</strong> Perform privacy audits and comply with regulations like GDPR or HIPAA.</li></ol> <p>Would you like more details on any of these strategies?</p>",
           //"<p>To stay focused and productive, set daily goals by:</p> <ol><li><strong>Identifying Priorities:</strong> List important tasks based on deadlines or significance.</li> <li><strong>Breaking Down Tasks:</strong> Divide larger tasks into smaller, manageable steps.</li> <li><strong>Setting SMART Goals:</strong> Ensure goals are Specific, Measurable, Achievable, Relevant, and Time-bound. </li> <li><strong>Time Blocking:</strong> Schedule specific times for each task to stay organized.</li></ol> <p> Need more tips on any of these steps? </p>",
           //"<p>Here are some tools to help you prioritize tasks effectively:</p> <ol><li><strong>Google Keep:</strong> For simple note-taking and task organization with labels and reminders.</li> <li><strong>Scoro:</strong> A project management tool for streamlining activities and team collaboration.</li> <li><strong>Evernote:</strong> Great for note-taking, to-do lists, and reminders.</li> <li><strong>Todoist:</strong> A powerful task manager for setting priorities and tracking progress.</li></ol> <p>Are you looking for tools to manage a specific type of task or project?</p>",
           //"<p>Creating an eye-catching e-book cover involves a few key steps:</p> <ol><li><strong>Understand your genre and audience:</strong> Research covers of popular books in your genre to see what appeals to your target readers.</li> <li><strong>Choose the right imagery and colors:</strong> Use high-quality images and a color scheme that reflects the tone and theme of your book.</li> <li><strong>Focus on typography:</strong> Select fonts that are readable and complement the overall design. The title should be prominent and easy to read even in thumbnail size.</li> <li><strong>Use design tools or hire a professional:</strong> Tools like Canva or Adobe Spark can help you create a professional-looking cover. Alternatively, consider hiring a graphic designer for a more polished result.</li></ol> <p>Would you like some tips on where to find good images or fonts for your cover?</p>",
           //"<p>Here are some common mistakes to avoid when designing an e-book cover:</p> <ol><li><strong>Cluttered design:</strong> Overloading the cover with too many elements can make it look messy and unprofessional. Keep it simple and focused.</li> <li><strong>Poor quality images:</strong> Using low-resolution or generic stock images can detract from the overall appeal. Always opt for high-quality, relevant visuals.</li> <li><strong>Unreadable fonts:</strong> Fancy or overly intricate fonts can be hard to read, especially in thumbnail size. Choose clear, legible fonts for the title and author name.</li> <li><strong>Ignoring genre conventions:</strong> Each genre has its own visual cues. Not adhering to these can confuse potential readers about the book’s content.</li> <li><strong>Inconsistent branding:</strong> If you have a series or multiple books, ensure a consistent style across all covers to build a recognizable brand.</li></ol> <p>Would you like any specific advice on designing your cover?</p>",

        };

        #endregion
    }
}
