using Microsoft.AspNetCore.Http;
using ValidationService.EndpointcValidations.Interface;
using ValidationService.ValidationMessageFactory.Interface;
using ValidationService.Exceptions;

namespace ValidationService.EndpointcValidations.Implementation
{
    public class EndpointValidation : IEndpointValidation
    {
        private readonly IValidationService _validationService;

        public EndpointValidation(IValidationService validationService)
        {
            _validationService = validationService;
        }

        public void ValidateCacheControllerResetAllProvider(HttpRequest request)
        {
            ValidateNitesToken(request.Headers["NitesToken"]);
        }

        public async Task ValidateExternalProvidersControllerGet(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }
        }

        public async Task ValidateExternalProvidersControllerGetActiveProviders(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }
        }

        public async Task ValidatePatientCasesControllerGetData(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }

            ValidatePatientIdentificator(request.Query);

            bool isRadiology = false;
            if (!bool.TryParse(request.Query["IsRadiology"], out isRadiology))
                isRadiology = false;

            if (!nitesTokenIsValid)
                ValidateGetDataHeaders(request.Headers, isRadiology);
        }

        public async Task ValidatePatientCasesControllerGetByInstitution(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }

            ValidatePatientIdentificator(request.Query);
            string institutionCode = request.Query["InstitutionCode"];
            ValidateInstitutionCode(institutionCode);
        }

        public async Task ValidatePatientCasesControllerPushPatientCase(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }

            string ssn = request.Query["SSN"];
            string npi = request.Query["NPI"];
            ValidateSsnNpi(ssn, npi);
        }

        public async Task ValidatePatientLocationControllerGet(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }

            ValidatePatientIdentificator(request.Query);
        }

        public async Task ValidatePatientPortalControllerCreateConfirmation(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }
        }

        public async Task ValidatePatientPortalControllerCheckConfirmation(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }
        }

        public void ValidatePatientPortalControllerSearchRequestLoggerByNPI(HttpRequest request)
        {
            ValidateNitesToken(request.Headers["NitesToken"]);

            string npi = request.Query["NPI"];
            string pageNumber = request.Query["PageNumber"];

            ValidateNpiNumber(npi);
            ValidatePageNumber(pageNumber);
        }

        public async Task ValidateRefreshCacheControllerGet(HttpRequest request)
        {
            var nitesTokenIsValid = _validationService.ValidateNitesToken(request.Headers["NitesToken"]).IsValid;

            if (!nitesTokenIsValid)
            {
                await ValidateBearerToken(request.Headers["Authorization"]);
            }

            ValidatePatientIdentificator(request.Query);
        }

        public void ValidateRefreshDoctorsCollectionControllerRefresh(HttpRequest request)
        {
            ValidateNitesToken(request.Headers["NitesToken"]);
        }

        public void ValidateStatisticControllerGetData(HttpRequest request)
        {
            ValidateNitesToken(request.Headers["NitesToken"]);
        }

        private void ValidatePatientIdentificator(IQueryCollection routeValues)
        {
            string patientIdentificationType = routeValues["PatientIdentificationType"];
            string patientIdentificationValue = routeValues["PatientIdentificationValue"];
            var result = _validationService.ValidatePatientIdentificator(patientIdentificationType, patientIdentificationValue);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }

        private void ValidateNitesToken(string token)
        {
            var result = _validationService.ValidateNitesToken(token);
            if (!result.IsValid)
                throw new NotAuthenticatedException(result.Message);
        }

        private async Task ValidateBearerToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                token = string.Empty;

            string trimedToken = System.Text.RegularExpressions.Regex.Replace(token, "Bearer", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase).Trim();
            var result = await _validationService.ValidateToken(trimedToken);
            if (!result.IsValid)
                throw new NotAuthenticatedException(result.Message);
        }

        private void ValidateGetDataHeaders(IHeaderDictionary headers, bool isRadiology)
        {
            var rfzoDoctorID = headers["rfzoDoctorID"].ToString();
            var rfzoDoctorLBO = headers["rfzoDoctorLBO"].ToString();
            var institutionCode = headers["institutionCode"].ToString();
            ValidateDoctorsCodeNumber(rfzoDoctorID, rfzoDoctorLBO, isRadiology);
            ValidateInstitutionCode(institutionCode, true);
        }

        private void ValidateDoctorsCodeNumber(string rfzoDoctorID, string rfzoDoctorLBO, bool isRadiology)
        {
            var result = _validationService.ValidateDoctorsCodeNumber(rfzoDoctorID, rfzoDoctorLBO, isRadiology);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }

        private void ValidateInstitutionCode(string code, bool isHeader = false)
        {
            var result = _validationService.ValidateInstitutionCode(code, isHeader);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }

        private void ValidateSsnNpi(string ssn, string npi)
        {
            var result = _validationService.ValidateSsnNpi(ssn, npi);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }

        private void ValidateNpiNumber(string npiNumber)
        {
            var result = _validationService.ValidateNpiNumber(npiNumber);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }

        private void ValidatePageNumber(string pageNumber)
        {
            var result = _validationService.ValidatePageNumber(pageNumber);
            if (!result.IsValid)
                throw new NotValidatedException(result.Message);
        }
    }
}
