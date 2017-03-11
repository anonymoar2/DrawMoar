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
