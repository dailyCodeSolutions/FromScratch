using MagicznyMVVM.Model;
using MagicznyMVVM.Utils.ProxyCreator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace MagicznyMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            customer = ProxyCreator.MakeINotifyPropertyChanged<Customer>();

            DataContext = customer;
            new Timer(ChangeName, null, 1000, 1000);
        }

        public Customer customer { get; set; }


        private void ChangeName(object state)
        {
            var list = new List<string>()
            {
                "Grzegorz",
                "Tomasz",
                "Dawid",
                "Orest"
            };

            customer.Name = list[new Random().Next(0, list.Count - 1)];
        }
    }
}
