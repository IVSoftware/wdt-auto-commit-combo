using IVSoftware.Portable;

namespace wdt_auto_commit_combo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox.TextChanged += (sender, e) =>
            {
                _wdt.StartOrRestart(action: () =>
                {
                    if (!string.IsNullOrWhiteSpace(comboBox.Text))
                    {
                        // Condition 1: Applying "Contains" produces a unique result
                        object[] suggestions = 
                            comboBox
                            .Items
                            .OfType<object>()
                            .Where(_ => $"{_}".Contains(comboBox.Text, StringComparison.OrdinalIgnoreCase))
                            .ToArray();

                        // Condition 2: "Starts With" produces a unique result
                        if (suggestions.Length == 1) 
                        {
                            comboBox.SelectedIndex = comboBox.Items.IndexOf(suggestions[0]);
                            return;
                        }
                        suggestions =
                            comboBox
                            .Items
                            .OfType<object>()
                            .Where(_ => $"{_}".IndexOf(comboBox.Text, StringComparison.OrdinalIgnoreCase) ==0)
                            .ToArray();

                        if(suggestions.Length == 1) 
                        {
                            comboBox.SelectedIndex = comboBox.Items.IndexOf(suggestions[0]);
                        }
                    }
                });
            };
        }
        // "Any" Watchdog timer.
        // For example install NuGet: IVSoftware.Portable.WatchdogTimer
        WatchdogTimer _wdt = new WatchdogTimer { Interval = TimeSpan.FromSeconds(0.5) };
    }
}
