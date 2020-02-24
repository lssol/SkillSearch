using System.Collections;
using System.Collections.Generic;

namespace SkillSearch.Models
{
    public class Document
    {
        public Skill Doc { get; set; }
    }
    public class Skill
    {
        public string Label { get; set; }
        public long Occurence { get; set; }
        public IEnumerable<string> Alias { get; set; }
//        public string Aliases { get; set; }
    }
}