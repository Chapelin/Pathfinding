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
            int[,] res = new int[5, 5] { { 0, 0, 0, 9, 0 }, { 0, 9, 0, 9, 0 }, { 0, 9, 0, 9, 0 }, { 0, 9, 0, 9, 0 }, {0,9,0,0,0}};//
            Coordonnees depart = new Coordonnees(4,0);
            Coordonnees arrive = new Coordonnees(0,4);
            var resultat = Pathfinding.FindPath(res, depart, arrive);
            resultat.ForEach(x=> Console.WriteLine(x.pos));
            Console.ReadLine();
        }
    }
}
