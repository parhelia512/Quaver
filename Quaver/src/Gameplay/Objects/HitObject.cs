﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Quaver.Graphics;
using Quaver.Main;

namespace Quaver.Gameplay
{
    public class HitObject
    {
        //Sprite Objects
        internal Sprite _HitBodySprite;
        internal Sprite _HoldBodySprite;
        internal Sprite _HoldEndSprite;

        public HitObject() //Remove ContentManager Later
        {
            _HitBodySprite = new Sprite()
            {
                Image = GameBase.LoadedSkin.NoteHitObject1,
                Size = Vector2.One * 100,
                Alignment = Alignment.TopLeft
            };

            _HoldBodySprite = new Sprite()
            {
                Image = GameBase.LoadedSkin.NoteHoldBody,
                Size = Vector2.One * 100,
                Position = new Vector2(0, _HitBodySprite.AbsoluteSize.Y/2f),
                Alignment = Alignment.TopLeft
            };

            _HoldEndSprite = new Sprite()
            {
                Image = GameBase.LoadedSkin.NoteHoldEnd,
                Size = Vector2.One * 100,
                Position = new Vector2(0, _HitBodySprite.AbsoluteSize.Y/2f + _HoldBodySprite.AbsoluteSize.Y - 50),
                Alignment = Alignment.TopLeft
            };
        }

        /// <summary>
        /// When the note is supposed to be pressed.
        /// </summary>
        public float StartTime
        {
            get; set;
        }

        /// <summary>
        /// When the note is supposed to be held until. (If the HitObject is a long note)
        /// </summary>
        public float EndTime
        {
            get; set;
        }

        /// <summary>
        /// Determines if the HitObject is a long note or a regular note.
        /// </summary>
        public bool isLongNote
        {
            get; set;
        } = true;
        
        public void Draw()
        {
            //If the object is a long note
            if (isLongNote)
            {
                _HoldBodySprite.Draw();
                _HoldEndSprite.Draw();
            }

            _HitBodySprite.Draw();
        }
    }
}
