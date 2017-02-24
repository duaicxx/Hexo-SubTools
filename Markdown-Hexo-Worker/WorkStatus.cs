using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown_Hexo_Worker
{
   public class WorkStatus
    {
        private int _status;
        public WorkStatus(int id ,string name,int status)
        {
            Name = name;
            Status = status;
            Id = id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status {
            get { return _status; }
            set {
                if (value == 100)
                {
                    _status = 0;
                } else {
                    _status = value;
                }
            }
        }


    }
}
