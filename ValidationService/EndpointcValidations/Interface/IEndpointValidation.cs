
using Microsoft.AspNetCore.Http;

namespace ValidationService.EndpointcValidations.Interface
{
    public interface IEndpointValidation
    {
        public void ValidateCacheControllerResetAllProvider(HttpRequest request);
        public Task ValidateExternalProvidersControllerGet(HttpRequest request);
        public Task ValidateExternalProvidersControllerGetActiveProviders(HttpRequest request);
        public Task ValidatePatientCasesControllerGetData(HttpRequest request);
        public Task ValidatePatientCasesControllerGetByInstitution(HttpRequest request);
        public Task ValidatePatientCasesControllerPushPatientCase(HttpRequest request);
        public Task ValidatePatientLocationControllerGet(HttpRequest request);
        public Task ValidatePatientPortalControllerCreateConfirmation(HttpRequest request);
        public Task ValidatePatientPortalControllerCheckConfirmation(HttpRequest request);
        public void ValidatePatientPortalControllerSearchRequestLoggerByNPI(HttpRequest request);
        public Task ValidateRefreshCacheControllerGet(HttpRequest request);
        public void ValidateRefreshDoctorsCollectionControllerRefresh(HttpRequest request);
        public void ValidateStatisticControllerGetData(HttpRequest request);

    }
}
