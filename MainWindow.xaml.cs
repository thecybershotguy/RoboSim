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


            model = new Model("Kuka");

            link = model.LoadModel();

            viewPort.Children.Add(model.RobotModel);

            

        }

        public void SetCamera()
        {

            viewPort.RotateGesture = new MouseGesture(MouseAction.RightClick);
            viewPort.PanGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort.Camera.LookDirection = new Vector3D(966.788,-2535.089,-2206.779);
            viewPort.Camera.UpDirection = new Vector3D(-0.074,0.194,0.978);
            viewPort.Camera.Position = new Point3D(-770.491,2361.411,2370.249);
        }
  

        public void IntialiseValues()
        {

         

            // Base - Link [0]


            // Fifth Link 
            link[1].Angle = 0;
            link[1].AxisX = 0;
            link[1].AxisY = 1;
            link[1].AxisZ = 0;
            link[1].RotX = 280;
            link[1].RotY = 44;
            link[1].RotZ = 625;
            link[1].MaxAngle = 180;
            link[1].MinAngle = -180;

            // First Link
            link[2].AxisX = 0;
            link[2].AxisY = 0;
            link[2].AxisZ = 1;
            link[2].RotX = 0;  // Zero
            link[2].RotY = 0;   // Zero
            link[2].RotZ = 0;   // Zero
            link[2].Angle = 0;
            link[2].MaxAngle = 180;
            link[2].MinAngle = -180;
            

            // Fourth Link
            link[3].AxisX = 1;
            link[3].AxisY = 0;
            link[3].AxisZ = 0;
            link[3].RotX = 267.956158;
            link[3].RotY = 0;
            link[3].RotZ = 625;
            link[3].Angle = 0;
            link[3].MaxAngle = 180;
            link[3].MinAngle = -180;

            // Platform = link[4]
        
            // Second Link 
            link[5].AxisX = 0;
            link[5].AxisY = 1;
            link[5].AxisZ = 0;
            link[5].RotX =  20;
            link[5].RotY = 100;
            link[5].RotZ = 340;
            link[5].Angle = 0;
            link[5].MaxAngle = 180;
            link[5].MinAngle = -180;

            // Sixth Link
            link[6].AxisX = 1;
            link[6].AxisY = 0;
            link[6].AxisZ = 0;
            link[6].RotX = 349;
            link[6].RotY = 0;
            link[6].RotZ = 625;
            link[6].Angle = 0;
            link[6].MaxAngle = 180;
            link[6].MinAngle = -180;

            // Third  Link
            link[7].AxisX = 0;
            link[7].AxisY = 1;
            link[7].AxisZ = 0;
            link[7].RotX = 15;
            link[7].RotY = 49;
            link[7].RotZ = 600;
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


            //link[4].modelCad.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1),e.NewValue),new Point3D(link[4].RotX, link[4].RotY, link[4].RotZ));



            //link[7].modelCad.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), e.NewValue), new Point3D(15.689891, 48.9 ,600.221402));

        }
    }
}
