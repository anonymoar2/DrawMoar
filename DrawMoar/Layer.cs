using System;
using System.Collections.Generic;

using DrawMoar.BaseElements;


namespace DrawMoar
{
    public abstract class Layer
    {
        List<Tuple<ILayer, List<Transformation>>> layer;
        /// Ещё что-то из интерфейса
    }
}
