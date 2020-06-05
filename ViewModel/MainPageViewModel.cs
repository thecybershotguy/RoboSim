using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RoboSim.ViewModel
{
    public class MainPageViewModel
    {
        public 
        public Page SimulatorPage {
            get => _contentPage;
            set
            {
                if (_contentPage == value)
                    return;

                _contentPage = value;
                NotifyPropertyChanged();
            }

        }
}
