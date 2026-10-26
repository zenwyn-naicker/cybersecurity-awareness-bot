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
    /// Interaction logic for ActivityLogWindow.xaml
    /// </summary>

public partial class ActivityLogWindow : Window
{
    private ActivityLog activityLog;

    public ActivityLogWindow(ActivityLog log)
    {
        InitializeComponent();
        activityLog = log;
        LoadLogEntries();
    }

    private void RefreshLog_Click(object sender, RoutedEventArgs e)
    {
        LoadLogEntries();
    }

    private void ClearLog_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to clear all log entries?",
                "Confirm Clear Log",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                activityLog.ClearLog();
                LoadLogEntries();
                MessageBox.Show("Activity log cleared successfully!", "Log Cleared",
                              MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error clearing log: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void LoadLogEntries()
    {
        try
        {
            LogPanel.Children.Clear();
            List<LogEntry> entries = activityLog.GetRecentEntries(20); // Show last 20 entries

            LogCountText.Text = $"{entries.Count} activities logged";

            if (entries.Count == 0)
            {
                TextBlock noEntriesText = new TextBlock
                {
                    Text = "No activities logged yet. Start using the application to see your activity here!",
                    FontSize = 16,
                    Foreground = Brushes.White,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(0, 50, 0, 0)
                };
                LogPanel.Children.Add(noEntriesText);
                return;
            }

            foreach (LogEntry entry in entries)
            {
                CreateLogEntryCard(entry);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading log entries: {ex.Message}", "Error",
                          MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void CreateLogEntryCard(LogEntry entry)
    {
        Border entryCard = new Border
        {
            Style = (Style)Resources["LogEntryStyle"]
        };

        Grid cardGrid = new Grid();
        cardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        cardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        // Activity description
        TextBlock activityText = new TextBlock
        {
            Text = GetActivityIcon(entry.Activity) + " " + entry.Activity,
            FontSize = 14,
            Foreground = Brushes.Black,
            TextWrapping = TextWrapping.Wrap,
            VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetColumn(activityText, 0);
        cardGrid.Children.Add(activityText);

        // Timestamp
        TextBlock timeText = new TextBlock
        {
            Text = entry.Timestamp.ToString("MMM dd, HH:mm"),
            FontSize = 12,
            Foreground = Brushes.Gray,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Right
        };
        Grid.SetColumn(timeText, 1);
        cardGrid.Children.Add(timeText);

        entryCard.Child = cardGrid;
        LogPanel.Children.Add(entryCard);
    }

    private string GetActivityIcon(string activity)
    {
        string lowerActivity = activity.ToLower();

        if (lowerActivity.Contains("login") || lowerActivity.Contains("logged"))
            return "👤";
        if (lowerActivity.Contains("chat") || lowerActivity.Contains("message"))
            return "💬";
        if (lowerActivity.Contains("task"))
            return "📋";
        if (lowerActivity.Contains("quiz"))
            return "🎯";
        if (lowerActivity.Contains("open"))
            return "📂";
        if (lowerActivity.Contains("complete"))
            return "✅";
        if (lowerActivity.Contains("delete"))
            return "🗑️";
        if (lowerActivity.Contains("view"))
            return "👀";

        return "📝";
    }
}
}
