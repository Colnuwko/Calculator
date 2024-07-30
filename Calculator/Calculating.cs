using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculating
    {
        private CustomStack<int> stack_of_number;
        private CustomStack<string> stack_of_char; // вообще уже все написано и есть структура Stack также как и List но СВОе же лучше? :)
        private string[] input_string;
        private char[] mass_of_operators = { '+', '-', '*', '/' };
        private Dictionary<string, int> operationPriority = new Dictionary<string, int> {
            {"+", 0},
            {"-", 0},
            {"*", 1},
            {"/", 1},   
         }; //Не стал писать свой словарик он мне нужен только для сравнения
        private string[] poliz_string;
        private string result;

        public Calculating(string str)
        {
            input_string = new string[5];
            int count_add = 0;
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {

                if (str[i].ToString().IndexOfAny(mass_of_operators) != -1)
                {
                    if (input_string.Length - count_add < 3)
                    {
                        Array.Resize(ref input_string, input_string.Length + 5); //расширение массива достаточно ресурсозатратный процесс поэтому по 5 будем выделять.
                    }
                    input_string[count_add++] = str.Substring(count, i-count);
                    input_string[count_add++] = str[i].ToString();
                    count = i+1; 
                }
                
            } 
            poliz_string = new string[input_string.Length];
            stack_of_number = new CustomStack<int>(str.Length - str.Length/3 + 1); 
            stack_of_char = new CustomStack<string>(str.Length/2); // знаков всегда меньше чем половина длины строки
        }

        private void set_poliz()
        {
            int count_in_poliz = 0;
            for (int i = 0; i < input_string.Length; i++) 
            {
                if (input_string[i].IndexOfAny(mass_of_operators) == -1)
                {
                    poliz_string[count_in_poliz++] = input_string[i].ToString();
                }
                else
                {
                    if (stack_of_char.IsEmpty) // если пустой то вернет true
                    {
                        stack_of_char.Push(input_string[i]);
                    }
                    else
                    {
                        if(operationPriority[stack_of_char.Check()] < operationPriority[input_string[i]]) // если меньше
                        {
                            stack_of_char.Push(input_string[i]);
                        }
                        else // если больше
                        {
                            while(operationPriority[stack_of_char.Check()] >= operationPriority[input_string[i]])
                            {
                                poliz_string[count_in_poliz++]=stack_of_char.Pop();
                            }
                        }
                    }
                }
            }
           
        }

        public string get_result()
        {
            set_poliz();
            return "123";
        }
    }
}
