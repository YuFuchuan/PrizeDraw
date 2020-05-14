using System;
using System.Threading;
using System.Windows.Forms;

namespace PrizeDraw
{
    public partial class Form1 : Form
    {
        Boolean _bool = false;
        Thread thread, thread2;
        RandomNumber rn = new RandomNumber();

        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(delegate { Run(); }));
            thread2 = new Thread(new ThreadStart(delegate { Decelerate(); }));
        }
        
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(20);
                label2.Text = GetRandomNumber(rn, false);
            }
        }
        
        public void Decelerate()
        {
            for(int time = 50; time < 500; time += 100)
            {
                Thread.Sleep(time);
                label2.Text = GetRandomNumber(rn, false);
            }
            Thread.Sleep(550);
            label2.Text = GetRandomNumber(rn, true);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                if (!_bool)
                {
                    rn = SetRandomNumber();
                    
                    if (thread.ThreadState == ThreadState.Unstarted)
                    {
                        thread.Start();
                    }
                    else if (thread.ThreadState == ThreadState.Suspended ||
                            thread.ThreadState == ThreadState.SuspendRequested ||
                            thread.ThreadState == ThreadState.WaitSleepJoin ||
                            Convert.ToInt32(thread.ThreadState) == 96)
                    {
                        thread.Resume();
                    }

                    button1.Text = "停止";
                }
                else
                {
                    thread.Suspend();
                    thread2 = new Thread(new ThreadStart(delegate { Decelerate(); }));
                    thread2.Start();

                    button1.Text = "开始";
                }
                _bool = !_bool;
            }
            else
            {
                rn = SetRandomNumber();
                label2.Text = string.Format("{0}", GetRandomNumber(rn, true));
            }
        }
        
        private RandomNumber SetRandomNumber()
        {
            int min = Convert.ToInt32(textBox1.Text);
            int max = Convert.ToInt32(textBox2.Text);
            RandomNumber rn = new RandomNumber(min, max);

            return rn;
        }
        
        private string GetRandomNumber(RandomNumber rn,Boolean isRecord)
        {
            int num = rn.GetRandomNunber();

            while (true)
            {
                if (checkBox1.Checked)
                {
                    int key = 0;
                    for (int i = 0; i < RandomNumber.maxRecord; i++)
                    {
                        if (num == rn.record[i])
                        {
                            key = 1;
                        }
                    }

                    if (key == 0)
                    {
                        if (isRecord)
                        {
                            for (int i = 0; i < RandomNumber.maxRecord - 1; i++)
                            {
                                rn.record[i] = rn.record[i + 1];
                            }
                            rn.record[RandomNumber.maxRecord - 1] = num;
                        }

                        return GetString(num);
                    }
                    else
                    {
                        return GetRandomNumber(rn, isRecord);
                    }
                }
                else
                {
                    return GetString(num);
                }
            }
        }

        private string GetString(int num)
        {
            if (num >= 0 && num < 10)
            {
                return string.Format("    {0}   ", num);
            }
            else if(num >= 10 && num < 100)
            {
                return string.Format("   {0}   ", num);
            }
            else if (num >= 100 && num < 1000)
            {
                return string.Format("   {0}  ", num);
            }
            else if (num >= 1000 && num < 10000)
            {
                return string.Format("  {0}  ", num);
            }
            else if (num >= 10000 && num < 100000)
            {
                return string.Format("  {0} ", num);
            }
            else if (num >= 100000 && num < 1000000)
            {
                return string.Format(" {0} ", num);
            }
            else if (num >= 1000000 && num < 10000000)
            {
                return string.Format(" {0}", num);
            }
            else if (num >= 10000000 && num < 100000000)
            {
                return string.Format("{0}", num);
            }
            return " ";
        }
    }
}
