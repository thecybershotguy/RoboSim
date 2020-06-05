using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;




namespace RoboSim
{

    public partial class SimulatorPage : Page 
    {
        #region SimulatorPage Class Properties
        public int[] _orderOfJoints = new int[] { 2, 4, 6, 3, 1, 5 };
        readonly MathCal _math = new MathCal();

        private readonly ModelImporter _model;
        private readonly List<Joint> _link = null;

        public GeometryModel3D _redBall { get; set; }
        public ModelVisual3D _visual { get; set; }

        Transform3DGroup _F1;
        Transform3DGroup _F2;
        Transform3DGroup _F3;
        Transform3DGroup _F4;
        Transform3DGroup _F5;
        Transform3DGroup _F6;

        RotateTransform3D _R;

        public Transform3DGroup _F7 { get; private set; }

        TranslateTransform3D _T;


        public Matrix4x4 _workFrame = new Matrix4x4(1, 0, 0, 0,
                                                     0, 1, 0, 0,
                                                     0, 0, 1, 0,
                                                     0, 0, 0, 1);

        #endregion

        public SimulatorPage()
        {
            SetCamera();

            var builder = new MeshBuilder(true, true);
            var position = new Point3D(0, 0, 0);
            builder.AddSphere(position, 15, 15, 15);
            _redBall = new GeometryModel3D(builder.ToMesh(), Materials.Brown);
            _visual = new ModelVisual3D();
            _visual.Content = _redBall;


            _model = new ModelImporter("Kuka");

            _link = _model.LoadModel();

            IntialiseValues();

            viewPort.Children.Add(_model.RobotModel);
            viewPort.Children.Add(_visual);
        }

        public void SetCamera()
        {

            viewPort.RotateGesture = new MouseGesture(MouseAction.RightClick);
            viewPort.PanGesture = new MouseGesture(MouseAction.LeftClick);
            viewPort.Camera.LookDirection = new Vector3D(966.788, -2535.089, -2206.779);
            viewPort.Camera.UpDirection = new Vector3D(-0.074, 0.194, 0.978);
            viewPort.Camera.Position = new Point3D(-770.491, 2361.411, 2370.249);
        }
        public void IntialiseValues()
        {

            // Base - Link [0]


            // Fifth Link 
            _link[1].Angle = 0;
            _link[1].AxisX = 0;
            _link[1].AxisY = 1;
            _link[1].AxisZ = 0;
            _link[1].RotX = 280;
            _link[1].RotY = 44;
            _link[1].RotZ = 625;
            _link[1].MaxAngle = 180;
            _link[1].MinAngle = -180;


            // First Link
            _link[2].Length = 215;
            _link[2].AxisX = 0;
            _link[2].AxisY = 0;
            _link[2].AxisZ = 1;
            _link[2].RotX = 0;  // Zero
            _link[2].RotY = 0;   // Zero
            _link[2].RotZ = 0;   // Zero
            _link[2].Angle = 0;
            _link[2].MaxAngle = 180;
            _link[2].MinAngle = -180;



            // Fourth Link
            _link[3].AxisX = 1;
            _link[3].AxisY = 0;
            _link[3].AxisZ = 0;
            _link[3].RotX = 267.956158;
            _link[3].RotY = 0;
            _link[3].RotZ = 625;
            _link[3].Angle = 0;
            _link[3].MaxAngle = 180;
            _link[3].MinAngle = -180;


            // Platform = link[4]

            // Second Link 
            _link[4].Length = 260;
            _link[4].AxisX = 0;
            _link[4].AxisY = 1;
            _link[4].AxisZ = 0;
            _link[4].RotX = 20;
            _link[4].RotY = 100;
            _link[4].RotZ = 340;
            _link[4].Angle = 0;
            _link[4].MaxAngle = 180;
            _link[4].MinAngle = -180;

            // Sixth Link
            _link[5].AxisX = 1;
            _link[5].AxisY = 0;
            _link[5].AxisZ = 0;
            _link[5].RotX = 355;
            _link[5].RotY = 0;
            _link[5].RotZ = 625;
            _link[5].Angle = 0;
            _link[5].MaxAngle = 180;
            _link[5].MinAngle = -180;

            // Third  Link
            _link[6].Length = 355;
            _link[6].AxisX = 0;
            _link[6].AxisY = 1;
            _link[6].AxisZ = 0;
            _link[6].RotX = 15;
            _link[6].RotY = 49;
            _link[6].RotZ = 600;
            _link[6].Angle = 0;
            _link[6].MaxAngle = 85;
            _link[6].MinAngle = -85;



            //Add DH Parameters for each link except base

            //      thetha          alpha    d       r        

            //  1   thetha          90      345      20
            //  2   thetha + 90     0        0      260
            //  3   thetha          90       0       0
            //  4   thetha         -90      260      0   
            //  5   thetha          90       0       0
            //  6   thetha + 180    0        0       0





            // Compute Rotation Matrix


        }
        private void Joint_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _link[2].Angle = joint1.Value;
            _link[4].Angle = joint2.Value;
            _link[6].Angle = joint3.Value;
            _link[3].Angle = joint4.Value;
            _link[1].Angle = joint5.Value;
            _link[5].Angle = joint6.Value;
            CalculateForwardKinematics();
        }
        public void CalculateForwardKinematics()
        {
            Vector3D Position = UpdateRotationMatrix();

            _F1 = new Transform3DGroup();
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), _link[2].Angle), new Point3D(_link[2].RotX, _link[2].RotY, _link[2].RotZ));
            _F1.Children.Add(_R);


            _F2 = new Transform3DGroup();
            _T = new TranslateTransform3D(0, 0, 0);
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _link[4].Angle), new Point3D(_link[4].RotX, _link[4].RotY, _link[4].RotZ));
            _F2.Children.Add(_T);
            _F2.Children.Add(_R);
            _F2.Children.Add(_F1);

            _F3 = new Transform3DGroup();
            _T = new TranslateTransform3D(0, 0, 0);
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _link[6].Angle), new Point3D(_link[6].RotX, _link[6].RotY, _link[6].RotZ));
            _F3.Children.Add(_T);
            _F3.Children.Add(_R);
            _F3.Children.Add(_F2);

            _F4 = new Transform3DGroup();
            _T = new TranslateTransform3D(0, 0, 0);
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), _link[3].Angle), new Point3D(_link[3].RotX, _link[3].RotY, _link[3].RotZ));
            _F4.Children.Add(_T);
            _F4.Children.Add(_R);
            _F4.Children.Add(_F3);

            _F5 = new Transform3DGroup();
            _T = new TranslateTransform3D(0, 0, 0);
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), _link[1].Angle), new Point3D(_link[1].RotX, _link[1].RotY, _link[1].RotZ));
            _F5.Children.Add(_T);
            _F5.Children.Add(_R);
            _F5.Children.Add(_F4);

            _F6 = new Transform3DGroup();
            _T = new TranslateTransform3D(0, 0, 0);
            _R = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), _link[5].Angle), new Point3D(_link[5].RotX, _link[5].RotY, _link[5].RotZ));
            _F6.Children.Add(_T);
            _F6.Children.Add(_R);
            _F6.Children.Add(_F5);

            _link[2].modelCad.Transform = _F1;
            _link[4].modelCad.Transform = _F2;
            _link[6].modelCad.Transform = _F3;
            _link[3].modelCad.Transform = _F4;
            _link[1].modelCad.Transform = _F5;
            _link[5].modelCad.Transform = _F6;

            _redBall.Transform = new TranslateTransform3D(Position);
            X.Content = "X : " + Position.X;
            Y.Content = "Y : " + Position.Y;
            Z.Content = "Z : " + Position.Z;
        }


        #region Temporary Rotatary Matrix
        public Vector3D UpdateRotationMatrix()
        {


            _link[2].DHparameter = new double[] { _math.ToRadians(_link[2].Angle), _math.ToRadians(90), 345, 20 };
            _link[4].DHparameter = new double[] { _math.ToRadians(-_link[4].Angle + 90), 0, 0, 260 };
            _link[6].DHparameter = new double[] { _math.ToRadians(-_link[6].Angle), _math.ToRadians(90), 0, 20 };
            _link[3].DHparameter = new double[] { _math.ToRadians(_link[3].Angle), _math.ToRadians(-90), 260, 0 };
            _link[1].DHparameter = new double[] { _math.ToRadians(-_link[1].Angle), _math.ToRadians(90), 0, 0 };
            _link[5].DHparameter = new double[] { _math.ToRadians(_link[5].Angle + 180), 0, 75, 0 };


            for (int i = 0; i < _orderOfJoints.Length; i++)
            {


                _link[_orderOfJoints[i]].JointMatrix = new Matrix4x4(
                                                          (float)Math.Cos(_link[_orderOfJoints[i]].DHparameter[0]), 
                                                         (float)(-Math.Sin(_link[_orderOfJoints[i]].DHparameter[0]) * Math.Cos(_link[_orderOfJoints[i]].DHparameter[1])),
                                                        (float)(Math.Sin(_link[_orderOfJoints[i]].DHparameter[0]) * Math.Sin(_link[_orderOfJoints[i]].DHparameter[1])),
                                                         (float)(_link[_orderOfJoints[i]].DHparameter[3] * Math.Cos(_link[_orderOfJoints[i]].DHparameter[0])),
                                                                    (float)(Math.Sin(_link[_orderOfJoints[i]].DHparameter[0])),
                                                                    (float)(Math.Cos(_link[_orderOfJoints[i]].DHparameter[0]) * Math.Cos(_link[_orderOfJoints[i]].DHparameter[1])),
                                                                    (float)(-Math.Cos(_link[_orderOfJoints[i]].DHparameter[0]) * Math.Sin(_link[_orderOfJoints[i]].DHparameter[1])),
                                                                    (float)(_link[_orderOfJoints[i]].DHparameter[3] * Math.Sin(_link[_orderOfJoints[i]].DHparameter[0])),
                                                                    0,
                                                                    (float)(Math.Sin(_link[_orderOfJoints[i]].DHparameter[1])),
                                                                    (float)(Math.Cos(_link[_orderOfJoints[i]].DHparameter[1])),
                                                                   (float)(_link[_orderOfJoints[i]].DHparameter[2]),
                                                                     0,
                                                                    0,
                                                                     0,
                                                                    1);
            }


            Matrix4x4 Temporary = Matrix4x4.Multiply(_workFrame, _link[_orderOfJoints[0]].JointMatrix);
            Matrix4x4 Temporary1 = Matrix4x4.Multiply(Temporary, _link[_orderOfJoints[1]].JointMatrix);
            Matrix4x4 Temporary2 = Matrix4x4.Multiply(Temporary1, _link[_orderOfJoints[2]].JointMatrix);
            Matrix4x4 Temporary3 = Matrix4x4.Multiply(Temporary2, _link[_orderOfJoints[3]].JointMatrix);
            Matrix4x4 Temporary4 = Matrix4x4.Multiply(Temporary3, _link[_orderOfJoints[4]].JointMatrix);
            Matrix4x4 Temporary5 = Matrix4x4.Multiply(Temporary4, _link[_orderOfJoints[5]].JointMatrix);

            return new Vector3D(Temporary5.M14, Temporary5.M24, Temporary5.M34);
        }

        #endregion
        private void ViewPort_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetCamera();
        }


    }
}
