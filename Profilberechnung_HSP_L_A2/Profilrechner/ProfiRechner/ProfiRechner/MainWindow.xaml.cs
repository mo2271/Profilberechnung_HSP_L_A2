﻿using System;
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

namespace ProfiRechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Besser Rein in die Algen
            InitializeComponent();
        }

        private void lb_item_Rechteckprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tab_Rechteckprofil.Visibility = Visibility.Visible;
        }

        
    }
}
