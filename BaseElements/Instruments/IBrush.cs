using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Brush
{
    public interface IBrush
    {
        int Width { get; set; }

        Color MainColor { get; set; }
        
    }
}
