﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers
{
    class Program
    {
        static void Main(string[] args)
        {
            Game thisGame = new Game();
            thisGame.Test();
            thisGame.PrintBoard();
            Console.ReadLine();
        }
    }
}
