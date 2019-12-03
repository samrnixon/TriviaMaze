using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class Question
    {
        private enum QuestionTypes
        {
            TrueFalse = 0,
            ShortAnswer = 1,
            MultipleChoice = 2
        }
        
        private Question _Question;
        private string _Answer;
        private QuestionTypes _Type;

        public Question(Question question)
        {
            _Type = GenerateRandomNumberForQuestionTypeValue();
            _Question = question;
            _Answer = "";

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
