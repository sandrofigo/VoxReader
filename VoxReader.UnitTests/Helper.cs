using System.Drawing;
using VoxReader.Interfaces;

namespace VoxReader.UnitTests
{
    public static class Helper
    {
        public static Bitmap GetImageFromModel(IModel model)
        {
            // Get final image dimensions
            int x = model.Size.X * model.Size.Y;
            int y = model.Size.Z;

            var image = new Bitmap(x, y);

            foreach (Voxel voxel in model.Voxels)
            {
                int imagePositionX = model.Size.X * voxel.Position.Z + voxel.Position.X;
                int imagePositionY = voxel.Position.Y;

                image.SetPixel(imagePositionX, y - 1 - imagePositionY, System.Drawing.Color.FromArgb(voxel.Color.A, voxel.Color.R, voxel.Color.G, voxel.Color.B));
            }
            
            return image;
        }
    }
}