using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Exporter;
using Exporter.Video;

namespace Demo
{
    class Program
    {
        static void Main(string[] args) {
            
            MP4.Save(@"C:\Users\Home\Desktop\Dipper", new string[] { @"C:\Users\Home\Desktop\Dipper\moveDipper\2.png", @"C:\Users\Home\Desktop\Dipper\moveDipper\1.png" });

            
        }
    }
}
