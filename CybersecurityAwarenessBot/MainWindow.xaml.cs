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

using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CybersecurityAwarenessBot
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserSession userSession;
        private ActivityLog activityLog;
        private TaskManager sharedTaskManager; // Shared instance for all windows
        private AudioManager audioManager; 

        public MainWindow()
        {
            InitializeComponent();
            userSession = new UserSession();
            activityLog = new ActivityLog();
            sharedTaskManager = new TaskManager(); 
            audioManager = new AudioManager(); 

            // Play welcome message when main window loads
            PlayWelcomeAudio();
        }

        private void PlayWelcomeAudio()
        {
            try
            {
                // Play welcome message in a separate task to avoid blocking UI
                Task.Run(() => audioManager.PlayWelcomeMessage());
                activityLog.LogAction("Played welcome audio message");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not play welcome audio: {ex.Message}");
            }
        }

        private void EnterName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = NameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(username))
                {
                    MessageBox.Show("Please enter your name.", "Name Required",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                userSession.SetUsername(username);
                activityLog.LogAction($"User '{username}' logged in");
                WelcomeTextBlock.Text = $"Welcome, {username}! Choose an option below:";
                NameInputPanel.Visibility = Visibility.Collapsed;
                NavigationGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenChatbot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pass the shared TaskManager to ChatbotWindow
                ChatbotWindow chatbotWindow = new ChatbotWindow(userSession, activityLog, sharedTaskManager);

                this.Hide();
                chatbotWindow.Closed += (s, args) => this.Show();
                chatbotWindow.Show();

                activityLog.LogAction("Opened Chatbot Assistant");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening chatbot: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenTaskManager_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Pass the shared TaskManager to TaskManagerWindow
                TaskManagerWindow taskWindow = new TaskManagerWindow(userSession, activityLog, sharedTaskManager);

                this.Hide();
                taskWindow.Closed += (s, args) => this.Show();
                taskWindow.Show();

                activityLog.LogAction("Opened Task Manager");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening task manager: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenQuizGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                QuizGameWindow quizWindow = new QuizGameWindow(userSession, activityLog);

                this.Hide();
                quizWindow.Closed += (s, args) => this.Show();
                quizWindow.Show();

                activityLog.LogAction("Opened Quiz Game");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening quiz game: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenActivityLog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ActivityLogWindow logWindow = new ActivityLogWindow(activityLog);

                this.Hide();
                logWindow.Closed += (s, args) => this.Show();
                logWindow.Show();

                activityLog.LogAction("Viewed Activity Log");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening activity log: {ex.Message}", "Error",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}