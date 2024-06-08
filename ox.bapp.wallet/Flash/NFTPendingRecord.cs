using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using OX.IO;

namespace OX.Wallets.Flash
{
    public class NFTPendingRecord : ISerializable
    {
        public StringWrapper IssueId;
        public NFTPending Pending;
        public virtual int Size => IssueId.Size + Pending.Size;

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(IssueId);
            writer.Write(Pending);
        }
        public void Deserialize(BinaryReader reader)
        {
            IssueId = reader.ReadSerializable<StringWrapper>();
            Pending = reader.ReadSerializable<NFTPending>();
        }

    }
}
