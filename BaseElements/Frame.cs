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

        public float duration {
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
            // в будущем создавать новую папку в workingdirectore "rasterlayers" и уже туда закидввать этот новый слой и вообще все растровые
            layers.Add(new RasterLayer(/*workingDirectory*/));
        }

        // метод засунуть в cartoon
        /// <summary>
        /// Save all layers and return filename
        /// </summary>
        /// <returns></returns>
        public string Save() {
            foreach (var layer in layers) {
                if (layer.Visible) {
                    if (!layer.save) {
                        layer.Save(WorkingDirectory); // сохраним все несохраненые видимые слои в картинки
                    }
                }
            }
            // и тут они склеиваются но я пока забила это писать, сложненько там всё вроде
            // и склеиваются в картинку имя ниже
            return $"img.png";
        }

        public void GetPicture() {
            string pathToPicture = ""; // этой строки потом не будет
            // Вызывает метод из импортера, котрый вернёт путь до картинки string path;
            layers.Add(new RasterLayer(/*pathToPicture*/));
        }

        // объединение текущего слоя с предыдущим если indexLayer >=1, иначе кидаем исключение
        public void MergeLayers(int indexLayer) {
            // 1. создаём новый слой-объединение этих вот, где downLayer снизу
            // 2. удаляем оба слоя из списка слоёв (то есть слои с индексами indexLayer и indexLayer - 1)
            // 3. вставляем новый получившийся слой по индексу indexLayer -1
        }

    }
}
