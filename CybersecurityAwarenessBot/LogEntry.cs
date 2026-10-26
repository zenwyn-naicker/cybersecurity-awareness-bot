/*Author: Zenwyn Naicker*/
/*Code Attribution: */
/**************************************************************************************************************
 * Author: w3schools
 * Date: 2025
 * Title: C# Tutorial
 * Type: [Source Code]
 * Available at: <https://www.w3schools.com/cs/index.php> [Accessed 09 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Activity { get; set; }

        public LogEntry(string activity)
        {
            Timestamp = DateTime.Now;
            Activity = activity;
        }
    }
}
