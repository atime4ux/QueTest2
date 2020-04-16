using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueTest2
{
    public partial class Form1 : Form
    {
        libCommon.clsUtil objUtil = new libCommon.clsUtil();
        System.Threading.Thread t1;

        string qPath;
        bool start;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            qPath = ".\\private$\\myTest";
            start = false;

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                t1.Abort();
            }
            catch
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (start)
                start = false;
            else
                start = true;

            t1 = new System.Threading.Thread(new System.Threading.ThreadStart(runTest));
            t1.Start();
        }
        private void runTest()
        {
            libMyUtil.clsMSMQ objMSMQ = new libMyUtil.clsMSMQ();
            long sendData = 0;
            if (objMSMQ.canReadQueue(qPath))
            {
                while (start)
                {
                    objMSMQ.sendData(qPath, sendData);
                    sendData++;
                }
            }
            else
                MessageBox.Show("접근 불가");
        }
    }
}
