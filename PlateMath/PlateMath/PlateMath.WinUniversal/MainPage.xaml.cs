using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PlateMath.WinUniversal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private PlateMathCalculator Calculator;
        private UIElementCollection WeightBarCollection;
        private int AppliedWeightCount = 0;
        private readonly int MaxAppliedWeightCount = 6;
        private PlateMathSettings UserSettings = new PlateMathSettings();

        public MainPage()
        {
            // TODO: Load AppSettings
            InitAppData();
            
            this.InitializeComponent();
            WeightBarCollection = BarWeightsPanel.Children;
            Calculator = new PlateMathCalculator(UserSettings);
            weightTotalText.Text = Calculator.TotalWeight + " lbs";
        }
        
        private void InitAppData()
        {
            
            Windows.Storage.ApplicationDataContainer roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            Windows.Storage.StorageFolder roamingFolder = Windows.Storage.ApplicationData.Current.RoamingFolder;

            Windows.Storage.ApplicationData.Current.DataChanged += (appData, o) =>
            {
                // TODO: Refresh your data
            };

            object value;
            if (roamingSettings.Values.TryGetValue("Settings.BarWeight", out value))
            {
                UserSettings.BarWeight = (float)value;
            }
            else
            {
                UserSettings.BarWeight = 45f;
            }
    }
    
    private void AddPlateButton(float value, double height)
    {
        // <Button Width="32" Height="{height}" BorderBrush="Black" Background="{StaticResource WeightBrushVert}" />
        var button = new Button { Width=32, Height=height, BorderBrush= new SolidColorBrush(Color.FromArgb(255, 0, 0, 0)), Background=App.Current.Resources["WeightBrushVert"] as Brush };

        button.Click += (s, e) => 
        {
            WeightBarCollection.Remove(button);
            float total = Calculator.RemoveWeight(value);
            weightTotalText.Text = total + " lbs";
            AppliedWeightCount--;
        };

        WeightBarCollection.Add(button);
        AppliedWeightCount++;
    }

    private void OnWeightAddClicked(object sender, RoutedEventArgs e)
    {
        if (AppliedWeightCount >= MaxAppliedWeightCount) return;

        var callingButton = sender as Button;
        if (callingButton == null) return;

        float value;
        var strValue = callingButton.Content as String;
        if (strValue == null) return;
        strValue = strValue.Split(' ')[0];
        if (!float.TryParse(strValue, out value)) return;

        AddPlateButton(value, callingButton.Width);

        float total = Calculator.AddWeight(value);

        weightTotalText.Text = total + " lbs";
    }

    private void OnClearWeight(object sender, RoutedEventArgs e)
    {
        float total = Calculator.ClearWeight();
        weightTotalText.Text = total + " lbs";
        WeightBarCollection.Clear();
        AppliedWeightCount = 0;
    }
}
}
