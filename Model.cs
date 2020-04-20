using System.IO;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace RoboSim
{
    public class Model
    {
        public ModelImporter importer { get; set; }

        public Model3DGroup ModelGroup { get; set; }

        public ModelVisual3D Visual3D { get; set; }

        public string[] NameofFiles { get; set; }

        public Model(string BasePath)
        {
            importer = new ModelImporter();
            ModelGroup = new Model3DGroup();
            Visual3D = new ModelVisual3D();
            
            FileNames(BasePath);


        }

        private void FileNames(string BasePath)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "\\Robot Models\\" + $"{BasePath}" ;

            NameofFiles = Directory.GetFiles(path);




        }




    }
}
