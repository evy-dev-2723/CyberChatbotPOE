using System;
using System.Collections.Generic;
using System.Linq;

namespace CyberChatbotPOE
{
    public class ActivityLogger
    {
        private List<string> _log = new List<string>();

        public void Log(string action)
        {
            _log.Add($"[{DateTime.Now:HH:mm}] {action}");
        }

        public string GetRecentLog(int count = 10)
        {
            if (_log.Count == 0) return "No activities logged yet.";
            string result = "RECENT ACTIVITIES:\n";
            var recent = _log.Skip(Math.Max(0, _log.Count - count)).ToList();
            for (int i = 0; i < recent.Count; i++)
                result += $"{i + 1}. {recent[i]}\n";
            if (_log.Count > count)
                result += $"\n{_log.Count - count} more entries. Type 'show more' to see all.";
            return result;
        }

        public string GetFullLog()
        {
            if (_log.Count == 0) return "No activities logged yet.";
            string result = "FULL ACTIVITY LOG:\n";
            for (int i = 0; i < _log.Count; i++)
                result += $"{i + 1}. {_log[i]}\n";
            return result;
        }
    }
}