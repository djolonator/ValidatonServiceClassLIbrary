
namespace ValidationService.Constants
{
    public class ValidationMessages
    {
        // Patient identificator
        public const string PatientIdentificatorTypeReq = "Tip identifikatora mora biti prosleđen.";
        public const string PatientIdentificatorType = "Tip identifikatora mora biti LBO, SSN ili NPI.";
        public const string PatientidentificatorValueReq = "Vrednost identifikatora mora biti prosleđena.";
        public const string PatientidentificatorValue = "Vrednost za LBO i SSN mora da bude 11 cifara, odnosno 13 za NPI.";
        public const string PatientidentificatorValueNpi = "Vrednost za NPI mora da bude 13 cifara";

        // Patient identificator for PushPatientCase
        public const string SSNorNPIReq = "Obavezni su parametri NPI ili SSN";

        // Token
        public const string TokenInvalid = "Token nije validan";

        // Institution code
        public const string institutionCodeParam = "InstitutionCode je obavezan parametar";
        public const string institutionCodeHeader = "U header je potrebno da se prosledi InstitutionCode.";

        // Doctor identificator
        public const string DoctorIdentificatorIDReq = "U header je potrebno da se prosledi rfzoDoctorID";
        public const string DoctorIdentificatorIDValue = "rfzoDoctorID mora da sadrzi 8 cifara";
        public const string DoctorIdentificatorLBOValue = "rfzoDoctorLBO mora da sadrzi 11 cifara";
        public const string DoctorIdentificator = "U header je potrebno da se unese rfzoDoctorID ili rfzoDoctorLBO";

        // Institution
        public const string InstitutionNotFound = "Ne postoji šifra institucije koja je prosleđena kroz header.";
        public const string InstitutionNotActive = "Nije aktivna institucija koja je prosleđena kroz header.";

        // Doctor
        public const string DoctorNotFound = "Nije pronadjen lekar sa prosledjenim identifikacionim brojem";

        // PageNumber
        public const string PageNumberGTZero = "pageNumber mora biti ceo broj veci od 0";
    }
}
