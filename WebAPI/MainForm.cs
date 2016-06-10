using log4net.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Windows.Forms;
using WebApi.Log4net;
using WebApi.Secure;

namespace WebApi
{
    public partial class MainForm : Form, ILogger
    {
        private HttpSelfHostServer m_server;
        private IptLogger m_log = new IptLogger("MainForm");

        public MainForm()
        {
            InitializeComponent();

            string path = Assembly.GetEntryAssembly().Location;
            XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Path.GetDirectoryName(path) + "\\Log4net\\Logger.config"));
            comboBox_loggerLevel.SelectedIndex = 0;//Debug
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var payload = new Dictionary<string, object>() 
                {
                    { "email", "abc@gmail.com" },
                    {"createAt", DateTime.Now}
                };
            string token = SecureHelper.Instance.CreateToken(payload);
            bool v = SecureHelper.Instance.VerifySignature(token);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //web api selfhost using console app
            string baseAddress = ConfigurationManager.AppSettings["baseAddress"];
            baseAddress = "http://localhost:8888";
            var config = new HttpSelfHostConfiguration(baseAddress);
            config.MaxReceivedMessageSize = 2147483647;
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.Routes.MapHttpRoute(
              name: "API",
              routeTemplate: "{controller}/{action}/{id}",
              defaults: new { id = RouteParameter.Optional }
                );
            HttpSelfHostServer server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            //Console.WriteLine("Starts successfully: " + baseAddress);
            //Console.WriteLine("Press Enter to quit.");
            //Console.ReadLine();
            //server.CloseAsync().Wait();            
        }

        private void comboBox_loggerLevel_SelectedValueChanged(object sender, EventArgs e)
        {
            int loggerLevel = comboBox_loggerLevel.SelectedIndex;
            LoggerEventType level = (LoggerEventType)(loggerLevel + 1);
            IptLogger.RegisterEvent(this, level);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            listView_logger.Items.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string ip = tbIp.Text.Trim();
                string port = tbPort.Text.Trim();
                string baseAddress = "http://" + ip + ":" + port;
                var config = new HttpSelfHostConfiguration(baseAddress);
                config.MaxReceivedMessageSize = 2147483647;
                config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
                config.Routes.MapHttpRoute(
                  name: "API",
                  routeTemplate: "{controller}/{action}/{id}",
                  defaults: new { id = RouteParameter.Optional }
                    );
                m_server = new HttpSelfHostServer(config);
                m_server.OpenAsync().Wait();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                m_log.Debug("Server starts at " + baseAddress + " successfully.");
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message + ". Can't start server.");
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_server != null)
                    m_server.CloseAsync().Wait();
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                m_log.Debug("Server stops.");
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_server != null)
            {
                m_server.CloseAsync().Wait();
            }
        }

        #region Implement ILogger interface

        public void OnFatalEventLogger(string message)
        {
            UpdateLogFile(message);
        }

        public void OnErrorEventLogger(string message)
        {
            UpdateLogFile(message);
        }

        public void OnWarnEventLogger(string message)
        {
            UpdateLogFile(message);
        }

        public void OnInfoEventLogger(string message)
        {
            UpdateLogFile(message);
        }

        public void OnDebugEventLogger(string message)
        {
            UpdateLogFile(message);
        }

        private void UpdateLogFile(string message)
        {
            Invoke((MethodInvoker)delegate
            {
                ListViewItem item = new ListViewItem(DateTime.Now.ToString("dd/MM HH:mm:ss "));
                item.SubItems.Add(message);
                listView_logger.Items.Add(item);
            });
        }

        #endregion
    }
}
