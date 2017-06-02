using DrawMoar.Shapes;
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;

namespace DrawMoar.BaseElements {
    public class Cartoon {
        public List<Scene> scenes = new List<Scene>();

        private const int MinimalWidth = 256;
        private const int MinimalHeight = 144;
        private const int MaximumWidth = 3840;
        private const int MaximumHeight = 2160;

        public static string Name { get; private set; }

        private int width;
        public int Width {
            get { return width; }
            set {
                if (value >= MinimalWidth && value <= MaximumWidth) {
                    width = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's width must not be lower " +
                                                $"than {MinimalWidth} or bigger " +
                                                $"than {MaximumWidth} pixels.");
                }
            }
        }

        private int height;
        public int Height {
            get { return height; }
            set {
                if (value >= MinimalHeight || value <= MaximumHeight) {
                    height = value;
                }
                else {
                    throw new ArgumentException($"Cartoon's height must not be lower " +
                                                $"than {MinimalHeight} or bigger " +
                                                $"than {MaximumHeight} pixels.");
                }
            }
        }

        private string workingDirectory;
        public string WorkingDirectory {
            get {
                return workingDirectory;
            }
            set {
                if (Directory.Exists(value)) {
                    workingDirectory = value;
                }
                else if (Directory.Exists(Path.GetDirectoryName(value))) {
                    Directory.CreateDirectory(value);
                    workingDirectory = value;
                }
                else {
                    throw new ArgumentException($"Can't open directory \"{value}\".");
                }
            }
        }


        public string pathToAudio;


        public Cartoon(string name, int width, int height, string workingDirectory) {
            Name = name;
            Width = width;
            Height = height;
            WorkingDirectory = workingDirectory;
            scenes.Add(new Scene("Scene0"));
        }


        public int TotalTime { get; set; }

        public ILayer CurrentLayer { get; set; }
        public Animation CurrentAnimation { get; set; }
        public Frame CurrentFrame { get; set; }
        public Scene CurrentScene { get; set; }
        private Cartoon prev;
        public Cartoon Prev
        {
            get
            {
                return prev;
            }
            set
            {
                prev = (Cartoon)value.Clone();
                GC.Collect();
            }
        }
        public int PrevCurrentFrameNumber { get; set; }
        public int PrevCurrentSceneNumber { get; set; }
        public int PrevCurrentLayerNumber { get; set; }

        public object Clone()
        {
            var bufScenes = new List<Scene>();

            foreach (var scene in scenes)
            {
                bufScenes.Add((Scene)scene.Clone());
            }

            var bufCartoon = new Cartoon(Name, Width, Height, WorkingDirectory);
            bufCartoon.scenes = bufScenes;

            return bufCartoon;
        }

        public void SaveToFile(string pathToFile) {
            string pathToDrm = Path.Combine(pathToFile, $"{Name}_drm");
            Directory.CreateDirectory(pathToDrm);
            Directory.CreateDirectory(Path.Combine(pathToDrm, "images"));
            FileInfo f1 = new FileInfo(Path.Combine(pathToDrm, "list.txt"));
            List<string> lines = new List<string>();
            lines.Add($"{Name}*{Width}*{Height}");
            lines.Add($"{WorkingDirectory}");
            foreach (var scene in scenes) {
                lines.AddRange(scene.SaveToFile(pathToDrm));
            }
            foreach (var l in lines) {
                using (StreamWriter sw = f1.CreateText()) {
                    sw.WriteLine(l);
                }
            }
            File.WriteAllLines(Path.Combine(pathToDrm, "list.txt"), lines.ToArray());
        }

        public void OpenFile(string[] lines) {
            // здесь в принципе было бы неплохо удалять нулевую сцену, нулевой кадр и слой которые автоматически сгенерились
            for(int i = 2; i < lines.Length; i++) {
                string[] lineSet = lines[i].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                if(lineSet[0] == "Scene") {
                    scenes.Add(new Scene(lineSet[1]));
                }
                else if(lineSet[0] == "Frame") {
                    scenes.Last().frames.Add(new Frame(lineSet[1]));
                    scenes.Last().frames.Last().duration = Convert.ToSingle(lineSet[2]);
                }
                else if(lineSet[0] == "Animation") {
                    string[] layerSet = lines[i + 1].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries);
                    i++;
                    int k = i;
                    for(int j = i; !Equals(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[0], "Transformation") && j < lines.Length - 1; j++) {
                        k = j;
                    }
                    List<Transformation> transformations = new List<Transformation>();
                    for(int j = k; Equals(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[0], "Transformation"); j++) {
                        if(Equals(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "translate")) {
                            transformations.Add(new TranslateTransformation(new System.Windows.Point(Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2]), Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[3]))));   
                        }
                        if (Equals(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "rotate")) {
                            transformations.Add(new RotateTransformation(new System.Windows.Point(Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2]), Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[3])), Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[4])));
                        }
                        if (Equals(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "scale")) {
                            transformations.Add(new ScaleTransformation(new System.Windows.Point(Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2]), Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[3])), Convert.ToDouble(lines[j].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[4])));
                        }
                    }
                    ILayer layer = new VectorLayer(layerSet[1]);
                    if(layerSet[2] == "r") {
                        layer = new RasterLayer(layerSet[1]);
                        ((RasterLayer)layer).Picture.Position = new System.Windows.Point(Convert.ToInt32(layerSet[3]), Convert.ToInt32(layerSet[4]));
                        ((RasterLayer)layer).Picture.Image = System.Drawing.Image.FromFile(lines[i + 1]);
                    }
                    if(layerSet[2] == "v") {
                        for(int s = i + 1; s < lines.Length && Equals(lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[0], "Shape"); s++) {
                            if (Equals(lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "line")) {
                                string[] parameters = lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                ((VectorLayer)layer).Picture.shapes.Add(new Line(new System.Windows.Point(Convert.ToDouble(parameters[0]), Convert.ToDouble(parameters[1])), new System.Windows.Point(Convert.ToDouble(parameters[2]), Convert.ToDouble(parameters[3]))));
                                ((VectorLayer)layer).Picture.shapes.Last().Thickness = Convert.ToDouble(parameters[4]);
                                ((VectorLayer)layer).Picture.shapes.Last().Color = new Color(Convert.ToByte(parameters[5]), Convert.ToByte(parameters[6]), Convert.ToByte(parameters[7]), Convert.ToByte(parameters[8]));
                             }
                            if (Equals(lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "ellipse")) {
                                string[] parameters = lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                ((VectorLayer)layer).Picture.shapes.Add(new Ellipse(new System.Windows.Point(Convert.ToDouble(parameters[0]), Convert.ToDouble(parameters[1])), new System.Windows.Size(Convert.ToDouble(parameters[7]), Convert.ToDouble(parameters[8])), Convert.ToDouble(9), Convert.ToDouble(10), Convert.ToDouble(11)));
                                ((VectorLayer)layer).Picture.shapes.Last().Thickness = Convert.ToDouble(parameters[2]);
                                ((VectorLayer)layer).Picture.shapes.Last().Color = new Color(Convert.ToByte(parameters[3]), Convert.ToByte(parameters[4]), Convert.ToByte(parameters[5]), Convert.ToByte(parameters[6]));
                            }
                            if (Equals(lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[1], "rectangle")) {
                                string[] parameters = lines[s].Split(new char[] { '*' }, StringSplitOptions.RemoveEmptyEntries)[2].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                ((VectorLayer)layer).Picture.shapes.Add(new Rectangle(new System.Windows.Point(Convert.ToDouble(parameters[0]), Convert.ToDouble(parameters[1])), new System.Windows.Size(Convert.ToDouble(parameters[7]), Convert.ToDouble(parameters[8])), Convert.ToDouble(9), Convert.ToDouble(10), Convert.ToDouble(11)));
                                ((VectorLayer)layer).Picture.shapes.Last().Thickness = Convert.ToDouble(parameters[2]);
                                ((VectorLayer)layer).Picture.shapes.Last().Color = new Color(Convert.ToByte(parameters[3]), Convert.ToByte(parameters[4]), Convert.ToByte(parameters[5]), Convert.ToByte(parameters[6]));
                            }
                        }
                    }
                    scenes.Last().frames.Last().animations.Add(new Animation(layer, transformations));
                    i = k;
                }
            }
        }

    }
}