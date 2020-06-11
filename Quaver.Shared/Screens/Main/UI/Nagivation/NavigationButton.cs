﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Quaver.Shared.Assets;
using Quaver.Shared.Screens.Menu.UI.Jukebox;
using Wobble.Assets;
using Wobble.Graphics;
using Wobble.Graphics.Animations;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.Sprites.Text;
using Wobble.Managers;

namespace Quaver.Shared.Screens.Main.UI.Nagivation
{
    public class NavigationButton : IconButton
    {
        public Sprite Icon { get; private set; }

        /// <summary>
        /// </summary>
        public SpriteTextPlus Name { get; private set; }

        /// <summary>
        /// </summary>
        public bool IsSelected { get; private set; }

        private static Texture2D DeselectedButton => UserInterface.NavigationButton;

        private static Texture2D SelectedButton => UserInterface.NavigationButtonSelected;

        /// <summary>
        /// </summary>
        /// <param name="icon"></param>
        /// <param name="text"></param>
        /// <param name="clickAction"></param>
        public NavigationButton(Texture2D icon, string text, EventHandler clickAction = null)
            : base(DeselectedButton, clickAction)
        {
            Size = new ScalableVector2(Image.Width, Image.Height);

            CreateIcon(icon);
            CreateName(text);
        }
        
        private void CreateIcon(Texture2D icon) => Icon = new Sprite
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            Size = new ScalableVector2(20, 20),
            X = 20,
            Image = icon
        };

        private void CreateName(string name) => Name = new SpriteTextPlus(FontManager.GetWobbleFont(Fonts.LatoBlack),
            name.ToUpper(), 22)
        {
            Parent = this,
            Alignment = Alignment.MidLeft,
            X = Icon.X + Icon.Width + 14
        };

        public void Select(bool instantWidth = false)
        {
            ClearAnimations();

            var width = Image.Width + 20;

            if (instantWidth)
                Width = width;
            else
                ChangeWidthTo(width, Easing.OutQuint, 450);


            Image = SelectedButton;
            IsSelected = true;
        }

        public void Deselect()
        {
            ClearAnimations();
            ChangeWidthTo(Image.Width, Easing.OutQuint, 450);
            Image = DeselectedButton;
            IsSelected = false;
        }
    }
}