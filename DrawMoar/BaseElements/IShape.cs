namespace DrawMoar.BaseElements
{
    public interface IShape
    {
        string Alias { get; set; }
        void Transform(ITransformation trans);
    }
}