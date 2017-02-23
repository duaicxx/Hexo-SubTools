using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Markdown_Hexo_Worker
{
  public  class DelegateProgressValue
    {
        public int ProgressHashCode = 0;
        public delegate void SetProgressValue(int hashCode, int value,ref int statusValue);
        public event  SetProgressValue setProgressValue;
        public void SetValue (int hashCode, int value, ref int statusValue)
        {
            if (ProgressHashCode == hashCode)
            {
                statusValue = value;
            }
        }
    }
}
