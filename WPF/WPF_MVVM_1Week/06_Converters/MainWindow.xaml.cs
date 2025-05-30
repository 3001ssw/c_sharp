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



namespace _06_Converters
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var Person = new List<Human>();
            for (int i = 0; i < 5; i++)
            {
                Person.Add(new Human()
                {
                    Name = $"{i}번째 사람",
                    Age = i * 10
                }); ;
            }

            this.DataContext = Person;
        }
    }
}

