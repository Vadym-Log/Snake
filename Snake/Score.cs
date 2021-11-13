using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Snake
{
    class Score
    {
        private static string path = @"Score.txt";
        private static string[] text;
        public static int maxScore = 0;
        public static int currentScore = 0;
        private static StringBuilder sb = new StringBuilder();
        private static string sbText;

        public static void CreateScore()
        {
            Console.SetCursorPosition(45, 25);
            Console.Write("Score:");

            if (!File.Exists(path))
                File.Create(path).Close();
            else               
            { 
                text = File.ReadAllLines(path);
                for (int i = 0; i <= text.Length - 1; i++)
                { 
                    if (int.Parse(text[i]) > maxScore) maxScore = int.Parse(text[i]);
                    sb.AppendLine(text[i]);
                }
                sbText = sb.ToString();           
            }
            ShowScore();
        }

        public static void ShowScore()
        {
            Console.SetCursorPosition(45, 30);
            Console.Write($"Max Score = {maxScore}");
            Console.SetCursorPosition(45, 35);
            Console.Write($"Current Score = {currentScore}");
        }

        public static void ShowCurrentScore()
        {
            Console.SetCursorPosition(61, 35);
            Console.Write(currentScore);
        }

        public static void RecordScore()
        {
            File.WriteAllText(path, sbText);         
            File.AppendAllLines(path, new[] { currentScore.ToString() });
        }
    }
}
