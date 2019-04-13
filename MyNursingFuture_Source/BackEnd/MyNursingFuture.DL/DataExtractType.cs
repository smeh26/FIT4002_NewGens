using System.ComponentModel;

namespace MyNursingFuture.DL
{
    public enum DataExtractType
    {
        [Description("Self-assessment")]
        SelfAssessment = 1,
        [Description("Quiz details")]
        QuizDetails = 2,
        [Description("Quiz scores")]
        QuizScores = 3,
        [Description("Anon Self-assessment")]
        AnonSelfAssessment = 4,
        [Description("Anon Quiz details")]
        AnonQuizDetails = 5,
        [Description("Anon Quiz scores")]
        AnonQuizScores = 6
    }
}
