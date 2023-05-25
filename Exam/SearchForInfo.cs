using PropertyChanged;

namespace Exam
{
    [AddINotifyPropertyChangedInterface]
    class SearchForInfo
    {
        public SearchForInfo(){}
        public SearchForInfo(string filename, string path, int count)
        {
            FileName = filename;
            Path = path;
            CountWord = count;
        }
        public string FileName { get; set; }
        public string Path { get; set; }
        public int CountWord { get; set; }
        public double Percentage { get; set; }
        public int PercentageInt => (int)Percentage;
    }
}
