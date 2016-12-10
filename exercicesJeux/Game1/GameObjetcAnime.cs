using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GameObjectAnime
    {
        public Texture2D sprite;
        public Vector2 vitesse;
        public Vector2 direction;
        public Rectangle position;
        public Rectangle spriteAfficher;
        public bool estVivant;


        public enum etats { attenteDroite, attenteGauche, runDroite, runGauche, runHaut, runBas,attenteHaut, attenteBas };
        public etats objetState;


        //Compteur qui changera le sprite affiché
        private int cpt = 0;

        //GESTION DES TABLEAUX DE SPRITES (chaque sprite est un rectangle dans le tableau)
        int runState = 0; //État de départ
        int nbEtatRun = 3; //Combien il y a de rectangles pour l’état “courrir”
        public Rectangle[] tabRunGauche = {
            new Rectangle(1, 41, 28, 23),
            new Rectangle(34, 40, 28, 24),
            new Rectangle(66, 42, 29, 22),
            };

        public Rectangle[] tabRunDroite = {
            new Rectangle(1, 74, 29, 22),
            new Rectangle(34, 72, 28, 24),
            new Rectangle(66, 73, 28, 23),
             };
        public Rectangle[] tabRunBas =
        {
            new Rectangle(9,3,14,29),
            new Rectangle(41,2,14,30),
            new Rectangle(73,3,14,29),
        };
        public Rectangle[] tabRunHaut =
        {
            new Rectangle(8,105,16,23),
            new Rectangle(40,103,16,25),
            new Rectangle(72,105,16,23),
        };

        int waitState = 0;
        public Rectangle[] tabAttenteDroite =
        {
            new Rectangle(41, 2, 14, 30)
        };
        
        public Rectangle[] tabAttenteGauche =
        {
            new Rectangle(41, 2, 14, 30)
        };

        public Rectangle[] tabAttenteBas =
        {
            new Rectangle(41, 2, 14, 30)
        };
        public Rectangle[] tabAttenteHaut =
        {
            new Rectangle(40,103,16,25),
        };

        public virtual void Update(GameTime gameTime)
        {
            if (objetState == etats.attenteDroite)
            {
                spriteAfficher = tabAttenteDroite[waitState];
            }
            if (objetState == etats.attenteGauche)
            {
                spriteAfficher = tabAttenteGauche[waitState];
            }
            if (objetState == etats.runDroite)
            {
                spriteAfficher = tabRunDroite[runState];
            }
            if (objetState == etats.runGauche)
            {
                spriteAfficher = tabRunGauche[runState];
            }
            if (objetState == etats.attenteBas)
            {
                spriteAfficher = tabAttenteBas[waitState];
            }
            if (objetState == etats.runBas)
            {
                spriteAfficher = tabRunBas[runState];
            }
            if (objetState == etats.attenteHaut)
            {
                spriteAfficher = tabAttenteHaut[waitState];
            }
            if (objetState == etats.runHaut)
            {
                spriteAfficher = tabRunHaut[runState];
            }

            //Compteur permettant de gérer le changement d'images
            cpt++;
            if (cpt == 6) //Vitesse défilement
            {
                //Gestion de la course
                runState++;
                if (runState == nbEtatRun)
                {
                    runState = 0;
                }
                cpt = 0;
            }
        }



    }
}
