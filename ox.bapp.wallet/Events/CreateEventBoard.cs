using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class CreateEventBoard : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        public Module Module { get; set; }
        public CreateEventBoard(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("创建事件板", "New Event Board");
            this.lb_name.Text = UIHelper.LocalString("事件板名称:", "Board Name:");
            this.lb_remark.Text = UIHelper.LocalString("事件板备注:", "Board Remark:");
            this.lb_latitude.Text = UIHelper.LocalString("纬度:", "Latitude:");
            this.lb_longitude.Text = UIHelper.LocalString("经度:", "Longitude:");
            this.lb_from.Text = UIHelper.LocalString("板主:", "Board Leader:");
            this.lb_Private.Text = UIHelper.LocalString("权限:", "Permission:");
            this.cb_Private.Text = UIHelper.LocalString("私有写", "Private Writing");
            this.btnOk.Text = UIHelper.LocalString("创建", "Create");
            initAccounts();
        }
        public EventTransaction GetTransaction(out UInt160 from)
        {
            from = default;
            if (this.tb_name.Text.IsNullOrEmpty() || this.tb_name.Text.Trim().IsNullOrEmpty()) return default;
            var remark = this.tb_remark.Text;
            if (remark.IsNotNullAndEmpty()) remark = remark.Trim();
            var latitude = this.tb_latitude.Text;
            if (!Verifylatitude(latitude)) return default;
            var longitude = this.tb_longitude.Text;
            if (!Verifylongitude(longitude)) return default;
            var la = double.Parse(latitude) * 1000000;
            var lo = double.Parse(longitude) * 1000000;

            Board board = new Board()
            {
                Name = this.tb_name.Text.Trim(),
                Remark = remark,
                latitude = (long)la,
                longitude = (long)lo
            };
            if (this.cb_Private.Checked)
            {
                board.Data = new byte[] { 0x01 };
            }
            from = this.cbAccounts.Text.ToScriptHash();
            EventTransaction tx = new EventTransaction()
            {
                EventType = EventType.Board,
                ScriptHash = from,
                Data = board.ToArray()
            };
            return tx;
        }
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
        }
        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            initAccounts();
        }
        public void OnRebuild()
        {

        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        this.cbAccounts.Items.Add(act.Address);
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }
        private void tb_latitude_TextChanged(object sender, EventArgs e)
        {
            string s = tb_latitude.Text;
            if (!Verifylatitude(s))
            {
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_latitude.Clear();
                    this.tb_latitude.AppendText(s);
                }
            }
        }

        private void tb_longitude_TextChanged(object sender, EventArgs e)
        {
            string s = tb_longitude.Text;
            if (!Verifylongitude(s))
            {
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_longitude.Clear();
                    this.tb_longitude.AppendText(s);
                }
            }
        }
        bool Verifylongitude(string longitude)
        {
            var reg_longitude = @"^(\+|-)?(?:180(?:(?:\.0{1,6})?)|(?:[0-9]|[1-9][0-9]|1[0-7][0-9])(?:(?:\.[0-9]{1,6})?))$";
            return Regex.IsMatch(longitude, reg_longitude);
        }
        bool Verifylatitude(string latitude)
        {
            var reg_latitude = @"^(\+|-)?(?:90(?:(?:\.0{1,6})?)|(?:[0-9]|[1-8][0-9])(?:(?:\.[0-9]{1,6})?))$";
            return Regex.IsMatch(latitude, reg_latitude);
        }
    }
}
