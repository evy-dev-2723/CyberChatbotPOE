using System.Collections.Generic;

namespace CyberChatbotPOE
{
    public class TaskManager
    {
        private TaskStorageHelper _storage = new TaskStorageHelper();
        private ActivityLogger _logger;

        public TaskManager(ActivityLogger logger = null) { _logger = logger; }

        public string AddTask(string title, string description, string reminder)
        {
            if (string.IsNullOrWhiteSpace(title)) return "Title cannot be empty.";
            _storage.AddTask(title, description, reminder);
            _logger?.Log($"Task added: {title}");
            return $"✅ Task added: {title}";
        }

        public List<CyberTask> GetAllTasks() => _storage.LoadTasks();

        public string MarkAsComplete(int id)
        {
            var task = _storage.GetTask(id);
            if (task == null) return $"Task ID {id} not found.";
            if (task.IsComplete) return $"Task '{task.Title}' is already completed.";
            _storage.MarkAsComplete(id);
            _logger?.Log($"Task completed: {task.Title}");
            return $"✅ Task '{task.Title}' completed!";
        }

        public string DeleteTask(int id)
        {
            var task = _storage.GetTask(id);
            if (task == null) return $"Task ID {id} not found.";
            _storage.DeleteTask(id);
            _logger?.Log($"Task deleted: {task.Title}");
            return $"🗑 Task '{task.Title}' deleted!";
        }

        public string GetFormattedTasks()
        {
            var tasks = _storage.LoadTasks();
            if (tasks.Count == 0) return "No tasks found.";
            string result = "YOUR TASKS:\n";
            foreach (var t in tasks)
                result += $"[{(t.IsComplete ? "✅" : "⏳")}] ID:{t.Id} | {t.Title}\n";
            return result;
        }
    }
}