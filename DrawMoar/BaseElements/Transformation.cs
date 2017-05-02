namespace DrawMoar.BaseElements
{
    public abstract class Transformation
    {       
        public abstract Picture Apply(Picture picture);
        public abstract System.Windows.Point Apply(System.Windows.Point point);
        public abstract void Decompose(out System.Windows.Point translation, out System.Windows.Point scale, out double rotation);     
    }
}
