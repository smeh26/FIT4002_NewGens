using AutoMapper;
using MyNursingFuture.BL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNursingFuture.Cms.Models
{
    public class QuestionViewModel
    {
        public int QuestionId { get; set; }
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string Text { get; set; }
        public string SubText { get; set; }
        public string Type { get; set; }
        public string QuizType { get; set; }
        public int? AspectId { get; set; }
        [Required]
        public string Operation { get; set; }
        public List<AnswerEntity> Answers { get; set; }
        public List<SectorsQuestionsEntity> SectorsQuestions { get; set; }

        public string Requirements { get; set; }
        public string AnswersJson { get; set; }
        public string SectorsJson { get; set; }
        public string Examples { get; set; }
    }

    public class QuestionProfile: Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionViewModel, QuestionEntity>().ReverseMap();
        }
    }
}