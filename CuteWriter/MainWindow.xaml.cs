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
using System.IO;
using Microsoft.Win32;

namespace CuteWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentFile = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UserInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BlackLabelDisplay.Content = "The text is being modified!! :)";
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new About();
            aboutWindow.Show();
        }

        private void SaveAsDoc_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.ShowDialog();
            currentFile = saveFile.FileName;
            File.WriteAllText(currentFile, UserInputBox.Text);
        }
    }
}
