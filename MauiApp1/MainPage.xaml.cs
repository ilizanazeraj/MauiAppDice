using Plugin.Maui.Audio;
namespace MauiApp1;
/**
 * Project: Dive Development App 
 * Purpose Details: Get familiar with MAUI 
 * Course: CMPSC 488
 * Author: Iliza Nazeraj 
 * Date Developed: 09/28/2022
 * Last Date Changed: 09/29/2022
 * Rev:1
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

        // Display the number of total dice rolled
        RollBtn.Text = $"You rolled  {count} times ";
        SemanticScreenReader.Announce(RollBtn.Text);

        

    }

}

