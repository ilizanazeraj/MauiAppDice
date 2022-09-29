using Plugin.Maui.Audio;

namespace MauiApp1;

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
        string v = $" {playerRandomNum} ";
        Images.Source = ImageSource.FromFile($"dice{playerRandomNum}.png");

        // Text-speech number of total dice rolled
        TextToSpeech.Default.SpeakAsync($"You rolled {playerRandomNum}");

        // Play a sound of the dice rolling
        var playSound = audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("foley_2_dice_throw_on_table.mp3"));
        playSound.Play();

        // Display the number of total dice rolled
        RollBtn.Text = $"You rolled  {count} times ";
        SemanticScreenReader.Announce(RollBtn.Text);

        // 
        TimeSpan vibrationLength = TimeSpan.FromSeconds(3);
        Vibration.Default.Vibrate(vibrationLength);
        Vibration.Default.Cancel();

    }

}

