using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MI_ketszemelyes_beadando.Ermek
{
    class Erme
    {
        Button gomb;
        public Button Gomb { get; set; }

        bool isChecked;
        public bool IsChecked { get; set; }

        // Init
        public Erme(Button gomb, bool isChecked)
        {
            this.Gomb = gomb;
            this.IsChecked = isChecked;
        }
    }
}
