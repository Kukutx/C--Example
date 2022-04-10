using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCallQtDialogDll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("qtdialog.dll", EntryPoint = "showDialog", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool showDialog(IntPtr parent);

        [DllImport("qtdialog.dll", EntryPoint = "ShowDirDialog", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool ShowDirDialog(IntPtr parent);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            showDialog(hwnd);
        }

        private void ShowDirDialog_Click(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            ShowDirDialog(hwnd);
        }
    }
}



