using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Base.Events
{
    public class BoardButton : DarkButton
    {
        BoardKey BoardKey;
        Board Board;
        INotecase Operater;
        public BoardButton(INotecase operate, BoardKey boardKey, Board board)
        {
            Operater = operate;
            BoardKey = boardKey;
            Board = board;
            var msg = $"{boardKey.BoardTxIndex}-{boardKey.BoardTxPosition}:{board.Name}";
            Text = msg;
            ToolTip toolTip1 = new ToolTip();

            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this, msg);
            Click += RiddlesHashButton_Click;
            MouseDown += BoardButton_MouseDown;
            Width = 500;
            Height = 40;
            Margin = new Padding() { All = 5 };
        }

        private void BoardButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {

            }
        }



        private void RiddlesHashButton_Click(object sender, EventArgs e)
        {
            //var h = $"{BoardKey.BoardTxIndex}-{BoardKey.BoardTxPosition}";
            //Clipboard.SetText(h);
            //string msg = h + UIHelper.LocalString("  已复制", "  copied");
            //this.Operater.SendMesssage(5, msg);
            //DarkMessageBox.ShowInformation(msg, "");

            if (Operater.IsNotNull() && Operater.Wallet is NEP6Wallet wlt)
            {
                //Favorites Boards
                wlt.AddStone("collectionboard", $"{BoardKey.BoardTxIndex}-{BoardKey.BoardTxPosition}", Board.Name);
                wlt.Save();
                Bapp.GetBapp<WalletBapp>().PushEvent(new BappEvent() { EventItems = new BappEventItem[] { new BappEventItem() { EventType = WalletBappEventType.CollectionBoardEvent.Value() } } });
            }
        }
    }
}
