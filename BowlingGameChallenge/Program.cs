using System;
using System.Collections.Generic;

namespace BowlingGameChallenge
{
    class Program
    {
        public static void OtherMain()//string[] args
        {
            //Console.WriteLine("Enter the scores of the game");
            String game_scores = "Strike, 7, Spare, 9, Miss, Strike, Miss, 8, 8, Spare, Miss, 6, Strike, Strike, Strike, 8, 1";//Console.ReadLine();
            /*
             Solution Tested for following test cases
             6, 1, 9, Miss, 8, Spare, 5, Spare, 8, Miss, 6, 2, 9, Spare, 7, 2, 8, Spare, 9, Spare
             Strike, Strike, 7, Spare, 8, Spare, Strike, 9, Spare, Strike, Strike, Strike, Strike, 7,Spare
             Strike, 7, Spare, 9, Miss, Strike, Miss, 8, 8, Spare, Miss, 6, Strike, Strike, Strike, 8, 1
             Strike, Strike, Strike, Strike, Strike, Strike, Strike, Strike, Strike, Strike, Strike, Strike
             
             */
            String[] ScoreArray = game_scores.Split(",");
            int size = ScoreArray.Length;
            int[] frameArray = new int[size];
            frameArray[0] = ScoreArray[0].Equals("Strike") ? 10 : ScoreArray[0].Equals("Miss") ? 0 : int.Parse(ScoreArray[0].Trim()); // Initializing the first element
            int previousScore = frameArray[0];
            for (int a = 1; a < ScoreArray.Length; a++)
            {

                if (ScoreArray[a].Trim().Equals("Strike"))
                {
                    frameArray[a] = 10;
                }
                else if (ScoreArray[a].Trim().Equals("Miss"))
                {
                    frameArray[a] = (int)ScoreState.Miss;
                }
                else if (ScoreArray[a].Trim().Equals("Spare"))
                {
                    frameArray[a] = 10 - previousScore;
                }
                else
                {
                    frameArray[a] = int.Parse(ScoreArray[a]);
                }

                previousScore = frameArray[a];

            }

            TempFrame[] array = new TempFrame[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = new TempFrame(frameArray[i]);
                if (ScoreArray[i].Trim().Equals("Strike"))
                    array[i].isStrike = true;
                else if (ScoreArray[i].Trim().Equals("Spare"))
                    array[i].isSpare = true;
                else if (ScoreArray[i].Trim().Equals("Miss"))
                    array[i].isMiss = true;
            }

            List<TempFrame[]> scoreboard = new List<TempFrame[]>();

            int k = 0;
            for (int i = 0; i < 9; i++)
            {
                scoreboard.Add(SetFramesForBoard(ScoreArray, array, 2, ref k));
            }

            // For the tenth frame
            if (array[k].isStrike)
            {
                scoreboard.Add(SetFramesForBoard(ScoreArray, array, 3, ref k));
            }
            else
            {
                scoreboard.Add(SetFramesForBoard(ScoreArray, array, 2, ref k));
            }

            int[] total = new int[10];
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {

                TempFrame[] f = scoreboard[i];

                if (f[0].isStrike)
                {
                    int p = 0;
                    int b = i;
                    if (scoreboard[b + 1][0].isStrike)
                    {
                        while (b < 9 && scoreboard[b + 1][0].isStrike && p < 2)
                        {
                            sum += 10;
                            ++p; b++;
                        }
                        if (p == 1 && b == 9)
                        {
                            sum += scoreboard[b][1].FrameValue;
                        }
                        if (p < 2 && b != 9)
                        {
                            sum += scoreboard[b + 1][0].FrameValue;
                        }

                    }
                    else
                        sum += Sum_of_Frame(scoreboard[i + 1]);
                }
                if (f[1].isSpare)
                {
                    sum += scoreboard[i + 1][0].FrameValue;
                }
                sum += Sum_of_Frame(f);
                total[i] = sum;
            }
            total[9] = sum + Sum_of_Frame(scoreboard[9]);

            int ap = 0;
        }

        public static int Sum_of_Frame(TempFrame[] array)
        {
            int sum = 0;
            Array.ForEach(array, i => sum += i.FrameValue);
            return sum;
        
        }


        public static TempFrame[] SetFramesForBoard(String[] ScoreArray, TempFrame[] scores,int framelength, ref int startindex)
        {
            int index = startindex;
            if (framelength == 2)
            {
                if (scores[startindex].isStrike)
                {
                    startindex+=1;
                    return new TempFrame[] { scores[index], new TempFrame(0) };
                    
                }
                else
                {
                    startindex += 2;
                    return new TempFrame[] { scores[index], scores[index + 1] };
                    
                }


            }
            else
            {
                startindex += 3;
                return new TempFrame[] { scores[index], scores[index+1],scores[index+2] };
                
            }

            
        }
    }

    
    enum ScoreState {
        Miss=0,
        One=1,
        Two=2,
        Three=3,
        Four=4,
        Five=5,
        Six=6,
        Seven=7,
        Eight=8,
        Nine=9,
        Strike=10
    }
    /*
     bool bpreviousSpare = false;
        bool bpreviousStrike = false;
        bool isNext10thFrame = false;
     */
    class TempFrame {
        public int FrameValue = 0;
        public bool isSpare = false;
        public bool isStrike = false;
        public bool isMiss = false;
        public TempFrame(int frameValue)
        {
            this.FrameValue = frameValue;
        }
    }
}
