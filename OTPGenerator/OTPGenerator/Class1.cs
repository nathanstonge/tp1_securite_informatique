using System;
using System.Security.Cryptography;
using System.Text;

namespace OTPGenerator
{
    public class OTPGenerator
    {

        public static string Generate(string _dateTime, int _userId)
        {
            // 1 - Generation de la date (temps) actuel.
            //DateTime _dateTime = DateTime.UtcNow;
            //return _dateTime.ToString("dd-MM-yyyy-HH-mm-ss");

            // 2 - Transformation du temps en hash code.
            //int hash = 0;
            //hash = _dateTime.Second.GetHashCode();
            SHA1 _hash = SHA1.Create();  
            var _dateBytes = Encoding.Default.GetBytes(_dateTime.ToString());
            var _hashCode = _hash.ComputeHash(_dateBytes);
            var _codeDate = Convert.ToHexString(_hashCode);

            var _idBytes = Encoding.Default.GetBytes(_userId.ToString());
            var _hashCodeId = _hash.ComputeHash(_idBytes);
            var _codeId = Convert.ToHexString(_hashCodeId);

            var _code = string.Concat(_codeId, _codeDate);

            // 3 - Recuperation du dernier chiffre de l'identifiant d'usager.
            string _stringUserId = _userId.ToString();
            int _increment = Int32.Parse((_stringUserId.Last()).ToString());

            if (_increment == 0) 
            {
                _increment = 10;
            }

            // 4 - Generation du code de 8 chiffres a partir du hash et du dernier chiffre du id d'usager.

            string _finalCode = "";
            int _cursor = 0;

            for (int i=0; i<=7; i++)
            {
                if (_cursor > _code.Length)
                {
                    _cursor = _cursor - _code.Length;
                }

                _finalCode = string.Concat(_finalCode, _code[_cursor]);
                _cursor = _cursor + _increment;
            }

            // 5 - Remplacement des lettres hexa par des chiffres (A=1, B=2, C=3, D=4, E=5, F=6).
            _finalCode = _finalCode.Replace("A", "1");
            _finalCode = _finalCode.Replace("B", "2");
            _finalCode = _finalCode.Replace("C", "3");
            _finalCode = _finalCode.Replace("D", "4");
            _finalCode = _finalCode.Replace("E", "5");
            _finalCode = _finalCode.Replace("F", "6");

            return _finalCode;
        }
    }
}