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
    public class TaskManager
    {
        private List<CyberTask> tasks;

        public TaskManager()
        {
            tasks = new List<CyberTask>();
        }

        public void AddTask(CyberTask task)
        {
            try
            {
                if (task == null) throw new ArgumentNullException(nameof(task));
                tasks.Add(task);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error adding task: {ex.Message}");
                throw;
            }
        }

        public List<CyberTask> GetAllTasks()
        {
            try
            {
                return tasks.OrderByDescending(t => t.CreatedDate).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting tasks: {ex.Message}");
                return new List<CyberTask>();
            }
        }

        public void CompleteTask(string taskId)
        {
            try
            {
                var task = tasks.FirstOrDefault(t => t.Id == taskId);
                if (task != null)
                {
                    task.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error completing task: {ex.Message}");
            }
        }

        public void DeleteTask(string taskId)
        {
            try
            {
                tasks.RemoveAll(t => t.Id == taskId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error deleting task: {ex.Message}");
            }
        }
    }
}