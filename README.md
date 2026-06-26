# 🔐 Cybersecurity Awareness Chatbot - POE (Part 3)

## 📋 Project Overview

This is the **final POE submission** for the Cybersecurity Awareness Chatbot project. The application is a **GUI-based chatbot** built with Windows Forms that educates South African citizens about cybersecurity threats. This version includes all features from **Parts 1, 2, and 3**.

---

## 🎯 Features Implemented

| # | Feature | Description |
|---|---------|-------------|
| 1 | **Voice Greeting** | Plays WAV audio file at startup |
| 2 | **ASCII Art Logo** | Cybersecurity logo displayed on launch |
| 3 | **Keyword Recognition** | Recognizes: password, phishing, scam, privacy, 2fa |
| 4 | **Random Responses** | Multiple responses per topic |
| 5 | **Memory & Recall** | Remembers user's name and interests |
| 6 | **Sentiment Detection** | Detects worried, confused, curious emotions |
| 7 | **Task Assistant** | Add, view, complete, delete tasks with JSON storage |
| 8 | **Cybersecurity Quiz** | 12 questions with immediate feedback and scoring |
| 9 | **NLP Simulation** | Understands different phrasing for tasks, quiz, log |
| 10 | **Activity Log** | Tracks all actions with "show more" feature |

---

## 🚀 How to Run the Application

### Prerequisites
- Windows OS
- .NET 10.0 SDK
- Visual Studio 2022 or later

### Steps to Run

1. **Clone the repository**
   ```bash
   git clone https://github.com/evy-dev-2723/CyberChatbotPOE.git
   cd CyberChatbotPOE
Open in Visual Studio

Double-click CyberChatbotPOE.sln or open Visual Studio and select Open Project

Install NuGet Package

Right-click project → Manage NuGet Packages

Search for Newtonsoft.Json

Click Install

Build the project

Press Ctrl + Shift + B

Run the application

Press F5

💬 Chatbot Commands
Command	Response
[Your Name]	Bot remembers your name
password	Strong password tips
phishing	How to spot fake emails
scam	South African scams explained
privacy	Online privacy protection
help	List of all commands
add task: Title - Description	Adds a task to JSON file
show tasks	Displays all tasks
complete task id 5	Marks task as complete
delete task id 5	Deletes a task
start quiz	Starts cybersecurity quiz
A, B, C, or D	Answers quiz question
show log	Shows recent activity log
show full log	Shows complete activity log
exit	Close the application
📁 Project Structure
text
CyberChatbotPOE/
│
├── Form1.cs                 # Main GUI with 4 tabs (Chat, Tasks, Quiz, Log)
├── Chatbot.cs               # Bot logic with NLP intent detection
├── VoiceGreeting.cs         # Audio playback for voice greeting
├── CyberTask.cs             # Task model class
├── TaskStorageHelper.cs     # JSON file storage for tasks
├── TaskManager.cs           # Task business logic
├── QuizManager.cs           # Quiz logic with 12 questions
├── ActivityLogger.cs        # Activity logging with show more feature
├── Program.cs               # Application entry point
├── audio/
│   └── greeting.wav         # Voice greeting audio file
├── tasks.json               # Auto-created when first task is added
├── README.md                # This file
└── .gitignore               # Git ignore rules
🎮 Example Conversation
text
[Voice Greeting Plays]

╔═══════════════════════════════════════════════════════════════════╗
║         🔐  CYBERSECURITY AWARENESS BOT - POE  🔐               ║
╚═══════════════════════════════════════════════════════════════════╝

[Bot] Hello! What is your name?

[You]: Thabo

[Bot] Nice to meet you, Thabo! 🔐
[Bot] Try: password, add task, start quiz, show log, or help

[Thabo]: password

[Bot]: 🔐 Use strong passwords with 12+ characters, numbers, and symbols!

[Thabo]: add task: Update antivirus - Run full system scan

[Bot]: ✅ Task added: Update antivirus

[Thabo]: show tasks

[Bot]: YOUR TASKS:
⏳ ID:1 | Update antivirus

[Thabo]: start quiz

[Bot]: 🎮 QUIZ STARTED!
QUESTION 1 of 12: What should you do if you receive an email asking for your password?

[Thabo]: C

[Bot]: ✅ CORRECT! Report phishing emails!

[Thabo]: show log

[Bot]: RECENT ACTIVITIES:
1. [14:32] New user: Thabo
2. [14:33] Task added: Update antivirus
3. [14:35] Quiz started
4. [14:36] Quiz answer correct: C

[Thabo]: exit

[Bot]: Goodbye, Thabo! Stay safe!
🛠️ Technical Details
JSON Task Storage
Tasks are stored in tasks.json in the same folder as the .exe

Auto-created when the first task is added

All CRUD operations sync with the JSON file

NLP Intent Detection
Uses string.Contains() to detect task, quiz, and log intents

Handles different phrasing: "add task", "create task", "new task"

"remind me", "reminder" triggers task creation

Quiz System
12 cybersecurity questions

Immediate feedback after each answer

Final score with personalized message

Activity Log
Timestamped entries for all actions

Shows last 10 entries by default

"Show More" option for full history

🔗 GitHub Repository
Repository URL: https://github.com/evy-dev-2723/CyberChatbotPOE

Repository Statistics
✅ 6+ commits with meaningful messages

✅ 3 tagged releases: v3.0, v3.1, v3.2

✅ Complete source code included

✅ README.md included

✅ Newtonsoft.Json NuGet package required

👨‍💻 Author
Field	Information
Name	[Evelyne Clementine Filloi]
Student ID	[ST10492340]
Course	PROG6221 - Programming 2A
Project	POE - Cybersecurity Chatbot
Date 26	June 2026
📚 Topics Covered
The chatbot provides educational information on:

Topic	Description
Password Safety	Creating strong passwords, avoiding common mistakes
Phishing Scams	Recognizing fake emails, SMS, and WhatsApp messages
South African Scams	Bank scams, lottery scams, "Hello Mum" scam, SARS scams
Online Privacy	Social media settings, app permissions, data protection
Safe Browsing	HTTPS, public Wi-Fi dangers, browser security
Two-Factor Authentication	What it is, how to enable it, best practices
Social Engineering	Manipulation tactics, how to recognize them
📞 Emergency Contacts for South Africa
Service	Phone Number
SAPS Crime Stop	08600 10111
SAFPS (Fraud Prevention)	011 867 2234
Report Phishing	report@phishing.org.za
✅ POE Submission Checklist
GUI application with Windows Forms

Voice greeting with WAV file

ASCII art logo display

Keyword recognition for cybersecurity topics

Random responses using Lists

Memory and recall (remembers name and interests)

Sentiment detection (worried, confused, curious)

Task Assistant with JSON storage (CRUD operations)

Cybersecurity Quiz with 12 questions

NLP Simulation (understands different phrasing)

Activity Log with "show more" feature

All Parts 1, 2, and 3 features working together

GitHub repository with 6+ commits

3 tagged releases (v3.0, v3.1, v3.2)

YouTube video demonstration (unlisted)

Newtonsoft.Json NuGet package installed


✅ FEATURES:
- Task Assistant with JSON storage (add, view, complete, delete)
- Cybersecurity Quiz (12 questions with immediate feedback)
- NLP Simulation (understands different phrasing)
- Activity Log with "show more" feature
- Voice greeting with ASCII art
- Keyword recognition and random responses
- Memory, sentiment detection, and conversation flow

🔗 GITHUB:
https://github.com/evy-dev-2723/CyberChatbotPOE

📁 FILES:
- Form1.cs (GUI with 4 tabs)
- Chatbot.cs (NLP intent detection)
- TaskManager.cs (Task logic)
- QuizManager.cs (12 questions)
- ActivityLogger.cs (Activity log)
- TaskStorageHelper.cs (JSON storage)

👨‍💻 Author: [EVELYNE CLEMENTINE FILLOI]
📅 Date: June 2026
