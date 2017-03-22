namespace DrawMoar.BaseElements
{
    public interface IShape
    {
        string Alias { get; set; }

        void Transform(Transformation transformation);

        //Тут необходимые Draw и Print ещё, пока сложно сказать что нужно, а что нет
    }
}