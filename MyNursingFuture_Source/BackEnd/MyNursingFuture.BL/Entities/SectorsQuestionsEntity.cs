using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Entities
{
    public class SectorsQuestionsEntity
    {
        public int QuestionId { get; set; }
        public int SectorId { get; set; }
        public string Name { get; set; }
        public decimal? Value { get; set; }
    }

    public class SectorsQuestionsResponseEntity
    {
        public int SectorId { get; set; }
        public string Name { get; set; }
        public List<IdealAnswersResponseEntity> IdealAnswers { get; set; }             
    }

    public class IdealAnswersResponseEntity
    {
        public int QuestionId { get; set; }
        public decimal? Value { get; set; }
    }
}
