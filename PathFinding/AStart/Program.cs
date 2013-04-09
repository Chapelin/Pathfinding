using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunXnaFree.Spacialisation;

namespace AStart
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] res = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 9, 9, 9, 9 }, { 0, 0, 0, 0, 0 }, { 9, 9, 9, 9, 0 }, {0,0,0,0,0}};//[0,4] = 5ieme lement du premier 
            Coordonnees depart = new Coordonnees(0,4);
            Coordonnees arrive = new Coordonnees(4,0);
            var resultat = Pathfinding.Singleton().FindPath(res, depart, arrive);
            resultat.ForEach(x=> Console.WriteLine(x.pos));
            Console.ReadLine();
        }
    }
}
