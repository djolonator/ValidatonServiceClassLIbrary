
using ValidationService.Assertions;
using ValidationService.Constants;
using ValidationService.ValidationMessageFactory.Interface;
using static ValidationService.Models;

namespace ValidationService.ValidationMessageFactory.Implementation
{
    public class ValidationService : IValidationService
    {
        private readonly TokenValidationService _tokenValidation;
        public ValidationService()
        {
            _tokenValidation = new TokenValidationService();
        }

        public ValidationModel ValidatePatientIdentificator(string patientIdentificationType, string patientIdentificationValue)
        {
            string message = string.Empty;
            bool isValid = false;
            if (string.IsNullOrEmpty(patientIdentificationType))
                message = ValidationMessages.PatientIdentificatorTypeReq;
            else if (string.IsNullOrEmpty(patientIdentificationValue))
                message = ValidationMessages.PatientidentificatorValueReq;
            else if (!ValidationsForQueryParameters.ValidateIdentificatorType(patientIdentificationType))
                message = ValidationMessages.PatientIdentificatorType;
            else if (!ValidationsForQueryParameters.ValidateIdentificatorValue(patientIdentificationType, patientIdentificationValue))
                message = ValidationMessages.PatientidentificatorValue;
            else
                isValid = true;

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidateSsnNpi(string ssn, string npi)
        {
            string message = string.Empty;
            bool isValid = false;
            if (string.IsNullOrEmpty(ssn) && string.IsNullOrEmpty(npi))
                message = ValidationMessages.SSNorNPIReq;
            else
            {
                bool isValidSsn = ValidationsForQueryParameters.ValidateLBO(ssn);
                bool isValidNpi = ValidationsForQueryParameters.ValidateNPI(npi);
                if (!isValidSsn && !isValidNpi)
                    message = ValidationMessages.PatientidentificatorValue;
                else
                    isValid = true;
            }

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public async Task<ValidationModel> ValidateToken(string token)
        {
            string message = string.Empty;
            bool isValid = false;

            if (token != string.Empty)
                isValid = await _tokenValidation.ValidateToken(token);

            if (!isValid)
                message = ValidationMessages.TokenInvalid;

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidateNitesToken(string token)
        {
            string message = string.Empty;
            bool isValid = _tokenValidation.ValidateNitesTokenValue(token);
            if (!isValid)
                message = ValidationMessages.TokenInvalid;

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidateInstitutionCode(string institutionCode, bool isHeader = false)
        {
            string message = string.Empty;
            bool isValid = true;
            if (string.IsNullOrEmpty(institutionCode))
            {
                isValid = false;
                message = isHeader ? ValidationMessages.institutionCodeHeader : ValidationMessages.institutionCodeParam;
            }

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidateDoctorsCodeNumber(string rfzoDoctorID, string rfzoDoctorLBO, bool isRadiology)
        {
            bool isValid = false;
            string message = string.Empty;

            if (!isRadiology && string.IsNullOrEmpty(rfzoDoctorID))
                message = ValidationMessages.DoctorIdentificatorIDReq;
            else if (!isRadiology && !string.IsNullOrEmpty(rfzoDoctorID) && !ValidationsForQueryParameters.ValidateDoctorID(rfzoDoctorID))
                message = ValidationMessages.DoctorIdentificatorIDValue;
            else if (isRadiology && string.IsNullOrEmpty(rfzoDoctorID) && string.IsNullOrEmpty(rfzoDoctorLBO))
                message = ValidationMessages.DoctorIdentificator;
            else if (isRadiology && string.IsNullOrEmpty(rfzoDoctorID) && !ValidationsForQueryParameters.ValidateLBO(rfzoDoctorLBO))
                message = ValidationMessages.DoctorIdentificatorLBOValue;
            else if (isRadiology && string.IsNullOrEmpty(rfzoDoctorLBO) && !ValidationsForQueryParameters.ValidateDoctorID(rfzoDoctorID))
                message = ValidationMessages.DoctorIdentificatorIDValue;
            else
                isValid = true;

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidateNpiNumber(string npi)
        {
            bool isValid = false;
            string message = string.Empty;
            if (!ValidationsForQueryParameters.ValidateNPI(npi))
                message = ValidationMessages.PatientidentificatorValueNpi;
            else
                isValid = true;

            return new ValidationModel { IsValid = isValid, Message = message };
        }

        public ValidationModel ValidatePageNumber(string pageNumber)
        {
            bool isValid = false;
            string message = string.Empty;
            if (!ValidationsForQueryParameters.ValidateNumberGreaterThanZero(pageNumber))
                message = ValidationMessages.PageNumberGTZero;
            else
                isValid = true;

            return new ValidationModel { IsValid = isValid, Message = message };
        }
    }
}
