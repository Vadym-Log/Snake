using System;
using System.Collections.Generic;
using System.Text;

namespace Snake
{
    struct WallPoint
    {
        private int currentX;
        private int currentY;
        private const char wallChar = 'x';

        public WallPoint(int curX, int curY)
        {
            currentX = curX;
            currentY = curY;
        }

        public void DrawWall()
        {
            Console.SetCursorPosition(currentX, currentY);
            Console.Write(wallChar);
        }
    }

    static class Walls
    {
        public const int minX = 10;
        public const int maxX = 40;
        public const int minY = 20;
        public const int maxY = 50;
        private static List<WallPoint> wall = new List<WallPoint>();
        public static ConsoleKeyInfo keyinfo;
        
        //private static char wall = 'x';
        //public static int[][] walls = new int[31][];

        public static void CreateWalls()
        {
            Console.SetWindowSize(80, 60);
            Console.CursorVisible = false;
            Console.WriteLine("Управление \"змейкой\" осуществляется стрелками -");
            Console.WriteLine("\"вверх\", \"вниз\",\"влево\" и \"вправо\".");
            Console.WriteLine("Для начала игры \"змейку\" надо запустить соответствующей стрелкой.");
            Console.WriteLine("Изначально, возможно движение \"вверх\", \"влево\" или \"вправо\".");
            Console.WriteLine("В процессе игры возможны повороты перпендикулярно ходу движения,");
            Console.WriteLine("то есть невозможны развороты на 180 градусов.");
            Console.WriteLine("Игра заканчивается при столкновении со стенкой или");
            Console.WriteLine("когда \"змейка\" наталкивается на саму себя.");
            Console.WriteLine("Для сохранения текущего состояния игры необходимо нажать клавишу Space.");
            Console.WriteLine("Если Вы хотите загрузить сохранённое состояние,");
            Console.WriteLine("чтобы продолжить предыдущую игру,");
            Console.Write("то нажмите клавишу Enter. Иначе, начнётся новая игра: ");
            keyinfo = Console.ReadKey();

            WallPoint wallPoint;
            for (int i = minX; i <= maxX; i++)
            {
                wallPoint = new WallPoint(i, minY);
                wall.Add(wallPoint);
            }
            for (int i = minX; i <= maxX; i++)
            {
                wallPoint = new WallPoint(i, maxY);
                wall.Add(wallPoint);
            }
            for (int i = minY; i <= maxY; i++)
            {
                wallPoint = new WallPoint(minX, i);
                wall.Add(wallPoint);
            }
            for (int i = minY; i <= maxY; i++)
            {
                wallPoint = new WallPoint(maxX, i);
                wall.Add(wallPoint);
            }
            foreach (WallPoint item in wall)
            {
                item.DrawWall();
            }

            //walls[0] = new int[31];
            //walls[30] = new int[31];  
            //for (int i = 1; i < walls.Length - 1; i++)
            //{
            //    walls[i] = new int[2];
            //}                      
            //for (int i = 1; i < walls.Length - 1; i++)
            //{
            //    walls[i][0] = 10;
            //    walls[i][1] = 40;
            //}
            //for (int i = 10; i <= 40; i++)
            //{
            //    walls[0][i - 10] = i;
            //}
            //for (int i = 10; i <= 40; i++)
            //{
            //    walls[30][i - 10] = i;
            //}
            //for (int i = 0; i <= walls.Length - 1; i++)
            //{
            //    Console.CursorTop = 10 + i;
            //    for (int j = 0; j <= walls[i].Length - 1; j++)
            //    { 
            //        Console.CursorLeft = walls[i][j];
            //        Console.Write(wall);
            //    }
            //}
        }
    }
}
