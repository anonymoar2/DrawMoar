using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Figures
{
    // May be it must be abstract class
    interface IShape
    {
        int Width { get; set; }
        
        System.Drawing.Color MainColor { get; set; } // основной
    }
}
