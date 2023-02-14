using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<Member> GetMembers();
        void CreateMember(string id, string name, string mail, Member m);
        void UpdateMember(int id, Member member);
        void DeleteMember(int id);
        Member GetMemberByID(int id);
        List<Member> GetMembersByName(string name);
        List<Member> SortMembers();
        Member Login(string email, string pass);
        List<string> GetCity();
        List<string> GetCountry();
        List<Member> Search(string name);
        List<Member> SearchbyID(int id);
        List<Member> Filter(string city, string country);
    }
}
