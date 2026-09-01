namespace Solar_Mind_AI;
public partial class DashboardPage : ContentPage

{
    // 1. الكونستركتور الأساسي لو الصفحة اتفتحت مباشرة
    public DashboardPage()
    {
        InitializeComponent();
        LoadDynamicData("Mariam"); // اسم افتراضي لو مش متمرر
    }

    // 2. الكونستركتور اللي بيستقبل الاسم من الـ Login أو Sign Up
    public DashboardPage(string userName) : this()
    {
        if (!string.IsNullOrEmpty(userName))
        {
            LoadDynamicData(userName);
        }
    }

    private void LoadDynamicData(string userName)
    {
        int currentHour = DateTime.Now.Hour;

        // 1. الترحيب والاسم حسب الوقت
        if (currentHour >= 5 && currentHour < 12)
        {
            GreetingLabel.Text = "Good Morning,";
        }
        else if (currentHour >= 12 && currentHour < 17)
        {
            GreetingLabel.Text = "Good Afternoon,";
        }
        else
        {
            GreetingLabel.Text = "Good Evening,";
        }

        UserNameLabel.Text = $"{userName} ☀️";

        // 2. تحديث الطقس تلقائياً حسب الوقت (ليل أو نهار)
        WeatherTempLabel.Text = "31°C";

        if (currentHour >= 6 && currentHour < 18)
        {
            WeatherConditionLabel.Text = "Sunny";
        }
        else
        {
            WeatherConditionLabel.Text = "Clear"; // تظهر تلقائياً بالليل زي الشاشة اللي عرضتيها
        }
    }
}