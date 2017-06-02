using DrawMoar.BaseElements;


namespace DrawMoar.IO
{
    class ExportToImage
    {
        public static void SaveToImage(Frame frame, string pathToFile) {
            frame.Join().Save(pathToFile);
        }
    }
}
