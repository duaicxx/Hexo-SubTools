using Markdown_Hexo_Worker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown_Hexo.ModelView
{
   public class ModelLogData: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        //日志记录
        private string _log;
         public string Log 
	     { 
	        get { return _log; } 
	        set {
                _log = value;
                OnPropertyChanged("Log");
            } 
	    }
        //信息弹窗
        private string _msg;
        public string Msg
        {
            get { return _msg; }
            set
            {
                _msg = value;
                OnPropertyChanged("Msg");
            }
        }
        //绑定数据
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        //临时数据
        private WorkStatus _tempWork;
        public WorkStatus Work
        {
            get { return _tempWork; }
            set
            {
                _tempWork = value;
                OnPropertyChanged("Work");
            }
        }
        //任务状态
        private List<WorkStatus> _workers = new List<WorkStatus>();
        public List<WorkStatus> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged("Workers");
            }
        }
        //添加任务
        public void AddWorker(int id ,string name,int status)
        {
            _workers.Add(new WorkStatus(id,name, status));
        }
        //删除任务
        public void RemoveWorker(int id)
        {
            _workers.Remove(findById(id));
        }
        //查询任务
        public WorkStatus findById(int id)
        {
            return _workers.Where(s => s.Id == id).ToList<WorkStatus>()[0];
        }
        //任务进度
        private int _status;
        public int Status
        {
            set { _status = value; OnPropertyChanged("Status"); }
            get { return _status; }
        }
    }
}
