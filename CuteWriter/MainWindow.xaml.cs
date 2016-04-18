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
using System.ComponentModel;
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
            CurrentFontSize.Text = Convert.ToString(UserInputBox.FontSize);
            ColourComboBox.SelectedIndex = 0;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow1);
            player.Play();
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's already text in the editor - do you want to save that first?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        if (currentFile == null)
                        {
                            bool successful = SaveFileAs();
                            OpenFile(successful);
                        }
                        else
                        {
                            bool success = SaveFile();
                            OpenFile(success);
                        }
                        break;

                    case MessageBoxResult.No:
                        OpenFile(false);
                        break;

                    case MessageBoxResult.Cancel:
                        BlackLabelDisplay.Content = "You did not open a file! :)";
                        break;
                }
            }
            else
            {
                OpenFile(false);
            }
        }

        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (currentFile != null || currentFile == "")
            {
                SaveFile();
            }
            else
            {
                SaveFileAs();
            }
        }

        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's already text in the editor - do you want to save that first?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        if (currentFile == null)
                        {
                            bool successful = SaveFileAs();
                            NewDoc(successful);
                        }
                        else
                        {
                            bool success = SaveFile();
                            NewDoc(success);
                        }
                        break;

                    case MessageBoxResult.No:
                        NewDoc(false);
                        break;

                    case MessageBoxResult.Cancel:
                        currentFile = null;
                        BlackLabelDisplay.Content = "You did not create a new document. :)";
                        break;
                }
            }
            else
            {
                currentFile = null;
                BlackLabelDisplay.Content = "Here is your new document! <3";
            }
        }

        private void UserInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BlackLabelDisplay.Content = "The text is being modified!! :)";
        }

        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new About();
            aboutWindow.Show();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow3);
            player.Play();
        }

        private bool SaveFileAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.ShowDialog();
            currentFile = saveFile.FileName;
            return SaveFile();
        }

        private bool SaveFile()
        {
            try
            {
                File.WriteAllText(currentFile, UserInputBox.Text);
                BlackLabelDisplay.Content = "Well done, you saved your work! :3";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow2);
                player.Play();
                return true;
            }
            catch (ArgumentException)
            {
                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem saving your work. Would you like to try again?", "Work Not Saved!", MessageBoxButton.YesNo);
                switch (warning)
                {
                    case MessageBoxResult.Yes:
                        SaveFileAs();
                        return true;

                    case MessageBoxResult.No:
                        BlackLabelDisplay.Content = "Hey, just letting you know, we weren't able to save your work!";
                        return false;
                }
            }

            return true;
        }

        
        private void NewDoc(bool wasSaved)
        {

            UserInputBox.Text = "";
            currentFile = null;
            if (wasSaved)
                BlackLabelDisplay.Content = "Your document was saved!! Here is your new document! <3";
            else
                BlackLabelDisplay.Content = "Your previous file was not saved. Here is your new document! <3";
        }

        private void OpenFile(bool wasSaved)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFile.ShowDialog();
            currentFile = openFile.FileName;
            try
            {
                string openText = File.ReadAllText(currentFile);
                UserInputBox.Text = openText;
                if (wasSaved)
                {
                    BlackLabelDisplay.Content = "Here's that file you wanted! Your previous file was saved!";
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow2);
                    player.Play();
                }
                else
                {
                    BlackLabelDisplay.Content = "Here's that file you wanted! Your previous file was not saved.";
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow2);
                    player.Play();
                }
            }
            catch (ArgumentException)
            {
                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
            }
        }

        private void ShutdownApp()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string defaultFile = desktop + "/CuteWriterDoc.txt";
            string defaultFileContent = File.ReadAllText(defaultFile);
            if (defaultFileContent == "" || defaultFileContent == null)
                File.Delete(defaultFile);
            System.Media.SoundPlayer player1 = new System.Media.SoundPlayer(Properties.Resources.meow4);
            player1.PlaySync();
            App.Current.Shutdown();
        }

        private void SaveAsDoc_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                SaveFileAs();
            }
            else
            {
                BlackLabelDisplay.Content = "Hey! You don't seem to have written anything! Try typing before you save!";
            }
        }
        
        private void SaveDoc_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null || currentFile == "")
            {
                SaveFile();
            }
            else
            {
                SaveFileAs();
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
                NewDoc(false);
            }
        }

        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                            SaveFileAs();
                            ShutdownApp();
                        }
                        else
                        {
                            SaveFile();
                            ShutdownApp();
                        }
                        break;

                    case MessageBoxResult.No:
                        ShutdownApp();
                        break;

                    case MessageBoxResult.Cancel:
                        e.Cancel = true;
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
                        ShutdownApp();
                        break;

                    case MessageBoxResult.No:
                        e.Cancel = true;
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

        private void FontSizeDown_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.FontSize > 10)
            {
                UserInputBox.FontSize = FontSize--;
                CurrentFontSize.Text = Convert.ToString(UserInputBox.FontSize);
            }
            else
                BlackLabelDisplay.Content = "Sorry, you can't make that text any smaller!! :3";
        }

        private void FontSizeUp_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.FontSize < 32)
            {
                UserInputBox.FontSize = FontSize++;
                CurrentFontSize.Text = Convert.ToString(UserInputBox.FontSize);
            }
            else
                BlackLabelDisplay.Content = "Sorry, you can't make that text any bigger!! :3";
        }

        private void ResetFontsChange_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontSize = 12;
            UserInputBox.FontFamily = new FontFamily("Courier New");
            UserInputBox.Foreground = Brushes.Fuchsia;
        }

        private void ColourPink_Selected(object sender, RoutedEventArgs e)
        {
            UserInputBox.Foreground = Brushes.Fuchsia;
        }

        private void ColourNavy_Selected(object sender, RoutedEventArgs e)
        {
            UserInputBox.Foreground = Brushes.Navy;
        }

        private void ColourBlack_Selected(object sender, RoutedEventArgs e)
        {
            UserInputBox.Foreground = Brushes.Black;
        }

        private void ColourYellow_Selected(object sender, RoutedEventArgs e)
        {
            UserInputBox.Foreground = Brushes.Yellow;
        }
    }
}
