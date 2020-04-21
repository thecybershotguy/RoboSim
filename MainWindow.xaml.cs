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
  

        public void IntialiseValues()
        {
            link[0].AxisX = 0;
            link[0].AxisY = 0;
            link[0].AxisZ = 0;
            link[0].RotX = 0;
            link[0].RotY = 0;
            link[0].RotZ = 0;
            link[0].Angle = 0;
            link[0].MaxAngle = 180;
            link[0].MinAngle = -180;

            link[1].AxisX = 0;
            link[1].AxisY = 0;
            link[1].AxisZ = 0;
            link[1].RotX = 0;
            link[1].RotY = 0;
            link[1].RotZ = 0;
            link[1].Angle = 0;
            link[1].MaxAngle = 180;
            link[1].MinAngle = -180;

            link[2].AxisX = 0;
            link[2].AxisY = 0;
            link[2].AxisZ = 0;
            link[2].RotX = 0;
            link[2].RotY = 0;
            link[2].RotZ = 0;
            link[2].Angle = 0;
            link[2].MaxAngle = 180;
            link[2].MinAngle = -180;

            link[3].AxisX = 0;
            link[3].AxisY = 0;
            link[3].AxisZ = 0;
            link[3].RotX = 0;
            link[3].RotY = 0;
            link[3].RotZ = 0;
            link[3].Angle = 0;
            link[3].MaxAngle = 180;
            link[3].MinAngle = -180;

            link[4].AxisX = 0;
            link[4].AxisY = 0;
            link[4].AxisZ = 0;
            link[4].RotX = 0;
            link[4].RotY = 0;
            link[4].RotZ = 0;
            link[4].Angle = 0;
            link[4].MaxAngle = 180;
            link[4].MinAngle = -180;

            link[5].AxisX = 0;
            link[5].AxisY = 0;
            link[5].AxisZ = 0;
            link[5].RotX = 0;
            link[5].RotY = 0;
            link[5].RotZ = 0;
            link[5].Angle = 0;
            link[5].MaxAngle = 180;
            link[5].MinAngle = -180;

            link[6].AxisX = 0;
            link[6].AxisY = 0;
            link[6].AxisZ = 0;
            link[6].RotX = 0;
            link[6].RotY = 0;
            link[6].RotZ = 0;
            link[6].Angle = 0;
            link[6].MaxAngle = 180;
            link[6].MinAngle = -180;

            link[7].AxisX = 0;
            link[7].AxisY = 0;
            link[7].AxisZ = 0;
            link[7].RotX = 0;
            link[7].RotY = 0;
            link[7].RotZ = 0;
            link[7].Angle = 0;
            link[7].MaxAngle = 180;
            link[7].MinAngle = -180;

   


        }

        private void viewPort_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetCamera();
        }

        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
