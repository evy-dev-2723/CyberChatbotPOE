using System;
using System.Collections.Generic;

namespace CyberChatbotPOE
{
    public class QuizManager
    {
        private List<QuizQuestion> _questions;
        private int _index, _score;
        private bool _active;

        public QuizManager()
        {
            _questions = new List<QuizQuestion>();
            _index = 0; _score = 0; _active = false;
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            _questions.Add(new QuizQuestion("What should you do if you receive an email asking for your password?",
                new List<string> { "Reply with password", "Delete it", "Report as phishing", "Ignore it" }, "C",
                "Report phishing emails!"));
            _questions.Add(new QuizQuestion("Which is a strong password?",
                new List<string> { "password123", "12345678", "S@uth4fr1c@R0cks!", "qwerty" }, "C",
                "Strong passwords use uppercase, lowercase, numbers, and symbols!"));
            _questions.Add(new QuizQuestion("What does 'https' mean?",
                new List<string> { "Highly Trusted", "HyperText Transfer Protocol Secure", "HyperText Transfer System", "None" }, "B",
                "'s' stands for SECURE!"));
            _questions.Add(new QuizQuestion("True or False: Use the same password for all accounts.",
                new List<string> { "True", "False" }, "B", "If one is hacked, ALL are vulnerable!"));
            _questions.Add(new QuizQuestion("What is phishing?",
                new List<string> { "A type of fishing", "Fake emails/links", "A computer virus", "A password manager" }, "B",
                "Phishing steals your info!"));
            _questions.Add(new QuizQuestion("What is Two-Factor Authentication?",
                new List<string> { "A second password", "An extra security layer", "A fingerprint", "A backup email" }, "B",
                "2FA adds extra security!"));
            _questions.Add(new QuizQuestion("True or False: Public Wi-Fi is safe for banking.",
                new List<string> { "True", "False" }, "B", "Public Wi-Fi is NOT secure!"));
            _questions.Add(new QuizQuestion("What to do if you've been scammed?",
                new List<string> { "Do nothing", "Contact bank immediately", "Send more money", "Delete messages" }, "B",
                "Contact your bank IMMEDIATELY!"));
            _questions.Add(new QuizQuestion("Sign of a phishing email?",
                new List<string> { "Correct spelling", "Urgent language", "Familiar sender", "Professional design" }, "B",
                "Phishing creates URGENCY!"));
            _questions.Add(new QuizQuestion("True or False: Banks ask for OTP over phone.",
                new List<string> { "True", "False" }, "B", "Banks NEVER ask for OTP!"));
            _questions.Add(new QuizQuestion("What is social engineering?",
                new List<string> { "Building websites", "Manipulating people", "Programming", "Network security" }, "B",
                "Social engineering manipulates PEOPLE!"));
            _questions.Add(new QuizQuestion("How often should you change passwords?",
                new List<string> { "Never", "Every 3-6 months", "Once a year", "Only when hacked" }, "B",
                "Change passwords every 3-6 months!"));
        }

        public void StartQuiz() { _index = 0; _score = 0; _active = true; }
        public void SetActive(bool a) { _active = a; }
        public bool IsActive() => _active;
        public bool IsFinished() => _index >= _questions.Count;
        public QuizQuestion GetCurrentQuestion() => _index < _questions.Count ? _questions[_index] : null;
        public int GetScore() => _score;
        public int GetTotal() => _questions.Count;

        public bool SubmitAnswer(string answer)
        {
            if (!_active || _index >= _questions.Count) return false;
            bool correct = _questions[_index].CheckAnswer(answer);
            if (correct) _score++;
            _index++;
            return correct;
        }

        public string GetFinalMessage()
        {
            int p = (_score * 100) / _questions.Count;
            if (p >= 90) return "🎉 EXCELLENT! You're a cybersecurity expert!";
            if (p >= 70) return "👍 GOOD JOB! Keep learning!";
            if (p >= 50) return "📚 NOT BAD! Review the tips!";
            return "📚 KEEP LEARNING! Try again!";
        }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string Explanation { get; set; }

        public QuizQuestion(string q, List<string> o, string c, string e)
        { Question = q; Options = o; CorrectAnswer = c; Explanation = e; }

        public bool CheckAnswer(string a) => a?.ToUpper() == CorrectAnswer?.ToUpper();

        public string GetQuestionText(int c, int t)
        {
            string text = $"QUESTION {c} of {t}:\n{Question}\n";
            char l = 'A';
            foreach (var o in Options) { text += $"   {l}. {o}\n"; l++; }
            text += "\nType A, B, C, or D";
            return text;
        }
    }
}