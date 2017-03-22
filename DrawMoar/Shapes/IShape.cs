using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DrawMoar.Shapes
{
    public interface IShape
    {
        string Alias { get; set; }

        void Draw(Canvas canvas);
        void Print();
    }
}
