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
            //Create default file
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            currentFile = desktop + "/CuteWriterDoc.txt";
            File.WriteAllText(currentFile, UserInputBox.Text);
            //Initialise font size and colour formatting
            CurrentFontSize.Text = Convert.ToString(UserInputBox.FontSize);
            ColourComboBox.SelectedIndex = 0;
            //Play meow
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow1);
            player.Play();
        }

        //Open file hotkey function
        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //If user has input already in editor
            if (UserInputBox.Text != "")
            {
                MessageBoxResult haveInput = MessageBox.Show("Hey user! There's already text in the editor - do you want to save that first?", "Save?", MessageBoxButton.YesNoCancel);

                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        if (currentFile == null)
                        {
                            //run save as
                            bool successful = SaveFileAs();
                            OpenFile(successful);
                        }
                        else
                        {
                            //run save
                            bool success = SaveFile();
                            OpenFile(success);
                        }
                        break;

                    case MessageBoxResult.No:
                        //do not run any saving
                        OpenFile(false);
                        break;

                    case MessageBoxResult.Cancel:
                        //No change
                        BlackLabelDisplay.Content = "You did not open a file! :)";
                        break;
                }
            }
            else
            {
                //do not run any saving
                OpenFile(false);
            }
        }

        //Save hotkey
        private void SaveCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //If file has a current path
            if (currentFile != null || currentFile == "")
            {
                if (UserInputBox.Text != "")
                {
                    SaveFile();
                }
                else
                {
                    //Does not allow user to save an empty file
                    BlackLabelDisplay.Content = "Hey! You don't seem to have written anything! Try typing before you save!";
                }
            }
            else
            {
                if (UserInputBox.Text != "")
                {
                    //Create a path and save as
                    SaveFileAs();
                }
                else
                {
                    //Does not allow user to save an empty file
                    BlackLabelDisplay.Content = "Hey! You don't seem to have written anything! Try typing before you save!";
                }
            }
        }

        //New hotkey functionality
        private void NewCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //If user already has input, run standard saving dialogs thn open a new doc
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
                        BlackLabelDisplay.Content = "You did not create a new document. :)";
                        break;
                }
            }
            else
            {
                //Simulates an empty document
                currentFile = null;
                BlackLabelDisplay.Content = "Here is your new document! <3";
            }
        }

        //notify user as input is modified
        private void UserInputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            BlackLabelDisplay.Content = "The text is being modified!! :)";
        }

        //Open About window and play meow
        private void AboutMenu_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new About();
            aboutWindow.Show();
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.meow3);
            player.Play();
        }

        //General save file as method for when there is no current file path
        private bool SaveFileAs()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.DefaultExt = "txt";
            saveFile.Filter = "Text files (*.txt)|*.txt";
            saveFile.ShowDialog();
            currentFile = saveFile.FileName;
            return SaveFile();
        }

        //General save file method for when there is a current file path
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
                //Ask user if they want to retry saving
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

        //Method that allows for new doc creation
        private void NewDoc(bool wasSaved)
        {

            UserInputBox.Text = "";
            currentFile = null;
            //Allows me to feed through a message presenting whether the previous doc was saved or not
            if (wasSaved)
                BlackLabelDisplay.Content = "Your document was saved!! Here is your new document! <3";
            else
                BlackLabelDisplay.Content = "Your previous file was not saved. Here is your new document! <3";
        }

        //Open File method, returns a bool whether it was saved or not allowing me to present a corresponding message
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
                //Lets user know that there was an issue in opening the file so that they are able to try again if need be.
                //Message repeated in pop up and status bar
                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem opening your work. Please try again if you would like to open that file!", "File Not Opened!");
                BlackLabelDisplay.Content = "Hey, just letting you know, we had a problem opening that file!";
            }
        }

        //Additions to shut down method
        private void ShutdownApp()
        {
            //Check if the default file is empty, if it is, delete it.
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string defaultFile = desktop + "/CuteWriterDoc.txt";
            string defaultFileContent = File.ReadAllText(defaultFile);
            if (defaultFileContent == "" || defaultFileContent == null)
                File.Delete(defaultFile);
            //Play meow
            System.Media.SoundPlayer player1 = new System.Media.SoundPlayer(Properties.Resources.meow4);
            player1.PlaySync();
            //Shut down the app
            App.Current.Shutdown();
        }

        //Save as menu click method
        private void SaveAsDoc_Click(object sender, RoutedEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                SaveFileAs();
            }
            else
            {
                //Does not allow user to save an empty file
                BlackLabelDisplay.Content = "Hey! You don't seem to have written anything! Try typing before you save!";
            }
        }

        //Providefunctionality to delete current doc from PC
        private void DeleteDoc_Click(object sender, RoutedEventArgs e)
        {
            if (currentFile != null || currentFile != "")
            {
                try
                {
                    File.Delete(currentFile);
                    //Deletes document and simulates creation of a new one for the user to continue working
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
                //If theres no current document to delete, show a message box stating this and provide a new doc
                MessageBoxResult warning = MessageBox.Show("Sorry, there was a problem deleting your file. You have not yet saved this file, so there was nothing to delete. We've given you a nice empty file to work on now! Make sure to save it!", "Work Not Deleted!");
                NewDoc(false);
            }
        }

        private void ExitProgram_Click(object sender, RoutedEventArgs e)
        {
            //Runs window closing method before closing anyway, so do not need to specify this
            this.Close();
        }

        //Provide additional instructions for closing
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (UserInputBox.Text != "")
            {
                //Check if user wants to save before exiting
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
                        //Cancel the shut down
                        e.Cancel = true;
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
            else
            {
                //Double check that the user wishes to exit
                MessageBoxResult haveInput = MessageBox.Show("Hey user! Are you sure that you want to exit?", "Exit?", MessageBoxButton.YesNo);
                switch (haveInput)
                {
                    case MessageBoxResult.Yes:
                        ShutdownApp();
                        break;

                    case MessageBoxResult.No:
                        //Cancel the Shut down
                        e.Cancel = true;
                        BlackLabelDisplay.Content = "You did not exit the program! :D";
                        break;
                }
            }
        }

        //Various font choices:
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

        //Font sizing functions:
        private void FontSizeDown_Click(object sender, RoutedEventArgs e)
        {
            //Prevents fontbecoming too small for editor
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
            //Prevents font becoming too large for editor
            if (UserInputBox.FontSize < 32)
            {
                UserInputBox.FontSize = FontSize++;
                CurrentFontSize.Text = Convert.ToString(UserInputBox.FontSize);
            }
            else
                BlackLabelDisplay.Content = "Sorry, you can't make that text any bigger!! :3";
        }

        //Reset fonts to original specifications functionality
        private void ResetFontsChange_Click(object sender, RoutedEventArgs e)
        {
            UserInputBox.FontSize = 12;
            UserInputBox.FontFamily = new FontFamily("Courier New");
            UserInputBox.Foreground = Brushes.Fuchsia;
        }

        //Various font colour change functionality
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
