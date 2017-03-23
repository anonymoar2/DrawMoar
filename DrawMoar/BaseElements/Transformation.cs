namespace DrawMoar.BaseElements
{
    /// <summary>
    /// Перепишу нормально
    /// </summary>
    public abstract class Transformation
    {
        /// <summary>
        /// TODO: Думаю можно не возвращать тут ничего, менять содержимое, подумать над этим (!)
        /// </summary>
        /// <param name="picture"></param>
        /// <returns></returns>
        public abstract Picture Apply(Picture picture);
        public abstract System.Windows.Point Apply(System.Windows.Point point);
        public abstract void Decompose(out System.Windows.Point translation, out System.Windows.Point scale, out double rotation);
    }
}
