using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Nest;
using Newtonsoft.Json;
using SkillSearch.Models;

namespace SkillSearch.Repository
{
    public enum Index
    {
        Smart,
        Dedup,
        Wiki
    }

    public class ElsRepo
    {
        public static string IndexName(Index index) => index switch
        {
            Index.Smart => "smartskills",
            Index.Dedup => "skillsclusters",
            Index.Wiki  => "refskills"
        };

        private readonly ElasticClient _client;

        public ElsRepo()
        {
            var settings = new ConnectionSettings(new Uri("https://elasticsearch.mantulab.com:9200/"));
            settings.EnableDebugMode();
            _client = new ElasticClient(settings);
        }

        public IEnumerable<Skill> GetSkills(string input, Index index)
        {
            var inputWithWildcard = input;
            var res = _client.Search<dynamic>(s => s
                .Index(IndexName(index))
                .Size(30)
                .Query(q => q.SimpleQueryString(qs => qs.Query(input).Fields("doc.Label"))));
//                .Query(q => 
//                    q.Prefix(qs => qs.Field("doc.Label").Value(input).Boost(3)) 
//                    || q.Prefix(p => p.Field("doc.Alias").Value(input))
//                    && q.FunctionScore(f => f.Functions(fun => fun.Gauss(g => g.Field("Occurence").Decay())))
//                ));

            var json = JsonConvert.SerializeObject(res.Documents);
            var documents = JsonConvert.DeserializeObject<IEnumerable<Document>>(json);
            var skills = documents.Select(d => d.Doc);
            return skills;
        }
    }
}