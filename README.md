# Cybersecurity Awareness Bot

A Windows desktop app that teaches everyday cybersecurity through conversation. A chatbot answers questions about passwords, phishing, malware and privacy, remembers what the user is interested in, and turns natural-language requests into tasks and reminders. A quiz game and an activity log round out the experience. Built with C#, WPF and XAML.

Built for PROG6221 at Emeris Durban North (2025).

## Features

- **Chatbot:** answers cybersecurity questions, follows up with "tell me more", and adjusts its replies to the user's sentiment and remembered topics.
- **Natural-language commands:** phrases such as "Remind me to update my password in 3 days" or "Add a task to enable 2FA" are parsed into tasks with reminder dates.
- **Task manager:** create, complete and delete security tasks, with indicators for upcoming and overdue reminders.
- **Quiz game:** ten multiple-choice and true/false questions, with explanations after each answer and a final score.
- **Activity log:** a timestamped record of tasks, reminders, quiz attempts and chatbot interactions for the session.
- **Voice greeting:** an audio welcome message on start-up.

## Tech stack

| Area | Technologies |
| --- | --- |
| App | C#, .NET 9, WPF, XAML |
| Platform | Windows desktop |

## Architecture

```text
MainWindow (navigation hub)
  |-- ChatbotWindow ------> ResponseEngine, NLPProcessor, UserSession (memory and sentiment)
  |-- TaskManagerWindow --> TaskManager, CyberTask
  |-- QuizGameWindow -----> QuizManager, QuizQuestion
  '-- ActivityLogWindow --> ActivityLog, LogEntry
```

Each window has its own XAML view and code-behind, while the conversation logic (intent detection, keyword matching and response selection with delegates) lives in plain C# classes that the windows share through the user session.

## Getting started

### Prerequisites

- Windows 10 or later
- .NET 9 SDK, or Visual Studio 2022 with the .NET desktop development workload

### Run locally

```bash
dotnet run --project CybersecurityAwarenessBot
```

Or open `CybersecurityAwarenessBot/CybersecurityAwarenessBot.sln` in Visual Studio and start the project. No external services are required.

## Project structure

```text
CybersecurityAwarenessBot/
  *Window.xaml(.cs)    Main hub, chatbot, task manager, quiz and activity log windows
  ResponseEngine.cs    Response selection and topic follow-ups
  NLPProcessor.cs      Intent and keyword detection, task and reminder parsing
  UserSession.cs       User name, remembered topics and sentiment
  TaskManager.cs       Tasks and reminders
  QuizManager.cs       Quiz questions, scoring and feedback
  ActivityLog.cs       Session activity log
  welcomeMessage.wav   Start-up audio
```

## Acknowledgements

Documentation, tutorials and adapted code patterns are credited in comments at the top of the source files.
