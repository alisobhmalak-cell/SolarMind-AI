using Firebase.Auth;

namespace Solar_Mind_AI;

public partial class SignUpPage : ContentPage
{
    private readonly FirebaseAuthClient _authClient;

    public SignUpPage(FirebaseAuthClient authClient)
    {
        InitializeComponent();
        _authClient = authClient;
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.MainPage = new WelcomePage();
        return true;
    }

    private void OnBackArrowTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new WelcomePage();
    }

    private void OnEyeClicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }

    private void OnConfirmEyeClicked(object sender, EventArgs e)
    {
        ConfirmPasswordEntry.IsPassword =
            !ConfirmPasswordEntry.IsPassword;
    }

    private async void OnCreateAccountClicked(object sender, EventArgs e)
    {
        string fullName = FullNameEntry.Text?.Trim() ?? "";
        string email = EmailEntry.Text?.Trim() ?? "";
        string password = PasswordEntry.Text ?? "";
        string confirmPassword = ConfirmPasswordEntry.Text ?? "";

        // التأكد من ملء البيانات
        if (string.IsNullOrWhiteSpace(fullName))
        {
            await DisplayAlert("Missing Information",
                "Please enter your full name.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Missing Information",
                "Please enter your email address.", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Missing Information",
                "Please enter a password.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Password Error",
                "Passwords do not match.", "OK");
            return;
        }

        if (password.Length < 8)
        {
            await DisplayAlert("Password Error",
                "Password must be at least 8 characters.", "OK");
            return;
        }

        try
        {
            // إنشاء الحساب في Firebase
            var userCredential =
                await _authClient.CreateUserWithEmailAndPasswordAsync(
                    email,
                    password,
                    fullName);

            if (userCredential?.User != null)
            {
                await DisplayAlert(
                    "Account Created",
                    "Your account has been created successfully!",
                    "OK");

                // بعد نجاح التسجيل نروح للـ Login
                Application.Current.MainPage = new LoginPage(MauiProgram.AuthClient);
            }
        }
        catch (FirebaseAuthException ex)
        {
            await DisplayAlert(
                "Sign Up Failed",
                ex.Reason.ToString(),
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private void OnLoginTextTapped(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage(MauiProgram.AuthClient);
    }   
}