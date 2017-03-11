using BaseElements.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements
{
    internal class VectorLayer: Layer
    {
        // пока паблик, потом скорее всего приват с методами вставить/удалить и т.д.
        public List<IFigure> figures;

        



        /// <summary>
        /// 
        /// </summary>
        /// <param name="WorkingDirectory"></param>
        /// <returns></returns>
        public override void Save(string WorkingDirectory) {
            base.Save(WorkingDirectory);
        }
    }
}
