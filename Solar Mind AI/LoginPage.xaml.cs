using Firebase.Auth;

namespace Solar_Mind_AI;

public partial class LoginPage : ContentPage
{
    private readonly FirebaseAuthClient _authClient;

    public LoginPage(FirebaseAuthClient authClient)
    {
        InitializeComponent();
        _authClient = authClient;
    }

    // زرار Login
    private async void OnLogInClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text ?? "";

        ErrorLabel.IsVisible = false;
        ErrorLabel.Text = "";

        // التأكد من البيانات
        if (string.IsNullOrWhiteSpace(email))
        {
            ErrorLabel.Text = "Please enter your email.";
            ErrorLabel.IsVisible = true;
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Please enter your password.";
            ErrorLabel.IsVisible = true;
            return;
        }

        try
        {
            // تسجيل الدخول باستخدام Firebase
            var userCredential =
                await _authClient.SignInWithEmailAndPasswordAsync(
                    email,
                    password);

            if (userCredential?.User != null)
            {
                // تسجيل الدخول نجح
                Application.Current.MainPage = new HomePage();
            }
        }
        catch (FirebaseAuthException ex)
        {
            ErrorLabel.Text = "Incorrect Email or Password, try again.";
            ErrorLabel.IsVisible = true;

            System.Diagnostics.Debug.WriteLine(
                $"Firebase Login Error: {ex.Reason}");
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = "Something went wrong. Please try again.";
            ErrorLabel.IsVisible = true;

            System.Diagnostics.Debug.WriteLine(
                $"Login Error: {ex.Message}");
        }
    }

    // زرار العين
    private void OnEyeClicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }

    // زرار الرجوع
    protected override bool OnBackButtonPressed()
    {
        Application.Current.MainPage = new WelcomePage();
        return true;
    }

    // Forgot Password
    private async void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        await DisplayAlert(
            "Forgot Password",
            "Please contact support or check your email for reset instructions.",
            "OK");
    }

    // Sign Up
    private void OnSignUpTextTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage =
            new SignUpPage(MauiProgram.AuthClient);
    }
}