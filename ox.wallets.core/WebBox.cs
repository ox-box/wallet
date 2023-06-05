using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OX.Wallets
{
    public abstract class WebBox : IWebBox
    {
        static List<WebBox> boxes = new List<WebBox>();
        public INotecase Notecase { get; private set; } = default;
        public abstract uint BoxIndex { get; }
        public static IEnumerable<WebBox> Boxes { get { return boxes; } }
        public bool Valid { get { return Notecase.IsNotNull() && Notecase.Wallet.IsNotNull(); } }

        public WebBox()
        {
            Init();
            boxes.Add(this);
        }
        public static void SetNotecase(INotecase notecase)
        {
            foreach (var box in boxes)
                box.Notecase = notecase;
            //throw new Exception(boxes.Count.ToString());
        }
        public static T GetWebBox<T>() where T : WebBox
        {
            return boxes.FirstOrDefault(m => m is T) as T;
        }
        public abstract void Init();

    }
}
