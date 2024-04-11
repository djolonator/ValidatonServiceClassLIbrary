using static ValidationService.Models;

namespace ValidationService.ValidationMessageFactory.Interface
{
    public interface IValidationService
    {
        public ValidationModel ValidatePatientIdentificator(string patientIdentificationType, string patientIdentificationValue);
        public ValidationModel ValidateSsnNpi(string ssn, string npi);
        Task<ValidationModel> ValidateToken(string token);
        public ValidationModel ValidateNitesToken(string token);
        public ValidationModel ValidateInstitutionCode(string institutionCode, bool isHeader = false);
        public ValidationModel ValidateDoctorsCodeNumber(string rfzoDoctorID, string rfzoDoctorLBO, bool isRadiology);
        public ValidationModel ValidateNpiNumber(string npi);
        public ValidationModel ValidatePageNumber(string pageNumber);
    }
}
