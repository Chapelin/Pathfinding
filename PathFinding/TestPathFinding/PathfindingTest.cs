using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AStart;
using CommunXnaFree.Spacialisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestPathFinding
{
    [TestClass]
    public class PathfindingTest
    {
        [TestMethod]
        public void TestGetAdjacent()
        {
            var temp = CreerTable(3, 2);
            Pathfinding path = Pathfinding.Singleton();
            var t = path.GetAdjacents(new Coordonnees(0, 0), temp);
            Assert.AreEqual(t.Count, 3, "Le nombre d'adjacent trouvé par le coin haut gauche n'est pas 3");
            t = path.GetAdjacents(new Coordonnees(2, 0), temp);
            Assert.AreEqual(t.Count, 3, "Le nombre d'adjacent trouvé par le coin haut droit n'est pas 3");
            t = path.GetAdjacents(new Coordonnees(2, 1), temp);
            Assert.AreEqual(t.Count, 3, "Le nombre d'adjacent trouvé par le coin bas droit n'est pas 3");

            t = path.GetAdjacents(new Coordonnees(0, 1), temp);
            Assert.AreEqual(t.Count, 3, "Le nombre d'adjacent trouvé par le coin bas gauche n'est pas 3");
            temp = CreerTable(5, 5);

            t = path.GetAdjacents(new Coordonnees(2, 2), temp);
            Assert.AreEqual(t.Count, 8, "Le nombre d'adjacents trouvé n'est pas de 8 pour une case situé en milieu de plateau");
        }

        private static Node[,] CreerTable(int tx, int ty)
        {
            Node[,] t = new Node[tx, ty];
            for (int i = 0; i < tx; i++)
            {
                for (int j = 0; j < ty; j++)
                {
                    Node temp = new Node(i, j);
                    t[i, j] = temp;
                }
            }
            return t;
        }

        [TestMethod]
        public void TestFindPath()
        {
            Pathfinding path = Pathfinding.Singleton();
            int[,] res = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 9, 9, 9, 9 }, { 0, 0, 0, 0, 0 }, { 9, 9, 9, 9, 0 }, { 0, 0, 0, 0, 0 } };//[0,4] = 5ieme lement du premier 
            Coordonnees depart = new Coordonnees(0, 4);
            Coordonnees arrive = new Coordonnees(4, 0);
            var resultat = path.FindPath(res, depart, arrive);
            Assert.AreEqual(resultat.Count, 13, "le chemin en serpent est erronné");
            res = new int[6, 10]
                      {
                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {9, 9, 9, 0, 9, 9, 0, 9, 0, 0},
                          {0, 0, 0, 0, 0, 0, 0, 9, 0, 0}, {0, 9, 9, 9, 9, 9, 9, 9, 9, 9}, 
                          {0, 9, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 9, 9, 9, 9, 9, 0, 0}
                      };
            resultat = path.FindPath(res, new Coordonnees(0, 0), new Coordonnees(5, 9));
            Assert.AreEqual(resultat.Count, 17, "Le chemin trouvé ne correspond pas à la solution 1");
            res = new int[6, 10]
                      {
                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {9, 9, 9, 0, 9, 9, 0, 9, 0, 0},
                          {0, 0, 0, 0, 0, 0, 0, 9, 0, 0}, {0, 9, 9, 9, 9, 9, 9, 9, 0, 9}, 
                          {0, 9, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 9, 9, 9, 9, 9, 0, 0}
                      };
            resultat = path.FindPath(res, new Coordonnees(0, 0), new Coordonnees(5, 9));
            Assert.AreEqual(resultat.Count, 13, "Le chemin trouvé ne correspond pas au plus court");

        }

        [TestMethod]
        public void TestCustomDiag()
        {
            Pathfinding path = Pathfinding.Singleton(99, 1000,true);
            int[,] res = new int[5, 5] { { 0, 0, 0, 0, 0 }, { 0, 9, 9, 9, 9 }, { 0, 0, 0, 0, 0 }, { 9, 9, 9, 9, 0 }, { 0, 0, 0, 0, 0 } };//[0,4] = 5ieme lement du premier 
            Coordonnees depart = new Coordonnees(0, 4);
            Coordonnees arrive = new Coordonnees(4, 0);
            var resultat = path.FindPath(res, depart, arrive);
            Assert.AreEqual(resultat.Count, 17, "le chemin en serpent est erronné");
            res = new int[6, 10]
                      {
                          {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}, {9, 9, 9, 0, 9, 9, 0, 9, 0, 0},
                          {0, 0, 0, 0, 0, 0, 0, 9, 0, 0}, {0, 9, 9, 9, 9, 9, 9, 9, 9, 9}, 
                          {0, 9, 0, 0, 0, 0, 0, 0, 0, 0}, {0, 0, 0, 9, 9, 9, 9, 9, 0, 0}
                      };
        }
    }
}
