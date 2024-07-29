using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Model_Calc : INotifyPropertyChanged
    {
        private string input_str;
        private string result;

        public string Input_str
        { 
            get { return input_str;  }
            set 
            { 
                input_str = value;
                OnPropertyChanged("Input_str");
            }
        }

        public string Result { get {  return result; }  }

        public void add_input(string str_for_add)
        {
            input_str += str_for_add;
            Console.WriteLine(input_str);
            OnPropertyChanged("Input_str");
        }

        public void SetResult()
        {
            result = new DataTable().Compute(input_str, null).ToString();
            OnPropertyChanged("Result");
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
