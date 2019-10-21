using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System.Web;
using Newtonsoft.Json;
using System.Transactions;
using System.Configuration;

namespace MyNursingFuture.BL.Managers
{
    public interface ISelfAssessmentQuizzesManager
    {
        Result InsertQuizz(SelfAssessmentQuizzesEntity quizz);

    }

    public class SelfAssessmentQuizzesManager : ISelfAssessmentQuizzesManager
    {

        public Result InsertQuizz(SelfAssessmentQuizzesEntity quizz)
        {

            try
            {
                var con = new DapperConnectionManager();
                var query = new QueryEntity();
                query.Entity = quizz;
                var result = new Result();


                string queryString = @"
                                    BEGIN TRAN
                                                    IF EXISTS (SELECT * from [dbo].[SelfAssessmentQuizValue] WHERE 
                                                                                                                [UserId] = @UserId
                                                                                                            AND [QuizId] = @QuizId
                                                                                                            AND [DateVal] = @DateVal )
                                        BEGIN 
                                                UPDATE [dbo].[SelfAssessmentQuizValue]
                                                   SET [UserId] = @UserId
                                                      ,[QuizId] = @QuizId
                                                      ,[DateVal] = @DateVal
                                                      ,[Results] = @Results
                                                      ,[Completed] = @Completed
                                                      ,[Date] = @Date
                                                      ,[Type] = @Type
                                                      ,[Survey] = @Survey
                                                      ,[Aspect_1] = @Aspect_1
	                                                ,[Aspect_2] = @Aspect_2
	                                                ,[Aspect_3] = @Aspect_3
	                                                ,[Aspect_4] = @Aspect_4
	                                                ,[Aspect_5] = @Aspect_5
	                                                ,[Aspect_6] = @Aspect_6
	                                                ,[Aspect_7] = @Aspect_7
	                                                ,[Aspect_8] = @Aspect_8
	                                                ,[Aspect_9] = @Aspect_9
	                                                ,[Aspect_10] = @Aspect_10
	                                                ,[Aspect_11] = @Aspect_11
	                                                ,[Aspect_12] = @Aspect_12
	                                                ,[Aspect_13] = @Aspect_13
	                                                ,[Aspect_14] = @Aspect_14
	                                                ,[Aspect_15] = @Aspect_15
	                                                ,[Aspect_16] = @Aspect_16
	                                                ,[Aspect_17] = @Aspect_17
	                                                ,[Aspect_18] = @Aspect_18
	                                                ,[Aspect_19] = @Aspect_19
	                                                ,[Aspect_20] = @Aspect_20
	                                                ,[Aspect_21] = @Aspect_21
	                                                ,[Aspect_22] = @Aspect_22
	                                                ,[Aspect_23] = @Aspect_23
	                                                ,[Aspect_24] = @Aspect_24
	                                                ,[Aspect_25] = @Aspect_25
	                                                ,[Aspect_26] = @Aspect_26
	                                                ,[Aspect_27] = @Aspect_27
	                                                ,[Aspect_28] = @Aspect_28
	                                                ,[Aspect_29] = @Aspect_29
	                                                ,[Aspect_30] = @Aspect_30
	                                                ,[Aspect_31] = @Aspect_31
	                                                ,[Aspect_32] = @Aspect_32
	                                                ,[Aspect_33] = @Aspect_33
	                                                ,[Aspect_34] = @Aspect_34
	                                                ,[Aspect_35] = @Aspect_35
	                                                ,[Aspect_36] = @Aspect_36
	                                                ,[Aspect_37] = @Aspect_37
	                                                ,[Aspect_38] = @Aspect_38
	                                                ,[Aspect_39] = @Aspect_39
	                                                ,[Aspect_40] = @Aspect_40
	                                                ,[Aspect_41] = @Aspect_41
	                                                ,[Aspect_42] = @Aspect_42
	                                                ,[Aspect_43] = @Aspect_43
	                                                ,[Aspect_44] = @Aspect_44
	                                                ,[Aspect_45] = @Aspect_45
	                                                ,[Aspect_46] = @Aspect_46
	                                                ,[Aspect_47] = @Aspect_47
	                                                ,[Aspect_48] = @Aspect_48
	                                                ,[Aspect_49] = @Aspect_49
	                                                ,[Aspect_50] = @Aspect_50
	                                                ,[Aspect_51] = @Aspect_51
	                                                ,[Aspect_52] = @Aspect_52
	                                                ,[Aspect_53] = @Aspect_53
	                                                ,[Aspect_54] = @Aspect_54
	                                                ,[Aspect_55] = @Aspect_55
	                                                ,[Aspect_56] = @Aspect_56
	                                                ,[Aspect_57] = @Aspect_57
	                                                ,[Aspect_58] = @Aspect_58
	                                                ,[Aspect_59] = @Aspect_59
	                                                ,[Aspect_60] = @Aspect_60
	                                                ,[Aspect_61] = @Aspect_61
	                                                ,[Aspect_62] = @Aspect_62
	                                                ,[Aspect_63] = @Aspect_63
	                                                ,[Aspect_64] = @Aspect_64
	                                                ,[Aspect_65] = @Aspect_65
	                                                ,[Aspect_66] = @Aspect_66
	                                                ,[Aspect_67] = @Aspect_67
	                                                ,[Aspect_68] = @Aspect_68
	                                                ,[Aspect_69] = @Aspect_69
	                                                ,[Aspect_70] = @Aspect_70
	                                                ,[Aspect_71] = @Aspect_71
	                                                ,[Aspect_72] = @Aspect_72
	                                                ,[Aspect_73] = @Aspect_73
	                                                ,[Aspect_74] = @Aspect_74
	                                                ,[Aspect_75] = @Aspect_75
	                                                ,[Aspect_76] = @Aspect_76
	                                                ,[Aspect_77] = @Aspect_77
	                                                ,[Aspect_78] = @Aspect_78
	                                                ,[Aspect_79] = @Aspect_79
	                                                ,[Aspect_80] = @Aspect_80
                                                 WHERE  [UserId] = @UserId
                                                       AND [QuizId] = @QuizId
                                                      AND [DateVal] = @DateVal
                                        END 
                                        ELSE
                                        BEGIN
                                            INSERT INTO [dbo].[SelfAssessmentQuizValue]
                                                       ([UserId]
                                                       ,[QuizId]
                                                       ,[DateVal]
                                                       ,[Results]
                                                       ,[Completed]
                                                       ,[Date]
                                                       ,[Type]
                                                       ,[Survey]
                                                       ,[Aspect_1]
                                                       ,[Aspect_2]
                                                       ,[Aspect_3]
                                                       ,[Aspect_4]
                                                       ,[Aspect_5]
                                                       ,[Aspect_6]
                                                       ,[Aspect_7]
                                                       ,[Aspect_8]
                                                       ,[Aspect_9]
                                                       ,[Aspect_10]
                                                       ,[Aspect_11]
                                                       ,[Aspect_12]
                                                       ,[Aspect_13]
                                                       ,[Aspect_14]
                                                       ,[Aspect_15]
                                                       ,[Aspect_16]
                                                       ,[Aspect_17]
                                                       ,[Aspect_18]
                                                       ,[Aspect_19]
                                                       ,[Aspect_20]
                                                       ,[Aspect_21]
                                                       ,[Aspect_22]
                                                       ,[Aspect_23]
                                                       ,[Aspect_24]
                                                       ,[Aspect_25]
                                                       ,[Aspect_26]
                                                       ,[Aspect_27]
                                                       ,[Aspect_28]
                                                       ,[Aspect_29]
                                                       ,[Aspect_30]
                                                       ,[Aspect_31]
                                                       ,[Aspect_32]
                                                       ,[Aspect_33]
                                                       ,[Aspect_34]
                                                       ,[Aspect_35]
                                                       ,[Aspect_36]
                                                       ,[Aspect_37]
                                                       ,[Aspect_38]
                                                       ,[Aspect_39]
                                                       ,[Aspect_40]
                                                       ,[Aspect_41]
                                                       ,[Aspect_42]
                                                       ,[Aspect_43]
                                                       ,[Aspect_44]
                                                       ,[Aspect_45]
                                                       ,[Aspect_46]
                                                       ,[Aspect_47]
                                                       ,[Aspect_48]
                                                       ,[Aspect_49]
                                                       ,[Aspect_50]
                                                       ,[Aspect_51]
                                                       ,[Aspect_52]
                                                       ,[Aspect_53]
                                                       ,[Aspect_54]
                                                       ,[Aspect_55]
                                                       ,[Aspect_56]
                                                       ,[Aspect_57]
                                                       ,[Aspect_58]
                                                       ,[Aspect_59]
                                                       ,[Aspect_60]
                                                       ,[Aspect_61]
                                                       ,[Aspect_62]
                                                       ,[Aspect_63]
                                                       ,[Aspect_64]
                                                       ,[Aspect_65]
                                                       ,[Aspect_66]
                                                       ,[Aspect_67]
                                                       ,[Aspect_68]
                                                       ,[Aspect_69]
                                                       ,[Aspect_70]
                                                       ,[Aspect_71]
                                                       ,[Aspect_72]
                                                       ,[Aspect_73]
                                                       ,[Aspect_74]
                                                       ,[Aspect_75]
                                                       ,[Aspect_76]
                                                       ,[Aspect_77]
                                                       ,[Aspect_78]
                                                       ,[Aspect_79]
                                                       ,[Aspect_80])
                                            VALUES (
                                                        @UserId
                                                        ,@QuizId
                                                        ,@DateVal
                                                        ,@Results
                                                        ,@Completed
                                                        ,@Date
                                                        ,@Type
                                                        ,@Survey
                                                        ,@Aspect_1
                                                        ,@Aspect_2
                                                        ,@Aspect_3
                                                        ,@Aspect_4
                                                        ,@Aspect_5
                                                        ,@Aspect_6
                                                        ,@Aspect_7
                                                        ,@Aspect_8
                                                        ,@Aspect_9
                                                        ,@Aspect_10
                                                        ,@Aspect_11
                                                        ,@Aspect_12
                                                        ,@Aspect_13
                                                        ,@Aspect_14
                                                        ,@Aspect_15
                                                        ,@Aspect_16
                                                        ,@Aspect_17
                                                        ,@Aspect_18
                                                        ,@Aspect_19
                                                        ,@Aspect_20
                                                        ,@Aspect_21
                                                        ,@Aspect_22
                                                        ,@Aspect_23
                                                        ,@Aspect_24
                                                        ,@Aspect_25
                                                        ,@Aspect_26
                                                        ,@Aspect_27
                                                        ,@Aspect_28
                                                        ,@Aspect_29
                                                        ,@Aspect_30
                                                        ,@Aspect_31
                                                        ,@Aspect_32
                                                        ,@Aspect_33
                                                        ,@Aspect_34
                                                        ,@Aspect_35
                                                        ,@Aspect_36
                                                        ,@Aspect_37
                                                        ,@Aspect_38
                                                        ,@Aspect_39
                                                        ,@Aspect_40
                                                        ,@Aspect_41
                                                        ,@Aspect_42
                                                        ,@Aspect_43
                                                        ,@Aspect_44
                                                        ,@Aspect_45
                                                        ,@Aspect_46
                                                        ,@Aspect_47
                                                        ,@Aspect_48
                                                        ,@Aspect_49
                                                        ,@Aspect_50
                                                        ,@Aspect_51
                                                        ,@Aspect_52
                                                        ,@Aspect_53
                                                        ,@Aspect_54
                                                        ,@Aspect_55
                                                        ,@Aspect_56
                                                        ,@Aspect_57
                                                        ,@Aspect_58
                                                        ,@Aspect_59
                                                        ,@Aspect_60
                                                        ,@Aspect_61
                                                        ,@Aspect_62
                                                        ,@Aspect_63
                                                        ,@Aspect_64
                                                        ,@Aspect_65
                                                        ,@Aspect_66
                                                        ,@Aspect_67
                                                        ,@Aspect_68
                                                        ,@Aspect_69
                                                        ,@Aspect_70
                                                        ,@Aspect_71
                                                        ,@Aspect_72
                                                        ,@Aspect_73
                                                        ,@Aspect_74
                                                        ,@Aspect_75
                                                        ,@Aspect_76
                                                        ,@Aspect_77
                                                        ,@Aspect_78
                                                        ,@Aspect_79
                                                        ,@Aspect_80


                                    )
                                        END
                                     COMMIT TRAN


                                    ";
                query.Query = queryString;

                result = con.ExecuteQuery<UsersQuizzesEntity>(query);


                return result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            return new Result(false);
        }
    }
}

