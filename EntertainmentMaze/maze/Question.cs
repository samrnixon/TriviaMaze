using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace EntertainmentMaze.maze
{
    public class Question
    {
        private int QuestionID { get; }
        private int AnswerID { get; }
        private int TypeID { get; }
        public string CurrentQuestion { get; }
        public string Answer { get; }

        private enum QuestionTypes
        {
            TrueFalse = 0,
            ShortAnswer = 1,
            MultipleChoice = 2
        }

        public Question(int questionID, int answerID, int typeID, string question, string answer)
        {
            this.QuestionID = questionID;
            this.AnswerID = answerID;
            this.TypeID = typeID;
            this.CurrentQuestion = question ?? throw new ArgumentNullException(nameof(question));
            this.Answer = answer ?? throw new ArgumentNullException(nameof(answer));
        }

        private QuestionTypes GenerateRandomNumberForQuestionTypeValue()
        {
            Random random = new Random();
            int randomNumberInRange = random.Next(0, 3);
            switch(randomNumberInRange)
            {
                case 0:
                    return QuestionTypes.TrueFalse;
                case 1:
                    return QuestionTypes.ShortAnswer;
                case 2:
                    return QuestionTypes.MultipleChoice;
                default:
                    return QuestionTypes.TrueFalse;
            }
        }



    }
}
