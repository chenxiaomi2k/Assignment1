using BusinessObject;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Abstractions;
using System.Data;
using System.Diagnostics.Metrics;

namespace DataAccess
{
    public class MemberDAO : BaseDAl
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();

        private MemberDAO()
        {
        }

        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<Member> GetMembers()
        {
            IDataReader dataReader = null;
            string query = "select * from Member";
            var member = new List<Member>();
            try
            {
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public void AddMember(string id, string name, string mail, Member m)
        {
            try
            {
                Member pro = GetMembers().SingleOrDefault(c => c.MemberID == int.Parse(id) || c.MemberName == name);
                if (pro == null)
                {
                    string query = "insert Member values(@id, @name, @mail, @pass, @city, @country)";
                    var parameter = new List<SqlParameter>();
                    parameter.Add(dataProvider.CreateParameter("@id", 4, m.MemberID, DbType.Int32));
                    parameter.Add(dataProvider.CreateParameter("@name", 30, m.MemberName, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@mail", 30, m.Email, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@pass", 30, m.Password, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@city", 30, m.City, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@country", 30, m.Country, DbType.String));
                    dataProvider.Insert(query, CommandType.Text, parameter.ToArray());
                }
                else
                {
                    throw new Exception("Member is already exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void UpdateMember(Member m, int id)
        {
            try
            {
                Member pro = GetMemberByID(id);
                if (!pro.MemberName.Equals(m.MemberName))
                {
                    throw new Exception("Member is already exist");
                }
                if (pro != null)
                {
                    string query = "update Member set MemberName = @name, Email = @mail," +
                        " Password = @pass, City = @city, Country = @country where MemberID = @id";
                    var parameter = new List<SqlParameter>();
                    parameter.Add(dataProvider.CreateParameter("@id", 4, m.MemberID, DbType.Int32));
                    parameter.Add(dataProvider.CreateParameter("@name", 30, m.MemberName, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@mail", 30, m.Email, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@pass", 30, m.Password, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@city", 30, m.City, DbType.String));
                    parameter.Add(dataProvider.CreateParameter("@country", 30, m.Country, DbType.String));
                    dataProvider.Insert(query, CommandType.Text, parameter.ToArray());
                }
                else
                {
                    throw new Exception("Member is not exist");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void DeleteMember(int id)
        {
            try
            {
                Member member = GetMemberByID(id);
                if (member != null)
                {
                    string query = "Delete Member where MemberID = @id";
                    var param = dataProvider.CreateParameter("@id", 4, id, DbType.Int32);
                    dataProvider.Delete(query, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("Member is not exist");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public Member? GetMemberByID(int id)
        {
            Member member = null;
            IDataReader dataReader = null;
            string query = "select * from Member where MemberID = @id";
            try
            {
                var param = dataProvider.CreateParameter("@id", 4, id, DbType.Int32);
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    member = new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public List<Member> GetMemberByName(string name)
        {
            var member = new List<Member>();
            IDataReader dataReader = null;
            string query = "select * from Member where MemberName = @name";
            try
            {
                var param = dataProvider.CreateParameter("@name", 30, name, DbType.String);
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public List<Member> SortMember()
        {
            IDataReader dataReader = null;
            var member = new List<Member>();
            string query = "select * from Member order by MemberName desc";
            try
            {
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public Member Login(string email, string pass)
        {
            IDataReader dataReader = null;
            Member member = null;
            string query = "select * from Member where Email = @mail and Password = @pass";
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(dataProvider.CreateParameter("@mail", 30, email, DbType.String));
                parameter.Add(dataProvider.CreateParameter("@pass", 30, pass, DbType.String));
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, parameter.ToArray());
                while (dataReader.Read())
                {
                    member = new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public List<string> GetCity()
        {
            IDataReader dataReader = null;
            var lcity = new List<string>();
            string query = "select distinct City from Member";
            try
            {
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    lcity.Add(dataReader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return lcity;
        }

        public List<string> GetCountry()
        {
            IDataReader dataReader = null;
            var country = new List<string>();
            string query = "select distinct Country from Member";
            try
            {
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    country.Add(dataReader.GetString(0));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return country;
        }

        public List<Member> Search(string name)
        {
            var member = new List<Member>();
            IDataReader dataReader = null;
            SqlCommand command = new SqlCommand();
            string query = "select * from Member where MemberName like @name";
            try
            {
                var param = dataProvider.CreateParameter("@name", 30, "%" + name + "%", DbType.String);
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public List<Member> SearchbyID(int id)
        {
            var member = new List<Member>();
            IDataReader dataReader = null;
            string query = "select * from Member where MemberID = @id";
            try
            {
                var param = dataProvider.CreateParameter("@id", 4, id, DbType.Int32);
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }

        public List<Member> Filter(string city, string country)
        {
            IDataReader dataReader = null;
            var member = new List<Member>();
            string query = "select * from Member where City = @city and Country = @country";
            try
            {
                var parameter = new List<SqlParameter>();
                parameter.Add(dataProvider.CreateParameter("@city", 30, city, DbType.String));
                parameter.Add(dataProvider.CreateParameter("@country", 30, country, DbType.String));
                dataReader = dataProvider.GetDataReader(query, CommandType.Text, out connection, parameter.ToArray());
                while (dataReader.Read())
                {
                    member.Add(new Member
                    {
                        MemberID = dataReader.GetInt32(0),
                        MemberName = dataReader.GetString(1),
                        Email = dataReader.GetString(2),
                        Password = dataReader.GetString(3),
                        City = dataReader.GetString(4),
                        Country = dataReader.GetString(5),
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return member;
        }
    }
}