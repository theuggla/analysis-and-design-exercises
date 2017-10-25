using System;
using System.Collections.Generic;
using System.Linq;

namespace MemberRegistry.model.searchcriteria
{
	class HasCanoeCriteria : ISearchCriteria
	{
        public IEnumerable<model.Member> MeetCriteria(IEnumerable<model.Member> memberList)
        {
            return memberList
                    .Where(x => x.Boats.Count > 0)
                    .Where(x => x.Boats.Where(boat => boat.Type == BoatType.Canoe).ToList().Count > 0)
                    .ToList();
        }

        public string GetDescription()
        {
            return "have a canoe";
        }
    }
}