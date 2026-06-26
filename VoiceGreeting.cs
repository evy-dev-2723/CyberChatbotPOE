using System;
using System.IO;
using System.Media;

namespace CyberChatbotPOE
{
    public class VoiceGreeting
    {
        public void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "audio", "greeting.wav");
                if (File.Exists(path))
                {
                    using (SoundPlayer player = new SoundPlayer(path))
                    {
                        player.PlaySync();
                    }
                }
            }
            catch (Exception) { }
        }
    }
}