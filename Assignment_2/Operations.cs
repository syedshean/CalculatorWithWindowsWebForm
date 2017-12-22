using System;



namespace Assignment_2
{
    class Operations
    {
        private char[] operators = { '+', '-', 'x', '/', '%', '^' };
        private char[] powerOperator = { '^' };
        private char[] mulOperators = { 'x', '/', '%' };
        private char[] addOperators = { '+', '-' };
        private char[] validOperators1 = { 'x', '/', '%', '-' };
        private char[] validOperators2 = { 'x', '/', '%', '+' };        
        private string result = string.Empty;
        private string[] numbers = new string[] { };
        private string[] numbers1 = new string[] { };
        private string[] numbers2 = new string[] { };

        //Takes the input from main() class and passes it to Calculator method
        // and prints the final result
        public string CalculateTheInput(string text)
        {
            result = Calculator(text);
            double finalResult = 0;  
                  
            if(Double.TryParse(result, out finalResult))
            {
                return Convert.ToString(finalResult);
            }
            else
            {
                return result;
            }
        }

        //Passes the input for validation and then to Multiplicative and Additive 
        //operation and returns the final result as a string
        private string Calculator(string text)
        {            
            string validation = string.Empty;
            string sub = string.Empty;            

            validation = Validation(text);
            if (!(string.IsNullOrEmpty(validation))) return validation;   
                     
            text = text.Replace("++", "+").Replace("--", "+").Replace("-+", "-")
                       .Replace("+-", "-");

            if (text.IndexOf('+') == 0)
            {
                text = text.Remove(0, 1);
            }

            if (text.IndexOf('^') > 0)
            {
                text = PowerOperation(text);
            }

            if ((text.IndexOf('x') > 0) || (text.IndexOf('/') > 0) || (text.IndexOf('%') > 0))
            {
                text = MultiplicativeOperation(text); 
            }
            if ((text.IndexOf('+') < 0) && (text.IndexOf('-') == 0) && 
                (text.Split('-').Length-1) == 1)
            {
                sub = text.Substring(0, 1);
                text = text.Remove(0, 1);
            }
            if ((text.IndexOf('+') > 0) || (text.IndexOf('-') >= 0))
            {
                text = AdditiveOperation(text);
            }
            if (sub != null) text = sub + text;
            return result=text;
        }

        //Validates the input
        private string Validation(string text)
        {
            string valid = string.Empty;

            //checks for any operator at the end and for multiplicative operator at the front
            if (((text.Substring(text.Length - 1, 1)).IndexOfAny(operators) >= 0) ||
                (text.IndexOfAny(mulOperators) == 0))
            {                
                return valid = "Invalid double";
            }

            //Checks for single number input
            double result = 0;            
            if ((text.IndexOfAny(validOperators2) < 0) && (Double.TryParse(text, out result)))
            {                
                return Convert.ToString(result);
            }

            if ((text.IndexOf("-x") >= 0) || (text.IndexOf("-/") >= 0) ||
                (text.IndexOf("x/") >= 0) || (text.IndexOf("x%") >= 0) ||
                (text.IndexOf("x^") >= 0) || (text.IndexOf("/x") >= 0) ||
                (text.IndexOf("/%") >= 0) || (text.IndexOf("/^") >= 0) ||
                (text.IndexOf("%x") >= 0) || (text.IndexOf("%/") >= 0) ||
                (text.IndexOf("%^") >= 0) || (text.IndexOf("^x") >= 0) ||
                (text.IndexOf("^/") >= 0) || (text.IndexOf("^%") >= 0) ||
                (text.IndexOf("-%") >= 0) || (text.IndexOf("+x") >= 0) ||
                (text.IndexOf("+/") >= 0) || (text.IndexOf("+%") >= 0) ||
                (text.IndexOf("---") >= 0) || (text.IndexOf("+++") >= 0) ||
                (text.IndexOf("+--") >= 0) || (text.IndexOf("--+") >= 0) ||
                (text.IndexOf("+-+") >= 0) || (text.IndexOf("-+-") >= 0) ||
                (text.IndexOf("++-") >= 0) || (text.IndexOf("-++") >= 0))
            {
                return valid = "Invalid double";
            }

            string[] vtext = text.Split(operators, StringSplitOptions.RemoveEmptyEntries);
            double number = 0;

            foreach (string a in vtext)
            {
                if (!Double.TryParse(a, out number) || number > Double.MaxValue)
                {                    
                    return valid = "Invalid double";
                }
            }

            if (text.IndexOf("%0") > 0)
            {                
                return valid = "Division by zero";
            }

            if(text.IndexOf("/0") > 0)
            {
                return valid = "∞";
            }

            return valid;
        }

        //Splits the input at power operators and calculates them
        private string PowerOperation(string text)
        {
            //Loops through the input untill all the power operators are calculated
            do
            {
                numbers = text.Split(powerOperator, 2, StringSplitOptions.RemoveEmptyEntries);                

                numbers1 = numbers[0].Split(operators, StringSplitOptions.RemoveEmptyEntries);
                numbers2 = numbers[1].Split(operators, StringSplitOptions.RemoveEmptyEntries);

                int nlength1 = numbers1.Length;
                double[] temp1 = new double[nlength1];
                temp1 = Array.ConvertAll(numbers1, Double.Parse);
                int nlength2 = numbers2.Length;
                double[] temp2 = new double[nlength2];
                temp2 = Array.ConvertAll(numbers2, Double.Parse);
                double temp3 = 0;

                if(numbers[1].IndexOf('-') == 0)
                {                    
                    temp3 = Math.Pow(temp1[nlength1 - 1], -temp2[0]);
                    numbers[1] = numbers[1].Remove(0, 1);
                }
                else
                {
                    temp3 = Math.Pow(temp1[nlength1 - 1], temp2[0]);
                }                
                
                if (temp3 > Double.MaxValue || temp3 < Double.MinValue) return "Out of Range";

                //combines into one string with the calculation result
                string text2 = Convert.ToString(temp3);
                string text1 = numbers[0].Substring(0,
                                          (numbers[0].Length - numbers1[nlength1 - 1].Length));
                string text3 = numbers[1].Substring(0 + (numbers2[0].Length));
                text = text1 + text2 + text3;
               
            } while (text.IndexOf('^') >= 0);

            
            return text;
        }

        //Splits the input according to operators and passes them to MultiplicativeCalculation
        //method for calculation 
        private string MultiplicativeOperation(string text)
        {
            string sub = string.Empty;
            string sub2 = string.Empty;

            //Loops through the input untill all the mulplicative operators are calculated
            do
            {                
                numbers = text.Split(mulOperators, 2, StringSplitOptions.RemoveEmptyEntries);

                //Checks for input like a-bx+c, -bx-c etc.
                if ((numbers[1].Substring(0, 1) == "+") || (numbers[1].Substring(0, 1) == "-"))
                {
                    sub2 = numbers[1].Substring(0, 1);
                    numbers[1] = numbers[1].Remove(0, 1);
                }

                numbers1 = numbers[0].Split(operators, StringSplitOptions.RemoveEmptyEntries);
                numbers2 = numbers[1].Split(operators, StringSplitOptions.RemoveEmptyEntries);

                int x = numbers1.Length;
                double[] temp1 = new double[x];
                temp1 = Array.ConvertAll(numbers1, Double.Parse);
                int y = numbers2.Length;
                double[] temp2 = new double[y];
                temp2 = Array.ConvertAll(numbers2, Double.Parse);
                double temp3 = 0;

                temp3 = MultiplicativeCalculation(text, numbers, numbers1, numbers2, temp1, 
                                                  temp2, sub2);            
                sub2 = null;
                if (temp3 > Double.MaxValue || temp3 < Double.MinValue) return "Out of Range";

                //combines into one string with the calculation result
                string text2 = Convert.ToString(temp3);
                string text1 = numbers[0].Substring(0,
                                          (numbers[0].Length - numbers1[x - 1].Length));
                string text3 = numbers[1].Substring(0 + (numbers2[0].Length));                
                text = text1 + text2 + text3;
                
                if((text.IndexOfAny(validOperators1) < 0) && (text.IndexOf('+') == 0))
                {
                    text = text.Remove(0, 1);
                }
                if ((text.IndexOfAny(validOperators2) < 0) && (text.IndexOf('-') == 0))
                {
                    sub = text.Substring(0, 1);
                    text = text.Remove(0, 1);
                }
            } while ((text.IndexOf('x') >= 0) || (text.IndexOf('/') >= 0) || 
                     (text.IndexOf('%') >= 0));

            if (sub != null) text = sub + text;            

            return result=text;
        }

        //Calculates for multiplicative operators
        private double MultiplicativeCalculation(string text, string[] numbers, string[] numbers1, 
                                               string[] numbers2, double[] temp1, double[] temp2,
                                               string sub2)
        {
            double temp3 = 0;
            int nLength1 = numbers1.Length;
                        
            //For calculation like a+bxc, a-bxc etc
            if ((text.Substring(numbers[0].Length, 1) == "x") && string.IsNullOrEmpty(sub2))
            {
                temp3 = temp1[nLength1 - 1] * temp2[0];
            }

            //For calculation like a+bx-c, -bx-c etc
            else if ((numbers[0].Length > numbers1[nLength1 - 1].Length) &&
                    (text.Substring(numbers[0].Length, 1) == "x"))
            {
                temp3 = (sub2 != numbers[0].Substring(
                         numbers[0].Length - numbers1[nLength1 - 1].Length - 1, 1)) ?
                         -temp1[nLength1 - 1] * temp2[0] : temp1[nLength1 - 1] * temp2[0];
                numbers[0] = numbers[0].Remove(
                             numbers[0].Length - numbers1[nLength1 - 1].Length - 1, 1);
                numbers[0] = (temp3 >= 0) ? numbers[0].Insert(
                          numbers[0].Length - numbers1[nLength1 - 1].Length, "+") : numbers[0];
            }

            //For calculation like ax-b, ax+b etc
            else if ((text.Substring(numbers[0].Length, 1) == "x") &&
                     !(string.IsNullOrEmpty(sub2)))
            {
                temp3 = (sub2 == "-") ? -temp1[nLength1 - 1] * temp2[0] : 
                                         temp1[nLength1 - 1] * temp2[0];
            }
            else if ((text.Substring(numbers[0].Length, 1) == "/") &&
               string.IsNullOrEmpty(sub2))
            {
                temp3 = temp1[nLength1 - 1] / temp2[0];
            }
            else if ((numbers[0].Length > numbers1[nLength1 - 1].Length) &&
                    (text.Substring(numbers[0].Length, 1) == "/"))
            {
                temp3 = (sub2 != numbers[0].Substring(
                         numbers[0].Length - numbers1[nLength1 - 1].Length - 1, 1)) ?
                          -temp1[nLength1 - 1] / temp2[0] : temp1[nLength1 - 1] / temp2[0];
                numbers[0] = numbers[0].Remove(
                             numbers[0].Length - numbers1[nLength1 - 1].Length - 1, 1);
                numbers[0] = temp3 >= 0 ? numbers[0].Insert(
                          numbers[0].Length - numbers1[nLength1 - 1].Length, "+") : numbers[0];
            }
            else if ((text.Substring(numbers[0].Length, 1) == "/") && 
                     !(string.IsNullOrEmpty(sub2)))
            {
                temp3 = (sub2 == "-") ? -temp1[nLength1 - 1] / temp2[0] : 
                                        temp1[nLength1 - 1] / temp2[0];
            }
            else if (text.Substring(numbers[0].Length, 1) == "%")
            {
                temp3 = temp1[nLength1 - 1] % temp2[0];
            }
            return temp3;
        }

        //Splits the input according to operators and passes them to Additivecalculation
        //method for calculation 
        private string AdditiveOperation(string text)
        {
            string sub = string.Empty;
            string sub1 = string.Empty;

            //Loops through the input untill all the additive operators are calculated
            do
            {
                if ((text.IndexOf('-') == 0))
                {
                    sub1 = text.Substring(0, 1);
                    text = text.Remove(0, 1);                    
                }

                numbers = text.Split(addOperators, 2, StringSplitOptions.RemoveEmptyEntries);
                
                if (!(string.IsNullOrEmpty(sub1)))
                {
                    numbers[0] = sub1 + numbers[0];                    
                    text = sub1 + text;                                        
                }
                sub1 = null;

                numbers1 = numbers[0].Split(operators, StringSplitOptions.RemoveEmptyEntries);
                numbers2 = numbers[1].Split(operators, StringSplitOptions.RemoveEmptyEntries);

                int x = numbers1.Length;
                double[] temp1 = new double[x];
                temp1 = Array.ConvertAll(numbers1, Double.Parse);
                int y = numbers2.Length;
                double[] temp2 = new double[y];
                temp2 = Array.ConvertAll(numbers2, Double.Parse);
                double temp3 = 0;

                temp3= AdditiveCalculation(text, numbers, numbers1, numbers2, temp1, temp2);

                if (temp3 > Double.MaxValue || temp3 < Double.MinValue) return "Out of Range";

                // combining into one string with the result
                string text2 = Convert.ToString(temp3);                
                string text1 = numbers[0].Substring(0, 
                                          (numbers[0].Length - numbers1[x - 1].Length));
                string text3 = numbers[1].Substring(0 + (numbers2[0].Length));             
                text = text1 + text2 + text3;

                if ((text.IndexOfAny(validOperators1) < 0) && (text.IndexOf('+') == 0))
                {
                    text = text.Remove(0, 1);
                }
                if ((text.IndexOfAny(validOperators2) < 0) && (text.IndexOf('-') == 0))
                {
                    sub = text.Substring(0, 1);
                    text = text.Remove(0, 1);
                }
            } while ((text.IndexOf('+') >= 0) || (text.IndexOf('-') >= 0));

            if (sub != null) text = sub + text;

            return result = text;
        }

        //Calculates for additive operators
        private double AdditiveCalculation(string text, string[] numbers, string[] numbers1,
                                         string[] numbers2, double[] temp1, double[] temp2)
        {
            double temp3 = 0;
            int nLength1 = numbers1.Length;

            //Following if else block calculates for addition and substraction
            if ((text.Substring(numbers[0].Length, 1) == "+") && (numbers[0].IndexOf('-') == 0))
            {
                temp3 = -temp1[nLength1 - 1] + temp2[0];
                numbers[0] = numbers[0].Remove(0, 1);               
            }
            else if (text.Substring(numbers[0].Length, 1) == "+")
            {
                temp3 = temp1[nLength1 - 1] + temp2[0];
            }
            else if ((text.Substring(numbers[0].Length, 1) == "-") &&
                    (numbers[0].IndexOf('-') != 0))
            {
                temp3 = temp1[nLength1 - 1] - temp2[0];
            }
            else
            {
                temp3 = -temp1[nLength1 - 1] - temp2[0];
                numbers[0] = numbers[0].Remove(0, 1);
            }

            return temp3;
        }

    }
}
