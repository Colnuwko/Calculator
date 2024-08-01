using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.String;
using System.Linq;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Model_Calc : INotifyPropertyChanged
    {
        private string input_str = "";
        private string result;
        private char[] mass_of_operators = {'+', '-', '*', '/'};
        private bool use_del_last_input = false;
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

            if (str_for_add.LastIndexOfAny(mass_of_operators) != -1 && input_str.Length != 0)
            {
                if (input_str[input_str.Length - 1].ToString().LastIndexOfAny(mass_of_operators) != -1)
                {
                    rm_last_symbol();
                }
                
            }
            input_str += str_for_add;

            Console.WriteLine(input_str);
            OnPropertyChanged("Input_str");
        }

        public void SetResult()
        {
            Calculating ex = new Calculating();
            result = ex.get_result(input_str);
            OnPropertyChanged("Result");
        }

        public void rm_last_input()
        {
            if (!use_del_last_input)
            {
                use_del_last_input = true;
                input_str = input_str.Substring(0, input_str.LastIndexOfAny(mass_of_operators) + 1);
                result = "";
                OnPropertyChanged("Result");
                OnPropertyChanged("Input_str");
            }
            else { rm_all_input(); use_del_last_input = false; }
        }
        public void rm_all_input()
        {
            input_str = "";
            result = "";
            OnPropertyChanged("Result");
            OnPropertyChanged("Input_str");
        }
        public void rm_last_symbol()
        {

            if (input_str.Length != 0)
            {
                input_str = input_str.Remove(input_str.Length - 1);
                result = "";
            }
            OnPropertyChanged("Result");
            OnPropertyChanged("Input_str");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
