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
    /// Interaction logic for ChatbotWindow.xaml
    /// </summary>
    public partial class ChatbotWindow : Window
    {
        private UserSession userSession;
        private ResponseEngine responseEngine;
        private ActivityLog activityLog;
        private NLPProcessor nlpProcessor;
        private TaskManager taskManager;

        public ChatbotWindow(UserSession session, ActivityLog log, TaskManager taskManager)
        {
            InitializeComponent();
            userSession = session;
            activityLog = log;
            this.taskManager = taskManager;
            responseEngine = new ResponseEngine();
            nlpProcessor = new NLPProcessor(taskManager); // Pass TaskManager to NLPProcessor

            AddBotMessage($"Hello {userSession.GetUsername()}! 👋 I'm your cybersecurity assistant. How can I help you today?");
        }

        private void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            SendUserMessage();
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendUserMessage();
            }
        }

        private void SendUserMessage()
        {
            try
            {
                string message = MessageTextBox.Text.Trim();
                if (string.IsNullOrEmpty(message)) return;

                AddUserMessage(message);
                MessageTextBox.Clear();

                string response = null;

                // STEP 1: Check NLP for COMMANDS first (task creation, quiz, etc.)
                string nlpResponse = nlpProcessor.ProcessInput(message, userSession, activityLog);
                if (!string.IsNullOrEmpty(nlpResponse))
                {
                    response = nlpResponse;
                }
                else
                {
                    // STEP 2: Use ResponseEngine for cybersecurity advice
                    response = responseEngine.ProcessInput(message, userSession);
                }

                // STEP 3: Fallback
                if (string.IsNullOrEmpty(response) || response.Contains("❓ I'm not sure about that"))
                {
                    response = "❓ I'm not sure about that. Try asking about password safety, phishing, privacy, or other cybersecurity topics. You can also ask me to create tasks like 'Add task - Review privacy settings'.";
                }

                AddBotMessage(response);
                activityLog.LogAction($"Chat interaction: '{message.Substring(0, Math.Min(30, message.Length))}...'");

                // Clean up old pending tasks periodically
                nlpProcessor.CleanupPendingTasks();
            }
            catch (Exception ex)
            {
                AddBotMessage($"❌ Sorry, I encountered an error: {ex.Message}");
            }
        }

        private void AddUserMessage(string message)
        {
            Border bubble = new Border
            {
                Style = (Style)Resources["ChatBubbleUser"]
            };
            TextBlock text = new TextBlock
            {
                Text = message,
                Style = (Style)Resources["ChatText"]
            };
            bubble.Child = text;
            ChatPanel.Children.Add(bubble);
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            Border bubble = new Border
            {
                Style = (Style)Resources["ChatBubbleBot"]
            };
            TextBlock text = new TextBlock
            {
                Text = message,
                Style = (Style)Resources["ChatText"]
            };
            bubble.Child = text;
            ChatPanel.Children.Add(bubble);
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToEnd();
        }
    }
}