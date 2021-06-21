using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;


namespace AlphaKilo_GameJam32
{
    /**
     * Cette classe permet de charger en mémoire des assets 
     * Cela évite ainsi les temps de chargement
     * Evitez de charger des éléments trop lourd
     * Chargez ces derniers directement dans la scène ad-hoc  
     */
    public class AssetManager
    {
        public static SpriteFont mainFont{ get; private set; }
        public static Song musicGamePlay { get; private set; }
        public static void Load(ContentManager pContent)
        {
            mainFont = pContent.Load<SpriteFont>("_Fonts_/MainFont");
            musicGamePlay = pContent.Load<Song>("_Sounds_/techno");
        }
    }
}
