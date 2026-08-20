namespace Solar_Mind_AI;

public partial class WelcomePage : ContentPage
{
    public WelcomePage()
    {
        InitializeComponent();
    }

    private void OnLogInClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new LoginPage();
    }
    private void OnGetStartedClicked(object sender, EventArgs e)
    {
        // الكود ده بيبدل الشاشة الحالية بشاشة تسجيل الدخول
        Application.Current.MainPage = new SignUpPage();

        // ملاحظة: لو حابة الزرار يودي لصفحة إنشاء الحساب الأول بدلي LoginPage بـ SignUpPage
    }

}
