using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Newtonsoft.Json; // i need this in order to use the Json file that stores the text
namespace Scenario_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WonderlandWindow : Window
    {
        private List<string> linesOfText; //this stores the texts
        private List<string> charcterNames; //this stores the name
        private int currentIndex = 0; // this is for parsing where in the JSON file the program is.
        private MediaPlayer mediaPlayer; // this is the Background Music
        public WonderlandWindow()
        {
            InitializeComponent();
            // Initialize the media player
            //mediaPlayer = new MediaPlayer();

            //// Load the media file
            //LoadMediaFile("BGM.mp3");

            //// Play the media file
            //PlayMedia();

            //// Play background music
            //mediaPlayer.Play();
            this.WindowState = WindowState.Maximized;

            LoadTextFromJson();
        }
        private void LoadTextFromJson()
        {
            try
            {
                // Read the JSON file
                string jsonFilePath = "TEXT_JSON/scenario3.json";
                string jsonText = File.ReadAllText(jsonFilePath);

                // Deserialize JSON to an object
                TextModel textModel = JsonConvert.DeserializeObject<TextModel>(jsonText);

                // Get lines of text
                linesOfText = textModel.Text;
                charcterNames = textModel.Name;

                // Display first line of text
                DisplayNextLine();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading text from JSON: " + ex.Message);
            }
        }
        private void DisplayNextLine()
        {
            if (currentIndex < linesOfText.Count)
            {
                txtVisualNovelText.Text = linesOfText[currentIndex];
                ApplyTypingAnimation(linesOfText[currentIndex]);
                txtVisualNovelName.Text = charcterNames[currentIndex];
                currentIndex++;
            }
            else
            {
                // End of text reached
                txtVisualNovelText.Text = "End of text.";
                // Environment.Exit(0); //if spacebar is pressed again \ clicks again, the user exits the scene
                this.Close();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                DisplayNextLine();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Stop the media player when the window is closing
            mediaPlayer.Stop();
        }

        //Media player for the Background Music . Thank you Sam for all this hardwork.
        //Like a good programmer I will give credit then take what you have done.
        private void LoadMediaFile(string filename)
        {
            try
            {
                // Get the full path of the media file
                // Set the media file path
                string mediaPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);


                // Check if the file exists
                if (File.Exists(mediaPath))
                {
                    // Set the media player's source
                    mediaPlayer.Open(new Uri(mediaPath));
                }
                else
                {
                    MessageBox.Show("Media file not found: " + mediaPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading media file: " + ex.Message);
            }
        }


        private void PlayMedia()
        {
            try
            {
                // Play the media file
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error playing media: " + ex.Message);
            }
        }


        // Defining a class to represent the structure of JSON data
        public class TextModel
        {
            public List<string> Text { get; set; }
            public List<string> Name { get; set; }
            public List<string> Images { get; set; }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // Initialize the media player
            mediaPlayer = new MediaPlayer();

            // Load the media file
            LoadMediaFile("BGM.mp3");

            // Play the media file
            PlayMedia();

            // Play background music
            mediaPlayer.Play();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            mediaPlayer.Stop();
        }

        private void ApplyTypingAnimation(string text) //GPT v3.5 Generated
        {
            try
            {
                // Create a Storyboard
                Storyboard storyboard = new Storyboard();

                // Add StringAnimationUsingKeyFrames to simulate typing effect
                StringAnimationUsingKeyFrames animation = new StringAnimationUsingKeyFrames();
                animation.Duration = TimeSpan.FromSeconds(text.Length * 0.1); // Adjust duration based on text length
                for (int i = 0; i <= text.Length; i++)
                {
                    animation.KeyFrames.Add(new DiscreteStringKeyFrame(text.Substring(0, i), KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i * 0.022))));
                }

                // Set the TargetProperty
                Storyboard.SetTarget(animation, txtVisualNovelText);
                Storyboard.SetTargetProperty(animation, new PropertyPath(TextBlock.TextProperty));

                // Add the animation to the Storyboard
                storyboard.Children.Add(animation);

                // Begin the animation
                storyboard.Begin();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error applying typing animation: " + ex.Message);
            }
        }
    }
}
