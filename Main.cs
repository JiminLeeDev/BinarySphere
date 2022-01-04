using System;
using System.Drawing;
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

            core.Routine= new Action(()=>{

            });
        }
    }
}
