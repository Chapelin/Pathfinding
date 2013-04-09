using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunXnaFree.Spacialisation;

namespace AStart
{
    public class Pathfinding
    {

        private static Pathfinding _singleton;

        private int vertical;
        private int diag;

        public static Pathfinding Singleton(int vertical=100, int diag=140, bool force = false)
        {
            if(_singleton==null || force)
                _singleton = new Pathfinding(vertical,diag);
            return _singleton;
        }

        private Pathfinding(int vertical, int diag)
        {
            this.vertical = vertical;
            this.diag = diag;
        }


        /// <summary>
        /// REtourne le chemin possible entre le depart et son arrivée
        /// </summary>
        /// <param name="tab">Labyrinthe, avec 9 = 0bstacle, int[y,x]</param>
        /// <param name="depart"></param>
        /// <param name="arrive"></param>
        /// <returns></returns>
        public List<Node> FindPath(int[,] tab, Coordonnees depart, Coordonnees arrive)
        {
            var tabNode = PreparerTableau(tab);
            var result = new List<Node>();
            var listeOuverte = new List<Node>();
            var listeFerme = new List<Node>();
            if (!tabNode[depart.X, depart.Y].Obstacle && !tabNode[arrive.X, arrive.Y].Obstacle)
            {
                listeOuverte.Add(tabNode[depart.X, depart.Y]);
                while (listeOuverte.Count>0 && !listeFerme.Contains(tabNode[arrive.X,arrive.Y]))
                {
                    //on recupere le plus petit F de la liste ouverte
                    listeOuverte = listeOuverte.OrderBy(cont => cont.F).ToList();
                    var current = listeOuverte.First();
                    //on le vire de la liste ouverte
                    listeOuverte.Remove(current);
                    //on ajoute à la liste fermé
                    listeFerme.Add(current);
                    foreach (var node in GetAdjacents(current.pos, tabNode))
                    {
                        //obstalce ou deja dans liste fermée, ou c'est le depart; on skipe
                        if (node.Obstacle || listeFerme.Contains(node) || node.pos==depart)
                            continue;

                        //si pas dans liste ouverte
                        if (!listeOuverte.Contains(node))
                        {//on l'ajoute et on indique son parent
                            listeOuverte.Add(node);
                            node.Parent = current;
                            node.CalculerG(this.vertical,this.diag);
                            node.CalculerH(arrive, this.vertical,this.diag);

                        }
                        else if (listeOuverte.Contains(node))
                        {
                            var newG = node.SimulateCalculerG(current, this.vertical,this.diag);
                            if (newG < node.G && newG > 0)
                            {
                                node.Parent = current;
                                node.CalculerG(this.vertical, this.diag);
                            }
                        }
                    }
                }

            }

            var t = tabNode[arrive.X, arrive.Y];
            //si l'arrivée à un parent, le chemin est faisable
            while(t.Parent!=null)
            {
                result.Add(t);
                t = t.Parent;
            }
            if(result.Count>0)
                result.Add(t);
            return result;
        }


        private Node[,] PreparerTableau(int[,] tab)
        {

            int t1 = tab.GetLength(0);
            int t2 = tab.GetLength(1);
            var temp = new Node[t1, t2];
            for (int x = 0; x < t1; x++)
            {
                for (int j = 0; j < t2; j++)
                {
                    temp[x, j] = new Node(x, j, tab[x, j] == 9);
                }
            }
            return temp;
        }


        public List<Node> GetAdjacents(Coordonnees c, Node[,] table)
        {
            List<Node> list = new List<Node>();
            bool flagXMin = c.X > 0;
            bool flagXMax = c.X < table.GetLength(0) - 1;
            bool flagYMin = c.Y > 0;
            bool flagYMax = c.Y < table.GetLength(1) - 1;
            var flagDiagoOk = this.diag < 2*this.vertical;
            //if (flagXMin)
            //{
            //    if (flagYMin)
            //        list.Add(table[c.X - 1, c.Y - 1]);
            //    list.Add(table[c.X - 1, c.Y]);
            //    if (flagYMax)
            //        list.Add(table[c.X - 1, c.Y + 1]);

            //}

            //if (flagXMax)
            //{
            //    if (flagYMin)
            //        list.Add(table[c.X + 1, c.Y - 1]);
            //    list.Add(table[c.X + 1, c.Y]);
            //    if (flagYMax)
            //        list.Add(table[c.X + 1, c.Y + 1]);

            //}

            //if (flagYMin)
            //    list.Add(table[c.X, c.Y - 1]);
            //if (flagYMax)
            //    list.Add(table[c.X, c.Y + 1]);
            if (flagXMin)
                list.Add(table[c.X - 1, c.Y]);
            if (flagXMax)
                list.Add(table[c.X + 1, c.Y]);

            if (flagYMin)
                list.Add(table[c.X, c.Y - 1]);
            if (flagYMax)
                list.Add(table[c.X, c.Y + 1]);
            
            var flagUseDiago = list.Where(x => !x.Obstacle).Count() < 2; //necessité utiliser diago pour avancer
            if(flagDiagoOk || (flagUseDiago)) //si les diago sont ok pour utiliser ou si on a pas le choix
            {
                if(flagXMin&&flagYMin)
                    list.Add(table[c.X - 1, c.Y - 1]);
                if(flagXMin&&flagYMax)
                    list.Add(table[c.X - 1, c.Y + 1]);

                if(flagXMax&&flagYMin)
                    list.Add(table[c.X + 1, c.Y - 1]);
                if(flagXMax&&flagYMax)
                    list.Add(table[c.X + 1, c.Y + 1]);
            }

            return list;
        }




    }
}
