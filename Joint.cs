
using System.Windows.Media.Media3D;

namespace RoboSim
{
	public class Joint
	{
		public double RotX { get; set; }
		public double RotY { get; set; }
		public double RotZ { get; set; }

		public double Angle { get; set; }
		public double MinAngle { get; set; }

		public double MaxAngle { get; set; }
		public double AxisX { get; set; }
		public double AxisY { get; set; }
		public double AxisZ { get; set; }

		public Model3D modelCad { get; set; }

		public double Length { get; set; }
	}
	
}
