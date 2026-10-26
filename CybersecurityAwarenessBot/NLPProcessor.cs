/*Author: Zenwyn Naicker*/
/*Code Attribution: */
/**************************************************************************************************************
 * Author: w3schools
 * Date: 2025
 * Title: C# Tutorial
 * Type: [Source Code]
 * Available at: <https://www.w3schools.com/cs/index.php> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: UT
 * Date: 2024
 * Title: Collections in C#
 * Type: [Source Code]
 * Available at: <https://www.c-sharpcorner.com/blogs/nongeneric-collection-with-arraylist-in-c-sharp> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2024
 * Title: Regex Tutorial - How to write Regular Expressions?
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/write-regular-expressions/> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: OpenAi
 * Date: 2025
 * Title: Example of C# code to extract reminder time "in X days/hours/minutes" pattern using regex
 * Type: [Source Code]
 * Link to chat: <https://chatgpt.com/share/684913b8-19c8-8009-b707-738db27031ee> [Accessed 11 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    public class NLPProcessor
    {
        private Dictionary<string, string[]> commandKeywords;
        private TaskManager taskManager;
        private Dictionary<string, PendingTask> pendingTasks; // Store tasks awaiting reminder setup

        public NLPProcessor(TaskManager taskManager = null)
        {
            this.taskManager = taskManager ?? new TaskManager();
            pendingTasks = new Dictionary<string, PendingTask>();
            InitializeKeywords();
        }

        private void InitializeKeywords()
        {
            commandKeywords = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                { "add_task", new[] { "add task", "create task", "new task", "task", "remind me", "set reminder" } },
                { "start_quiz", new[] { "start quiz", "quiz", "test", "game", "questions" } },
                { "show_log", new[] { "show log", "activity log", "what have you done for me?", "show activity", "view log", "what have you done for me" } },
                { "help", new[] { "help", "what can you do", "commands", "options" } },
                { "reminder_response", new[] { "yes", "sure", "okay", "ok", "yep", "yeah", "remind me" } },
                { "no_reminder", new[] { "no", "nope", "not needed", "skip", "no thanks" } },
                { "time_pattern", new[] { "in", "after", "tomorrow", "next week", "days", "hours", "minutes" } }
            };
        }

        public string ProcessInput(string input, UserSession session, ActivityLog activityLog)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input)) return null;

                string lowerInput = input.ToLower();
                string userId = session?.GetUsername() ?? "user";

                // Check if this is a response to a pending reminder question
                if (pendingTasks.ContainsKey(userId))
                {
                    return HandleReminderResponse(input, userId, session, activityLog);
                }

                // Check for task-related commands
                if (ContainsKeywords(lowerInput, commandKeywords["add_task"]))
                {
                    return HandleTaskCreation(input, userId, session, activityLog);
                }

                // Check for quiz commands
                if (ContainsKeywords(lowerInput, commandKeywords["start_quiz"]))
                {
                    activityLog.LogAction("NLP detected quiz request");
                    return "I see you want to test your knowledge! Please use the Quiz Game window to start the cybersecurity quiz.";
                }

                // Check for log commands 
                if (ContainsKeywords(lowerInput, commandKeywords["show_log"]))
                {
                    activityLog.LogAction("User requested activity log display");

                    var recentEntries = activityLog.GetRecentEntries(10);
                    if (!recentEntries.Any())
                    {
                        return "📋 Your activity log is currently empty.";
                    }

                    StringBuilder logDisplay = new StringBuilder();
                    logDisplay.AppendLine("📋 Recent Activity Log:");

                    foreach (var entry in recentEntries.Take(10))
                    {
                        logDisplay.AppendLine($"• {entry.Activity} ({entry.Timestamp:HH:mm})");
                    }

                    return logDisplay.ToString();
                }

                // Check for help commands
                if (ContainsKeywords(lowerInput, commandKeywords["help"]))
                {
                    return "I can help you with cybersecurity topics! I understand requests like:\n" +
                           "• 'Add task to enable 2FA' - I'll create the task for you\n" +
                           "• 'Start quiz' - I'll direct you to the Quiz Game\n" +
                           "• 'Show activity log' - I'll show your recent activity\n" +
                           "• Ask about passwords, phishing, malware, or privacy for advice!";
                }

                return null; // Let the original response engine handle it
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in NLP processing: {ex.Message}");
                return null;
            }
        }

        private string HandleTaskCreation(string input, string userId, UserSession session, ActivityLog activityLog)
        {
            try
            {
                string extractedTask = ExtractTaskFromInput(input);

                if (string.IsNullOrEmpty(extractedTask))
                {
                    // Generic task creation request
                    return "I'd be happy to help you create a task! Please specify what you'd like to add. For example: 'Add task - Review privacy settings' or 'Remind me to enable 2FA'.";
                }

                // Enhance the task with cybersecurity context
                string enhancedDescription = EnhanceTaskDescription(extractedTask);

                // Create the task
                CyberTask newTask = new CyberTask
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = extractedTask,
                    Description = enhancedDescription,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now
                };

                // Store as pending task
                pendingTasks[userId] = new PendingTask
                {
                    Task = newTask,
                    CreatedAt = DateTime.Now
                };

                activityLog.LogAction($"NLP created task: '{extractedTask}'");

                return $"Task added with the description \"{enhancedDescription}\" Would you like a reminder?";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling task creation: {ex.Message}");
                return "I encountered an error creating your task. Please try again.";
            }
        }

        private string HandleReminderResponse(string input, string userId, UserSession session, ActivityLog activityLog)
        {
            try
            {
                if (!pendingTasks.ContainsKey(userId))
                    return null;

                string lowerInput = input.ToLower();
                PendingTask pendingTask = pendingTasks[userId];

                // Check if user wants a reminder
                if (ContainsKeywords(lowerInput, commandKeywords["reminder_response"]) ||
                    lowerInput.Contains("remind"))
                {
                    // Extract time information
                    DateTime? reminderDate = ExtractReminderTime(input);

                    if (reminderDate.HasValue)
                    {
                        pendingTask.Task.ReminderDate = reminderDate;
                        taskManager.AddTask(pendingTask.Task);
                        pendingTasks.Remove(userId);

                        string timeDescription = GetTimeDescription(reminderDate.Value);
                        activityLog.LogAction($"Task created with reminder: {timeDescription}");

                        return $"Got it! I'll remind you {timeDescription}.";
                    }
                    else
                    {
                        // Ask for specific time
                        return "When would you like to be reminded? You can say things like 'in 3 days', 'tomorrow', 'in 1 week', or 'next Monday'.";
                    }
                }
                else if (ContainsKeywords(lowerInput, commandKeywords["no_reminder"]))
                {
                    // No reminder needed
                    taskManager.AddTask(pendingTask.Task);
                    pendingTasks.Remove(userId);
                    activityLog.LogAction("Task created without reminder");

                    return "Perfect! Your task has been added without a reminder.";
                }
                else
                {
                    // Try to parse as time input
                    DateTime? reminderDate = ExtractReminderTime(input);

                    if (reminderDate.HasValue)
                    {
                        pendingTask.Task.ReminderDate = reminderDate;
                        taskManager.AddTask(pendingTask.Task);
                        pendingTasks.Remove(userId);

                        string timeDescription = GetTimeDescription(reminderDate.Value);
                        activityLog.LogAction($"Task created with reminder: {timeDescription}");

                        return $"Got it! I'll remind you {timeDescription}.";
                    }
                    else
                    {
                        return "I didn't understand that timing. Please specify when you'd like to be reminded (e.g., 'in 3 days', 'tomorrow', 'next week') or say 'no' if you don't want a reminder.";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling reminder response: {ex.Message}");
                return "I encountered an error setting up your reminder. Please try again.";
            }
        }

        private DateTime? ExtractReminderTime(string input)
        {
            try
            {
                string lowerInput = input.ToLower();
                DateTime now = DateTime.Now;

                // Tomorrow
                if (lowerInput.Contains("tomorrow"))
                    return now.Date.AddDays(1).AddHours(9); // Set to 9 AM tomorrow

                // Next week
                if (lowerInput.Contains("next week"))
                    return now.Date.AddDays(7).AddHours(9); // Set to 9 AM next week

                // Pattern: "in X days/hours/minutes" - prioritize "in X days" pattern
                var timePatterns = new[]
                {
                    @"in (\d+) days?",
                    @"in (\d+) hours?",
                    @"in (\d+) minutes?",
                    @"(\d+) days?(?!\s*hours?)",  // Match "X days" but not if followed by "hours"
                    @"(\d+) hours?",
                    @"(\d+) minutes?"
                };

                foreach (string pattern in timePatterns)
                {
                    Match match = Regex.Match(lowerInput, pattern);
                    if (match.Success && int.TryParse(match.Groups[1].Value, out int value))
                    {
                        if (pattern.Contains("day"))
                        {
                            // For days, set to 9 AM on the target date to avoid confusion
                            return now.Date.AddDays(value).AddHours(9);
                        }
                        else if (pattern.Contains("hour"))
                        {
                            return now.AddHours(value);
                        }
                        else if (pattern.Contains("minute"))
                        {
                            return now.AddMinutes(value);
                        }
                    }
                }

                // Specific days of the week
                string[] daysOfWeek = { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
                for (int i = 0; i < daysOfWeek.Length; i++)
                {
                    if (lowerInput.Contains(daysOfWeek[i]))
                    {
                        int daysUntil = ((int)DayOfWeek.Monday + i - (int)now.DayOfWeek + 7) % 7;
                        if (daysUntil == 0) daysUntil = 7; // Next week if it's the same day
                        return now.Date.AddDays(daysUntil).AddHours(9);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting reminder time: {ex.Message}");
                return null;
            }
        }

        private string GetTimeDescription(DateTime reminderDate)
        {
            DateTime now = DateTime.Now;
            DateTime reminderDay = reminderDate.Date;
            DateTime today = now.Date;

            int daysDifference = (int)(reminderDay - today).TotalDays;

            if (daysDifference == 0)
            {
                return "later today";
            }
            else if (daysDifference == 1)
            {
                return "tomorrow";
            }
            else if (daysDifference == 7)
            {
                return "in 1 week";
            }
            else if (daysDifference > 1)
            {
                return $"in {daysDifference} days";
            }
            else
            {
                // Handle hours/minutes for same day or very soon
                TimeSpan difference = reminderDate - now;
                if (difference.TotalHours >= 1)
                {
                    int hours = (int)difference.TotalHours;
                    return $"in {hours} hour{(hours > 1 ? "s" : "")}";
                }
                else
                {
                    int minutes = Math.Max(1, (int)difference.TotalMinutes);
                    return $"in {minutes} minute{(minutes > 1 ? "s" : "")}";
                }
            }
        }

        private string EnhanceTaskDescription(string taskTitle)
        {
            string lowerTitle = taskTitle.ToLower();

            // Add context-aware descriptions based on common cybersecurity tasks
            if (lowerTitle.Contains("privacy") && lowerTitle.Contains("setting"))
                return "Review account privacy settings to ensure your data is protected.";
            else if (lowerTitle.Contains("2fa") || lowerTitle.Contains("two factor"))
                return "Enable two-factor authentication on important accounts for added security.";
            else if (lowerTitle.Contains("password") && lowerTitle.Contains("update"))
                return "Update passwords to strong, unique combinations for better account security.";
            else if (lowerTitle.Contains("software") && lowerTitle.Contains("update"))
                return "Install the latest software updates to patch security vulnerabilities.";
            else if (lowerTitle.Contains("backup"))
                return "Create secure backups of important data to protect against data loss.";
            else if (lowerTitle.Contains("antivirus"))
                return "Run antivirus scan to detect and remove potential malware threats.";
            else if (lowerTitle.Contains("phishing"))
                return "Learn to identify and avoid phishing attempts to protect personal information.";
            else
                return $"Complete cybersecurity task: {taskTitle}";
        }

        private bool ContainsKeywords(string input, string[] keywords)
        {
            return keywords.Any(keyword => input.Contains(keyword.ToLower()));
        }

        private string ExtractTaskFromInput(string input)
        {
            try
            {
                string lowerInput = input.ToLower();

                // Pattern matching for common task phrases
                var patterns = new[]
                {
                    @"add task[\s\-–—]*(.+)",
                    @"remind me to (.+)",
                    @"create task[\s\-–—]*(.+)",
                    @"set reminder[\s\-–—]*(.+)",
                    @"new task[\s\-–—]*(.+)",
                    @"task[\s\-–—]*(.+)"
                };

                foreach (string pattern in patterns)
                {
                    Match match = Regex.Match(lowerInput, pattern, RegexOptions.IgnoreCase);
                    if (match.Success && match.Groups.Count > 1)
                    {
                        string extracted = match.Groups[1].Value.Trim();
                        // Clean up common prefixes
                        extracted = Regex.Replace(extracted, @"^(to\s+|for\s+|\-\s*|–\s*|—\s*)", "", RegexOptions.IgnoreCase);
                        return extracted.Trim();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting task: {ex.Message}");
                return null;
            }
        }

        // Clean up old pending tasks (call this periodically)
        public void CleanupPendingTasks()
        {
            var expiredTasks = pendingTasks.Where(kvp =>
                DateTime.Now - kvp.Value.CreatedAt > TimeSpan.FromMinutes(10)).ToList();

            foreach (var expired in expiredTasks)
            {
                pendingTasks.Remove(expired.Key);
            }
        }
    }

    // Helper class to store pending tasks
    public class PendingTask
    {
        public CyberTask Task { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}