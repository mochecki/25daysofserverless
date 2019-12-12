using System;

namespace Backend.Model
{
    [Serializable]  
    public class MarkdownCard
    {
        public string Filename { get; set; }

        public string Content { get; set; }
    }
}