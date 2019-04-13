using MyNursingFuture.BL.Entities;
using MyNursingFuture.DL;
using MyNursingFuture.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNursingFuture.BL.Managers
{
    public interface IFrameworkManager
    {
        Result Get();
        Result GetArticles();
        Result GetArticles(int id);
        Result FeedbackArticle(object model);
        Result ContactMessage(object model);
    }
    public class FrameworkManager : IFrameworkManager
    {
        public Result Get()
        {
            try
            {
                var con = new DapperConnectionManager();
                ///Sections

                var query = new QueryEntity();
                query.Query = @"SELECT s.SectionId, s.Name as SectionName, s.Title as SectionTitle,
                                   c.Name, c.Title, c.Type, c.ContentItemId, c.Position, c.Text, c.ButtonLink, c.Image, c.Carousel, c.TitleImage, c.Video, c.Link from Sections as s
                            LEFT JOIN ContentItems as c
                            on s.SectionId = c.SectionId
                            where s.Published = 1 and s.Active = 1
                            ORDER BY s.SectionId, c.Position ASC";
                query.Entity = new { };
                var result = con.ExecuteQuery(query);
                var sections = FormatSections((IEnumerable<dynamic>)result.Entity);


                //DOMAINS
                query.Query = @"SELECT d.DomainId, d.Name, d.Text, d.Image, d.Icon, d.Title, d.Framework, d.Position,
                                   a.ActionId, a.Title as ActionTitle, a.Text as ActionText, a.Type
                            FROM Domains as d
                            LEFT JOIN DomainsActions as da on d.DomainId = da.DomainId
                            LEFT JOIN Actions as a on da.ActionId = a.ActionId
                            WHERE d.Active = 1 ORDER BY d.Position ASC, d.Name, da.Position ASC";
                result = con.ExecuteQuery(query);
                var domains = FormatDomains((IEnumerable<dynamic>)result.Entity);

                //ASPECTS
                query.Query = @"SELECT d.DomainId, d.Name as DomainName, d.Framework,
                                   ap.AspectId, ap.Text, ap.Title, ap.Examples, ap.FurtherEducation,
                                   ap.Levels, ap.OnlineResources, ap.PeopleContact, ap.Position,
                                   a.ActionId, a.Title as ActionTitle, a.Text as ActionText, a.Type, aa.LevelAction
                            FROM Aspects as ap
                            LEFT JOIN Domains as d on ap.DomainId = d.DomainId
                            LEFT JOIN AspectsActions as aa on ap.AspectId = aa.AspectId
                            LEFT JOIN Actions as a on aa.ActionId = a.ActionId
                            WHERE ap.Active = 1 ORDER BY ap.Position ASC, ap.Title  ASC";
                result = con.ExecuteQuery(query);
                var aspects = FormatAspects((IEnumerable<dynamic>)result.Entity);


                //SECTORS
                query.Query = @"SELECT 
                                   s.SectorId, s.Title, s.Name, sv.Intro, sv.SectorViewId,
                                   sv.Video, sv.Type, sv.CareerOpportunities,
                                   sv.CareerPathways, sv.ContactText, sv.EducationOpportunities, sv.Type, sv.Framework,
                                   sv.MoreStories, sv.WorkEnvironments, sv.Active, sv.OnlineResources
                            FROM Sectors  as s
                            LEFT JOIN SectorViews as sv on s.SectorId = sv.SectorId
                            WHERE s.Active = 1 and s.Published = 1 ORDER BY s.Title  ASC";
                result = con.ExecuteQuery(query);
                var sectors = FormatSectors((IEnumerable<dynamic>)result.Entity);

                query.Query = @"SELECT * FROM Roles WHERE Active = 1 ORDER BY Name ASC";
                var resultRoles = con.ExecuteQuery<RoleEntity>(query);
                var roles = (IEnumerable<RoleEntity>)resultRoles.Entity;


                query.Query = @"SELECT * FROM PostCards";
                var resultPostCards = con.ExecuteQuery<PostCardEntity>(query);
                var postCards = (IEnumerable<PostCardEntity>)resultPostCards.Entity;

                query.Query = @"SELECT * FROM EndorsedLogos";
                var resultEndorsedLogos = con.ExecuteQuery<EndorsedLogoEntity>(query);
                var endorsedLogos = (IEnumerable<EndorsedLogoEntity>)resultEndorsedLogos.Entity;

                query.Query = @"SELECT * FROM Definitions";
                var resultDefinitions = con.ExecuteQuery<DefinitionEntity>(query);
                var definitions = (IEnumerable<DefinitionEntity>)resultDefinitions.Entity;

                query.Query = @"SELECT * FROM Actions";
                var resultActions = con.ExecuteQuery<ActionEntity>(query);
                var actions = (IEnumerable<ActionEntity>)resultActions.Entity;

                query.Query = @"SELECT * FROM Menus";
                var resultMenus = con.ExecuteQuery<MenuEntity>(query);
                var menus = (IEnumerable<MenuEntity>)resultMenus.Entity;

                //questions
                query.Query = @"SELECT DISTINCT
                                   q.QuestionId, q.QuizId, q.Type, q.AspectId, q.Text,
                                   q.SubText, q.Requirements, q.Position,
                                   q.Examples, a.AnswerId, a.Text as AnswerText, a.Value, a.MatchText, qu.Type as QuizType, usd.FieldName, a.Type as AnswerType, a.TextValue
                            FROM Questions  as q
                            LEFT JOIN Answers as a on q.QuestionId = a.QuestionId
                            LEFT JOIN Quizzes as qu on q.QuizId = qu.QuizId
                            LEFT JOIN UserDataQuestions as usd on q.QuestionId = usd.QuestionId
                            WHERE q.Active = 1 ORDER BY q.Position  ASC";
                result = con.ExecuteQuery(query);
                var questions = FormatQuestions((IEnumerable<dynamic>)result.Entity);


                //scoring sectors
                query.Query = @"SELECT sq.QuestionId, sq.SectorId, sq.Value, s.Name FROM SectorsQuestions as sq
                                JOIN Sectors as s on sq.SectorId = s.SectorId ";
                result = con.ExecuteQuery<SectorsQuestionsEntity>(query);
                var scoring = FormatSectorQuestions((IEnumerable<SectorsQuestionsEntity>)result.Entity);


                //scoring sectors
                query.Query = @"SELECT * from Reasons";
                result = con.ExecuteQuery<ReasonEntity>(query);
                var reasons = result.Entity;


                result.Entity = new { sections, domains, sectors, aspects, actions, roles, postCards, endorsedLogos, definitions, questions, menus, scoring, reasons };

                return result;
            }
            catch(Exception e)
            {
                Logger.Log(e);
                return new Result(false);
            }
            
        }
        

        public Result GetArticles()
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                ///Sections

                var query = new QueryEntity();
                query.Query = @"SELECT s.ArticleId, s.Name as ArticleName, s.Title as ArticleTitle, s.CategoryId, s.Date, cc.Name as CategoryName,
                                   c.Name, c.Title, c.Type, c.ContentItemId, c.Position, c.Text, c.ButtonLink, c.Image, c.Carousel, c.TitleImage, c.Video, c.Link from Articles as s
                            LEFT JOIN ContentItems as c
                            on s.ArticleId = c.ArticleId
                            LEFT JOIN Categories as cc
                            on s.CategoryId = cc.CategoryId
                            where s.Published = 1 and s.Active = 1
                            ORDER BY s.ArticleId, c.Position ASC";
                query.Entity = new { };
                result = con.ExecuteQuery(query);
                var articles = FormatArticles((IEnumerable<dynamic>)result.Entity);
                result.Entity = articles;
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Result(false);
            }
            return result ?? new Result(false);
        }

        public Result GetArticles(int id)
        {
            Result result = null;
            try
            {
                var con = new DapperConnectionManager();
                ///Sections

                var query = new QueryEntity();
                query.Query = @"SELECT s.ArticleId, s.Name as ArticleName, s.Title as ArticleTitle, s.CategoryId, s.Date, cc.Name as CategoryName,
                                   c.Name, c.Title, c.Type, c.ContentItemId, c.Position, c.Text, c.ButtonLink, c.Image, c.Carousel, c.TitleImage, c.Video, c.Link from Articles as s
                            LEFT JOIN ContentItems as c
                            on s.ArticleId = c.ArticleId
                            LEFT JOIN Categories as cc
                            on s.CategoryId = cc.CategoryId
                            where s.Published = 1 and s.ArticleId = @ArticleId
                            ORDER BY s.ArticleId, c.Position ASC";
                query.Entity = new { ArticleId = id};
                result = con.ExecuteQuery(query);
                var articles = FormatArticles((IEnumerable<dynamic>)result.Entity);
                result.Entity = articles.FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Result(false);
            }
            return result ?? new Result(false);
        }

        public Result FeedbackArticle(object body)
        {
            try
            {
                var feedBackemail = ConfigurationManager.AppSettings["configuration.email.feedback"];

                Task.Run(() => new EmailManager().SendEmail(feedBackemail, DL.Models.EmailType.Feedback, body));
                return new Result(true);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Result(false);
            }
            
        }

        private List<ArticleEntity> FormatArticles(IEnumerable<dynamic> sections)
        {
            var list = new List<ArticleEntity>();
            foreach (var item in sections)
            {
                ArticleEntity entity = list.FirstOrDefault(x => x.ArticleId == item.ArticleId);
                if (entity == null)
                {
                    entity = new ArticleEntity();
                    entity.ArticleId = item.ArticleId;
                    entity.Name = item.ArticleName;
                    entity.Title = item.ArticleTitle;
                    entity.CategoryId = item.CategoryId;
                    entity.CategoryName = item.CategoryName;
                    entity.Date = item.Date;
                    entity.DateFormatted = item.Date.ToString("d MMM yyyy");
                    entity.ContentItems = new List<ContentItemEntity>();
                    list.Add(entity);
                }
                if (item.ContentItemId == null)
                    continue;

                var contentItem = new ContentItemEntity
                {
                    Name = item.Name,
                    ContentItemId = item.ContentItemId,
                    Text = item.Text,
                    Position = item.Position,
                    Type = item.Type,
                    SectionId = item.SectionId,
                    Title = item.Title,
                    ButtonLink = item.ButtonLink,
                    Image = item.Image,
                    Carousel = item.Carousel,
                    TitleImage = item.TitleImage,
                    Video = item.Video,
                    Link = item.Link
                };

                entity.ContentItems.Add(contentItem);
            }

            return list;
        }


        private List<SectorsQuestionsResponseEntity> FormatSectorQuestions(IEnumerable<SectorsQuestionsEntity> questions)
        {
            var list = new List<SectorsQuestionsResponseEntity>();
            foreach (var item in questions)
            {
                SectorsQuestionsResponseEntity entity = list.FirstOrDefault(x => x.SectorId == item.SectorId);
                if (entity == null)
                {
                    entity = new SectorsQuestionsResponseEntity();
                    entity.SectorId = item.SectorId;
                    entity.Name = item.Name;
                    entity.IdealAnswers = new List<IdealAnswersResponseEntity>();
                    list.Add(entity);
                }

                var answerItem = new IdealAnswersResponseEntity()
                {
                    QuestionId = item.QuestionId,
                    Value = item.Value
                };

                entity.IdealAnswers.Add(answerItem);
            }

            return list;
        }


        private List<SectionEntity> FormatSections(IEnumerable<dynamic> sections)
        {
            var sectionsList = new List<SectionEntity>();
            foreach (var item in sections)
            {
                SectionEntity sectionEntity = sectionsList.FirstOrDefault(x => x.SectionId == item.SectionId);
                if (sectionEntity == null)
                {
                    sectionEntity = new SectionEntity();
                    sectionEntity.SectionId = item.SectionId;
                    sectionEntity.Name = item.SectionName;
                    sectionEntity.Title = item.SectionTitle;
                    sectionEntity.ContentItems = new List<ContentItemEntity>();
                    sectionsList.Add(sectionEntity);
                }
                if (item.ContentItemId == null)
                    continue;

                var contentItem = new ContentItemEntity
                {
                    Name = item.Name,
                    ContentItemId = item.ContentItemId,
                    Text = item.Text,
                    Position = item.Position,
                    Type = item.Type,
                    SectionId = item.SectionId,
                    Title = item.Title,
                    ButtonLink = item.ButtonLink,
                    Image = item.Image,
                    Carousel = item.Carousel,
                    TitleImage = item.TitleImage,
                    Video = item.Video,
                    Link = item.Link
                };

                sectionEntity.ContentItems.Add(contentItem);
            }

            return sectionsList;
        }


        private List<QuestionEntity> FormatQuestions(IEnumerable<dynamic> questions)
        {
            var questionsList = new List<QuestionEntity>();
            foreach (var item in questions)
            {
                QuestionEntity entity = questionsList.FirstOrDefault(x => x.QuestionId == item.QuestionId);
                if (entity == null)
                {
                    entity = new QuestionEntity();
                    entity.QuestionId = item.QuestionId;
                    entity.QuizId = item.QuizId;
                    entity.AspectId = item.AspectId;
                    entity.Answers = new List<AnswerEntity>();
                    entity.Text = item.Text;
                    entity.SubText = item.SubText;
                    entity.Requirements = item.Requirements;
                    entity.Position = item.Position;
                    entity.Examples = item.Examples;
                    entity.Type = item.Type;
                    entity.QuizType = item.QuizType;
                    entity.FieldName = item.FieldName;
                    questionsList.Add(entity);
                }
                if (item.AnswerId == null)
                    continue;

                var answerItem = new AnswerEntity
                {
                    Text = item.AnswerText,
                    AnswerId = item.AnswerId,
                    MatchText = item.MatchText,
                    QuestionId = item.QuestionId,
                    Value = item.Value,
                    Type = item.AnswerType,
                    TextValue = item.TextValue
                };

                entity.Answers.Add(answerItem);
            }

            return questionsList;
        }

        private List<DomainEntity> FormatDomains(IEnumerable<dynamic> domains)
        {
            var domainList = new List<DomainEntity>();
            foreach (var item in domains)
            {
                DomainEntity domainEntity = domainList.FirstOrDefault(x => x.DomainId == item.DomainId);
                if (domainEntity == null)
                {
                    domainEntity = new DomainEntity();
                    domainEntity.DomainId = item.DomainId;
                    domainEntity.Name = item.Name;
                    domainEntity.Title = item.Title;
                    domainEntity.Icon = item.Icon;
                    domainEntity.Image = item.Image;
                    domainEntity.Text = item.Text;
                    domainEntity.Framework = item.Framework;
                    domainEntity.ActionsList = new List<ActionEntity>();
                    domainEntity.Position = item.Position;
                    domainList.Add(domainEntity);
                }
                if (item.ActionId == null)
                    continue;

                var action = new ActionEntity
                {
                    Text = item.ActionText,
                    Title = item.ActionTitle,
                    ActionId = item.ActionId,
                    Type = item.Type,
                    Position = item.Position,
                    DomainId = item.DomainId
                };

                domainEntity.ActionsList.Add(action);
            }

            return domainList;
        }

        private List<AspectEntity> FormatAspects(IEnumerable<dynamic> aspects)
        {
            var aspectsList = new List<AspectEntity>();
            foreach (var item in aspects)
            {
                AspectEntity entity = aspectsList.FirstOrDefault(x => x.AspectId == item.AspectId);
                if (entity == null)
                {
                    entity = new AspectEntity();
                    entity.DomainId = item.DomainId;
                    entity.Text = item.Text;
                    entity.Title = item.Title;
                    entity.Framework = item.Framework;
                    entity.Examples = item.Examples;
                    entity.FurtherEducation = item.FurtherEducation;
                    entity.Levels = item.Levels;
                    entity.OnlineResources = item.OnlineResources;
                    entity.PeopleContact = item.PeopleContact;
                    entity.AspectId = item.AspectId;
                    entity.DomainName = item.DomainName;
                    entity.ActionsList = new List<ActionEntity>();
                    entity.Position = item.Position;
                    aspectsList.Add(entity);
                }
                if (item.ActionId == null)
                    continue;

                var action = new ActionEntity
                {
                    Text = item.ActionText,
                    Title = item.ActionTitle,
                    ActionId = item.ActionId,
                    Type = item.Type,
                    LevelAction = item.LevelAction, 
                    AspectId = item.AspectId
                };

                entity.ActionsList.Add(action);
            }

            return aspectsList;
        }

        private List<SectorEntity> FormatSectors(IEnumerable<dynamic> sectors)
        {
            var sectorsList = new List<SectorEntity>();
            foreach (var item in sectors)
            {
                SectorEntity entity = sectorsList.FirstOrDefault(x => x.SectorId == item.SectorId);
                if (entity == null)
                {
                    entity = new SectorEntity();
                    entity.SectorId = item.SectorId;
                    entity.Title = item.Title;
                    entity.Name = item.Name;
                    sectorsList.Add(entity);
                }
                if (item.SectorViewId == null)
                    continue;

                var view = new SectorViewEntity
                {
                    Intro = item.Intro,
                    SectorViewId = item.SectorViewId,
                    Video = item.Video,
                    Type = item.Type,
                    CareerOpportunities = item.CareerOpportunities,
                    CareerPathways = item.CareerPathways,
                    ContactText = item.ContactText,
                    EducationOpportunities = item.EducationOpportunities,
                    Framework = item.Framework,
                    MoreStories = item.MoreStories,
                    WorkEnvironments = item.WorkEnvironments,
                    SectorId = item.SectorId,
                    OnlineResources = item.OnlineResources,
                    Active = item.Active
                };
                if (item.Type == "EN")
                    entity.SectorEn = view;
                if (item.Type == "RN")
                    entity.SectorRn = view;
            }

            return sectorsList;
        }

        public Result ContactMessage(object model)
        {
            try
            {
                var email = ConfigurationManager.AppSettings["configuration.email.contact"];

                Task.Run(() => new EmailManager().SendEmail(email,DL.Models.EmailType.Contact, model));

                return new Result(true);
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return new Result(false);
            }
        }
    }
}
