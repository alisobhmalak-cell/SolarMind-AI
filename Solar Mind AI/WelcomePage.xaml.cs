namespace Solar_Mind_AI;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    private void OnLogInClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage(MauiProgram.AuthClient );
    }

    private void OnGetStartedClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage =
            new SignUpPage(MauiProgram.AuthClient);
    }
}