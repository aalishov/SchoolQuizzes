namespace SchoolQuizzes.Web.ViewModels.Ratings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PostRatingInputModel
    {
        public int QuestionId { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }
    }
}
