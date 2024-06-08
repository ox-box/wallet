using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets.Flash.Chat
{
    public interface IChatModel
    {
        bool IsRemote { get; set; }
        bool Read { get; set; }
        DateTime Time { get; set; }
        uint RecordIndex { get; set; }
        string Author { get; set; }
        string Type { get; }
    }

    public class TextChatModel : IChatModel
    {
        public bool IsRemote { get; set; }
        public bool Read { get; set; }
        public DateTime Time { get; set; }
        public uint RecordIndex { get; set; }
        public string Author { get; set; }
        public string Type { get; } = "text";

        public string Body { get; set; }
    }

    public class ImageChatModel : IChatModel
    {
        public bool IsRemote { get; set; }
        public bool Read { get; set; }
        public DateTime Time { get; set; }
        public uint RecordIndex { get; set; }
        public string Author { get; set; }
        public string Type { get; } = "image";

        public Image Image { get; set; }
    }

    public class AttachmentChatModel : IChatModel
    {
        public bool IsRemote { get; set; }
        public bool Read { get; set; }
        public DateTime Time { get; set; }
        public uint RecordIndex { get; set; }
        public string Author { get; set; }
        public string Type { get; } = "attachment";

        public byte[] Attachment { get; set; }
        public string Filename { get; set; }
    }
}
