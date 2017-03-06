using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements.Brush
{
    public class Brush : IBrush
    {
        public int size {
            get {
                throw new NotImplementedException();
            }

            set {
                // тут проверочка на min и max, они нам ещё не известны
                throw new NotImplementedException();
            }
        }
    }
}
