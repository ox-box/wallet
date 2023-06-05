using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.Network.P2P.Payloads;
using OX.Wallets.Base;
using OX.Wallets.Base.Events;

namespace OX.Wallets.Base.Wallets
{
    public interface IWalletProvider : IBappProvider
    {
        IEnumerable<KeyValuePair<BoardKey, UInt256>> GetRangeBoards(uint indexrange);
        UInt256 GetBoard(BoardKey key);
        EngravePageState GetEngravePageState(BoardKey key);
        HashPage GetEngravePageHash(BoardKey key, uint pageIndex);
        IEnumerable<KeyValuePair<HolderBoardKey, UInt256>> GetBoardsByHolder(UInt160 holder);
        DiggPageState GetDiggPageState(UInt256 engraveId);
        HashPage GetDiggPageHash(UInt256 engraveId, uint pageIndex);
        IEnumerable<KeyValuePair<EngraveHolder, Engrave>> GetEngravesByHolder(UInt160 holder);
        IEnumerable<KeyValuePair<MyBookKey, BookTransaction>> GetMyBooks();
        IEnumerable<KeyValuePair<SecretLetterKey, SecretLetterTransaction>> GetMyLetters();
    }
}
