using System.Collections.Generic;
using Nest;
using SkillSearch.Models;

namespace SkillSearch.Repository
{
    public class ElsRepo
    {
        private readonly ElasticClient _client;
        public ElsRepo()
        {
            var settings = new ConnectionSettings()
                .DefaultIndex("skills");
            _client = new ElasticClient(settings);
        }

        public IEnumerable<Skill> GetSkills(string input)
        {
            var inputWithWildcard = input + "*";
            var res = _client.Search<Skill>(s => s
                .Query(q => q
                    .QueryString(qs => qs
                        .Query(inputWithWildcard)
                    )
                ));
            return res.Documents;
        }
    }
}