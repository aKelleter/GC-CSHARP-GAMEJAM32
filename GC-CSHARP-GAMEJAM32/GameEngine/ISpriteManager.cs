using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaKilo_GameJam32
{
    public interface ISpriteManager
    {
        Vector2 position { get; }
        Rectangle boundingBox { get; }
        bool ToRemove { get; set; }
        float Energy { get; set; }

        // ----------------------------

        void Update(GameTime pGameTime);
        void Draw(SpriteBatch pSpriteBatch);
        void DamageOnSprite(ISpriteManager pActor);


    }
}
