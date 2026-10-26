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
    /// Interaction logic for QuizGameWindow.xaml
    /// </summary>
public partial class QuizGameWindow : Window
{
    private UserSession userSession;
    private ActivityLog activityLog;
    private QuizManager quizManager;
    private List<QuizQuestion> questions;
    private int currentQuestionIndex;
    private int score;
    private bool isAnswerSubmitted;

    public QuizGameWindow(UserSession session, ActivityLog log)
    {
        InitializeComponent();
        userSession = session;
        activityLog = log;
        quizManager = new QuizManager();
        questions = quizManager.GetQuestions();
        currentQuestionIndex = 0;
        score = 0;
        isAnswerSubmitted = false;
    }

    private void StartQuiz_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            StartPanel.Visibility = Visibility.Collapsed;
            QuestionPanel.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Visible;

            activityLog.LogAction("Started cybersecurity quiz");
            LoadCurrentQuestion();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error starting quiz: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LoadCurrentQuestion()
    {
        try
        {
            if (currentQuestionIndex >= questions.Count)
            {
                ShowResults();
                return;
            }

            QuizQuestion question = questions[currentQuestionIndex];
            QuestionNumberText.Text = $"Question {currentQuestionIndex + 1} of {questions.Count}";
            QuestionText.Text = question.Question;

            AnswerA.Content = question.OptionA;
            AnswerB.Content = question.OptionB;
            AnswerC.Content = question.OptionC;
            AnswerD.Content = question.OptionD;

            // Reset radio buttons
            AnswerA.IsChecked = false;
            AnswerB.IsChecked = false;
            AnswerC.IsChecked = false;
            AnswerD.IsChecked = false;

            // Reset UI state
            FeedbackText.Visibility = Visibility.Collapsed;
            SubmitButton.Visibility = Visibility.Visible;
            NextButton.Visibility = Visibility.Collapsed;
            isAnswerSubmitted = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading question: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (isAnswerSubmitted) return;

            string selectedAnswer = GetSelectedAnswer();
            if (string.IsNullOrEmpty(selectedAnswer))
            {
                MessageBox.Show("Please select an answer.", "No Answer Selected",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            QuizQuestion currentQuestion = questions[currentQuestionIndex];
            bool isCorrect = selectedAnswer == currentQuestion.CorrectAnswer;

            if (isCorrect)
            {
                score++;
                FeedbackText.Text = $"✅ Correct! {currentQuestion.Explanation}";
            }
            else
            {
                FeedbackText.Text = $"❌ Incorrect. The correct answer is {currentQuestion.CorrectAnswer}. {currentQuestion.Explanation}";
            }

            FeedbackText.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Visible;
            isAnswerSubmitted = true;

            UpdateScore();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error submitting answer: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void NextQuestion_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            currentQuestionIndex++;
            LoadCurrentQuestion();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading next question: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private string GetSelectedAnswer()
    {
        if (AnswerA.IsChecked == true) return "A";
        if (AnswerB.IsChecked == true) return "B";
        if (AnswerC.IsChecked == true) return "C";
        if (AnswerD.IsChecked == true) return "D";
        return null;
    }

    private void UpdateScore()
    {
        ScoreTextBlock.Text = $"Score: {score}/{questions.Count}";
    }

    private void ShowResults()
    {
        try
        {
            QuestionPanel.Visibility = Visibility.Collapsed;
            ResultsPanel.Visibility = Visibility.Visible;
            SubmitButton.Visibility = Visibility.Collapsed;
            NextButton.Visibility = Visibility.Collapsed;

            double percentage = (double)score / questions.Count * 100;
            FinalScoreText.Text = $"🏆 Final Score: {score}/{questions.Count} ({percentage:F0}%)";

            string resultMessage = percentage switch
            {
                >= 90 => "🌟 Outstanding! You're a cybersecurity expert!",
                >= 80 => "🎉 Excellent work! You have strong cybersecurity knowledge.",
                >= 70 => "👍 Good job! You understand most cybersecurity basics.",
                >= 60 => "📚 Not bad, but consider learning more about cybersecurity.",
                _ => "📖 Keep studying! Cybersecurity knowledge is crucial for staying safe online."
            };

            ResultMessageText.Text = resultMessage;
            activityLog.LogAction($"Completed quiz with score {score}/{questions.Count}");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error showing results: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RestartQuiz_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Reset game state
            currentQuestionIndex = 0;
            score = 0;
            isAnswerSubmitted = false;

            // Reset UI
            ResultsPanel.Visibility = Visibility.Collapsed;
            StartPanel.Visibility = Visibility.Visible;
            UpdateScore();

            activityLog.LogAction("Restarted cybersecurity quiz");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error restarting quiz: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
}
