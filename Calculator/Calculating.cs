using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculating
    {
        private CustomStack<double> stack_of_number;
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

        private void set_input_str(string str)
        {
            input_string = new string[5];
            int count_add = 0;
            int count = 0;
            for (int i = 0; i < str.Length - 1;)
            {
                if (input_string.Length - count_add < 3)
                {
                    Array.Resize(ref input_string, input_string.Length + 5); //расширение массива достаточно ресурсозатратный процесс поэтому по 5 будем выделять.
                }
                if (str[i].ToString().IndexOfAny(mass_of_operators) != -1 && i == 0)
                {

                    i++;
                    while (str[i].ToString().IndexOfAny(mass_of_operators) == -1)
                    {
                        i++;
                    }
                    count = i;
                    input_string[count_add++] = str.Substring(0, i);

                }
                else
                {
                    if (str[i].ToString().IndexOfAny(mass_of_operators) != -1)
                    {

                        input_string[count_add++] = str[i].ToString();
                        i++;
                        count = i;
                    }
                    else
                    {
                        while (str[i].ToString().IndexOfAny(mass_of_operators) == -1)
                        {
                            if (str.Length > i + 1)
                            {
                                i++;
                            }
                            else
                            {
                                i++;
                                break;
                            }
                        }
                        input_string[count_add++] = str.Substring(count, i - count);
                        count = i;
                    }
                }
            }
            if (str.Length - count != 0)
            {
                input_string[count_add] = str.Substring(count, str.Length - count);
            }

            poliz_string = new string[input_string.Length];
            stack_of_number = new CustomStack<double>(str.Length - str.Length / 3 + 1);
            stack_of_char = new CustomStack<string>(str.Length / 2); // знаков всегда меньше чем половина длины строки
    }

        private void set_poliz()
        {
            int count_in_poliz = 0;
            for (
                int i = 0; i < input_string.Length; i++) 
            {
                if (input_string[i] != null)
                {
                    if (input_string[i].IndexOfAny(mass_of_operators) == -1 || input_string[i].Length > 1)
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
                            if (operationPriority[stack_of_char.Check()] >= operationPriority[input_string[i]]) // если меньше
                            {
                                while (operationPriority[stack_of_char.Check()] >= operationPriority[input_string[i]])
                                {
                                    poliz_string[count_in_poliz++] = stack_of_char.Pop();
                                    if (stack_of_char.IsEmpty) { break; }
                                }
                            }
                            stack_of_char.Push(input_string[i]);
                        }
                    }
                }
            }
            while (!stack_of_char.IsEmpty)
            {
                poliz_string[count_in_poliz++] = stack_of_char.Pop();    
            }
           
        }

        
        private string calculate_poliz()
        {
            bool flag = false;
            string total_res = "Нет результата";
            for (int i = 0; i < poliz_string.Length; i++)
            {
                if (poliz_string[i] != null)
                {
                    if (poliz_string[i].IndexOfAny(mass_of_operators) == -1 || poliz_string[i].Length > 1)
                    {
                        stack_of_number.Push(double.Parse(poliz_string[i]));
                    }
                    else
                    {
                        double n2 = stack_of_number.Pop();
                        double n1 = 0;
                        if (stack_of_number.IsEmpty)
                        {
                            n1 = double.Parse(poliz_string[i + 1]);
                            flag = true;
                        }
                        else
                        {
                            n1 = stack_of_number.Pop();
                        }
                        switch (poliz_string[i])
                        {
                            case "+": total_res = (n1 + n2 + 0).ToString(); break;
                            case "-": total_res = (n1 - n2 + 0).ToString(); break;
                            case "*": total_res = (n1 * n2).ToString(); break;
                            case "/": total_res = (n1 / n2).ToString(); break;
                        }
                        if (flag) { i +=2; flag = false; }
                        stack_of_number.Push(double.Parse(total_res));

                    }
                }
            }
            result = stack_of_number.Pop().ToString();
            return result;
        }

        public string get_result(string str)
        {
            if(Regex.IsMatch(str, @"[a-zA-Zа-яА-Я]"))
            {
                return "Неверный ввод, нельзя использовать буквы, только цифры, только хардкор";
            }
            if(Regex.IsMatch(str, @"[.]"))
            {
                return "Для ввода дробных чисел воспользуйтесь запятой (,)";
            }
            set_input_str(str);
            set_poliz();
            return calculate_poliz();
            
        }
    }
}
