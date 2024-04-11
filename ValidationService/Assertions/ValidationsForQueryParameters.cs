
using System.Text.RegularExpressions;

namespace ValidationService.Assertions
{
    public static class ValidationsForQueryParameters
    {
        public static bool ValidateIdentificatorType(string type)
        {
            if (type.ToUpper() != "NPI" && type.ToUpper() != "SSN" && type.ToUpper() != "LBO")
                return false;
            else
                return true;
        }

        public static bool ValidateIdentificatorValue(string type, string value)
        {
            bool isValidated = false;

            if (type.ToUpper() == "LBO" || type.ToUpper() == "SSN")
                isValidated = ValidateLBO(value);
            else
                isValidated = ValidateNPI(value);

            return isValidated;
        }

        public static bool ValidateLBO(string value)
        {
            string LBOAndSSNRegPattern = "^\\d{11}$";
            return string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, LBOAndSSNRegPattern);
        }

        public static bool ValidateNPI(string value)
        {
            string NPIRegPattern = "^\\d{13}$";
            return string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, NPIRegPattern);
        }

        public static bool ValidateDoctorID(string value)
        {
            string doctorId = "^\\d{8}$";
            return string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, doctorId);
        }

        public static bool ValidateNumberGreaterThanZero(string value)
        {
            string numberGTZero = "^[1-9]\\d*$";
            return string.IsNullOrEmpty(value) ? false : Regex.IsMatch(value, numberGTZero);
        }
    }
}
