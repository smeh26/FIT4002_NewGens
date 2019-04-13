using System;
using System.Collections.Generic;
using System.Linq;

namespace MyNursingFuture.BL.Managers
{
    public enum ConfigNames
    {
        APNATOKEN
    }

    public enum ContentItemsType {
        DEFAULT,
        NEWSLETTERSIGNUP,
        HEADING,
        SINGLELINELINK,
        SINGLEBUTTONLINK,
        SECTORSLIST,
        ROLESLIST,
        RENDERARTICLE,
        VIDEOEMBED,
        MARKUP,
        LINK,
        ACCORDION
    }

    public enum LinksTypes
    {
        SECTION,
        ARTICLE,
        SECTOR,
        ROLE,
        DOMAIN,
        ASPECT,
        QUIZ,
    }

    public enum ActionTypes
    {
        Environment,
        Education,
        Exposure,
        Experience
    }

    public enum QuizTypes
    {
        ASSESSMENT,
        PATHWAY,
        ABOUT
    }

    public enum QuestionTypes
    {
        SINGLE,
        CHOICE,
        MULTI,
        RANGE,
        INPUT
    }

    public enum UserDataFields
    {
        NurseType,
        Area,
        Address,
        ActiveWorking,
        Age,
        Patients,
        PatientsTitle,
        Qualification,
        Setting
    }

    public interface IEnumManager
    {
        List<string> GetContentItemList();
        List<string> GetActionsTypes();
        List<string> GetUserDataFields();
    }

    public class EnumManager : IEnumManager
    {
        public List<string> GetContentItemList()
        {
            return Enum.GetNames(typeof(ContentItemsType)).ToList();
        }

        public List<string> GetActionsTypes()
        {
            return Enum.GetNames(typeof(ActionTypes)).ToList();
        }

        public List<string> GetUserDataFields()
        {
            return Enum.GetNames(typeof(UserDataFields)).ToList();
        }
    }
}
