



namespace Demineur
{
    internal class Program
    {
        static int nbCols;
        static int nbLignes;
        static int nbBombes;
        static Case[,] champDeMines = null!;//will be initialize later.

        //static void CountDown(int nb)
        //{
        //    Console.WriteLine(nb);
        //    if(nb == 0) //function recurssive. it will stop when there is 0
        //    {
        //        return;
        //    }
        //    CountDown(nb - 1);
        //}

        static void Main(string[] args)
        {
            //CountDown(10);
            nbCols = DemanderUnNombre("Nombre de collones? ");
            nbLignes = DemanderUnNombre("Nombre de lignes? ");
            nbBombes = DemanderUnNombre("Nombre de bombes? ");
            
            champDeMines = new Case[nbCols, nbLignes];
            AjouterBombes();
            //champDeMines[1,3].Visible = true;
            //champDeMines[4, 3].Visible = true;
            //champDeMines[3, 2].Visible = true;
            AfficherGrilles();
            //Console.WriteLine((nbCols, nbLignes, nbBombes));

            while (true)
            {
                AfficherGrilles();
                Console.WriteLine();
                int x = DemanderUnNombre("x? ");
                int y = DemanderUnNombre("y? ");
                DecouvrirCase(x, y);
                
            
            }


        }

        private static void DecouvrirCase(int x, int y)
        {
            if (champDeMines[x, y].Visible) //condition to stop the loop
            {
                return;
            }
            champDeMines[x, y].Visible = true;
            if (champDeMines[x, y].Value == 0)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    for (int dy = -1; dy <= 1; dy++)
                    {
                        int newX = x + dx;
                        int newY = y + dy;
                        //je verifie que je ne suis pas dehor de grille (try to avoid the error for "out of range")
                        if (newX >= 0 && newX < nbCols && newY >= 0 && newY < nbLignes)
                        {
                            DecouvrirCase(newX, newY);
                        }
                    }
                }
            }
        }

        private static void AjouterBombes()
        {
            for (int i = 0; i < nbBombes; i++)
            {
                int x = new Random().Next(0, nbCols);
                int y = new Random().Next(0, nbLignes);
                if (champDeMines[x, y].Value == 9)
                {
                    i--;
                }
                else
                {
                    AjouterBombe(x, y);
                }
                //int x, y;
                //do
                //{
                //     x = new Random().Next(0, nbCols);
                //     y = new Random().Next(0, nbLignes);
                //} while (champDeMines[x, y] == 9);
                //AjouterBombe(x, y);
            }
            //AdjouterBombe(0, 1);
            //AdjouterBombe(3, 3);
            //AdjouterBombe(4, 4);

            //champDeMines[0, 1] = 9;
            //champDeMines[2, 3] = 9;
            //champDeMines[4, 4] = 9;
        }
        private static void AjouterBombe(int x, int y)
        {
            champDeMines[x, y].Value = 9;

            for(int dx = -1; dx<= 1; dx++)
            {
                for(int dy = -1; dy <= 1; dy++)
                {
                    int newX = x + dx;
                    int newY = y + dy;
                    //je verifie que je ne suis pas dehor de grille (try to avoid the error for "out of range")
                    if(newX >= 0 && newX < nbCols && newY >=0 && newY < nbLignes)
                    {
                        //verifie si c'est une 9
                        if(champDeMines[newX, newY].Value !=9)
                        {
                            champDeMines[newX, newY].Value ++;
                        }
                    }

                }
            }

            //champDeMines[x - 1, y - 1]++;
            //champDeMines[x - 1, y + 0]++;
            //champDeMines[x - 1, y + 1]++;
            //champDeMines[x + 0, y - 1]++;
            //champDeMines[x + 0, y + 0]++;
            //champDeMines[x + 0, y + 1]++;
            //champDeMines[x + 1, y - 1]++;
            //champDeMines[x + 1, y + 0]++;
            //champDeMines[x + 1, y + 1]++;
        }

        private static void AfficherGrilles()
        {
            Console.Clear();
            for (int y = 0; y < nbLignes; y++)
            {
                for (int x = 0; x < nbCols; x++)
                {
                    Case c = champDeMines[x, y];
                    if (c.Visible)
                    {
                        if (c.Value == 9)
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                        }
                            Console.Write(c.Value);
                            Console.ResetColor();
                    }else
                    {
                        Console.Write("■");//ALT+254 = ASCII SQUARE
                    }
                }
                Console.WriteLine();
            }
        }

        private static int DemanderUnNombre(string message)
        {
            int nb;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out nb))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("La valeur est incorrecte");
                Console.ResetColor();
                Console.WriteLine(message);
            }
            return nb;
        }
    }
}
