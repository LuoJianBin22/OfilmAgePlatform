using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfilmAgePlatform
{
    public partial class Form_Communication : Form
    {
        static readonly Form_Communication instance = new Form_Communication();
        public static Form_Communication Instance => instance;

        private Form_Communication()
        {
            InitializeComponent();
        }

    }
}
