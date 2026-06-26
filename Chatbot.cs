using System;
using System.Collections.Generic;

namespace CyberChatbotPOE
{
    public class Chatbot
    {
        private string userName;
        private Random rand = new Random();
        private TaskManager _taskManager;
        private ActivityLogger _logger;
        private QuizManager _quizManager;
        private bool _quizMode;

        public void SetTaskManager(TaskManager tm) => _taskManager = tm;
        public void SetLogger(ActivityLogger l) => _logger = l;
        public void SetQuizManager(QuizManager qm) => _quizManager = qm;
        public void SetQuizMode(bool m) => _quizMode = m;
        public void SetUserName(string n) => userName = n;
        public bool IsQuizMode() => _quizMode;

        private Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            { "password", new List<string> { "🔐 Use 12+ characters with numbers and symbols!", "✅ Never reuse passwords!", "⚠️ Avoid 'password123'!" } },
            { "phishing", new List<string> { "🎣 Don't click suspicious links!", "📧 Check sender's email carefully!" } },
            { "scam", new List<string> { "⚠️ Never share OTPs!", "📞 Report scams to SAPS: 08600 10111" } },
            { "privacy", new List<string> { "🔏 Set social media to private!", "📱 Review app permissions!" } },
            { "help", new List<string> { "📚 Commands: password, phishing, scam, add task, show tasks, start quiz, show log" } }
        };

        public string GetResponse(string input)
        {
            string i = input.ToLower();

            // TASK INTENT
            if (i.Contains("add task") || i.Contains("create task"))
            {
                string text = input.Replace("add task", "").Replace("create task", "").Trim();
                string title = text, desc = "No description";
                if (text.Contains("-")) { var p = text.Split('-'); title = p[0].Trim(); desc = p[1].Trim(); }
                _logger?.Log($"Task added: {title}");
                return _taskManager?.AddTask(title, desc, "") ?? "Task manager not ready.";
            }

            // REMINDER
            if (i.Contains("remind me"))
            {
                string text = input.Replace("remind me", "").Trim();
                _logger?.Log($"Reminder set: {text}");
                return _taskManager?.AddTask(text, $"Reminder: {text}", "7 days") ?? "Task manager not ready.";
            }

            // SHOW TASKS
            if (i.Contains("show tasks") || i.Contains("list tasks"))
            {
                _logger?.Log("User viewed tasks");
                return _taskManager?.GetFormattedTasks() ?? "Task manager not ready.";
            }

            // COMPLETE TASK
            if (i.Contains("complete") && i.Contains("task"))
            {
                int id = ExtractId(input);
                if (id > 0) return _taskManager?.MarkAsComplete(id) ?? "Task manager not ready.";
                return "Please specify task ID. Example: 'complete task id 5'";
            }

            // DELETE TASK
            if ((i.Contains("delete") || i.Contains("remove")) && i.Contains("task"))
            {
                int id = ExtractId(input);
                if (id > 0) return _taskManager?.DeleteTask(id) ?? "Task manager not ready.";
                return "Please specify task ID. Example: 'delete task id 5'";
            }

            // START QUIZ
            if (i.Contains("start quiz") || i.Contains("quiz me"))
            {
                _quizMode = true;
                _quizManager?.StartQuiz();
                _logger?.Log("Quiz started");
                var q = _quizManager?.GetCurrentQuestion();
                return $"🎮 QUIZ STARTED!\n\n{q?.GetQuestionText(1, _quizManager?.GetTotal() ?? 12)}";
            }

            // QUIZ ANSWER
            if (_quizMode && _quizManager != null && _quizManager.IsActive() && !_quizManager.IsFinished())
            {
                string a = input.ToUpper().Trim();
                if (a == "A" || a == "B" || a == "C" || a == "D")
                {
                    bool correct = _quizManager.SubmitAnswer(a);
                    if (_quizManager.IsFinished())
                    {
                        _quizMode = false;
                        _logger?.Log($"Quiz complete. Score: {_quizManager.GetScore()}/{_quizManager.GetTotal()}");
                        return $"🎯 QUIZ COMPLETE!\nScore: {_quizManager.GetScore()}/{_quizManager.GetTotal()}\n{_quizManager.GetFinalMessage()}";
                    }
                    var q = _quizManager.GetCurrentQuestion();
                    return $"{(correct ? "✅ CORRECT!" : "❌ WRONG!")}\n\n{q?.GetQuestionText(_quizManager.GetScore() + 1, _quizManager.GetTotal())}";
                }
                return "Please answer A, B, C, or D";
            }

            // ACTIVITY LOG
            if (i.Contains("show log") || i.Contains("what have you done"))
            {
                if (i.Contains("full") || i.Contains("show more"))
                    return _logger?.GetFullLog() ?? "Logger not ready.";
                return _logger?.GetRecentLog(10) ?? "Logger not ready.";
            }

            // KEYWORDS
            foreach (var kv in responses)
                if (i.Contains(kv.Key)) return kv.Value[rand.Next(kv.Value.Count)];

            // SENTIMENT
            if (i.Contains("worried") || i.Contains("scared"))
                return "😟 I understand your concern. " + responses["password"][0];
            if (i.Contains("confused"))
                return "🤔 Let me explain. Type 'help' for topics.";

            return "🤖 I didn't understand. Try: password, phishing, add task, start quiz, show log, or help";
        }

        private int ExtractId(string input)
        {
            var p = input.Split(' ');
            for (int i = 0; i < p.Length; i++)
                if (p[i].ToLower() == "id" && i + 1 < p.Length && int.TryParse(p[i + 1], out int id))
                    return id;
            return -1;
        }
    }
}