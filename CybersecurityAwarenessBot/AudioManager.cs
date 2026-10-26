/*Author: Zenwyn Naicker*/
/*Code Attribution: */
/**************************************************************************************************************
 * Author: w3schools
 * Date: 2025
 * Title: C# Tutorial
 * Type: [Source Code]
 * Available at: <https://www.w3schools.com/cs/index.php> [Accessed 10 April 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: elevenlabs
 * Date: 2025
 * Title: Create the most realistic speech with our AI audio platform
 * Type: [Audio File]
 * Available at: <https://elevenlabs.io/> [Accessed 10 April 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 15 April 2025]
 **************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks; 

namespace CybersecurityAwarenessBot
{
    // Manages audio playback
    class AudioManager
    {
        // Plays the welcome audio message
        public void PlayWelcomeMessage()
        {
            try
            {
                SoundPlayer welcomeMessage = new SoundPlayer("welcomeMessage.wav"); // Loads the audio file
                welcomeMessage.PlaySync(); // Plays the audio file 
            }
            catch (FileNotFoundException) // Displays an error message if the audio file is missing
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: The welcome message audio file was not found.");
                Console.ResetColor();
            }
            catch (Exception ex) // Handles any other unexpected errors
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while playing the audio: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
