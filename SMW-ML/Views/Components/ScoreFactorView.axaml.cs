using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace SMW_ML.Views.Components
{
    public partial class ScoreFactorView : UserControl
    {
        public ScoreFactorView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }

}