USE [MyNursingFuture]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 5/04/2018 3:50:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[EmailId] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](50) NOT NULL,
	[Title] [varchar](200) NULL,
	[Body] [text] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Emails] ON 

GO
INSERT [dbo].[Emails] ([EmailId], [Type], [Title], [Body]) VALUES (1, N'Welcome', N'Welcome to My Nursing Future', N'<p style="position:absolute;top:0;left: 0;right: 0;">
    <img src="https://mynursingfuture.com.au/assets/email/top-header.png" alt="">
</p>
<p style="width:100%; text-align:center; padding-top:40px; padding-bottom: 40px; background-image:url(''https://mynursingfuture.com.au/assets/email/Group.png''); background-size: 100%; background-repeat: no-repeat">
    <img src="https://mynursingfuture.com.au/assets/email/titlemnf.png" style="max-width:450px" alt="">
</p>
<h3 style="text-align: left;">Welcome to My Nursing Future!</h3>

<img src="https://mynursingfuture.com.au/assets/email/separator.png" alt="" style="
    height: 8px
">
<p><strong>APNA are thrilled that you have decided to explore the world of primary health care nursing. My Nursing Future will provide you with guidance, knowledge and helpful contacts and resources to grow your primary health care nursing career.</strong>
    <br>
</p>
<h2></h2>
<h3>Here’s a quick guide to help you get started!</h3>
<h2><br><a href="@Model.SiteUrl/sections/2" target="_blank">Career Quiz</a></h2>
<p>Ever wanted to know where you could excel in the world of primary health care nursing? Take our career quiz. This matches your career aspirations to suitable primary health care settings. A short 5 minute quiz that will open your mind to the possibilities of nursing in primary health care.
    <br>
</p>
<h2><a href="@Model.SiteUrl/sections/3" target="_blank">Self-assesment</a></h2>
<p>For those nurses out there already working in primary health care, have you ever felt under-valued? Under-utilised? Not sure where to start when planning your CPD? Well we have the answer for you! Take the self-assessment and discover your strengths and areas to grow in relation to your practice. Receive a personalised report that provides you with a breakdown of your strengths, and provides suggestions on new approaches to continue your professional growth. Use this report to plan your CPD, demonstrate to your employer your worth or even to help you understand how crucial your skills and knowledge are to the team. You will also receive a CPD certificate for undertaking this professional reflective activity.</p>
<h2><a href="@Model.SiteUrl/sectors" target="_blank">Sectors in Primary Health Care</a></h2>
<p>Discover the different areas of primary health care nursing by clicking away on the sector profile pages. See what it is like to work in correctional health, general practice, community health, refugee health, residential care, Aboriginal Medical Health Services and many more by viewing the primary health care nursing videos. You will also have access to the necessary resources and contacts on how to get involved in these areas of primary health care, and you can also view the latest career opportunities.</p>
<h2><a href="@Model.SiteUrl/sections/9" target="_blank">Career Quiz</a></h2>
<p>Filled with a number of different ideas and templates on how to develop your CV, expand your professional network, promoting your worth to your employer, working in a team environment, where to access further education and many more!</p>
<p>Get started and grow your career in primary health care nursing.</p>
<p style="text-align: left;"><em>The APNA Team!</em></p>
<br>
<p style="text-align: center;">
    <a target="_blank" href="@Model.SiteUrl"><img src="https://mynursingfuture.com.au//assets/email/getstarted.png" alt="" style="background-color: initial;"></a>
</p>
<br>
<p style="text-align: center;">A nursing workforce initiative proudly developed by the Australian Primary Health Care Nurses Association (APNA).
    <br>
</p>
<p style="text-align: center; ">Funded by the Australian Government Department of Health under the Nursing in Primary Health Care Program.
    <br>
</p>
<h2></h2>')
GO
INSERT [dbo].[Emails] ([EmailId], [Type], [Title], [Body]) VALUES (18, N'Report', N'My Nursing Future - @Model.UserName''s Report', N'<p style="position:absolute;top:0;left: 0;right: 0;">
    <img src="https://mynursingfuture.com.au/assets/email/top-header.png" alt="">
</p>
<p style="width:100%; text-align:center; padding-top:40px; padding-bottom: 40px; background-image:url(''https://mynursingfuture.com.au/assets/email/Group.png''); background-size: 100%; background-repeat: no-repeat">
    <img src="https://mynursingfuture.com.au/assets/email/titlemnf.png" style="max-width:450px" alt=""></p>

<h1>Report Completed!</h1>
<img src="https://mynursingfuture.com.au/assets/email/separator.png" alt="" style="height: 8px;"><br><br>
<p style="text-align: center;">A nursing workforce initiative proudly developed by the Australian Primary Health Care Nurses Association (APNA).
    <br>
</p>
<p style="text-align: center; ">Funded by the Australian Government Department of Health under the Nursing in Primary Health Care Program.
    <br>
</p>
<h2></h2>')
GO
INSERT [dbo].[Emails] ([EmailId], [Type], [Title], [Body]) VALUES (19, N'Feedback', N'My Nursing Future - Feedback', N'<p style="position:absolute;top:0;left: 0;right: 0;">
    <img src="https://mynursingfuture.com.au/assets/email/top-header.png" alt="">
</p>
<p style="width:100%; text-align:center; padding-top:40px; padding-bottom: 40px; background-image:url(''https://mynursingfuture.com.au/assets/email/Group.png''); background-size: 100%; background-repeat: no-repeat">
    <img src="https://mynursingfuture.com.au/assets/email/titlemnf.png" style="max-width:450px" alt="">
</p>
            <h2>My Nursing Future Article Feedback</h2>

<img src="https://mynursingfuture.com.au/assets/email/separator.png" alt="" style="
    height: 8px
">

            <br>
			<h3>Article Title</h3>
			<p>@Model.Title</p>
			<h3>Article Id</h3>
			<p>@Model.ArticleId</p>
			<h3>Message</h3>
			<p>@Model.Feedback</p>
			<h3>Positive</h3>
			<p>@Model.Positive</p>

<br>
<p style="text-align: center;">A nursing workforce initiative proudly developed by the Australian Primary Health Care Nurses Association (APNA).
    <br>
</p>
<p style="text-align: center; ">Funded by the Australian Government Department of Health under the Nursing in Primary Health Care Program.
    <br>
</p>
<h2></h2>')
GO
INSERT [dbo].[Emails] ([EmailId], [Type], [Title], [Body]) VALUES (20, N'Contact', N'My Nursing Future - Contact', N'<p style="position:absolute;top:0;left: 0;right: 0;">
    <img src="@Model.AssetUrl/email/top-header.png" alt="">
</p>
<p style="width:100%; text-align:center; padding-top:40px; padding-bottom: 40px; background-image:url(''@Model.AssetUrl/email/Group.png''); background-size: 100%; background-repeat: no-repeat">
    <img src="@Model.AssetUrl/assets/email/titlemnf.png" style="max-width:450px" alt="">
</p>
            <h2>My Nursing Future Contact Message</h2>

<img src="@Model.AssetUrl/assets/email/separator.png" alt="" style="
    height: 8px
">

          <br>
			<h2>Name</h2>
			<p>@Model.Name</p>
			<h2>Email</h2>
			<p>@Model.Email</p>
			<h2>Phone</h2>
			<p>@Model.Phone</p>
			<h2>Message</h2>
			<p>@Model.Message</p>

<br>
<p style="text-align: center;">A nursing workforce initiative proudly developed by the Australian Primary Health Care Nurses Association (APNA).
    <br>
</p>
<p style="text-align: center; ">Funded by the Australian Government Department of Health under the Nursing in Primary Health Care Program.
    <br>
</p>
<h2></h2>')
GO
INSERT [dbo].[Emails] ([EmailId], [Type], [Title], [Body]) VALUES (21, N'RecoverPassword', N'My Nursing Future - Password Recovery', N'<p style="position:absolute;top:0;left: 0;right: 0;">
    <img src="https://mynursingfuture.com.au/assets/email/top-header.png" alt="">
</p>
<p style="width:100%; text-align:center; padding-top:40px; padding-bottom: 40px; background-image:url(''https://mynursingfuture.com.au/assets/email/Group.png''); background-size: 100%; background-repeat: no-repeat">
    <img src="https://mynursingfuture.com.au/assets/email/titlemnf.png" style="max-width:450px" alt=""></p>

<h1>Reset Password Requested</h1>
<img src="https://mynursingfuture.com.au/assets/email/separator.png" alt="" style="height: 8px;"><br><br>
<p>
                Someone requested to reset your My Nursing Future account password. If it wasn''t you, please ignore this e-mail and no changes will be made to your account. However, if you have requested to reset your password, please click the link below. You will be redirected to the My Nursing Future password reset form.
            </p>
            <h3 style="text-align: center;text-decoration: underline;"><a href="@Model.WebsiteUrl?resetToken=@Model.Token">Reset Password</a></h3><br>
<p style="text-align: center;">A nursing workforce initiative proudly developed by the Australian Primary Health Care Nurses Association (APNA).
    <br>
</p>
<p style="text-align: center; ">Funded by the Australian Government Department of Health under the Nursing in Primary Health Care Program.
    <br>
</p>
<h2></h2>')
GO
SET IDENTITY_INSERT [dbo].[Emails] OFF
GO
/****** Object:  StoredProcedure [dbo].[GetExtract]    Script Date: 5/04/2018 3:50:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetExtract]
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
			AssessmentDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			DomainId = scores.[key],
			Domain = (SELECT Title FROM Domains WHERE DomainId = scores.[key]),
			
			
			[Level] = (CASE WHEN convert(float,scores.[value]) > 0.67 THEN 'ADVANCED' WHEN convert(float,scores.[value]) > 0.34 THEN 'INTERMEDIATE' WHEN convert(float,scores.[value]) > 0.1 THEN 'FOUNDATION' ELSE 'None' END),
			QuestionId = answers.[key],
			
			QuestionText = (SELECT Text From Questions WHERE QuestionId = answers.[key]),
			AnswerValue= Convert(float, answers.[value]),
			AnswerText = (SELECT [Text] FROM Answers a WHERE a.[QuestionId] = answers.[key] AND Convert(float, a.[Value]) = Convert(float, answers.[value])),
			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			 
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			CROSS APPLY GetJSON(Results, '$.results.score') AS scores
			CROSS APPLY GetJSON(Results, '$.answers') AS answers
			INNER JOIN Aspects ON Aspects.DomainId = scores.[key]
			INNER JOIN Questions ON Questions.AspectId = Aspects.AspectId AND Questions.QuestionId = answers.[key]
		WHERE 
			UsersQuizzes.type = 'ASSESSMENT'
			AND results IS NOT NULL
			AND UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			Aspects.DomainId,
			Questions.QuestionId

	END

	-- carrer quiz
	IF (@ExtractType = 2)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			QuestionId = [key],
			QuestionText = (SELECT Text From Questions WHERE QuestionId = [key]),
			AnswerValue= [value],

			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			INNER JOIN Quizzes ON Quizzes.QuizId = UsersQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.answers')
		WHERE 
			UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			[key]
	END

	-- carrer quiz percentages
	IF (@ExtractType = 3)
	BEGIN
		SELECT 
			UserQuizId, 
			QuizDate = UsersQuizzes.Date,
			UsersQuizzes.Completed,
			SectorId = [key],
			Sector = (SELECT Title FROM Sectors WHERE SectorId = scores.[key]),
			Percentage = [value],

			Users.UserId, 
			Users.ApnaMemberId,
			UserName = Users.Name,
			Email, 
			UserCreateDate = Users.CreateDate, 
			
			NurseType, 
			NurseTypeText = (SELECT [Text] FROM Answers WHERE QuestionId = @NurseTypeQuestionID AND Convert(float, [Value]) = Convert(float, NurseType)),
			ActiveWorking, 
			ActiveWorkingText = (SELECT [Text] FROM Answers WHERE QuestionId = @ActiveWorkingQuestionID AND Convert(float, [Value]) = Convert(varchar, ActiveWorking)),
			Area, 
			AreaText = (SELECT [Text] FROM Answers WHERE QuestionId = @AreaQuestionID AND Convert(float, [Value]) = Convert(varchar, Area)),
			Setting, 
			SettingText = (SELECT [Text] FROM Answers WHERE QuestionId = @SettingQuestionID AND Convert(float, [Value]) = Convert(varchar, Setting)),
			Age, Country, Suburb, Postalcode, State, 
			Patients, patientsTitle, Qualification

		FROM 
			Users INNER JOIN UsersQuizzes ON Users.UserId = UsersQuizzes.UserId
			INNER JOIN Quizzes ON Quizzes.QuizId = UsersQuizzes.QuizId AND Quizzes.Type='PATHWAY'
			CROSS APPLY GetJSON(Results, '$.results.scorePercentages') AS scores
		WHERE 
			UsersQuizzes.DateVal > @DateStart
			AND UsersQuizzes.DateVal < @DateEnd
		ORDER BY
			UserQuizId,
			Users.UserId,
			[value] DESC
			
	END
		
END

GO
