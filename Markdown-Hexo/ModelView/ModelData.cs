using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Markdown_Hexo.ModelView
{
   public class ModelData: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _log;
         public string Log 
	     { 
	        get { return _log; } 
	        set {
                _log = value; 
	            if (this.PropertyChanged != null) 
	            {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Log"));
                } 
	        } 
	    }

    }
}
