using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evalulet.Storage
{

    public class Database : IStorage
    {
        SqlConnection conn = new SqlConnection("Data Source=den1.mssql6.gear.host;Initial Catalog=evalulet; User Id =evalulet; Password=Qb4g~lfT_0Az");

        public void AddNewUser(string createEmail, string createPassWord, int securityLevel)
        {
            string email = createEmail;
            string password = createPassWord;
            int security = securityLevel;

            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand command = new SqlCommand("INSERT INTO UserEval (email, userPassword, securityLevel) Values(@0,@1,@2)", conn);
                command.Parameters.AddWithValue("@0", email);
                command.Parameters.AddWithValue("@1", password);
                command.Parameters.AddWithValue("@2", security);
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public IEnumerable<string> GetClassEmail()
        {
            List<string> classEmail = new List<string>();
            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand select = new SqlCommand("Select email from classes", conn);

                select.Transaction = transaction;

                SqlDataReader sel = null;
                sel = select.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        classEmail.Add(sel[0].ToString());
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return classEmail;
        }

        public void GemSkema(string hold, string fag, string overskrift, string fritekst, string email)
        {
            SqlTransaction transaction2 = null;
            DateTime date = DateTime.Now;

            string nutid = date.ToString("M/dd/yyyy", CultureInfo.InvariantCulture);

            Guid g;
            g = Guid.NewGuid();
            string surveyCode = g.ToString();
            string[] codes = surveyCode.Split('-');
            try
            {
                conn.Open();
                transaction2 = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand command2 = new SqlCommand("INSERT INTO Survey (surveyCode, className, createDate, stopCirculating, lockSurvey, Fritext, Header, Email, teamName) Values(@0,@1,@2,@3,@4,@5,@6,@7,@8)", conn);


                command2.Parameters.AddWithValue("@0", codes[0]);
                command2.Parameters.AddWithValue("@1", fag);
                command2.Parameters.AddWithValue("@2", nutid);
                command2.Parameters.AddWithValue("@3", "0");
                command2.Parameters.AddWithValue("@4", "0");
                command2.Parameters.AddWithValue("@5", fritekst);
                command2.Parameters.AddWithValue("@6", overskrift);
                command2.Parameters.AddWithValue("@7", email);
                command2.Parameters.AddWithValue("@8", hold);

                command2.Transaction = transaction2;
                command2.ExecuteNonQuery();
                transaction2.Commit();
            }
            catch (Exception)
            {
                if (transaction2 != null)
                {
                    transaction2.Rollback();
                }
            }
            finally
            {
                conn.Close();
            }
        }

        public IEnumerable<string> GetTeam()
        {
            List<string> team = new List<string>();

            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand select = new SqlCommand("Select * from Teams", conn);

                select.Transaction = transaction;

                SqlDataReader sel = null;
                sel = select.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        team.Add(sel[0].ToString());
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return team;
        }
        public IEnumerable<string> GetEmail()
        {
            List<string> email = new List<string>();
            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand select = new SqlCommand("Select * from UserEval", conn);

                select.Transaction = transaction;

                SqlDataReader sel = null;
                sel = select.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        email.Add(sel[0].ToString());
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return email;
        }

        public IEnumerable<string> GetClassName()
        {
            List<string> classNames = new List<string>();
            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand selectClassName = new SqlCommand("select className from Classes", conn);

                selectClassName.Transaction = transaction;

                SqlDataReader sel = null;
                sel = selectClassName.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        classNames.Add(sel[0].ToString());
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return classNames;

        }

        public IEnumerable<string> GetSurvey()
        {
            List<string> survey = new List<string>();
            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand selectClassName = new SqlCommand("select * from Survey order by teamName, className, createDate desc", conn);

                selectClassName.Transaction = transaction;

                SqlDataReader sel = null;
                sel = selectClassName.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {

                        survey.Add(sel[8].ToString() + "-" + sel[1].ToString() + "-" + sel[0].ToString() + "-" + sel[2].ToString().Substring(0, 10) + "-" + sel[5].ToString() + "-" + sel[6].ToString() + "-" + sel[7].ToString());
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return survey;
        }
        public void GetSurveyAnswer(string code, ref List<Models.Teacher.SurveyObject> surveyFordele, ref List<Models.Teacher.SurveyObject> surveyForbedringer)
        {

            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand selectClassName = new SqlCommand("select * from SurveyAnswer where surveyCode='" + code + "' order by likeCounter DESC", conn);

                selectClassName.Transaction = transaction;

                SqlDataReader sel = null;
                sel = selectClassName.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        if (sel[2].ToString() == "Fordel")
                        {
                            surveyFordele.Add(new Models.Teacher.SurveyObject
                            {
                                SurveyCode = sel[0].ToString(),
                                AnswerType = sel[2].ToString(),
                                Answer = sel[1].ToString(),
                                LikeCounter = sel[3].ToString()
                            });
                        }
                        else
                        {
                            surveyForbedringer.Add(new Models.Teacher.SurveyObject
                            {
                                SurveyCode = sel[0].ToString(),
                                AnswerType = sel[2].ToString(),
                                Answer = sel[1].ToString(),
                                LikeCounter = sel[3].ToString()
                            });
                        }
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void SaveTeam(string team)
        {
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand command = new SqlCommand("insert into Teams (teamName) values (@0)", conn);
                command.Parameters.AddWithValue("@0", team);
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                conn.Close();
            }
        }
        public void SaveClass(string classe, string email)
        {
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction(IsolationLevel.ReadUncommitted);
                SqlCommand command = new SqlCommand("Insert Into Classes(className, email) values (@0, @1)", conn);
                command.Parameters.AddWithValue("@0", classe);
                command.Parameters.AddWithValue("@1", email);
                command.Transaction = transaction;
                command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
            }
            finally
            {
                conn.Close();
            }
        }
        public void ValidateUser(string email, string password, ref string message, ref int userType)
        {
            conn.Open();
            SqlCommand comm = new SqlCommand("select count(1) from UserEval where email like '" + email + "'", conn);

            int userCount = (int)comm.ExecuteScalar();
            if (userCount > 0)
            {
                comm = new SqlCommand("select count(1) from UserEval where email like '" + email +
                    "' and userPassword like '" + password + "'", conn);
                userCount = (int)comm.ExecuteScalar();
                if (userCount > 0)
                {
                    comm = new SqlCommand("select securityLevel from UserEval where email like '" + email +
                        "' and userPassword like '" + password + "'", conn);
                    int result = (int)comm.ExecuteScalar();
                    if (result == 0)
                        userType = 0;
                    else
                        userType = 1;
                }
                else
                    message = "Forkert password. Prøv igen";
            }
            else
                message = "Emailen findes ikke i systemet. Prøv igen";
            conn.Close();
        }

        public string GetStudentCode()
        {
            string elevKode = "";

            SqlTransaction transaction = null;

            try
            {
                conn.Open();

                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand selectElevKode = new SqlCommand("Select surveyCode from Survey where teamname = 'MartinProg1'", conn);

                selectElevKode.Transaction = transaction;

                SqlDataReader sel = null;
                sel = selectElevKode.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        elevKode = sel[0].ToString();
                    }
                }
                sel.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                conn.Close();
            }

            return elevKode;
        }


        public void GemStudentAnswer(List<string> fordelList, List<string> forbedringList, string surveyCode)
        {
            conn.Open();
            try
            {
                foreach (string x in fordelList)
                {
                    SqlCommand command = new SqlCommand("insert into SurveyAnswer values (@0,@1,@2,@3)", conn);
                    command.Parameters.AddWithValue("@0", surveyCode);
                    command.Parameters.AddWithValue("@1", x);
                    command.Parameters.AddWithValue("@2", "Fordel");
                    command.Parameters.AddWithValue("@3", "0");
                    command.ExecuteNonQuery();
                }

            }
            catch (Exception)
            {

            }

            try
            {
                foreach (string y in forbedringList)
                {
                    SqlCommand command = new SqlCommand("insert into SurveyAnswer values (@0,@1,@2,@3)", conn);
                    command.Parameters.AddWithValue("@0", surveyCode);
                    command.Parameters.AddWithValue("@1", y);
                    command.Parameters.AddWithValue("@2", "Forbedring");
                    command.Parameters.AddWithValue("@3", "0");
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
            }

            finally
            {
                conn.Close();
            }
        }

        public bool CheckSurveyCode(string surveyCode)
        {
            bool check = false;
            conn.Open();
            SqlCommand comm = new SqlCommand("select count(1) from Survey where surveyCode like '" + surveyCode + "'", conn);
            int count = (int)comm.ExecuteScalar();
            conn.Close();
            if (count > 0)
                check = true;

            return check;
        }
        public bool CheckUser(string email, string hashedPassword)
        {
            bool check = false;
            conn.Open();
            SqlCommand comm = new SqlCommand("select count(1) from UserEval where email like '" + email +
                "' and userPassword like '" + hashedPassword + "'", conn);

            int count = (int)comm.ExecuteScalar();
            if (count > 0)
                check = true;

            conn.Close();
            return check;
        }

        public void GemStudentCheckBox(string answer)
        {
            conn.Open();
            try
            {
                SqlTransaction transaction = null;
                transaction = conn.BeginTransaction(IsolationLevel.RepeatableRead); // Isolation level

                SqlCommand select = new SqlCommand("SELECT MAX(likeCounter) FROM surveyAnswer WHERE answer = '" + answer + "' ", conn);
                string likeCounterstring = "";
                int likeCounter = 0;
                select.Transaction = transaction;

                SqlDataReader sel = null;
                sel = select.ExecuteReader();

                if (sel.HasRows)
                {
                    while (sel.Read())
                    {
                        likeCounterstring = (sel[0].ToString());
                    }
                }
                likeCounter = Convert.ToInt32(likeCounterstring);
                likeCounter++;
                sel.Close();
                transaction.Commit();

                SqlCommand command = new SqlCommand("update surveyAnswer set likeCounter = " + likeCounter + " where answer = '" + answer + "'", conn);
                command.Parameters.AddWithValue("@0", likeCounter);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

            }

            finally
            {
                conn.Close();
            }
        }
    }
}
