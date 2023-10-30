using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viktorina26042022.Database_Clases
{
    public class QuestionsDB
    {
        private Viktorina26042022Entities question;

        public QuestionsDB()
        {
            question = new Viktorina26042022Entities();
            if(question.QuestionSet.Count() == 0)
            {
                InsertData();
                addPhotoQuest();
            }
            else
            {

            }
        }
        public static byte[] GetPhoto(string filePath)
        {
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
        private void addPhotoQuest()
        {
            byte[] photo = GetPhoto("C:\\Users\\chuwi\\Desktop\\photo.png");
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "К какой функции относиться график?",
                Answer1 = "y=x",
                Answer2 = "y=x+12",
                Answer3 = "y=x^3",
                AnswerTrue = "y=x^2",
                Img = photo,
            });
            question.SaveChanges();
        }

        private void InsertData()
        {
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "Сколько будет 1+2*0?",
                Answer1 = "1",
                Answer2 = "2",
                Answer3 = "3",
                AnswerTrue = "0",
            });
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "Сколько будет 2+2*2?",
                Answer1 = "Ничему",
                Answer2 = "2",
                Answer3 = "4",
                AnswerTrue = "6",
            });
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "Сколько будет ln(e*e)?",
                Answer1 = "1",
                Answer2 = "Ничему",
                Answer3 = "0",
                AnswerTrue = "3",
            });
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "Чему равен х? если: (36*х)^2+(132+x)^2=0",
                Answer1 = "1",
                Answer2 = "2",
                Answer3 = "3",
                AnswerTrue = "Ничему",
            });
            question.QuestionSet.Add(new QuestionSet
            {
                QuestionText = "Чему равен х? если: x^2-6x+5=0",
                Answer1 = "5",
                Answer2 = "2",
                Answer3 = "3",
                AnswerTrue = "1",
                CountOfAnsvers = 2,
            });
            question.SaveChanges();
        }
        public short? getCountOfAnsvers(int numOfQuest)
        {
            int i = 0;
            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.CountOfAnsvers;
                i++;
            }
            return 1;
        }
        public byte[] getImg(int numOfQuest)
        {
            int i = 0;
            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.Img;
                i++;
            }
            return null;
        }
        public string getQuestion(int numOfQuest)
        {
            int i = 0;
            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.QuestionText;
                i++;                
            }
            return "Eror";
        }

        internal void ChangeQuestion(int currentQuestion, QuestionSet questionSet)
        {
            int i = 0;

            foreach (var item in question.QuestionSet)
            {
                if (i == currentQuestion)
                {
                    item.QuestionText = questionSet.QuestionText;
                    item.Answer1 = questionSet.Answer1;
                    item.Answer2 = questionSet.Answer2;
                    item.Answer3 = questionSet.Answer3;
                    item.AnswerTrue = questionSet.AnswerTrue;
                    item.CountOfAnsvers = questionSet.CountOfAnsvers;
                }
                i++;
            }
            question.SaveChanges();
            return;
        }

        public string getAnswer1(int numOfQuest)
        {
            int i = 0;

            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.Answer1;
                i++;
            }
            return "Eror";
        }
        public string getAnswer2(int numOfQuest)
        {
            int i = 0;

            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.Answer2;
                i++;
            }
            return "Eror";
        }
        public string getAnswer3(int numOfQuest)
        {
            int i = 0;

            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.Answer3;
                i++;
            }
            return "Eror";
        }
        public string getAnswerTrue(int numOfQuest)
        {
            int i = 0;

            foreach (var item in question.QuestionSet)
            {
                if (i == numOfQuest) return item.AnswerTrue;
                i++;
            }
            return "Eror";
        }
        public int getCountOfQuestions()
        {
            return question.QuestionSet.Count();
        }
    }
}
