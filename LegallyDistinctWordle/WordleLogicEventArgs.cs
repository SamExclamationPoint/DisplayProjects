using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubelSA2
{



    /// <summary>
    /// should somehow pass the answer from logic class to the board class so 
    /// we can use it there
    /// </summary>
    public class WordleLogicEventArgs : EventArgs
    {
        public string AnswerWord;


        // actual constructor to be used
       public  WordleLogicEventArgs(string answerWord)
        {
            this.AnswerWord = answerWord;
        }
    }
}
