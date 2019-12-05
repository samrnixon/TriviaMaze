using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EntertainmentMaze.maze
{
    public class Door
    {
        private Question _Question;
        private string questionString;
        private string answerString;
        private bool _IsLocked { get; }

        public Door()
        {
            _Question = GetRandomQuestion();
            questionString = _Question.question;
            answerString = _Question.answer;
            _IsLocked = false;
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
            return ($"{_Question.question.ToString()}");
        }

        public string DisplayAnswer()
        {
            return ($"{_Question.answer.ToString()}");
        }

    }
}
