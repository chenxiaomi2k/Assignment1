using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void CreateMember(string id, string name, string mail, Member m)
        {
            MemberDAO.Instance.AddMember(id, name, mail, m);
        }

        public void DeleteMember(int id)
        {
            MemberDAO.Instance.DeleteMember(id);
        }

        public List<Member> Filter(string city, string country)
        {
            return MemberDAO.Instance.Filter(city, country);
        }

        public List<string> GetCity()
        {
            return MemberDAO.Instance.GetCity();
        }

        public List<string> GetCountry()
        {
            return MemberDAO.Instance.GetCountry();
        }

        public Member GetMemberByID(int id)
        {
            return MemberDAO.Instance.GetMemberByID(id);
        }

        public IEnumerable<Member> GetMembers()
        {
            return MemberDAO.Instance.GetMembers();
        }

        public List<Member> GetMembersByName(string name)
        {
            return MemberDAO.Instance.GetMemberByName(name);
        }

        public Member Login(string email, string pass)
        {
            return MemberDAO.Instance.Login(email, pass);    
        }

        public List<Member> Search(string name)
        {
            return MemberDAO.Instance.Search(name);
        }

        public List<Member> SearchbyID(int id)
        {
            return MemberDAO.Instance.SearchbyID(id);
        }

        public List<Member> SortMembers()
        {
            return MemberDAO.Instance.SortMember();
        }

        public void UpdateMember(int id, Member member)
        {
            MemberDAO.Instance.UpdateMember(member, id);
        }
    }
}
