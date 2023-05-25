using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PropertyChanged;

namespace Exam
{
    [AddINotifyPropertyChangedInterface]
    class ViewModel
    {
        private ObservableCollection<SearchForInfo> processes;
        public ViewModel()
        {
            processes = new ObservableCollection<SearchForInfo>();
        }
        public IEnumerable<SearchForInfo> Processes => processes;
        public string Source { get; set; }
        public string Word { get; set; }
        public double Progress { get; set; }
        public bool IsWaiting => Progress == 0;
        public void AddProgress(SearchForInfo info)
        {
            processes.Add(info);
        }
        public void ClearProgress()
        {
            processes.Clear();
        }
    }
}
