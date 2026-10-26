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
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 09 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    public class ActivityLog
    {
        private List<LogEntry> logEntries;

        public ActivityLog()
        {
            logEntries = new List<LogEntry>();
        }

        public void LogAction(string activity)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(activity))
                {
                    logEntries.Add(new LogEntry(activity));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging activity: {ex.Message}");
            }
        }

        public List<LogEntry> GetRecentEntries(int count = 10)
        {
            try
            {
                return logEntries
                    .OrderByDescending(e => e.Timestamp)
                    .Take(count)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting log entries: {ex.Message}");
                return new List<LogEntry>();
            }
        }

        public void ClearLog()
        {
            try
            {
                logEntries.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing log: {ex.Message}");
            }
        }
    }
}
