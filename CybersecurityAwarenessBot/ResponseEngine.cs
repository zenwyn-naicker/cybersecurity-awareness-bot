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
 * Available at: <https://www.c-sharpcorner.com/blogs/nongeneric-collection-with-arraylist-in-c-sharp> [Accessed 15 April 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: Aliza Vigderman
 * Date: 2024
 * Title: Cyber Security Tips
 * Type: [Information]
 * Available at: <https://www.security.org/digital-safety/cyber-security-tips/> [Accessed 15 April 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: Ravi Patel
 * Date: 2024
 * Title: Understanding Delegates in C# with Practical Examples
 * Type: [Source Code]
 * Available at: <https://medium.com/@ravipatel.it/understanding-delegates-in-c-with-practical-examples-fdb253740ad7> [Accessed 15 May 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2024
 * Title: Regex Tutorial - How to write Regular Expressions?
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/write-regular-expressions/> [Accessed 15 May 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: geeksforgeeks
 * Date: 2025
 * Title: C# Exceptions
 * Type: [Source Code]
 * Available at: <https://www.geeksforgeeks.org/c-sharp-exception/> [Accessed 15 May 2025]
 **************************************************************************************************************/
/**************************************************************************************************************
 * Author: OpenAi
 * Date: 2025
 * Title: Comprehensive list of cybersecurity related emotes
 * Type: [Source Code]
 * Link to chat: <https://chatgpt.com/share/68491858-c978-8009-9e48-4d46c7a83d0a> [Accessed 11 June 2025]
 **************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CybersecurityAwarenessBot
{
    // Delegate for response processing events
    public delegate string ResponseProcessorDelegate(string input, UserSession session);

    // Manages cybersecurity chatbot responses with memory and sentiment awareness
    class ResponseEngine
    {
        private readonly Random random;
        private readonly Dictionary<string, string[]> responses;

        // Delegate for custom response processing
        public ResponseProcessorDelegate CustomResponseProcessor;

        public ResponseEngine()
        {
            try
            {
                random = new Random();

                // Initialize custom response processor delegate
                CustomResponseProcessor = ProcessWithMemoryAndSentiment;

                // cybersecurity keyword responses
                responses = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
                {
                    // Greetings 
                    { "hello", new[] { "👋 Hello! I'm your cybersecurity assistant. What can I help you with?", "👋 Hi! Ready to discuss digital safety. How can I help?" } },
                    { "hi", new[] { "👋 Hi! I'm here for cybersecurity questions. What's on your mind?", "👋 Hello! Your digital safety guide here. What interests you?" } },
                    { "hey", new[] { "👋 Hey! Ready to boost your online security. What can I help with?", "👋 Hey! Your cybersecurity assistant at your service." } },
                    { "how are you", new[] { "👍 Operating perfectly! Ready to help with cybersecurity questions.", "🔋 At 100% and focused on keeping you safe online." } },

                    // Purpose and capabilities
                    { "purpose", new[] { "🎯 I enhance your cybersecurity awareness with practical, actionable advice.", "🚩 I make cybersecurity accessible with tips to navigate the digital world safely." } },
                    { "what do you do", new[] { "🛡️ I provide guidance on passwords, phishing, device protection, and more.", "🔍 I explain cybersecurity concepts and offer practical safety tips." } },
                    { "why are you here", new[] { "💡 I'm your cybersecurity resource to protect your digital identity.", "🔒 I empower you with knowledge to navigate online threats confidently." } },

                    // Help and topics overview
                    { "help", new[] { "📋 I help with passwords, phishing, network security, safe shopping, and more. What interests you?", "🧰 Ask about encryption, 2FA, secure browsing, privacy, or identity theft protection." } },
                    { "what can i ask", new[] { "❓ Ask about password managers, VPNs, malware, banking security, or smart home protection.", "💬 Feel free to ask about scams, account security, privacy settings, or Wi-Fi safety." } },
                    { "topics", new[] { "📚 I cover passwords, phishing, browsing, malware, privacy, payments, and account security.", "🗂️ Ask about encryption, network security, mobile protection, or social engineering." } },

                    // Password keywords - definitions
                    { "password", new[] { "🔑 A password is your digital key—a secret code that proves your identity to access accounts and devices.", "🔐 Passwords are authentication credentials that verify you are who you claim to be online." } },
                    { "passwords", new[] { "🔒 Password security involves creating strong, unique codes for each account to prevent unauthorized access.", "🗝️ Good password practices include using complex, memorable phrases that are hard for others to guess." } },

                    // Phishing keywords - definitions
                    { "phishing", new[] { "🎣 Phishing is a cyber attack where criminals impersonate trusted sources to steal your personal information.", "🔍 Phishing scams trick people into revealing passwords, credit cards, or other sensitive data through fake messages." } },
                    { "fake email", new[] { "🚨 Fake emails are fraudulent messages designed to look legitimate but actually steal your information or install malware.", "🛑 Email scams often impersonate banks, companies, or government agencies to gain your trust before attacking." } },
                    { "scam", new[] { "⚠️ A scam is any fraudulent scheme designed to trick you into giving money or personal information to criminals.", "🚨 Scams exploit trust and urgency to make you act quickly without thinking critically about the request." } },

                    // Privacy keywords - definitions
                    { "privacy", new[] { "🕵️ Digital privacy is your right to control what personal information you share and who can access it online.", "🔏 Online privacy involves protecting your personal data from being collected, tracked, or misused by others." } },

                    // Browsing keywords - definitions
                    { "browsing", new[] { "🛡️ Safe browsing means navigating the internet while protecting yourself from malicious websites and downloads.", "🔒 Secure browsing involves using tools and habits that prevent exposure to online threats while surfing the web." } },
                    { "safe website", new[] { "✅ A safe website is a legitimate site that protects your data and doesn't contain malware or scams.", "🔍 Trustworthy websites have security certificates, clear policies, and verified contact information." } },
                    { "https", new[] { "🔒 HTTPS is a secure version of HTTP that encrypts data between your browser and the website.", "🛡️ The 'S' in HTTPS stands for 'Secure'—it means your connection is encrypted and harder to intercept." } },

                    // Malware keywords - definitions
                    { "malware", new[] { "🦠 Malware is malicious software designed to damage your device, steal data, or gain unauthorized access.", "🛡️ Short for 'malicious software,' malware includes viruses, spyware, ransomware, and other harmful programs." } },
                    { "virus", new[] { "🐞 A computer virus is malicious code that spreads by attaching itself to other programs or files.", "🧹 Viruses can corrupt files, steal information, or damage your system by replicating and spreading automatically." } },
                    { "ransomware", new[] { "🔓 Ransomware is malware that encrypts your files and demands payment to unlock them.", "💽 This type of cyber attack holds your data hostage until you pay the criminals, often in cryptocurrency." } },

                    // Additional cybersecurity terms
                    { "encryption", new[] { "🔐 Encryption scrambles your data into unreadable code that only authorized parties can decrypt.", "🛡️ It's like putting your information in a digital safe that requires a special key to open." } },
                    { "2fa", new[] { "🔑 Two-factor authentication (2FA) adds an extra security step beyond just your password.", "📱 2FA typically requires something you know (password) plus something you have (phone/app)." } },
                    { "vpn", new[] { "🌐 A VPN (Virtual Private Network) creates a secure, encrypted tunnel for your internet connection.", "🔒 VPNs hide your online activity and location by routing your traffic through secure servers." } },
                    { "firewall", new[] { "🧱 A firewall is a security barrier that monitors and controls network traffic entering and leaving your device.", "🛡️ Think of it as a digital gatekeeper that blocks unauthorized access while allowing legitimate traffic." } },

                    // Exit keywords
                    { "bye", new[] { "👋 Goodbye! Remember: cybersecurity is ongoing—stay vigilant and keep learning.", "🛡️ Take care! Stay informed and skeptical of unexpected requests." } },
                    { "exit", new[] { "🚪 Exiting. Your strongest security asset is a cautious mindset. Stay safe!", "👋 Session ended. Keep updating devices and apps. Be safe!" } },
                    { "quit", new[] { "🛑 Ending session. Regularly update security software and systems. Stay secure!", "👋 Quitting. Remember: cybersecurity is a journey. Keep learning!" } },
                    { "goodbye", new[] { "👋 Farewell! Practice good digital hygiene: strong passwords, updates, skepticism.", "🔒 Goodbye! Remember: 'Think before you click, type, or share.' Stay safe!" } },
                };
            }
            catch (Exception ex)
            {
                // Log error and initialize with minimal functionality
                Console.WriteLine($"⚠️ Warning: Error initializing ResponseEngine: {ex.Message}");
                random = new Random();
                responses = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
                CustomResponseProcessor = (input, session) => "❓ I'm experiencing technical difficulties. Please try again.";
            }
        }

        // Enhanced input processing with memory and sentiment awareness
        public string ProcessInput(string userInput, UserSession session)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    return "⚠️ Please enter a valid question or statement.";
                }

                // Add to conversation history
                if (session != null)
                {
                    session.AddToHistory(userInput);
                }
                else
                {
                    throw new ArgumentNullException(nameof(session), "User session cannot be null");
                }

                // Use delegate for custom processing
                return CustomResponseProcessor?.Invoke(userInput, session) ??
                       "❓ I'm experiencing technical difficulties. Please try again.";
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"⚠️ Error: {ex.Message}");
                return "❓ Session error occurred. Please restart the application.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error processing input: {ex.Message}");
                return "❓ I encountered an error processing your request. Please try rephrasing your question.";
            }
        }

        // Custom response processor using delegates
        private string ProcessWithMemoryAndSentiment(string userInput, UserSession session)
        {
            try
            {
                // Get current sentiment
                string currentSentiment = session?.GetRecentSentiment() ?? "neutral";

                // Check for interest expressions FIRST before keyword matching
                if (IsInterestExpression(userInput))
                {
                    string detectedTopic = DetectTopicFromInterest(userInput);
                    if (!string.IsNullOrEmpty(detectedTopic))
                    {
                        try
                        {
                            session?.StoreInMemory("favorite_topic", detectedTopic);
                            return GetInterestConfirmationResponse(detectedTopic);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"⚠️ Error storing memory: {ex.Message}");
                            return GetInterestConfirmationResponse(detectedTopic);
                        }
                    }
                }

                // Find keyword matches
                string baseResponse = GetKeywordResponse(userInput);

                if (baseResponse != null)
                {
                    try
                    {
                        // Add sentiment-appropriate prefix
                        string sentimentPrefix = session?.GetSentimentResponsePrefix(currentSentiment) ?? "";

                        // Personalize the response
                        string personalizedResponse = session?.PersonalizeResponse(baseResponse) ?? baseResponse;

                        return sentimentPrefix + personalizedResponse;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Error personalizing response: {ex.Message}");
                        return baseResponse;
                    }
                }

                // Handle follow-up questions with memory context
                return HandleFollowUpWithMemory(userInput, session, currentSentiment);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error in ProcessWithMemoryAndSentiment: {ex.Message}");
                return "❓ I encountered an error processing your request. Please try again.";
            }
        }

        // Check if input is an interest expression
        private bool IsInterestExpression(string userInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userInput))
                    return false;

                string lowerInput = userInput.ToLower();
                return lowerInput.Contains("interested in") || lowerInput.Contains("i like") ||
                       lowerInput.Contains("want to know about") || lowerInput.Contains("curious about") ||
                       lowerInput.Contains("tell me about") || lowerInput.Contains("i want to learn about");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error checking interest expression: {ex.Message}");
                return false;
            }
        }

        // Detect topic from interest expression
        private string DetectTopicFromInterest(string userInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userInput))
                    return null;

                string lowerInput = userInput.ToLower();

                var topicKeywords = new Dictionary<string, string[]>
                {
                    { "passwords", new[] { "password", "passwords" } },
                    { "phishing", new[] { "phishing", "scam", "fake email" } },
                    { "privacy", new[] { "privacy", "private", "personal data" } },
                    { "malware", new[] { "malware", "virus", "ransomware" } },
                    { "browsing", new[] { "browsing", "website", "online safety" } }
                };

                foreach (var topic in topicKeywords)
                {
                    if (topic.Value.Any(keyword => lowerInput.Contains(keyword)))
                    {
                        return topic.Key;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error detecting topic from interest: {ex.Message}");
                return null;
            }
        }

        // Gets interest confirmation response
        private string GetInterestConfirmationResponse(string topic)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topic))
                    return "Great! I'll remember your interest. It's always good to stay curious about cybersecurity.";

                return topic.ToLower() switch
                {
                    "passwords" => "Great! I'll remember that you're interested in passwords. Strong password security is the foundation of online safety.",
                    "phishing" => "Great! I'll remember that you're interested in phishing. Learning to spot these attacks is crucial for staying safe online.",
                    "privacy" => "Great! I'll remember that you're interested in privacy. It's a crucial part of staying safe online.",
                    "malware" => "Great! I'll remember that you're interested in malware protection. It's essential for keeping your devices secure.",
                    "browsing" => "Great! I'll remember that you're interested in safe browsing. Smart browsing habits protect you from many online threats.",
                    _ => $"Great! I'll remember your interest in {topic}. It's always good to stay curious about cybersecurity."
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error generating interest confirmation: {ex.Message}");
                return "Great! I'll remember your interest. It's always good to stay curious about cybersecurity.";
            }
        }

        // Gets base response for keywords
        private string GetKeywordResponse(string userInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userInput) || responses == null)
                    return null;

                foreach (var kvp in responses)
                {
                    if (Regex.IsMatch(userInput, $@"\b{Regex.Escape(kvp.Key)}\b", RegexOptions.IgnoreCase))
                    {
                        if (kvp.Value != null && kvp.Value.Length > 0)
                        {
                            return kvp.Value[random.Next(kvp.Value.Length)];
                        }
                    }
                }
                return null;
            }
            catch (RegexMatchTimeoutException ex)
            {
                Console.WriteLine($"⚠️ Regex timeout error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting keyword response: {ex.Message}");
                return null;
            }
        }

        // Handles follow-up questions with memory context
        private string HandleFollowUpWithMemory(string userInput, UserSession session, string sentiment)
        {
            try
            {
                string sentimentPrefix = session?.GetSentimentResponsePrefix(sentiment) ?? "";

                // Check specifically for "tell me more" or "more" requests
                if (userInput.ToLower().Contains("tell me more") || userInput.ToLower().Contains("more"))
                {
                    try
                    {
                        string favoriteTopic = session?.GetFromMemory("favorite_topic") ?? "";
                        string username = session?.GetUsername() ?? "";

                        if (!string.IsNullOrEmpty(favoriteTopic))
                        {
                            return sentimentPrefix + GetDetailedTopicInfo(favoriteTopic, username);
                        }
                        else if (!string.IsNullOrEmpty(username))
                        {
                            return sentimentPrefix + $"🔍 I'd love to share more details, {username}! Try asking about specific topics like passwords, phishing, privacy, malware, or browsing safety.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Error handling 'tell me more' request: {ex.Message}");
                    }
                }

                // Check for other question patterns
                if (userInput.Contains("what") || userInput.Contains("how") || userInput.Contains("why") ||
                    userInput.Contains("when") || userInput.Contains("where") || userInput.Contains("?"))
                {
                    try
                    {
                        string favoriteTopic = session?.GetFromMemory("favorite_topic") ?? "";
                        string username = session?.GetUsername() ?? "";

                        if (!string.IsNullOrEmpty(favoriteTopic))
                        {
                            return sentimentPrefix + $"🔍 I'm here to help with more information about {favoriteTopic} or any other cybersecurity topic, {username}. What specific aspect interests you?";
                        }
                        else if (!string.IsNullOrEmpty(username))
                        {
                            return sentimentPrefix + $"🔍 I'm here to help with any cybersecurity questions you have, {username}. What would you like to know more about?";
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"⚠️ Error handling question patterns: {ex.Message}");
                    }
                }

                // Default response with personalization
                string defaultResponse = "❓ I'm not sure about that. Try asking about password safety, phishing, privacy, or other cybersecurity topics.";
                return session?.PersonalizeResponse(sentimentPrefix + defaultResponse) ??
                       (sentimentPrefix + defaultResponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error handling follow-up with memory: {ex.Message}");
                return "❓ I'm not sure about that. Try asking about password safety, phishing, privacy, or other cybersecurity topics.";
            }
        }

        // Provides detailed information about specific topics
        private string GetDetailedTopicInfo(string topic, string username)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(topic))
                    return $"🔍 I'd love to provide more details! Try asking about specific topics like passwords, phishing, privacy, malware, or browsing safety for comprehensive information.";

                string userGreeting = string.IsNullOrWhiteSpace(username) ? "" : $", {username}";

                return topic.ToLower() switch
                {
                    "passwords" => $"🔐 Here's more about password security{userGreeting}: Use unique passwords for every account, especially banking and email. Password managers like Bitwarden or 1Password generate strong passwords and store them securely. Always enable 2FA when available for an extra security layer.",
                    "phishing" => $"🎣 Here's more about phishing protection{userGreeting}: Always verify sender email addresses carefully, not just display names. Look for urgent language and grammar errors. Hover over links to see real destinations before clicking. Never provide sensitive info through email links—go to official websites directly.",
                    "privacy" => $"🕵️ Here's more about privacy protection{userGreeting}: Use private browsing for sensitive activities and regularly review app permissions on all devices. Consider privacy-focused browsers like Firefox or Brave, and search engines like DuckDuckGo. Use ad blockers to prevent tracking.",
                    "malware" => $"🦠 Here's more about malware protection{userGreeting}: Keep all software and operating systems updated with automatic updates enabled. Use reputable antivirus software and run regular scans. Be extremely cautious with email attachments and only download from trusted sources. Maintain regular backups.",
                    "browsing" => $"🔒 Here's more about safe browsing{userGreeting}: Always check for HTTPS (padlock icon) before entering personal information. Use a VPN on public Wi-Fi networks for encryption. Keep browsers updated, use security extensions, and be cautious with downloads from unknown sites.",
                    _ => $"🔍 Ask about passwords, phishing, privacy, malware, or browsing for detailed cybersecurity guidance{userGreeting}!"
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error generating detailed topic info: {ex.Message}");
                return "🔍 I'd love to provide more details! Try asking about specific topics like passwords, phishing, privacy, malware, or browsing safety for comprehensive information.";
            }
        }

        // Memory-aware topic suggestions
        public string GetPersonalizedSuggestions(UserSession session)
        {
            try
            {
                string username = session?.GetUsername() ?? "there";
                return $"💡 {username}, here are some cybersecurity topics you might find interesting: passwords, phishing, privacy, malware, or safe browsing.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error getting personalized suggestions: {ex.Message}");
                return "💡 Here are some cybersecurity topics you might find interesting: passwords, phishing, privacy, malware, or safe browsing.";
            }
        }

        // Checks if user wants to exit
        public bool IsExitRequest(string userInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userInput))
                    return false;

                string lowerInput = userInput.ToLower();
                return lowerInput.Contains("exit") || lowerInput.Contains("quit") ||
                       lowerInput.Contains("bye") || lowerInput.Contains("goodbye");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error checking exit request: {ex.Message}");
                return false;
            }
        }
    }
}

