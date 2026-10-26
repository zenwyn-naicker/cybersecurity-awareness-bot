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
/**************************************************************************************************************
 * Author: Aliza Vigderman
 * Date: 2024
 * Title: Cyber Security Tips
 * Type: [Information]
 * Available at: <https://www.security.org/digital-safety/cyber-security-tips/> [Accessed 09 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    public class QuizManager
    {
        private List<QuizQuestion> questions;

        public QuizManager()
        {
            try
            {
                InitializeQuestions();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error initializing quiz: {ex.Message}");
                questions = new List<QuizQuestion>();
            }
        }

        private void InitializeQuestions()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    OptionA = "Reply with your password",
                    OptionB = "Delete the email",
                    OptionC = "Report the email as phishing",
                    OptionD = "Ignore it",
                    CorrectAnswer = "C",
                    Explanation = "Reporting phishing emails helps prevent scams and protects others."
                },
                new QuizQuestion
                {
                    Question = "What makes a strong password?",
                    OptionA = "Your birthday",
                    OptionB = "At least 12 characters with mixed case, numbers, and symbols",
                    OptionC = "Your pet's name",
                    OptionD = "The word 'password'",
                    CorrectAnswer = "B",
                    Explanation = "Strong passwords should be long and complex with various character types."
                },
                new QuizQuestion
                {
                    Question = "What does HTTPS indicate about a website?",
                    OptionA = "The site is completely safe",
                    OptionB = "The connection is encrypted",
                    OptionC = "The site is government-approved",
                    OptionD = "The site loads faster",
                    CorrectAnswer = "B",
                    Explanation = "HTTPS encrypts data between your browser and the website."
                },
                new QuizQuestion
                {
                    Question = "How often should you update your software?",
                    OptionA = "Never",
                    OptionB = "Once a year",
                    OptionC = "When updates are available",
                    OptionD = "Only when it breaks",
                    CorrectAnswer = "C",
                    Explanation = "Regular updates patch security vulnerabilities and improve protection."
                },
                new QuizQuestion
                {
                    Question = "What is two-factor authentication?",
                    OptionA = "Using two passwords",
                    OptionB = "Logging in twice",
                    OptionC = "An extra security step beyond passwords",
                    OptionD = "A type of antivirus",
                    CorrectAnswer = "C",
                    Explanation = "2FA adds an extra layer of security using something you know and something you have."
                },
                new QuizQuestion
                {
                    Question = "What should you avoid on public Wi-Fi?",
                    OptionA = "Reading news",
                    OptionB = "Online banking",
                    OptionC = "Checking weather",
                    OptionD = "Browsing social media",
                    CorrectAnswer = "B",
                    Explanation = "Avoid sensitive activities like banking on unsecured public networks."
                },
                new QuizQuestion
                {
                    Question = "What is ransomware?",
                    OptionA = "Free antivirus software",
                    OptionB = "Malware that encrypts your files for money",
                    OptionC = "A type of browser",
                    OptionD = "A password manager",
                    CorrectAnswer = "B",
                    Explanation = "Ransomware encrypts your files and demands payment for the decryption key."
                },
                new QuizQuestion
                {
                    Question = "Which is the safest way to download software?",
                    OptionA = "Random websites",
                    OptionB = "Email attachments",
                    OptionC = "Official websites and app stores",
                    OptionD = "Peer-to-peer networks",
                    CorrectAnswer = "C",
                    Explanation = "Official sources verify software authenticity and safety."
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    OptionA = "Building social networks",
                    OptionB = "Manipulating people to reveal information",
                    OptionC = "Creating social media accounts",
                    OptionD = "Engineering social apps",
                    CorrectAnswer = "B",
                    Explanation = "Social engineering tricks people into giving away confidential information."
                },
                new QuizQuestion
                {
                    Question = "How often should you back up important data?",
                    OptionA = "Never",
                    OptionB = "Once in a lifetime",
                    OptionC = "Regularly",
                    OptionD = "Only after losing data",
                    CorrectAnswer = "C",
                    Explanation = "Regular backups protect against data loss from hardware failure or malware."
                }
            };
        }

        public List<QuizQuestion> GetQuestions()
        {
            try
            {
                return new List<QuizQuestion>(questions);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting questions: {ex.Message}");
                return new List<QuizQuestion>();
            }
        }
    }
}

