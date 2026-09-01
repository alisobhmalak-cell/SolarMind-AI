using Solar_Mind_AI.Services;

namespace Solar_Mind_AI;

public partial class PlannerPage : ContentPage
{
    private readonly WeatherService _weatherService = new WeatherService();

    // قاموس المحافظات بالاسم المختصر بدون Egypt لتفادي أي قص في الشاشة
    private readonly Dictionary<string, (double Latitude, double Longitude)> _locations = new()
    {
        { "Cairo", (30.0444, 31.2357) },
        { "Giza", (30.0131, 31.2089) },
        { "Alexandria", (31.2001, 29.9187) },
        { "Port Said", (31.2653, 32.3019) },
        { "Suez", (29.9668, 32.5498) },
        { "Luxor", (25.6872, 32.6396) },
        { "Aswan", (24.0889, 32.8998) }
    };

    public PlannerPage()
    {
        InitializeComponent();

        // تحميل الطقس للقاهرة افتراضياً عند فتح الصفحة
        _ = LoadWeatherDataAsync(_locations["Cairo"].Latitude, _locations["Cairo"].Longitude);
    }

    // دالة القائمة المنسدلة لاختيار المدينة
    private async void OnLocationLayoutTapped(object sender, EventArgs e)
    {
        string action = await DisplayActionSheet("Select City", "Cancel", null, _locations.Keys.ToArray());

        if (!string.IsNullOrEmpty(action) && action != "Cancel" && _locations.ContainsKey(action))
        {
            CityDisplayLabel.Text = action;
            var coords = _locations[action];
            await LoadWeatherDataAsync(coords.Latitude, coords.Longitude);
        }
    }

    // دالة جلب بيانات الطقس من الـ API
    private async Task LoadWeatherDataAsync(double lat, double lon)
    {
        try
        {
            var weatherData = await _weatherService.GetWeatherAsync(lat, lon);

            if (weatherData != null && weatherData.Current != null)
            {
                TempLabel.Text = $"{Math.Round(weatherData.Current.Temperature)}°C";
                UvLabel.Text = $"UV {weatherData.Current.UVIndex}";
                ConditionLabel.Text = weatherData.Current.WeatherCode == 0 ? "Sunny" : "Clear";
                UvStatusLabel.Text = weatherData.Current.UVIndex > 5 ? "High" : "Moderate";
            }
        }
       catch (Exception)
        {
            TempLabel.Text = "25°C";
            ConditionLabel.Text = "Clear";
            UvLabel.Text = "UV 0";
            UvStatusLabel.Text = "Moderate";
        }
    }

    // ==================== معادلات حساب النظام الشمسي والنتائج ====================
    private void OnCalculatePlanClicked(object sender, EventArgs e)
    {
        if (double.TryParse(UsageEntry.Text, out double monthlyKwh) &&
            double.TryParse(AreaEntry.Text, out double roofArea) &&
            double.TryParse(BudgetEntry.Text, out double budget))
        {
            double dailyKwh = monthlyKwh / 30.0;
            double systemSizeKw = Math.Round(dailyKwh / 4.5, 1);
            if (systemSizeKw < 1.0) systemSizeKw = 1.0;

            int panelsCount = (int)Math.Ceiling(systemSizeKw / 0.55);
            double requiredArea = panelsCount * 2.0;

            if (requiredArea > roofArea)
            {
                DisplayAlert("Notice", $"Warning: System requires {requiredArea} m², which exceeds your roof area ({roofArea} m²).", "OK");
            }

            double estimatedCost = systemSizeKw * 15000.0;
            double dailyProduction = Math.Round(systemSizeKw * 4.5, 1);
            double monthlySavings = Math.Round(monthlyKwh * 2.2, 0);
            double paybackYears = Math.Round(estimatedCost / (monthlySavings * 12.0), 1);

            // ربط القيم الناتجة مباشرة بكروت الشاشة لتظهر في الصفحة
            PanelsResultLabel.Text = panelsCount.ToString();
            SizeResultLabel.Text = systemSizeKw.ToString();
            CostResultLabel.Text = estimatedCost.ToString("N0");
            ProductionResultLabel.Text = dailyProduction.ToString();
            SavingsResultLabel.Text = monthlySavings.ToString("N0");
            PaybackResultLabel.Text = paybackYears.ToString();

            // حساب التوفير السنوي (التوفير الشهري × 12)
            double yearlySavings = monthlySavings * 12.0;
            YearlySavingsSpan.Text = $"{yearlySavings:N0} EGP";
        }
        else
        {
            DisplayAlert("Error", "Please enter valid numeric values for all fields.", "OK");
        }
    }
}