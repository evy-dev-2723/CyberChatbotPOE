using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberChatbotPOE
{
    public partial class Form1 : Form
    {
        private Chatbot chatbot;
        private VoiceGreeting voice;
        private ActivityLogger logger;
        private TaskManager taskManager;
        private QuizManager quizManager;
        private string userName;

        // Controls
        private TabControl tabControl;
        private RichTextBox chatDisplay;
        private TextBox userInputBox;
        private Button sendBtn, voiceBtn, clearBtn;
        private Label statusLabel;

        private ListBox taskList;
        private TextBox taskTitle, taskDesc, taskReminder;
        private Button addTaskBtn, completeTaskBtn, deleteTaskBtn;

        private RichTextBox quizDisplay;
        private TextBox quizAnswer;
        private Button submitAnswerBtn, startQuizBtn;
        private Label quizScore;

        private RichTextBox logDisplay;
        private Button refreshLogBtn, fullLogBtn;

        public Form1()
        {
            // Initialize services
            logger = new ActivityLogger();
            taskManager = new TaskManager(logger);
            quizManager = new QuizManager();
            chatbot = new Chatbot();
            voice = new VoiceGreeting();

            chatbot.SetTaskManager(taskManager);
            chatbot.SetLogger(logger);
            chatbot.SetQuizManager(quizManager);

            // Form setup
            this.Text = "🔐 Cybersecurity Chatbot - POE";
            this.Size = new Size(1100, 750);
            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            CreateTabs();
            CreateChatTab();
            CreateTasksTab();
            CreateQuizTab();
            CreateLogTab();

            voice.PlayGreeting();
            DisplayWelcome();
            logger.Log("Chatbot started");
        }

        private void CreateTabs()
        {
            tabControl = new TabControl { Location = new Point(10, 10), Size = new Size(1060, 690), Font = new Font("Segoe UI", 10) };
            this.Controls.Add(tabControl);
        }

        private void CreateChatTab()
        {
            var tab = new TabPage { Text = "💬 Chat", BackColor = Color.Black };
            tabControl.Controls.Add(tab);

            chatDisplay = new RichTextBox
            {
                Location = new Point(15, 15),
                Size = new Size(1020, 450),
                BackColor = Color.Black,
                ForeColor = Color.Cyan,
                Font = new Font("Consolas", 9),
                ReadOnly = true
            };
            tab.Controls.Add(chatDisplay);

            userInputBox = new TextBox
            {
                Location = new Point(15, 480),
                Size = new Size(880, 30),
                BackColor = Color.DarkGray,
                ForeColor = Color.White
            };
            userInputBox.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) ProcessInput(); };
            tab.Controls.Add(userInputBox);

            sendBtn = new Button
            {
                Text = "Send",
                Location = new Point(905, 478),
                Size = new Size(130, 35),
                BackColor = Color.DarkCyan,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            sendBtn.Click += (s, e) => ProcessInput();
            tab.Controls.Add(sendBtn);

            voiceBtn = new Button
            {
                Text = "Voice",
                Location = new Point(15, 520),
                Size = new Size(100, 35),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            voiceBtn.Click += (s, e) => voice.PlayGreeting();
            tab.Controls.Add(voiceBtn);

            clearBtn = new Button
            {
                Text = "Clear",
                Location = new Point(905, 520),
                Size = new Size(130, 35),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            clearBtn.Click += (s, e) => chatDisplay.Clear();
            tab.Controls.Add(clearBtn);

            statusLabel = new Label
            {
                Text = "Ready",
                Location = new Point(130, 528),
                Size = new Size(760, 25),
                ForeColor = Color.LightGreen
            };
            tab.Controls.Add(statusLabel);
        }

        private void CreateTasksTab()
        {
            var tab = new TabPage { Text = "📋 Tasks", BackColor = Color.Black };
            tabControl.Controls.Add(tab);

            taskList = new ListBox
            {
                Location = new Point(15, 15),
                Size = new Size(1020, 200),
                BackColor = Color.Black,
                ForeColor = Color.Cyan,
                Font = new Font("Consolas", 10)
            };
            tab.Controls.Add(taskList);

            var refreshBtn = new Button
            {
                Text = "Refresh",
                Location = new Point(15, 230),
                Size = new Size(120, 35),
                BackColor = Color.DarkCyan,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            refreshBtn.Click += (s, e) => RefreshTasks();
            tab.Controls.Add(refreshBtn);

            var lbl1 = new Label { Text = "Title:", Location = new Point(15, 280), Size = new Size(60, 25), ForeColor = Color.White };
            tab.Controls.Add(lbl1);
            taskTitle = new TextBox { Location = new Point(80, 278), Size = new Size(400, 30), BackColor = Color.DarkGray, ForeColor = Color.White };
            tab.Controls.Add(taskTitle);

            var lbl2 = new Label { Text = "Desc:", Location = new Point(15, 320), Size = new Size(60, 25), ForeColor = Color.White };
            tab.Controls.Add(lbl2);
            taskDesc = new TextBox { Location = new Point(80, 318), Size = new Size(500, 30), BackColor = Color.DarkGray, ForeColor = Color.White };
            tab.Controls.Add(taskDesc);

            var lbl3 = new Label { Text = "Reminder:", Location = new Point(15, 360), Size = new Size(70, 25), ForeColor = Color.White };
            tab.Controls.Add(lbl3);
            taskReminder = new TextBox { Location = new Point(90, 358), Size = new Size(400, 30), BackColor = Color.DarkGray, ForeColor = Color.White, Text = "7 days" };
            tab.Controls.Add(taskReminder);

            addTaskBtn = new Button
            {
                Text = "Add",
                Location = new Point(15, 405),
                Size = new Size(100, 35),
                BackColor = Color.DarkGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            addTaskBtn.Click += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(taskTitle.Text))
                {
                    taskManager.AddTask(taskTitle.Text, taskDesc.Text, taskReminder.Text);
                    RefreshTasks();
                    taskTitle.Clear(); taskDesc.Clear();
                    AppendChat($"✅ Task added: {taskTitle.Text}", Color.Green);
                }
            };
            tab.Controls.Add(addTaskBtn);

            completeTaskBtn = new Button
            {
                Text = "Complete",
                Location = new Point(125, 405),
                Size = new Size(120, 35),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            completeTaskBtn.Click += (s, e) =>
            {
                if (taskList.SelectedItem != null)
                {
                    int id = ExtractId(taskList.SelectedItem.ToString());
                    if (id > 0) { taskManager.MarkAsComplete(id); RefreshTasks(); AppendChat($"✅ Task completed!", Color.Green); }
                }
            };
            tab.Controls.Add(completeTaskBtn);

            deleteTaskBtn = new Button
            {
                Text = "Delete",
                Location = new Point(255, 405),
                Size = new Size(120, 35),
                BackColor = Color.DarkRed,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            deleteTaskBtn.Click += (s, e) =>
            {
                if (taskList.SelectedItem != null)
                {
                    int id = ExtractId(taskList.SelectedItem.ToString());
                    if (id > 0) { taskManager.DeleteTask(id); RefreshTasks(); AppendChat($"🗑 Task deleted!", Color.Orange); }
                }
            };
            tab.Controls.Add(deleteTaskBtn);

            RefreshTasks();
        }

        private void RefreshTasks()
        {
            taskList.Items.Clear();
            var tasks = taskManager.GetAllTasks();
            foreach (var t in tasks)
                taskList.Items.Add($"{(t.IsComplete ? "✅" : "⏳")} ID:{t.Id} | {t.Title}");
            if (taskList.Items.Count == 0) taskList.Items.Add("No tasks found.");
        }

        private int ExtractId(string text)
        {
            try { int s = text.IndexOf("ID:") + 3; int e = text.IndexOf(" ", s); return int.Parse(text.Substring(s, (e == -1 ? text.Length : e) - s)); }
            catch { return -1; }
        }

        private void CreateQuizTab()
        {
            var tab = new TabPage { Text = "🎮 Quiz", BackColor = Color.Black };
            tabControl.Controls.Add(tab);

            quizDisplay = new RichTextBox
            {
                Location = new Point(15, 15),
                Size = new Size(1020, 450),
                BackColor = Color.Black,
                ForeColor = Color.Yellow,
                Font = new Font("Consolas", 10),
                ReadOnly = true
            };
            tab.Controls.Add(quizDisplay);

            quizAnswer = new TextBox
            {
                Location = new Point(15, 480),
                Size = new Size(880, 30),
                BackColor = Color.DarkGray,
                ForeColor = Color.White
            };
            quizAnswer.KeyPress += (s, e) => { if (e.KeyChar == (char)Keys.Enter) SubmitQuizAnswer(); };
            tab.Controls.Add(quizAnswer);

            submitAnswerBtn = new Button
            {
                Text = "Submit",
                Location = new Point(905, 478),
                Size = new Size(130, 35),
                BackColor = Color.DarkOrange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            submitAnswerBtn.Click += (s, e) => SubmitQuizAnswer();
            tab.Controls.Add(submitAnswerBtn);

            startQuizBtn = new Button
            {
                Text = "Start Quiz",
                Location = new Point(15, 520),
                Size = new Size(150, 35),
                BackColor = Color.Purple,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            startQuizBtn.Click += (s, e) =>
            {
                quizManager.StartQuiz();
                quizManager.SetActive(true);
                chatbot.SetQuizMode(true);
                var q = quizManager.GetCurrentQuestion();
                if (q != null)
                {
                    quizDisplay.Text = q.GetQuestionText(1, quizManager.GetTotal());
                    quizScore.Text = $"Score: 0 / {quizManager.GetTotal()}";
                    logger.Log("Quiz started");
                    AppendChat("🎮 Quiz started!", Color.Magenta);
                }
            };
            tab.Controls.Add(startQuizBtn);

            quizScore = new Label
            {
                Text = "Score: 0 / 0",
                Location = new Point(180, 528),
                Size = new Size(200, 25),
                ForeColor = Color.LightGreen
            };
            tab.Controls.Add(quizScore);

            quizDisplay.Text = "🎮 CYBERSECURITY QUIZ\n\nClick 'Start Quiz' to begin!";
        }

        private void SubmitQuizAnswer()
        {
            string a = quizAnswer.Text.Trim().ToUpper();
            if (!string.IsNullOrEmpty(a) && quizManager.IsActive() && !quizManager.IsFinished())
            {
                bool correct = quizManager.SubmitAnswer(a);
                quizAnswer.Clear();
                if (quizManager.IsFinished())
                {
                    int score = quizManager.GetScore();
                    int total = quizManager.GetTotal();
                    quizDisplay.Text = $"🎯 QUIZ COMPLETE!\n\nScore: {score} out of {total}\n\n{quizManager.GetFinalMessage()}";
                    quizScore.Text = $"Score: {score} / {total}";
                    chatbot.SetQuizMode(false);
                    logger.Log($"Quiz complete: {score}/{total}");
                    AppendChat($"🎯 Quiz complete! Score: {score}/{total}", Color.Magenta);
                }
                else
                {
                    var q = quizManager.GetCurrentQuestion();
                    quizDisplay.Text = $"{(correct ? "✅ CORRECT!" : "❌ WRONG!")}\n\n{q.GetQuestionText(quizManager.GetScore() + 1, quizManager.GetTotal())}";
                    quizScore.Text = $"Score: {quizManager.GetScore()} / {quizManager.GetTotal()}";
                }
            }
        }

        private void CreateLogTab()
        {
            var tab = new TabPage { Text = "📜 Activity Log", BackColor = Color.Black };
            tabControl.Controls.Add(tab);

            logDisplay = new RichTextBox
            {
                Location = new Point(15, 15),
                Size = new Size(1020, 520),
                BackColor = Color.Black,
                ForeColor = Color.Cyan,
                Font = new Font("Consolas", 9),
                ReadOnly = true
            };
            tab.Controls.Add(logDisplay);

            refreshLogBtn = new Button
            {
                Text = "Refresh (Last 10)",
                Location = new Point(15, 550),
                Size = new Size(170, 35),
                BackColor = Color.DarkCyan,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            refreshLogBtn.Click += (s, e) => logDisplay.Text = logger.GetRecentLog(10);
            tab.Controls.Add(refreshLogBtn);

            fullLogBtn = new Button
            {
                Text = "Show Full Log",
                Location = new Point(195, 550),
                Size = new Size(150, 35),
                BackColor = Color.DarkBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            fullLogBtn.Click += (s, e) => logDisplay.Text = logger.GetFullLog();
            tab.Controls.Add(fullLogBtn);

            logDisplay.Text = logger.GetRecentLog(10);
        }

        private void DisplayWelcome()
        {
            string art = @"
╔═══════════════════════════════════════════════════════════════════╗
║         🔐  CYBERSECURITY AWARENESS BOT - POE  🔐               ║
║              Protecting South Africa Online                      ║
╚═══════════════════════════════════════════════════════════════════╝";

            AppendChat(art, Color.Cyan);
            AppendChat("", Color.White);
            AppendChat("🌍 WELCOME TO CYBERSECURITY CHATBOT - POE", Color.Yellow);
            AppendChat("", Color.White);
            AppendChat("[Bot] Hello! What is your name?", Color.Cyan);
        }

        private void AppendChat(string msg, Color c)
        {
            if (chatDisplay.InvokeRequired) { chatDisplay.Invoke(new Action(() => AppendChat(msg, c))); return; }
            chatDisplay.SelectionStart = chatDisplay.TextLength;
            chatDisplay.SelectionColor = c;
            chatDisplay.AppendText(msg + "\n");
            chatDisplay.ScrollToCaret();
        }

        private void ProcessInput()
        {
            string input = userInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            string displayName = string.IsNullOrEmpty(userName) ? "You" : userName;
            AppendChat($"[{displayName}]: {input}", Color.LightGreen);
            userInputBox.Clear();

            if (string.IsNullOrEmpty(userName))
            {
                userName = input;
                chatbot.SetUserName(userName);
                AppendChat($"[Bot] Nice to meet you, {userName}! 🔐", Color.Cyan);
                AppendChat("[Bot] Try: password, add task, start quiz, show log, or help", Color.Cyan);
                logger.Log($"New user: {userName}");
                return;
            }

            if (input.ToLower() == "exit")
            {
                AppendChat($"[Bot] Goodbye, {userName}! Stay safe!", Color.Cyan);
                logger.Log($"User {userName} exited");
                Application.Exit();
                return;
            }

            statusLabel.Text = "Thinking...";
            Application.DoEvents();

            string response = chatbot.GetResponse(input);
            AppendChat($"[Bot]: {response}", Color.Cyan);

            statusLabel.Text = "Ready";
            userInputBox.Focus();
        }
    }
}