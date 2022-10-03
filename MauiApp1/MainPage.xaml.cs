using Plugin.Maui.Audio;
namespace MauiApp1;
/**
 * Project: Dive Development App 
 * Purpose Details: Get familiar with MAUI 
 * Course: CMPSC 488
 * Author: Iliza Nazeraj 
 * Date Developed: 09/28/2022
 * Last Date Changed: 10/02/2022
 * Rev:3
 */


public partial class MainPage : ContentPage
{
    private readonly IAudioManager audioManager;
    int playerRandomNum;
    Random random = new Random();
    int count = 0;
    public MainPage(IAudioManager audioManager)
	{
		InitializeComponent();
        this.audioManager = audioManager;
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		count++;    
        
        // Generate a random number
        playerRandomNum = random.Next(1, 7);
        Images.Source = ImageSource.FromFile($"dice{playerRandomNum}.png");

        // Text-speech number of total dice rolled
        TextToSpeech.Default.SpeakAsync($"You rolled {playerRandomNum}");

        // Play a sound of the dice rolling
        var playSound = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("foley_2_dice_throw_on_table.mp3"));
        playSound.Play();

        // Vibrate when the dice is rolling
        try
        {
            Vibration.Default.Vibrate(TimeSpan.FromSeconds(3));
        }
        catch (Exception )
        {
            Console.WriteLine("{0} Exception caught.");
        }


        // Turn Flashlight ON
        // Turn Flashlight On, wait one sec before turning back on
        for (int i = 0; i < count; i++)
        {
            try
            {
                await Flashlight.Default.TurnOnAsync();
                System.Threading.Thread.Sleep(1000);
                await Flashlight.Default.TurnOffAsync();
            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
        }

        // Display the number of total dice rolled
        RollBtn.Text = $"You rolled  {count} times ";
        SemanticScreenReader.Announce(RollBtn.Text);

    }

}

