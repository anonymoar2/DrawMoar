using BaseElements.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements
{
    // I don't yet know why it's internal 
    // ну его писать совсем маленько хочется
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
