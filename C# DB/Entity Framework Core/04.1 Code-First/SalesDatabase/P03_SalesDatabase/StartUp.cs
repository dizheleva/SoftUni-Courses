﻿using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var context = new SalesContext();
        }
    }
}
