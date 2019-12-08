﻿using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EntertainmentMaze.maze
{
    [Serializable]
    public class Door
    {
        private Question Question { get; }
        private bool IsLocked { get; set; }
        private bool IsOpened { get; set; }
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

        public bool GetDoorStatus()
        {
            return IsLocked;
        }
        internal bool GetDoorOpenedStatus()
        {
            return IsOpened;
        }

        public void OpenDoor()
        {
            IsOpened = true;
        }

        public void LockDoor()
        {
            IsLocked = true;
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
