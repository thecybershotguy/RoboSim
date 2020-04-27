using HelixToolkit.Wpf;
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

        public GeometryModel3D geom { get; set; }
        public ModelVisual3D visual { get; set; }

        double hypo = 541;

        Transform3DGroup F1;
        Transform3DGroup F2;
        Transform3DGroup F3;
        Transform3DGroup F41;
        Transform3DGroup F51;
        Transform3DGroup F61;
   
        RotateTransform3D R;

        public Transform3DGroup F71 { get; private set; }

        TranslateTransform3D T;

        public MainWindow()
        {
            InitializeComponent();
            SetCamera();
            


            //Tempory Geometry for testing x y z
            var builder = new MeshBuilder(true, true);
            var position = new Point3D(0, 0, 0);
            builder.AddSphere(position, 25, 15, 15);
            geom = new GeometryModel3D(builder.ToMesh(), Materials.Brown);
            visual = new ModelVisual3D();
            visual.Content = geom;


            model = new Model("Kuka");

            link = model.LoadModel();
            IntialiseValues();

            //link[4].modelCad.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 90), new Point3D(link[4].RotX, link[4].RotY, link[4].RotZ));
            //link[6].modelCad.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -90), new Point3D(link[6].RotX, link[6].RotY, link[6].RotZ));

            viewPort.Children.Add(model.RobotModel);
            viewPort.Children.Add(visual);
            compute_fk();



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
            link[2].Length = 215;
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
            link[4].Length = 260;
            link[4].AxisX = 0;
            link[4].AxisY = 1;
            link[4].AxisZ = 0;
            link[4].RotX =  20;
            link[4].RotY = 100;
            link[4].RotZ = 340;
            link[4].Angle = 0;
            link[4].MaxAngle = 180;
            link[4].MinAngle = -180;

            // Sixth Link
            link[5].AxisX = 1;
            link[5].AxisY = 0;
            link[5].AxisZ = 0;
            link[5].RotX = 349;
            link[5].RotY = 0;
            link[5].RotZ = 625;
            link[5].Angle = 0;
            link[5].MaxAngle = 180;
            link[5].MinAngle = -180;

            // Third  Link
            link[6].Length = 355;
            link[6].AxisX = 0;
            link[6].AxisY = 1;
            link[6].AxisZ = 0;
            link[6].RotX = 15;
            link[6].RotY = 49;
            link[6].RotZ = 600;
            link[6].Angle = 0;
            link[6].MaxAngle = 85;
            link[6].MinAngle = -85;




        }

        private void viewPort_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetCamera();
        }

        private void joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e )
        {

            link[2].Angle = joint1.Value;
            link[4].Angle = joint2.Value;
            link[6].Angle = joint3.Value ;
            link[3].Angle = joint4.Value;
            link[1].Angle = joint5.Value;
            link[5].Angle = joint6.Value;

            compute_fk();

        }


        public void compute_fk()
        {



            F1 = new Transform3DGroup();
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), link[2].Angle), new Point3D(link[2].RotX, link[2].RotY, link[2].RotZ));
            F1.Children.Add(R);



            F2 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), link[4].Angle), new Point3D(link[4].RotX, link[4].RotY, link[4].RotZ));
            F2.Children.Add(T);
            F2.Children.Add(R);
            F2.Children.Add(F1);




            F3 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), link[6].Angle), new Point3D(link[6].RotX, link[6].RotY, link[6].RotZ));
            F3.Children.Add(T);
            F3.Children.Add(R);
            F3.Children.Add(F2);


            F41 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), link[3].Angle), new Point3D(link[3].RotX, link[3].RotY, link[3].RotZ));
            F41.Children.Add(T);
            F41.Children.Add(R);
            F41.Children.Add(F3);


            F51 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), link[1].Angle), new Point3D(link[1].RotX, link[1].RotY, link[1].RotZ));
            F51.Children.Add(T);
            F51.Children.Add(R);
            F51.Children.Add(F41);


            F61 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), link[5].Angle), new Point3D(link[5].RotX, link[5].RotY, link[5].RotZ));
            F61.Children.Add(T);
            F61.Children.Add(R);
            F61.Children.Add(F51);

            F71 = new Transform3DGroup();
            T = new TranslateTransform3D(0, 0, 0);
            R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0), new Point3D(0,0,0));
            F71.Children.Add(T);
            F71.Children.Add(R);
            F71.Children.Add(F61);




            link[2].modelCad.Transform = F1;
            link[4].modelCad.Transform = F2;
            link[6].modelCad.Transform = F3;
            link[3].modelCad.Transform = F41;
            link[1].modelCad.Transform = F51;
            link[5].modelCad.Transform = F61;


            X.Content = "X : " + link[5].modelCad.Bounds.X;
            Y.Content = "Y : " +  link[5].modelCad.Bounds.Y;
            Z.Content = "Z : " + link[5].modelCad.Bounds.Z;





        }

    }
}
