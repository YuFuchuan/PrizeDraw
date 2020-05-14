using System;
using System.Windows.Forms;



namespace PrizeDraw
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
    class RandomNumber
    {
        public static int maxRecord = 5;
        public int minNum;
        public int maxNum;
        public int[] record = new int[maxRecord];

        public RandomNumber() : this(0, 0)
        {

        }
        
        public RandomNumber(int min, int max)
        {
            minNum = min;
            maxNum = max;

            for(int i = 0; i < maxRecord; i++)
            {
                record[i] = 0;
            }
        }
      
        public int GetRandomNunber
            ()
        {
            Random rd = new Random();
            int num = rd.Next(minNum, maxNum);

            return num;
        }
    }
}
