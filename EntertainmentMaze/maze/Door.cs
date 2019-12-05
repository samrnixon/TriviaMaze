using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EntertainmentMaze.maze
{
    public class Door
    {
        private Question Question { get; }
        private bool IsLocked { get; }
        private string QuestionString { get; }
        private string AnswerString { get; }

        public Door()
        {
            Question = GetRandomQuestion();
            QuestionString = Question.CurrentQuestion;
            AnswerString = Question.Answer;
            IsLocked = false;
        }

        private Question GetRandomQuestion()
        {
            Random rnd = new Random();
            List<Question> listOfQuestions = DatabaseListRetrieval.ListOfQuestions;
            int r = rnd.Next(listOfQuestions.Count);
            Question question = listOfQuestions.ElementAt(r);

            return question;
        }

        public string DisplayQuestion()
        {
            return ($"{Question.CurrentQuestion.ToString()}");
        }

        public string DisplayAnswer()
        {
            return ($"{Question.Answer.ToString()}");
        }

    }
}
