using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Storage
{
    interface IStorage
    {
        void GemSkema(string hold, string fag, string overskrift, string fritekst, string email);
        void AddNewUser(string CreateEmail, string createPassWord, int securityLevel);
        IEnumerable<string> GetTeam();
        IEnumerable<string> GetEmail();

        IEnumerable<string> GetClassEmail();

        IEnumerable<string> GetClassName();

        void SaveTeam(string team);
        void SaveClass(string classe, string email);
        string GetStudentCode();
        void GemStudentAnswer(List<string> fordelList, List<string> forbedringList, string surveyCode);
        void ValidateUser(string email, string password, ref string message, ref int userType);
        IEnumerable<string> GetSurvey();

        void GetSurveyAnswer(string code, ref List<Models.Teacher.SurveyObject> surveyFordele, ref List<Models.Teacher.SurveyObject> surveyForbedringer);
        bool CheckSurveyCode(string surveyCode);
        bool CheckUser(string email, string hashedPassword);
        void GemStudentCheckBox(string checkCheckBox);
    }
}
