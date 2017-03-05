using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseElements
{
    public class Frame
    {
        private string workingDirectory;

        private List<Layer> layers = new List<Layer>();

        private float duration {
            // May be need to check something
            get;
            set;
        }

        /// <summary>
        /// It is the subdirectory of cartoon which contain internal important files.
        /// </summary>
        public string WorkingDirectory {
            get {
                return workingDirectory;
            }
            private set {
                if (Directory.Exists(value)) {
                    workingDirectory = value;
                }
                else if (Directory.Exists(Path.GetDirectoryName(value))) {
                    // TODO: handle all possible exceptions here and rethrow ArgumentException.
                    Directory.CreateDirectory(value);
                    workingDirectory = value;
                }
                else {
                    throw new ArgumentException($"Can't open directory \"{value}\".");
                }
            }
        }

        public Frame(string workingDirectory) {
            WorkingDirectory = workingDirectory;
        }
    }
}
