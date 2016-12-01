using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    class GameObjectTile
    {
        //Nombre total de tuiles pour les lignes qui entrent dans l'écran
        public const int LIGNE = 12;
        //Le nombre total de tuiles par colonne dans un écran
        public const int COLONNE = 22;
        //Dimensions d'une tuile
        public const int LARGEUR_TUILE = 64;
        public const int HAUTEUR_TUILE = 65;

        //La texture tileLayer
        public Texture2D texture;
        public Vector2 position;

        public Rectangle rectSource = new Rectangle(0, 0, LARGEUR_TUILE, HAUTEUR_TUILE);

        // 1 = pierre, 2 = gazon 3 = sable (voir image tiles1.jpg)        
        // Vous pouvez aussi faire un tableau de tuiles si vous en avez plusieurs
        public Rectangle rectPierre = new Rectangle(0, 32, 64, 65);
        public Rectangle rectGazon = new Rectangle(384, 32, 64, 65);
        public Rectangle rectSable = new Rectangle(256, 352, 64, 65);

        //La carte qui est affichée
        public int[,] map = {
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            {1,2,2,1,1,1,1,2,2,2,1,1,1,1,1,1,1,2,2,2,2,1},
                            {1,2,2,2,2,1,1,2,1,2,2,2,2,2,2,2,3,3,1,1,1,1},
                            {1,2,1,1,2,3,3,2,3,3,1,2,1,1,2,1,1,1,1,1,1,1},
                            {1,2,1,1,1,1,3,1,1,1,1,2,1,1,2,2,2,2,1,1,1,1},
                            {1,2,2,2,2,2,2,1,1,1,1,2,1,1,2,1,1,2,1,1,1,1},
                            {1,2,1,2,1,1,2,1,1,2,2,2,3,3,2,2,1,2,2,2,2,2},
                            {1,2,1,2,1,1,2,1,1,2,1,1,1,1,1,2,1,1,1,1,1,1},
                            {1,2,1,2,3,3,2,2,2,2,1,3,3,3,1,2,2,2,1,1,1,1},
                            {1,2,1,1,1,1,1,1,1,1,1,3,3,3,1,1,1,3,1,1,1,1},
                            {1,2,2,2,2,2,3,3,3,1,1,3,3,3,1,1,1,3,3,2,2,1},
                            {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},
                            };

        public void Draw(SpriteBatch spriteBatch, Rectangle lumiere)
        {
            for (int i = 0; i < LIGNE; i++)
            {
                position.Y = (i * HAUTEUR_TUILE);
                for (int j = 0; j < COLONNE; j++)
                {
                    position.X = (j * LARGEUR_TUILE);

                    Rectangle rectangleTuile= new Rectangle((int)position.X,(int)position.Y,LARGEUR_TUILE,HAUTEUR_TUILE);
                    if (rectangleTuile.Intersects(lumiere))
                    {

                    
                    switch (map[i, j])
                    {
                        case 1:
                            spriteBatch.Draw(texture, position, rectPierre, Color.White);
                            break;
                        case 2:
                            spriteBatch.Draw(texture, position, rectGazon, Color.White);
                            break;
                        case 3:
                            spriteBatch.Draw(texture, position, rectSable, Color.White);
                            break;
                    }
                    }
                }
            }
        }

    }
}
