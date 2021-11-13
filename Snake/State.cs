using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Snake
{
    static class State
    {
        public static string path = @"State.txt";        
        private static StringBuilder sbSnake = new StringBuilder();
        private static StringBuilder sbFood = new StringBuilder();
        private static string maxScore;
        private static string currentScore;
        private static string currentDirection;
        private static string[] point;
        private static string[] state;
        private static string stateSnake;
        private static string[] stateSnakeArray;
        private static int[] tempPositionSnake;
        private static string stateFood;
        private static int[] tempPositionFood;
        private static string stateMaxScore;
        private static string stateCurrentScore;
        private static string stateDirection;

        public static void CreateState()
        {
            sbSnake.Append("Snake:");
            foreach (var item in Snake.positions)
                sbSnake.Append(" " + '{' + item[1] + ',' + item[0] + '}');
            File.WriteAllText(path, sbSnake.ToString());
            File.AppendAllLines(path, new[] { "" });

            sbFood.Append("Food:");
            sbFood.Append(" " + '{' + Food.foodPosition[1] + ',' + Food.foodPosition[0] + '}');
            File.AppendAllLines(path, new[] { sbFood.ToString() });

            if (Score.currentScore > Score.maxScore)
                Score.maxScore = Score.currentScore;
            maxScore = "Max Score: " + Score.maxScore.ToString();
            File.AppendAllLines(path, new[] { maxScore });

            currentScore = "Current Score: " + Score.currentScore.ToString();
            File.AppendAllLines(path, new[] { currentScore });

            currentDirection = "Current Direction: " + Snake.direction.ToString();
            File.AppendAllLines(path, new[] { currentDirection });
        }
        
        public static void ReadState()
        {
            state = File.ReadAllLines(path);
            stateSnake = state[0].Remove(0, 7);
            stateSnakeArray = stateSnake.Split(' ');
            foreach (string item in stateSnakeArray)
            {
                point = new string[2];
                point = item.Split(',');
                tempPositionSnake = new int[2];
                tempPositionSnake[1] = int.Parse(point[0].Substring(1, 2));
                tempPositionSnake[0] = int.Parse(point[1].Substring(0, 2));
                Snake.positions.Add(tempPositionSnake);
                Snake.tempYSnake = Snake.positions[0][0];
                Snake.tempXSnake = Snake.positions[0][1];
            }

            stateFood = state[1].Remove(0, 6);
            point = new string[2];
            point = stateFood.Split(',');
            tempPositionFood = new int[2];
            tempPositionFood[1] = int.Parse(point[0].Substring(1, 2));
            tempPositionFood[0] = int.Parse(point[1].Substring(0, 2));
            Food.foodPosition[0] = tempPositionFood[0];
            Food.foodPosition[1] = tempPositionFood[1];
            Food.DrawFood();

            stateMaxScore = state[2].Remove(0, 11);
            Score.maxScore = int.Parse(stateMaxScore);
            Score.ShowScore();

            stateCurrentScore = state[3].Remove(0, 15);
            Score.currentScore = int.Parse(stateCurrentScore);
            Score.ShowCurrentScore();

            stateDirection = state[4].Remove(0, 19);
            switch (stateDirection)
            {
                case "Up":
                    Snake.direction = Direction.Up;
                    break;
                case "Down":
                    Snake.direction = Direction.Down;
                    break;
                case "Left":
                    Snake.direction = Direction.Left;
                    break;
                case "Right":
                    Snake.direction = Direction.Right;
                    break;
            }
            Snake.isFirstMove = false;
            Snake.StartSnake();
        }
    }
}
