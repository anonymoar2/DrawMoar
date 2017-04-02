using System;
using System.Collections.Generic;

using System.Text.RegularExpressions;


namespace DrawMoar.BaseElements
{
    public class Scene
    {
        private string name;
        public string Name {
            get { return name; }
            set {
                // Change regex to more acceptable.
                if (Regex.IsMatch(value, @"[a-zA-Z0-9]+")) {
                    name = value;
                }
                else {
                    throw new ArgumentException("Frame name must contain only letters and numbers.");
                }
            }
        }


        public Scene() {
            name = "newScene";
            frames.Add(new Frame("Frame_0"));
        }


        public Scene(string name) {
            this.name = name;
            frames.Add(new Frame());
        }
        

        public List<Frame> frames = new List<Frame>();
    }
}