using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public class RawQuizDataEntity: IEntity
    {
        public string name { get; set; }
        public string email { get; set; }
        public string date { get; set; }     
        public Boolean? saveOnly { get; set; }    
        public Dictionary<string, string> domainNames { get; set; }
        public List<PostCardEntity> postcards { get; set; }
        public Dictionary<string, List<RawAspect>> aspects { get; set; }
        public Dictionary<string, float> domainScores { get; set; }
        public Dictionary<string, List<int>> domainStrengths { get; set; }
        public Dictionary<string, List<ActionEntity>> topActions { get; set; }
        public Dictionary<string, object> aboutYouAnswers { get; set; }
        public SelfAssessmentResults selfAssessmentResults { get; set; }

    }
    public class RawAspect : IEntity
    {
        public int aspectId { get; set; }
        public float answer { get; set; }
        public string answerText { get; set; }
        public string name { get; set; }
        public string definition { get; set; }
        public List<ActionEntity> actionsToGrow { get; set; }
    }

    public class SelfAssessmentResults : IEntity
    {
        public Dictionary<string, float> score { get; set; }
        public DateTime date { get; set; }
        public int id { get; set; }
        public string framework { get; set; }
        public Dictionary<string, List<int>> actions {get;set;}
    }

    public class AnonQuizResults : IEntity
    {
        public Dictionary<string, float> answers { get; set; }
        public SelfAssessmentResults results { get; set; }
    }

    public class RawCareerQuizDataEntity : IEntity
    {      
        public Dictionary<string, string> answers { get; set; }
        public QuizResults results { get; set; }
        public Dictionary<string, object> aboutYouAnswers { get; set; }
    }

    public class QuizResults : IEntity
    {
        public Dictionary<string, float> score { get; set; }
        public Dictionary<string, List<string>> scorePositives { get; set; }
        public Dictionary<string, int> scorePercentages { get; set; }
        public string date { get; set; }
    }

    public class CareerResults : IEntity
    {
        public Dictionary<string, string> answers { get; set; }
        public QuizResults results { get; set; }
    }

}
