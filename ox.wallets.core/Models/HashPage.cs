using OX.IO;
using System.IO;
using System.Linq;
using OX;

namespace OX.Wallets
{
    public class HashPage : ISerializable
    {
        public const uint MaxHashsPerPage = 10;
        public UInt256[] Hashes;

        public int Size => Hashes.GetVarSize();
        public void Deserialize(BinaryReader reader)
        {
            Hashes = reader.ReadSerializableArray<UInt256>();
        }
        public void Serialize(BinaryWriter writer)
        {
            writer.Write(Hashes);
        }
        public bool AddHash(UInt256 hash)
        {
            if (Hashes.IsNullOrEmpty())
            {
                Hashes = new UInt256[] { hash };
                return true;
            }
            if (Hashes.Length >= MaxHashsPerPage) return false;
            var l = Hashes.ToList();
            l.Add(hash);
            Hashes = l.ToArray();
            return true;
        }
    }
}
