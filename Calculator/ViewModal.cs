using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.RightsManagement;

namespace Calculator
{
    public class ViewModal : INotifyPropertyChanged
    {
        private Model_Calc my_class;
        private RelayCommand addInput;
        private RelayCommand setResult;
        private RelayCommand removeLastInput;
        private RelayCommand removeAllInput;
        private RelayCommand removeLastSymbol;
        public RelayCommand AddInput
        {
            get
            {
                
                return addInput ??
                  (addInput = new RelayCommand(obj =>
                  {
                      
                      my_class.add_input(obj.ToString());
                  }));
            }
        }

        public RelayCommand SetResult
        {
            get
            {
                return setResult ??
                    (setResult = new RelayCommand(obj =>
                    {
                        my_class.SetResult();
                    }));
            }
        }

        public RelayCommand RemoveLastInput
        {
            get
            {

                return removeLastInput ??
                  (removeLastInput = new RelayCommand(obj =>
                  {

                      my_class.rm_last_input();
                  }));
            }
        }

        public RelayCommand RemoveAllInput
        {
            get
            {

                return removeAllInput ??
                  (removeAllInput = new RelayCommand(obj =>
                  {

                      my_class.rm_all_input();
                  }));
            }
        }

        public RelayCommand RemoveLastSymbol
        {
            get
            {

                return removeLastSymbol ??
                  (removeLastSymbol = new RelayCommand(obj =>
                  {
                      my_class.rm_last_symbol();
                  }));
            }
        }

        public Model_Calc My_Class
        {
            get { return my_class; }
            set
            {
                my_class = value;
                OnPropertyChanged("SelectedPhone");
            }
        }
        public ViewModal()
        {
            my_class = new Model_Calc();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
