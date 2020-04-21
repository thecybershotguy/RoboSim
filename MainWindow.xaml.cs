using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoboSim
{




    public partial class MainWindow : Window
    {

        private readonly Model model;
        private readonly List<Joint> link = null;


        public MainWindow()
        {
            InitializeComponent();
            SetCamera();







            model = new Model("SwRobot");

            link = model.LoadModel();

            viewPort.Children.Add(model.RobotModel);

            

        }

        public void SetCamera()
        {

            viewPort.RotateGesture = new MouseGesture(MouseAction.RightClick);
            viewPort.PanGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort.Camera.LookDirection = new Vector3D(590.925,-1146.123,-427.042);
            viewPort.Camera.UpDirection = new Vector3D(-0.248,0.481,0.841);
            viewPort.Camera.Position = new Point3D(-590.925, 1146.123, 427.042);
        }

       

    }
}
