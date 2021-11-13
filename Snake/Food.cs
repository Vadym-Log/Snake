using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{    
    class Food
    {
        public static int[] foodPosition = new int[2];
        private static char food = '@';
        private static bool allowFood;        
        public static void CreateFood()
        {
            allowFood = true;
            do
            {
                foodPosition[0] = new Random().Next(Walls.minY + 2, Walls.maxY - 2);
                foodPosition[1] = new Random().Next(Walls.minX + 2, Walls.maxX - 2);
                foreach (int[] item in Snake.positions)
                {
                    if (item[0] == foodPosition[0] && item[1] == foodPosition[1])
                        allowFood = false;
                }
            } while (!allowFood);

            DrawFood();
        }

        public static void DrawFood()
        {
            Console.SetCursorPosition(foodPosition[1], foodPosition[0]);
            Console.Write(food);
        }
    }
}