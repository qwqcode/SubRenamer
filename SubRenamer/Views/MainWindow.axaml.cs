using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace SubRenamer.Views
{
    public partial class MainWindow : ReactiveWindow<Window>
    {
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
