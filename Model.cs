using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace RoboSim
{

    public class Model
    {
        public ModelImporter importer { get; set; }

        public Model3DGroup ModelGroup { get; set; }

        public ModelVisual3D RobotModel { get; set; }

        public string[] NameofFiles { get; set; }


        public Model(string BasePath)
        {
            importer = new ModelImporter();
            ModelGroup = new Model3DGroup();
            RobotModel = new ModelVisual3D();

            Material material = new DiffuseMaterial(new SolidColorBrush(Colors.Beige));
            importer.DefaultMaterial = material;



            FileNames(BasePath);
        }

        private void FileNames(string BasePath)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Robot Models\\" + $"{BasePath}" ;

            NameofFiles = Directory.GetFiles(path);

        }

        public List<Joint> LoadModel()
        {
            List<Joint> loadedLinks = new List<Joint>();

            for (int i = 0; i < NameofFiles.Length; i++)
            {
                if (i == 0)
                {
                    importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Black));
                }
                else
                {
                    importer.DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.Orange));
                }

                loadedLinks.Add(new Joint());
                loadedLinks[i].modelCad = importer.Load(NameofFiles[i]);
                ModelGroup.Children.Add(loadedLinks[i].modelCad);

            }
          
            

            RobotModel.Content = ModelGroup;
            RobotModel.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 360));

            


            return loadedLinks;
        }

      



    }
}
