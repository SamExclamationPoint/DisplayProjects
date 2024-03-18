using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubelSA2
{
    public class LegallyDistinctWordleLogic
    {
        // event handler
        public event EventHandler<WordleLogicEventArgs> AnswerWordPicked;

        // will be reading all the words from the file into a list,
        // and then randomly picking one
        public string answerWord = "";
        private List<string> wordListFromFile = new List<string>();
        StreamReader answerKeyFile = new StreamReader("wordle-answers-alphabetical.txt");


        /// <summary>
        /// call our event
        /// </summary>
        /// <param name="answerWord"></param> 
        private void OnAnswerWordPicked(string answerWord)
        {
            AnswerWordPicked?.Invoke(this, new WordleLogicEventArgs(answerWord));
        }


        /// <summary>
        /// loop thru file and add all words to list of words
        /// </summary>
        public void StoreWordsInList()
        {
            while (!answerKeyFile.EndOfStream) 
            {
                wordListFromFile.Add(answerKeyFile.ReadLine());
            }


           
        }




        /// <summary>
        /// Will pick a word from our list of 
        /// words read in from file
        /// </summary>
        public void PickWord()
        { 
            StoreWordsInList();
            Random random = new Random();
            int randWord = random.Next(wordListFromFile.Count);
            answerWord = wordListFromFile[randWord];
            OnAnswerWordPicked(answerWord);       

        }

        /// <summary>
        /// will check if the word taken from textboxes is a validword from answer key
        /// used in form file to determine if the guess box should appear
        /// </summary>
        /// <param name="word"></param> word from text boxes
        /// <returns></returns>
        public bool wordIsValid(string word)
        {
            bool isValid = false;
            for (int i = 0; i < wordListFromFile.Count; i++)
            {
                if (word == wordListFromFile[i])
                {
                    isValid = true;
                    break;
                }
            }

            return isValid;
        }

        








    }
}
