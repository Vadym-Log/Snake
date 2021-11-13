using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            KeyPress.OnKeyPressed += KeyPress_OnKeyPress;
            KeyPress.Start();
            //while (true)
            //{
            //}

            Walls.CreateWalls();
            Score.CreateScore();
            Snake.CreateSnake();
            Food.CreateFood();
            Snake.StartSnake();
        }

        static void KeyPress_OnKeyPress(KeyPress.Key Key)
        {
            //if (Key == KeyPress.Key.Enter)
            //    Console.Write("Enter");
            if (Key == KeyPress.Key.Space)
                State.CreateState();
        }
    }
}
