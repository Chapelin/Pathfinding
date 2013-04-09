using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommunXnaFree.Spacialisation;

namespace AStart
{
    public class Node
    {
        private Node _parent;
        private int _G;
        private int _H;
        public Coordonnees pos;
        public bool Obstacle;


        public Node(int x, int y, bool ob = false)
        {
            this.Parent = null;
           this.pos = new Coordonnees(x,y);
            this.Obstacle = ob;
        }



        public Node Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public int G
        {
            get { return _G; }
            set { _G = value; }
        }

        public int H
        {
            get { return _H; }
            set { _H = value; }
        }

        public int F
        {
            get { return G+H; }
        }

        public void CalculerG(int horizontal, int diagonal)
        {
            if (this.Parent != null)
            {
                var temp = this;
                var g = this.pos.DifferenceSurDeuxAxes(this._parent.pos) ? diagonal : horizontal; // si diagonale : 140 au depart
                while (temp.Parent != null)
                {
                    temp = temp.Parent;
                    g += temp.G;
                }
                this._G = g;
            }
        }

        public void CalculerH(Coordonnees cible, int horizontal, int diagonal)
        {
            int h = 0;
            var diff = (pos - cible).Abs();
            if (diagonal < 2 * horizontal) //si diagonal est utile
            {
                while (diff.X > 0 && diff.Y > 0)
                {
                    h += diagonal;
                    diff -= new Coordonnees(1, 1);
                }

                while (diff.X + diff.Y > 0)
                {
                    h += horizontal;
                    diff.X -= 1;

                }
            }
            else //sinon inutile : on compte le nombre de deplacement
            {
                while (diff.X + diff.Y > 0)
                {
                    h += horizontal;
                    diff.X -= 1;

                }
            }

            this._H = h;
        }


        public int SimulateCalculerG(Node parent, int horizontal, int diagonal)
        {
            if (parent != null)
            {
                Node temp;
                var g = this.pos.DifferenceSurDeuxAxes(parent.pos) ? diagonal : horizontal; // si diagonale : 140 au depart
                temp = parent;
                g += temp.G;
                while (temp.Parent != null)
                {
                    temp = temp.Parent;
                    g += temp.G;
                }
                return g;
            }
            return -1;
        }
    }
}
