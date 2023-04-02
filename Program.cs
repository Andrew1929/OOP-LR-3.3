using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace OOP_LR_3._3
{
    class  Program 
    {
        static void Main(string[] args)
        {
            string path = @"C:\Images";
            Func<Bitmap, Bitmap> grayscale = b =>
            {
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        Color c = b.GetPixel(x, y);
                        int gray = (int)(c.R * 0.299 + c.G * 0.587 + c.B * 0.114);
                        b.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                    }
                }
                return b;
            };
            Func<Bitmap, Bitmap> rotate = b => { b.RotateFlip(RotateFlipType.Rotate90FlipNone);return b; };
            Action<Bitmap> display = b => b.Show();
            Func<string, Bitmap> read = path =>
            {
                using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    return new Bitmap(stream);
                }
            };
            foreach (string file in Directory.GetFiles(path, "*.jpg"))
            {
                Bitmap image = read(file);
                var chain = grayscale.Combine(rotate);
                Bitmap result = chain(image);
                display(result);
            }
        }
    }
   
}
