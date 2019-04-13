USE [MyNursingFuture]
GO
/****** Object:  StoredProcedure [dbo].[GetAnonExtract]    Script Date: 10/07/2018 5:00:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetAnonExtract]
	-- Add the parameters for the stored procedure here
	@ExtractType int,
	@DateStart datetime,
	@DateEnd datetime
AS
BEGIN
	DECLARE @NurseTypeQuestionID INT
	SELECT @NurseTypeQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'NurseType'
	DECLARE @ActiveWorkingQuestionID INT
	SELECT @ActiveWorkingQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'ActiveWorking'
	DECLARE @AreaQuestionID INT
	SELECT @AreaQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Area'
	DECLARE @SettingQuestionID INT
	SELECT @SettingQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Setting'
	DECLARE @QualificationQuestionID INT
	SELECT @QualificationQuestionID = QuestionId FROM UserDataQuestions Where FieldName = 'Qualification'

	-- assessment
	IF (@ExtractType = 1)
	BEGIN
		SELECT 
			
			UserQuizId, 
			AssessmentDate = AnonUserQuizzes.Date,
			AnonUserQuizzes.Completed,
			DomainId = scores.[key],
			Domain = (SELECT Title FROM Domains WHERE DomainId = scores.[key]),
			
			
			[Level] = (CASE WHEN convert(float,scores.[value]) > 0.67 THEN 'ADVANCED' WHEN convert(float,scores.[value]) > 0.34 THEN 'INTERMEDIATE' WHEN convert(float,scores.[value]) > 0.1 THEN 'FOUNDATION' ELSE 'None' END),
			QuestionId = answers.[key],
			
			QuestionText = (SELECT Text From Questions WHERE QuestionId = answers.[key]),
			AnswerValue= Convert(float, answers.[value]),
			AnswerText = (SELECT [Text] FROM Answers a WHERE a.[QuestionId] = answers.[key] AND Convert(float, a.[Value]) = Convert(float, answers.[value])),
			UserName = Name,
			Email,  
			 
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postcode, State, 
			 patientsTitle, Qualification

		FROM 
			AnonUserQuizzes
			CROSS APPLY GetJSON(Results, '$.results.score') AS scores
			CROSS APPLY GetJSON(Results, '$.answers') AS answers
			INNER JOIN Aspects ON Aspects.DomainId = scores.[key]
			INNER JOIN Questions ON Questions.AspectId = Aspects.AspectId AND Questions.QuestionId = answers.[key]
		WHERE 
			AnonUserQuizzes.type = 'ASSESSMENT'
			AND results IS NOT NULL
			AND AnonUserQuizzes.DateVal > @DateStart
			AND AnonUserQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Aspects.DomainId,
			Questions.QuestionId

	END

	-- carrer quiz
	IF (@ExtractType = 2)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = AnonUserQuizzes.Date,
			QuestionId = [key],
			QuestionText = (SELECT Text From Questions WHERE QuestionId = [key]),
			AnswerValue= [value],

			UserName = AnonUserQuizzes.Name,
			Email, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postcode, State, 
			 patientsTitle, Qualification

		FROM 
			AnonUserQuizzes
			INNER JOIN Quizzes ON Quizzes.QuizId = AnonUserQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.answers')
		WHERE 
			AnonUserQuizzes.DateVal > @DateStart
			AND AnonUserQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			AnonUserQuizzes.Name,
			[key]
	END

	-- carrer quiz percentages
	IF (@ExtractType = 3)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = AnonUserQuizzes.Date,
			SectorId = [key],
			Sector = (SELECT Title FROM Sectors WHERE SectorId = scores.[key]),
			Percentage = [value],

			
			UserName = AnonUserQuizzes.Name,
			Email, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postcode, State, 
			patientsTitle, Qualification

		FROM 
			AnonUserQuizzes
			INNER JOIN Quizzes ON Quizzes.QuizId = AnonUserQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.results.scorePercentages') AS scores
		WHERE 
			AnonUserQuizzes.DateVal > @DateStart
			AND AnonUserQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			AnonUserQuizzes.Name,
			[value] DESC
			
	END
		
END

