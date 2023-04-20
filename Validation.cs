using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PROJECT
{
    class Validation
    {
        private bool type;
        private long digits;
        private float digit;

        public bool integerOnly(string input) //checks the input string consist of integer type only
        {
            type = long.TryParse(input, out digits);
            return type;
        }
        public bool checkLength(string input , int length) //calculates the length of the string
        {
            if (input.Length != length)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool stringOnly(string input) //checks the input string consist of string type only
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (!char.IsLetter(input[i])) //the string consist of letters
                {
                    if (char.IsWhiteSpace(input[i])) //if it is not the letter than it checks is it space between the letters than continue
                    {
                        continue;
                    }
                    else if(char.IsPunctuation(input[i]))
                    {
                        continue;
                    }
                    return false;
                }
            }
            return true;
        }
        public bool nonEmptyOrNullString(string input)//this checks is textbox is empty
        {
            if(input.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool floatOnly(string input) //checks the input string consist of float type only
        {
            type = float.TryParse(input, out digit);
            return type;
        } 
        public bool integerGreaterThanZero(int input , int limit) //this applies the limit on integer type input
        {
            if(input <= 0 || input > limit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }  
        public bool floatGreaterThanZero(float input, float limit) // this applies the limit on float type input
        {
            if (input <= 0.0 || input > limit)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 
        public string connectionString()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Furqan Waseem\Documents\library.mdf"";Integrated Security=True;Connect Timeout=30";
        }

        public string encrypt(string input)
        {
            using (MD5CryptoServiceProvider obj = new MD5CryptoServiceProvider())
            {
                UTF32Encoding utf = new UTF32Encoding();
                byte[] data = obj.ComputeHash(utf.GetBytes(input));
                return Convert.ToBase64String(data);
            }
        }
    }
}
