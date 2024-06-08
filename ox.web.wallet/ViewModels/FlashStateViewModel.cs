using OX.Wallets;
using OX.Wallets.Base.NFT;
using System.Collections.Generic;
namespace OX.Web
{
    public class TextWrapper
    {
        public string Text { get; set; }
    }
    public class CommentNode
    {
        public string SenderAddress;
        public string SenderName;
        public string ReplyName;
        public UInt256 Hash;
        public UInt256 ParentHash;
        public FlashStateCommentValue FSCV;
        public List<CommentNode> Sub = new List<CommentNode>();
    }

}
