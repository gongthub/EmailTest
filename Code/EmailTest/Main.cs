using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmailTest
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            txtServer.Text = "smtp.exmail.qq.com";
            txtFromUser.Text = "testadmin@vlinker.com.cn";
            txtToUser.Text = "gongtao@vlinker.com.cn";
            txtFromUser.Enabled = false;
        }

        /// <summary>
        /// 发送按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text;
            string fromuser = txtFromUser.Text;
            string tousers = txtToUser.Text.ToString();
            if (tousers == null || tousers.Trim() == "")
            {
                MessageBox.Show("收件人不能为空!");
            }
            else
            {
                List<string> users = new List<string>();
                users = tousers.Split(';').ToList();
                string logs = SendEmail(server, fromuser, users, "测试邮件");
                txtLogs.Text += logs + "\r\n";
            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sFrom"></param>
        /// <param name="sTo"></param>
        /// <param name="sSubject"></param>
        /// <param name="sMessageBody"></param>
        public static string SendEmail(string server, string sFrom, List<string> sTo, string sSubject)
        {
            try
            {
                if (sTo != null && sTo.Count > 0)
                {
                    string pwds = "Vlinker123";//密码
                    MailAddress from = new MailAddress(sFrom);
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = from;
                        foreach (string s in sTo)
                        {
                            MailAddress to = new MailAddress(s);
                            message.To.Add(to);
                        }

                        message.Body = "<p>发送测试邮件 （操作者： " + sFrom + "）</p>";
                        message.Subject = sSubject;
                        message.IsBodyHtml = true;

                        SmtpClient client = new SmtpClient(server);
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.Credentials = new System.Net.NetworkCredential(sFrom, pwds);
                        client.Send(message);
                    }
                }

                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：邮件发送成功";
            }
            catch (Exception e)
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：邮件发送失败:" + e.InnerException.Message;
            }
        }
    }
}
