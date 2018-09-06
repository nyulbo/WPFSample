using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace MessageBusSimulator
{

    /// <summary>
    /// Performance.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Performance : Page
    {
        private bool isStart = false;
        static PerformanceCounter theCPUCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        static PerformanceCounter theMemCounter = new PerformanceCounter("Memory", "Available MBytes");
        public Performance()
        {
            InitializeComponent();
        }
        private void doPerformanceCounter()
        {
            while (isStart)
            {
                Thread.Sleep(3000);
                txtBlockCpu.Dispatcher.BeginInvoke((Action)(() => txtBlockCpu.Text = theCPUCounter.NextValue().ToString()));
                txtBlockMemory.Dispatcher.BeginInvoke((Action)(() => txtBlockMemory.Text = theMemCounter.NextValue().ToString()));
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            isStart = true;
            new Thread(doPerformanceCounter).Start();

        }
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            isStart = false;
        }
    }
}
