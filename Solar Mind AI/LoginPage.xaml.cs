namespace Solar_Mind_AI;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }
    // برمجة زرار الدخول
    private void OnLogInClicked(object sender, EventArgs e)
    {
        string email = EmailEntry.Text;
        string password = PasswordEntry.Text;

        // 1. نخفي رسالة الخطأ في بداية الضغطة
        ErrorLabel.IsVisible = false;
        ErrorLabel.Text = "";

        // 2. نتأكد إن الخانات مش فاضية
        if (string.IsNullOrWhiteSpace(email))
        {
            ErrorLabel.Text = "Please enter your email or phone.";
            ErrorLabel.IsVisible = true;
            return; // بيوقف الكود هنا وميكملش
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Please enter your password.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // 3. اختبار بيانات الدخول (عملنا حساب وهمي للتجربة)
        if (email == "mariam@solar.com" && password == "123456")
        {
            // لو صح: هينقلنا للصفحة الرئيسية
            Application.Current.MainPage = new HomePage();
        }
        else
        {
            // لو غلط: هيظهر رسالة الخطأ دي باللون الأحمر
            ErrorLabel.Text = "Incorrect Email or Password, try again.";
            ErrorLabel.IsVisible = true;
        }
    }

    // برمجة زرار العين (بيقلب حالة الباسورد بين مخفي وظاهر)
    private void OnEyeClicked(object sender, EventArgs e)
    {
        PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
    }

    protected override bool OnBackButtonPressed()
    {
        Application.Current.MainPage = new WelcomePage();
        return true;
    }

    private void OnForgotPasswordTapped(object sender, EventArgs e)
    {
        // دي رسالة وهمية شيك هتظهر للمستخدم في نص الشاشة
        DisplayAlert("Forgot Password", "Please contact support or check your email for reset instructions.", "OK");
    }

    private void OnSignUpTextTapped(object sender, EventArgs e)
    {
        // هتبدل شاشة اللوج ان وتفتح شاشة إنشاء الحساب
        Application.Current.MainPage = new SignUpPage();
    }
}
