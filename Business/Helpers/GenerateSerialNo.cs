using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class GenerateSerialNo
    {

        public static string GenerateProductSerialNumber(string machineCode, string lastSerialNumber)
        {
            // Seri numarasının tarih bölümünü oluştur
            string datePart = DateTime.Now.ToString("yyyyMMdd");

            // Seri numarasının sayısal bölümünü oluştur
            string numberPart = "";
            if (lastSerialNumber != null)
            {
                string lastDatePart = lastSerialNumber.Substring(0, 8);
                if (datePart == lastDatePart)
                {
                    int lastNumber = int.Parse(lastSerialNumber.Substring(8, 5));
                    numberPart = (lastNumber + 1).ToString("D5");
                }
                else
                {
                    numberPart = "00001";
                }
            }
            else
            {
                numberPart = "00001";
            }

            // Seri numarasını oluştur ve döndür
            string serialNumber = datePart + numberPart + machineCode;
            return serialNumber;
        }


        public static string GenerateStokUrsSerialNo(string serialNo)
        {

            char[] chars = serialNo.ToCharArray();
            string stringPart = null;
            foreach (var letter in chars)
            {
                if (char.IsLetter(letter))
                {
                    stringPart += letter;
                }
            }


            string numberPart = serialNo[stringPart.Length..];
            var incrementedNumber = Convert.ToDouble(numberPart) + 1;
            string paddedNumber = incrementedNumber.ToString().PadLeft(15 - stringPart.Length, '0');

            string newSerialNumber = stringPart + paddedNumber;

            return newSerialNumber;
        }



    }
}
