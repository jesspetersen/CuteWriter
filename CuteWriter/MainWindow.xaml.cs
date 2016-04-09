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
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            currentFile = desktop + "/CuteWriterDoc.txt";
            File.WriteAllText(currentFile, UserInputBox.Text);
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
                        if (currentFile == null)
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
                                UserInputBox.Text = "";
                                currentFile = null;
                                BlackLabelDisplay.Content = "Your document was saved!! Here is your new document! <3";
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                            }
                        }
                        else
                        {
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
                        if (currentFile == null)
                        {
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
                        }
                        else
                        {
                            File.WriteAllText(currentFile, UserInputBox.Text);
                            MessageBoxResult warning = MessageBox.Show("Your work was successfully saved!", "Work Saved!");
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
                                MessageBoxResult warningUnopened = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
                            }
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
                    MessageBoxResult saved = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                    BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
                }
            }
        }

        private void SaveDoc_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null || currentFile == "")
            {
                File.WriteAllText(currentFile, UserInputBox.Text);
                BlackLabelDisplay.Content = "I just saved your file successfully! Yay! :D";
            }
            else
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
        }

        private void DeleteDoc_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null || currentFile != "")
            {
                try
                {
                    File.Delete(currentFile);
                    UserInputBox.Text = "";
                    currentFile = null;
                    BlackLabelDisplay.Content = "Your previous file was deleted. Here is a new document! <3";
                }
                catch (ArgumentException)
                {
                    MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem deleting your file. Please make sure that the file has been saved before you attempt to delete it.", "Work Not Deleted!");
                    BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to delete that file!";
                }
            }
            else
            {
                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem deleting your file. You have not yet saved this file, so there was nothing to delete. We've given you a nice empty file to work on now! Make sure to save it!", "Work Not Deleted!");
                UserInputBox.Text = "";
                currentFile = null;
                BlackLabelDisplay.Content = "Here is a new document! <3";
            }
        }

        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's text in the editor - do you want to save that before you exit?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        if (currentFile == null)
                        {
                            SaveFileDialog saveFile = new SaveFileDialog();
                            saveFile.DefaultExt = "txt";
                            saveFile.Filter = "Text files (*.txt)|*.txt";
                            saveFile.ShowDialog();
                            currentFile = saveFile.FileName;
                            try
                            {
                                File.WriteAllText(currentFile, UserInputBox.Text);
                                App.Current.Shutdown();
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                            }
                        }
                        else
                        {
                            try
                            {
                                File.WriteAllText(currentFile, UserInputBox.Text);
                                App.Current.Shutdown();
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                            }
                        }
                        break;

                    case MessageBoxResult.No:
                        App.Current.Shutdown();
                        break;

                    case MessageBoxResult.Cancel:
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
            else
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! Are you sure that you want to exit?", "Exit?", MessageBoxButton.YesNo);
                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        App.Current.Shutdown();
                        break;

                    case MessageBoxResult.No:
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's text in the editor - do you want to save that before you exit?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        if (currentFile == null)
                        {
                            SaveFileDialog saveFile = new SaveFileDialog();
                            saveFile.DefaultExt = "txt";
                            saveFile.Filter = "Text files (*.txt)|*.txt";
                            saveFile.ShowDialog();
                            currentFile = saveFile.FileName;
                            try
                            {
                                File.WriteAllText(currentFile, UserInputBox.Text);
                                App.Current.Shutdown();
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                            }
                        }
                        else
                        {
                            try
                            {
                                File.WriteAllText(currentFile, UserInputBox.Text);
                                App.Current.Shutdown();
                            }
                            catch (ArgumentException)
                            {
                                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Please try again to ensure that your work is saved!", "Work Not Saved!");
                                BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                            }
                        }
                        break;

                    case MessageBoxResult.No:
                        App.Current.Shutdown();
                        break;

                    case MessageBoxResult.Cancel:
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
            else
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! Are you sure that you want to exit?", "Exit?", MessageBoxButton.YesNo);
                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        App.Current.Shutdown();
                        break;

                    case MessageBoxResult.No:
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
        }

        private void FontSwirly_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontFamily = new FontFamily("Lucida Handwriting");
        }

        private void FontUgly_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontFamily = new FontFamily("Comic Sans MS");
        }

        private void FontReza_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontFamily = new FontFamily("Century Gothic");
        }

        private void FontBest_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontFamily = new FontFamily("Courier New");
        }
    }
}
