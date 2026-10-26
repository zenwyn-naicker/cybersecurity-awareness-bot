/*Author: Zenwyn Naicker*/
/*Code Attribution: */
/**************************************************************************************************************
 * Author: w3schools
 * Date: 2025
 * Title: C# Tutorial
 * Type: [Source Code]
 * Available at: <https://www.w3schools.com/cs/index.php> [Accessed 10 May 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: UT
 * Date: 2024
 * Title: Collections in C#
 * Type: [Source Code]
 * Available at: <https://www.c-sharpcorner.com/blogs/nongeneric-collection-with-arraylist-in-c-sharp> [Accessed 15 May 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 15 May 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    // Manages user session data with memory and sentiment detection
    public class UserSession
    {
        private string username;
        private Dictionary<string, string> userMemory;
        private List<string> conversationHistory;

        // Dictionary for sentiment keywords
        private readonly Dictionary<string, string[]> sentimentKeywords = new Dictionary<string, string[]>
        {
            { "worried", new[] { "worried", "concerned", "anxious", "scared", "afraid", "nervous" } },
            { "frustrated", new[] { "frustrated", "annoyed", "confused", "stuck", "lost", "overwhelmed" } },
            { "interested", new[] { "curious", "interested", "want to know" } },
            { "confident", new[] { "confident", "ready", "understand", "got it", "makes sense", "clear" } },
            { "grateful", new[] { "thank", "thanks", "appreciate", "helpful", "great", "awesome" } }
        };

        // Constructor
        public UserSession()
        {
            try
            {
                userMemory = new Dictionary<string, string>();
                conversationHistory = new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Warning: Error initializing UserSession: {ex.Message}");
                // Initialize with minimal functionality to prevent null reference errors
                userMemory = new Dictionary<string, string>();
                conversationHistory = new List<string>();
            }
        }

        // Sets username and stores it in memory
        public void SetUsername(string value)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Username cannot be empty or whitespace.");
                }

                username = value;
                StoreInMemory("username", value);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"⚠️ Error setting username: {ex.Message}");
                throw; // Re-throw to maintain the contract for validation errors
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Unexpected error setting username: {ex.Message}");
                // Still set the username even if memory storage fails
                username = value;
            }
        }

        // Gets username
        public string GetUsername()
        {
            try
            {
                return username;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting username: {ex.Message}");
                return null;
            }
        }

        // Stores information in user memory
        public void StoreInMemory(string key, string value)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(value))
                {
                    userMemory[key.ToLower()] = value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error storing in memory: {ex.Message}");
            }
        }

        // Retrieves information from memory
        public string GetFromMemory(string key)
        {
            try
            {
                return userMemory.ContainsKey(key.ToLower()) ? userMemory[key.ToLower()] : null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error retrieving from memory: {ex.Message}");
                return null;
            }
        }

        // Gets all stored memory as a dictionary
        public Dictionary<string, string> GetAllMemory()
        {
            try
            {
                return new Dictionary<string, string>(userMemory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting all memory: {ex.Message}");
                return new Dictionary<string, string>();
            }
        }

        // Adds message to conversation history and detects sentiment
        public void AddToHistory(string message)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    conversationHistory.Add(message);
                    DetectSentiment(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error adding to history: {ex.Message}");
            }
        }

        // Detects sentiment in user input
        private void DetectSentiment(string message)
        {
            try
            {
                string lowerMessage = message.ToLower();

                foreach (var sentiment in sentimentKeywords)
                {
                    if (sentiment.Value.Any(keyword => lowerMessage.Contains(keyword)))
                    {
                        break; // Only detect the first matching sentiment
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error detecting sentiment: {ex.Message}");
            }
        }

        // Gets the most recent detected sentiment from conversation
        public string GetRecentSentiment()
        {
            try
            {
                if (conversationHistory.Count > 0)
                {
                    string recentMessage = conversationHistory.Last().ToLower();

                    foreach (var sentiment in sentimentKeywords)
                    {
                        if (sentiment.Value.Any(keyword => recentMessage.Contains(keyword)))
                        {
                            return sentiment.Key;
                        }
                    }
                }
                return "neutral";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting recent sentiment: {ex.Message}");
                return "neutral";
            }
        }

        // Gets conversation history
        public List<string> GetConversationHistory()
        {
            try
            {
                return new List<string>(conversationHistory);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting conversation history: {ex.Message}");
                return new List<string>();
            }
        }

        // Personalizes a response based on stored memory
        public string PersonalizeResponse(string baseResponse)
        {
            try
            {
                string personalizedResponse = baseResponse;

                // Add name personalization
                if (!string.IsNullOrEmpty(username) && !baseResponse.Contains(username))
                {
                    personalizedResponse = $"{username}, {personalizedResponse.ToLowerInvariant()}";
                }

                // Add topic-specific personalization
                string favoriteTopic = GetFromMemory("favorite_topic");
                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    personalizedResponse += $" Since you're interested in {favoriteTopic}, this is especially relevant for you.";
                }

                return personalizedResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error personalizing response: {ex.Message}");
                return baseResponse;
            }
        }

        // Gets sentiment-appropriate response prefix
        public string GetSentimentResponsePrefix(string sentiment)
        {
            try
            {
                return sentiment switch
                {
                    "worried" => "It's completely understandable to feel that way. ",
                    "frustrated" => "I can sense this might be confusing. Let me help clarify things. ",
                    "interested" => "Great question! I love your curiosity about cybersecurity. ",
                    "confident" => "Excellent! You seem to have a good grasp of this. ",
                    "grateful" => "You're very welcome! I'm glad I could help. ",
                    _ => ""
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting sentiment response prefix: {ex.Message}");
                return "";
            }
        }
    }
}