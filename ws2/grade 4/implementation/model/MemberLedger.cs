using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberRegistry.model
{
	class MemberLedger
	{
		private List<model.Member> _members;
		private persistance.IPersistance _persistance;

		public MemberLedger(persistance.IPersistance persistance)
		{
			this._persistance = persistance;
			this._members = LoadMemberList();
		}

		public void CreateMember(string name, string password, string personalNumber)
		{
			int newId = getNextMemberID();
	
			Member newMember = new Member(name, password, personalNumber, newId);
			this._members.Add(newMember);
		}

		public Member GetMember(int id)
		{
			try
			{
				return this._members
				.Where(x => x.MemberID == id)
				.ToList()[0];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new NoSuchMemberException();
			}
			
		}

		public void DeleteMember(model.Member member)
		{
			this._members
				.Remove(member);
		}

		public void UpdateMember(model.Member member, string newName, string newPersonalNumber)
		{
			member.UpdateMember(newName, newPersonalNumber);
		}

        public IEnumerable<Member> GetMembers()
		{
			return this._members;
		}

		public void RegisterBoat(model.Member member, BoatType boatType, int boatLength)
		{
			member.AddBoat(boatType, boatLength);
		}

		public void DeleteBoat(model.Member member, model.Boat boat)
		{
			member.RemoveBoat(boat);
		}

		public void UpdateBoat(model.Member member, model.Boat boat, BoatType type, int length)
		{
			member.UpdateBoat(boat, type, length);
		}

		public void LoginMember(model.Member member, string attemptedPassword)
		{
			member.LoginMember(attemptedPassword);
		}

		public void LogoutMembers()
		{
			List<Member> loggedInMembers = this._members
				.Where(member => member.IsLoggedIn)
				.ToList();

				foreach (Member member in loggedInMembers)
				{
					member.LogoutMember();
				}
		}
		
		public void SaveMemberList()
		{
			this._persistance.SaveMemberList(this._members);
		}

		public IEnumerable<model.Member> Search(model.searchcriteria.ISearchCriteria criteria)
		{
			return criteria.MeetCriteria(this._members);
		}

		private model.Member GetLoggedInMember()
		{
			model.Member member = this._members
				.Where(x => x.IsLoggedIn == true)
				.ToList()[0];

			return member;
		}

        private List<model.Member> LoadMemberList()
		{
			List<model.Member> memberList = this._persistance.RetrieveMemberList() ?? new List<model.Member>();
			return memberList;
		}

		private int getNextMemberID() {
			int next = this._members.Count > 0 ? this._members[this._members.Count - 1].MemberID + 1 : 1;
			return next;
		}
	}
}