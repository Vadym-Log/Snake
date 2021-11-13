using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Reflection.Emit;

namespace Snake
{
    enum Direction
    {
        Null,
        Up,
        Down,
        Left,
        Right
    }

    static class Snake
    {
        public static List<int[]> positions = new List<int[]>();
        private static int[] headSnake = { 35, 25 };
        private static int[] bodySnake = { 36, 25 };
        private static int[] tailSnake = { 37, 25 };
        private static int[] head;
        public static int tempXSnake = 25;
        public static int tempYSnake = 35;
        private static char addSnake = '*';
        private static char delSnake = ' ';
        public static Direction direction;
        private static Direction checkDirection;
        public static bool isFirstMove = true;        

        public static void CreateSnake()
        {
            if (Walls.keyinfo.Key == ConsoleKey.Enter && File.Exists(State.path))
                State.ReadState();
            else
            {
                positions.Add(headSnake);
                positions.Add(bodySnake);
                positions.Add(tailSnake);
            }
            if (Walls.keyinfo.Key == ConsoleKey.Enter && !File.Exists(State.path))
            {
                Console.SetCursorPosition(0, 15);
                Console.WriteLine("Извините, файл состояния предыдущей игры отсутствует.");
                Console.WriteLine("Начинаем новую игру...");
            }
            foreach (int[] item in positions)
            {
                Console.SetCursorPosition(item[1], item[0]);
                Console.Write(addSnake);
            }
        }

        private static Direction ConvertPressedKeyToDirection(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    return Direction.Left;
                case ConsoleKey.UpArrow:
                    return Direction.Up;
                case ConsoleKey.RightArrow:
                    return Direction.Right;
                case ConsoleKey.DownArrow:
                    return Direction.Down;
            }
            return Direction.Null;
        }

        public static void StartSnake()
        {
            if (isFirstMove)
            {
                isFirstMove = false;
                while (true)
                {
                    if (Console.KeyAvailable) direction = ConvertPressedKeyToDirection(Console.ReadKey(true).Key);                    
                    if (direction == Direction.Up || direction == Direction.Right || direction == Direction.Left) break;
                }
            }
            checkDirection = direction;
            switch (direction)
            {
                case Direction.Up:
                case Direction.Down: MoveUpDown(checkDirection);
                    break;
                case Direction.Right:
                case Direction.Left: MoveRightLeft(checkDirection);
                    break;
            }
        }
        private static void MoveUpDown(Direction checkDir)
        {
            while (true)
            {
                if (Console.KeyAvailable) direction = ConvertPressedKeyToDirection(Console.ReadKey(true).Key);
                if (direction == Direction.Right || direction == Direction.Left) break;
                head = new int[2];
                head[0] = tempYSnake;
                head[1] = tempXSnake;                
                positions.Insert(0, head);
                if (checkDir == Direction.Up) --head[0];
                else ++head[0];
                DrawSnake();
            }
            StartSnake();
        }
        private static void MoveRightLeft(Direction checkDir)
        {
            while (true)
            {
                if (Console.KeyAvailable) direction = ConvertPressedKeyToDirection(Console.ReadKey(true).Key);
                if (direction == Direction.Up || direction == Direction.Down) break;

                head = new int[2];
                head[0] = tempYSnake;
                head[1] = tempXSnake;                
                positions.Insert(0, head);
                if (checkDir == Direction.Right) ++head[1];
                else --head[1];
                DrawSnake();
            }
            StartSnake();
        }
        private static void DrawSnake()
        {
            Console.SetCursorPosition(head[1], head[0]);
            Console.Write(addSnake);
            tempYSnake = positions[0][0];
            tempXSnake = positions[0][1];
            tailSnake[0] = positions[positions.Count - 1][0];
            tailSnake[1] = positions[positions.Count - 1][1];

            if (head[0] == Food.foodPosition[0] && head[1] == Food.foodPosition[1])
            {
                Food.CreateFood();
                Score.currentScore++;
                Score.ShowCurrentScore();
                Score.RecordScore();
            }                
            else
            { 
                Console.SetCursorPosition(tailSnake[1], tailSnake[0]);
                Console.Write(delSnake);
                positions.RemoveAt(positions.Count - 1);
            }

            if (head[0] == Walls.minY || head[0] == Walls.maxY || head[1] == Walls.minX || head[1] == Walls.maxX) DieSnake("wall");
            if (positions.Count >=5 )
                for (int i = 2; i <= positions.Count - 1; i++)
                    if (head[0] == positions[i][0] && head[1] == positions[i][1]) DieSnake("snake");
            
            Thread.Sleep(300);            
        }
        private static void DieSnake(string err)
        {            
            Console.SetCursorPosition(0, 55);
            if (err == "wall")
                Console.WriteLine("Игра окончена. \"Змейка\" ударилась о стену.");
            else
                Console.WriteLine("Игра окончена. \"Змейка\" съела сама себя.");
            Console.WriteLine("Для запуска игры заново перезапустите программу.");
            Console.ReadLine();
            try
            {
                Thread.ResetAbort();
            }
            catch
            {
                Console.WriteLine("Перезапуск...");
                Console.ReadLine();
            }
            finally
            {
                Console.WriteLine("Ещё раз напоминаем: закройте программу!!!");
                Console.ReadLine();
            }            
        }
    }
}
