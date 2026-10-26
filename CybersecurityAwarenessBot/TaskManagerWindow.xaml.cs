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
 * Author: geeksforgeeks
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: tutorialspoint
 * Date: 2025
 * Title: WPF Tutorial
 * Type: [Source Code]
 * Available at:<https://www.tutorialspoint.com/wpf/index.htm> [Accessed 09 June 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
* Author: microsoft
* Date: 2025
* Title: Tutorial: Create your first WPF application in Visual Studio 2019
* Type: [Source Code]
* Available at:<https://learn.microsoft.com/en-us/dotnet/desktop/wpf/get-started/walkthrough-my-first-wpf-desktop-application> [Accessed 09 June 2025]
**************************************************************************************************************/
/**************************************************************************************************************
 * Author: OpenAi
 * Date: 2025
 * Title: Example of how to create UI cards in WPF using either a StackPanel or Grid in C#
 * Type: [Source Code]
 * Link to chat: <https://chatgpt.com/share/68491788-ca74-8009-bc42-777c4d8a46f0> [Accessed 11 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CybersecurityAwarenessBot;

namespace CybersecurityAwarenessBot
{
    /// <summary>
    /// Interaction logic for TaskManagerWindow.xaml
    /// </summary>
    public partial class TaskManagerWindow : Window
    {
        private UserSession userSession;
        private ActivityLog activityLog;
        private TaskManager taskManager;

        public TaskManagerWindow(UserSession session, ActivityLog log, TaskManager taskManager)
        {
            InitializeComponent();
            userSession = session;
            activityLog = log;
            this.taskManager = taskManager; // Use the shared TaskManager instance

            LoadTasks();
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TaskTitleTextBox.Text.Trim();
                string description = TaskDescriptionTextBox.Text.Trim();
                DateTime? reminderDate = ReminderDatePicker.SelectedDate;

                if (string.IsNullOrEmpty(title))
                {
                    MessageBox.Show("Please enter a task title.", "Title Required",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                CyberTask newTask = new CyberTask
                {
                    Id = Guid.NewGuid().ToString(),
                    Title = title,
                    Description = description,
                    ReminderDate = reminderDate,
                    IsCompleted = false,
                    CreatedDate = DateTime.Now
                };

                taskManager.AddTask(newTask);
                activityLog.LogAction($"Added task: '{title}'");

                TaskTitleTextBox.Clear();
                TaskDescriptionTextBox.Clear();
                ReminderDatePicker.SelectedDate = null;

                LoadTasks();
                MessageBox.Show("Task added successfully!", "Success",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding task: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadTasks()
        {
            try
            {
                TasksPanel.Children.Clear();
                List<CyberTask> tasks = taskManager.GetAllTasks();

                if (tasks.Count == 0)
                {
                    TextBlock noTasksText = new TextBlock
                    {
                        Text = "No tasks yet. Add your first cybersecurity task above or create one in the chatbot!",
                        FontSize = 16,
                        Foreground = Brushes.Black,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 50, 0, 0),
                        TextWrapping = TextWrapping.Wrap
                    };
                    TasksPanel.Children.Add(noTasksText);
                    return;
                }

                foreach (CyberTask task in tasks)
                {
                    CreateTaskCard(task);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateTaskCard(CyberTask task)
        {
            Border taskCard = new Border
            {
                Style = (Style)Resources["TaskCardStyle"]
            };

            StackPanel cardContent = new StackPanel();

            // Task title
            TextBlock titleBlock = new TextBlock
            {
                Text = task.Title,
                FontSize = 16,
                FontWeight = FontWeights.Bold,
                Foreground = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 5)
            };
            if (task.IsCompleted)
            {
                titleBlock.TextDecorations = TextDecorations.Strikethrough;
                titleBlock.Foreground = Brushes.Gray;
            }
            cardContent.Children.Add(titleBlock);

            // Task description
            if (!string.IsNullOrEmpty(task.Description))
            {
                TextBlock descBlock = new TextBlock
                {
                    Text = task.Description,
                    FontSize = 14,
                    Foreground = Brushes.DarkGray,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                cardContent.Children.Add(descBlock);
            }

            // Reminder info
            if (task.ReminderDate.HasValue)
            {
                TextBlock reminderBlock = new TextBlock
                {
                    Text = $"📅 Reminder: {task.ReminderDate.Value.ToString("MMM dd, yyyy")}",
                    FontSize = 12,
                    Foreground = Brushes.Blue,
                    Margin = new Thickness(0, 0, 0, 10)
                };
                cardContent.Children.Add(reminderBlock);
            }

            // Buttons
            StackPanel buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            if (!task.IsCompleted)
            {
                Button completeButton = new Button
                {
                    Content = "✓ Complete",
                    Style = (Style)Resources["ButtonStyle"],
                    Background = Brushes.Green
                };
                completeButton.Click += (s, e) => CompleteTask(task.Id);
                buttonPanel.Children.Add(completeButton);
            }

            Button deleteButton = new Button
            {
                Content = "🗑️ Delete",
                Style = (Style)Resources["ButtonStyle"],
                Background = Brushes.Red
            };
            deleteButton.Click += (s, e) => DeleteTask(task.Id);
            buttonPanel.Children.Add(deleteButton);

            cardContent.Children.Add(buttonPanel);
            taskCard.Child = cardContent;
            TasksPanel.Children.Add(taskCard);
        }

        private void CompleteTask(string taskId)
        {
            try
            {
                taskManager.CompleteTask(taskId);
                activityLog.LogAction($"Completed task");
                LoadTasks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error completing task: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteTask(string taskId)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to delete this task?",
                                   "Confirm Delete", MessageBoxButton.YesNo,
                                   MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    taskManager.DeleteTask(taskId);
                    activityLog.LogAction("Deleted task");
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting task: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Method to refresh tasks 
        public void RefreshTasks()
        {
            LoadTasks();
        }
    }
}