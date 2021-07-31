using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingGameChallenge
{
    class Game
    {
        public static Game game;
        public static int bowlingScore = 0;
        public static int FrameCalculatedtill = 0;
        public static Boolean hasEnded=false;
        public static int FrameNumber=0;
        public Frame[] frames;
        //public List<Int32> FramesToSearchAheadFrom = new List<int>();
        public static Game getInstance()
        {
            if (game == null || Game.hasEnded)
            {
                game = new Game();
                game.frames = new Frame[10];
                Game.FrameNumber = 0;
            }
            return game;
        }
        //Reset the game
        public static void resetGame()
        {
            Game.hasEnded = true;
        }
        //Check the calculation of 
        public int CalculateScore(int frameNumber)
        {

            if (frameNumber == 9)
            {
                if (Game.FrameCalculatedtill == 0)
                {
                    return calculateFrameScore(frameNumber, 0) + game.frames[frameNumber].totalScore;
                }
                else if (Game.FrameCalculatedtill < frameNumber)
                {
                    return calculateFrameScore(frameNumber, Game.FrameCalculatedtill + 1) + game.frames[frameNumber].totalScore;
                }
                else
                {
                    return Game.bowlingScore + game.frames[frameNumber].totalScore;
                }
                Game.FrameCalculatedtill = 9;
            }
            else if (frameNumber >= 0 && frameNumber < 9)
            {
                if (game.frames[frameNumber].CanbeCalculated && Game.FrameCalculatedtill == 0)
                {
                    return calculateFrameScore(frameNumber, 0);
                }
                else if (game.frames[frameNumber].CanbeCalculated && Game.FrameCalculatedtill != 0)
                {
                    return calculateFrameScore(frameNumber, Game.FrameCalculatedtill + 1);
                }
                else
                {
                    Console.WriteLine("The score cannot be calculated as the previous score was a strike or a spare. So to calculate the score in the frame score of next rolls are needed");
                }

            }
            else
            {

                return Game.bowlingScore;

            }

            
            
            return 0;

        }
        //Does the calulation of the all the frames till a certain Frame Number
        public int calculateFrameScore(int framenumber,int startFromFrame)
        {
            
            int sum = 0;
            framenumber = framenumber == 9 ? framenumber - 1 : framenumber;
            for (int i = startFromFrame; i <= framenumber; i++)
            {

                List<Rounds> f = game.frames[i].rounds;

                if (f[0].isStrike)
                {
                    int p = 0;
                    int b = i;
                    if (game.frames[b + 1].rounds[0].isStrike)
                    {
                        while (b < 9 && game.frames[b + 1].rounds[0].isStrike && p < 2)
                        {
                            Game.bowlingScore += 10;
                            ++p; b++;
                        }
                        if (p == 1 && b == 9)
                        {
                            Game.bowlingScore += game.frames[b].rounds[1].RoundValue;
                        }
                        if (p < 2 && b != 9)
                        {
                            Game.bowlingScore += game.frames[b + 1].rounds[0].RoundValue;
                        }

                    }
                    else
                        Game.bowlingScore += game.frames[b + 1].totalScore;
                }
                else if (f[1].isSpare)
                {
                    
                        Game.bowlingScore += game.frames[i+1].rounds[0].RoundValue;
                    
                    
                }
                Game.bowlingScore += game.frames[i].totalScore;
                
                game.frames[i].totalScore = Game.bowlingScore;
            }
            Game.FrameCalculatedtill = framenumber;
            return Game.bowlingScore;

        }

        //Get the frame Score for a certain Frame Number
        public int GetFrameScore(int frameNumber) {
            if (Game.FrameNumber - 1 < frameNumber)
            {
                Console.WriteLine("Have not reached that part of game yet");
            }
            else if (game.frames[frameNumber].CanbeCalculated == false)
            {
                Console.WriteLine("The score cannot be calculated as the previous score was a strike or a spare. So to calculate the score in the frame score of next rolls are needed");
            }
            else if (frameNumber <= Game.FrameCalculatedtill)
            {
                return game.frames[frameNumber].totalScore;
            }
            else
            {
                if (Game.FrameCalculatedtill == 0)
                {
                    return calculateFrameScore(frameNumber, Game.FrameCalculatedtill);
                }
                else
                {
                    return calculateFrameScore(frameNumber, Game.FrameCalculatedtill + 1);
                }

            }
            return 0;
        }

        
        ////Obselete Method
        //public int calculateScore(int framenumber, int no_of_turns_tocheck, Boolean wasStrike)
        //{
        //    int score = 0;

        //    //Code to check if the the first value of the frame entered is Strike or the second value of the frame number is Spare
        //    if (no_of_turns_tocheck <= 0)
        //    {

        //        return game.frames[framenumber].rounds[0].RoundValue + game.frames[framenumber].rounds[1].RoundValue;
        //    }

        //    if (game.frames[framenumber].FrameState.Equals("Strike") && wasStrike)
        //    {
        //        score += 10 + calculateScore(framenumber + 1, no_of_turns_tocheck - 1, wasStrike);
        //    }
        //    else if (game.frames[framenumber].FrameState.Equals("Spare") && wasStrike)
        //    {
        //        return calculateScore(framenumber, no_of_turns_tocheck - 2, wasStrike);
        //    }

        //    // code for spare

        //    if (game.frames[framenumber].FrameState.Equals("Spare") && !wasStrike)
        //    {
        //        score += 10 + calculateScore(framenumber + 1, no_of_turns_tocheck - 1, wasStrike);
        //    }
        //    else if (game.frames[framenumber].FrameState.Equals("Strike") && !wasStrike)
        //    {
        //        score += 10 + calculateScore(framenumber + 1, no_of_turns_tocheck - 1, wasStrike);
        //    }
        //    else if (!wasStrike && no_of_turns_tocheck == 1)
        //        score += game.frames[framenumber].rounds[0].RoundValue;
        //    else if (!wasStrike && no_of_turns_tocheck == 2)
        //        return calculateScore(framenumber, 0, wasStrike);


        //return score;
        //}
       
        //Maps the score in string format to equivalent integer score
        public int ScoreMapper(String score, int prev=0)
        {
            int value = 0;
            String ch=score;
            
            switch (ch)
            {
                case "0": value = 0; break;
                case "1": value = 1; break;
                case "2": value = 2; break;
                case "3": value = 3; break;
                case "4": value = 4; break;
                case "5": value = 5; break;
                case "6": value = 6; break;
                case "7": value = 7; break;
                case "8": value = 8; break;
                case "9": value = 9; break;
                case "Strike": value = 10; break;
                case "Miss": value = 0; break;
                default: value = 10 - prev;break;
            }
            return value;
        }
        //Add the scores to each frame
        public void AddScoreToFrame(int FrameNumber, String Score)
        {
            Boolean bIsStrike = false;

            if (game.frames[Game.FrameNumber] == null)
            {
                game.frames[Game.FrameNumber] = new Frame(Game.FrameNumber);
            }

            if (Game.FrameNumber != 9)
            {

                int value = ScoreMapper(Score);
                
                if (Score.Equals("Strike")) {
                    game.frames[Game.FrameNumber].SetRoundScore(value);
                    game.frames[Game.FrameNumber].CanbeCalculated = false;
                    //game.FramesToSearchAheadFrom.Add(Game.FrameNumber);
                    Game.FrameNumber += 1; bIsStrike = true;


                }
                else if (Score.Equals("Spare"))
                {
                    int prev = game.frames[Game.FrameNumber].rounds[0].RoundValue;
                    int val = ScoreMapper(Score, prev);value = val;
                    game.frames[Game.FrameNumber].CanbeCalculated = false;
                    game.frames[Game.FrameNumber].SetRoundScore(value, true);
                    //game.FramesToSearchAheadFrom.Add(Game.FrameNumber);
                }
                else
                {
                    game.frames[Game.FrameNumber].SetRoundScore(value);
                    
                }

                if (!bIsStrike && game.frames[Game.FrameNumber].roundFilled == 2)
                {
                    Game.FrameNumber += 1;
                }

            }

            else
            {
                if ((Game.FrameNumber == 9))
                {
                    if (game.frames[Game.FrameNumber].rounds.Count > 0)
                    {
                        if ((game.frames[Game.FrameNumber].rounds[0].isStrike && game.frames[Game.FrameNumber].rounds.Count == 3) ||
                            (!game.frames[Game.FrameNumber].rounds[0].isStrike && game.frames[Game.FrameNumber].rounds.Count == 2))
                        {
                            Console.WriteLine("You cannot enter any new score as all the frame are filled");
                            Console.WriteLine("You can check the total score till then or reset the game");
                            return;
                        }
                    }

                }
                int value = ScoreMapper(Score);
                if (Score.Equals("Spare"))
                {
                    int prev = game.frames[Game.FrameNumber].rounds[0].RoundValue;
                    int val = ScoreMapper(Score, prev); value = val;
                    game.frames[Game.FrameNumber].SetRoundScore(value,true);
                }
                else
                {
                    game.frames[Game.FrameNumber].SetRoundScore(value);
                }

                
            }
        
        }
    }

    class Frame {
        public Boolean CanbeCalculated=true; //To check if Spare or Strike is scored in Frame. As if the Frame has Stike or Spare the score can only be calculated by next scores
        public int frameNumber;
        public int roundFilled=0;
        public List<Rounds> rounds=new List<Rounds>();
        public int totalScore=0;
        
        public Frame(int frameNumber)
        {
            this.frameNumber = frameNumber;
        }

        public void SetRoundScore(int Score,Boolean isSpare=false)
        {
            this.roundFilled += 1;// Keeping a track of rounds filled in frame
            this.rounds.Add(new Rounds(Score,isSpare)); // Addkng score to the frame
            this.totalScore += Score;// Keep track of total score
        }

        
    }
    class Rounds {
        public int RoundValue = 0;
        public bool isSpare = false;
        public bool isStrike = false;
        public bool isMiss = false;
        public Rounds(int roundValue,Boolean isSpare=false)
        {
            this.RoundValue = roundValue;
            this.isSpare = isSpare;
            //Setting the values of rounds
            if (roundValue == 10)
            {
                this.isStrike = true;
            }
            if (roundValue == 0)
            {
                this.isMiss = true;
            }
        }

    }

    class MainClass {

        public static void DisplayMessage()
        {
            Console.WriteLine("Choices that can be made are as follows: ");
            Console.WriteLine("1.Enter the score");
            Console.WriteLine("2.Check how far has the game reached");
            Console.WriteLine("3.Get Score till the current Frame");
            Console.WriteLine("4.Get Score of a Frame");
            Console.WriteLine("5.Reset the Game");
            Console.WriteLine("6.Exit the game");
            Console.WriteLine("Enter the choice you want from 1 - 4");
            Console.WriteLine();
        }
         static void Main(string[] args) {
            Program.OtherMain();
            Console.WriteLine("Welcome to the BowLing Game Challenge");
            DisplayMessage();
            int ch = Int32.Parse(Console.ReadLine());

            while (ch<6)
            {
                if (ch == 1)
                {
                    
                    Game.getInstance().AddScoreToFrame(Game.FrameNumber,Console.ReadLine());
                }
                if (ch == 3)
                {
                    Console.WriteLine("\nThe total score till now is : " + Game.getInstance().CalculateScore(Game.FrameNumber==9? Game.FrameNumber : Game.FrameNumber - 1));
                }
                if (ch == 2)
                {
                    if (Game.getInstance().frames[Game.FrameNumber] == null)
                    {
                        Console.WriteLine("\nThe current round which is going is from frame  " + (Game.FrameNumber + 1) + " and round no : " + 1+"\n");
                    }
                    else
                    {
                        Console.WriteLine("\nThe current round which is going is from frame  " + (Game.FrameNumber + 1) + " and round no : " + (Game.getInstance().frames[Game.FrameNumber].roundFilled + 1) + "\n");
                    }
                    

                }
                if (ch == 4)
                {
                    Console.WriteLine("\nEnter the Frame Number from 1-10 whose score you want to see : ");
                    int frameNumber = Convert.ToInt32(Console.ReadLine());
                    if (frameNumber > 10 && frameNumber < 1)
                    {
                        Console.WriteLine("\nYou have entered a invalid frame number. Please enter a valid phone number");
                        return;
                    }
                    Console.WriteLine("\nThe Score of that selected frame is : " + Game.getInstance().GetFrameScore(frameNumber - 1)+"\n");
                }
                if (ch == 5)
                {
                    Console.WriteLine("You have choosen to reset the game the values entered previous would be lost");
                    Game.resetGame();
                }

                DisplayMessage();
                ch = Int32.Parse(Console.ReadLine());
            }
        }
    }

}
