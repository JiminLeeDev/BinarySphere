using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinarySphere
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeForm();
        }

        private void InitializeForm()
        {
            InitializeComponent();

            Text = "Binary Sphere";

            var core = new Core();
            var a = new a();
            var buf = new BufferedGraphicsContext().Allocate(CreateGraphics(), ClientRectangle);

            core.SetFPS(100);

            core.Routine = new Action(() =>
            {
                buf.Graphics.Clear(Color.White);
                buf.Graphics.DrawImage(a.Draw(core.FPS), new Point(a.Location.X, a.Location.Y));
                buf.Render();
            });

            core.Initialize();

            FormClosing += async (o, e) =>
            {
                if (core.IsTerminated)
                {
                    return;
                }

                e.Cancel = true;

                core.Terminate();

                while (!core.IsTerminated)
                {
                    await Task.Delay(1);
                }

                e.Cancel = false;

                Close();
            };

            core.Run();
        }
    }
}