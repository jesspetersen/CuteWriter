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
            if (UserInputBox.Text != "")
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.DefaultExt = "txt";
                saveFile.Filter = "Text files (*.txt)|*.txt";
                saveFile.ShowDialog();
                currentFile = saveFile.FileName;
                try
                {
                    File.WriteAllText(currentFile, UserInputBox.Text);
                    BlackLabelDisplay.Content = "Well done, you saved your work! :3";
                }
                catch (ArgumentException)
                {
                    MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                    BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                }
            }
            else
            {
                BlackLabelDisplay.Content = "Hey! You don't seem to have written anything! Try typing before you save!";
            }
        }

        private void NewDoc_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's already text in the editor - do you want to save that first?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.DefaultExt = "txt";
                        saveFile.Filter = "Text files (*.txt)|*.txt";
                        saveFile.ShowDialog();
                        currentFile = saveFile.FileName;
                        try
                        {
                            File.WriteAllText(currentFile, UserInputBox.Text);
                            BlackLabelDisplay.Content = "Well done, you saved your work! :3";
                            UserInputBox.Text = "";
                            currentFile = null;
                            BlackLabelDisplay.Content = "Your document was saved!! Here is your new document! <3";
                        }
                        catch (ArgumentException)
                        {
                            MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                            BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                        }
                        break;

                    case MessageBoxResult.No:
                        UserInputBox.Text = "";
                        currentFile = null;
                        BlackLabelDisplay.Content = "Your previous file was not saved. Here is your new document! <3";
                        break;

                    case MessageBoxResult.Cancel:
                        currentFile = null;
                        BlackLabelDisplay.Content = "You did not create a new document. :)";
                        break;
                }
            }
            else
            {
                BlackLabelDisplay.Content = "Here is your new document! <3";
                currentFile = null;
            }
        }

        private void OpenDoc_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's already text in the editor - do you want to save that first?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.DefaultExt = "txt";
                        saveFile.Filter = "Text files (*.txt)|*.txt";
                        saveFile.ShowDialog();
                        currentFile = saveFile.FileName;
                        try
                        {
                            File.WriteAllText(currentFile, UserInputBox.Text);
                            OpenFileDialog openFile = new OpenFileDialog();
                            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            openFile.ShowDialog();
                            currentFile = openFile.FileName;
                            try
                            {
                                string openText = File.ReadAllText(currentFile);
                                UserInputBox.Text = openText;
                                BlackLabelDisplay.Content = "Here's that file you wanted!";
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
                            }
                        }
                        catch (ArgumentException)
                        {
                            MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                            BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                        }
                        break;

                    case MessageBoxResult.No:
                        OpenFileDialog opFile = new OpenFileDialog();
                        opFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        opFile.ShowDialog();
                        currentFile = opFile.FileName;
                        try
                        {
                            string openText = File.ReadAllText(currentFile);
                            UserInputBox.Text = openText;
                            BlackLabelDisplay.Content = "Here's that file you wanted! Your previous file was not saved!";
                        }
                        catch (ArgumentException)
                        {
                            MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                            BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
                        }
                        break;

                    case MessageBoxResult.Cancel:
                        BlackLabelDisplay.Content = "You did not open a file! :)";
                        break;
                }
            }
            else
            {
                OpenFileDialog opFile = new OpenFileDialog();
                opFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                opFile.ShowDialog();
                currentFile = opFile.FileName;
                try
                {
                    string openText = File.ReadAllText(currentFile);
                    UserInputBox.Text = openText;
                    BlackLabelDisplay.Content = "Here's that file you wanted! Your previous file was not saved!";
                }
                catch (ArgumentException)
                {
                    MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                    BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
                }
            }
        }
    }
}
