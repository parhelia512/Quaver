﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework;
using Quaver.Graphics.Overlays.Chat.Components.Messages;
using Quaver.Online;
using Quaver.Online.Chat;
using Quaver.Server.Client.Structures;
using Wobble.Graphics;
using Wobble.Graphics.Sprites;
using Wobble.Graphics.Transformations;
using Wobble.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace Quaver.Graphics.Overlays.Chat.Components.Channels
{
    public class ChatChannelList : ScrollContainer
    {
        /// <summary>
        ///     Reference to the parent overlay.
        /// </summary>
        public ChatOverlay Overlay { get; }

        /// <summary>
        ///     The list of available chat channel buttons.
        /// </summary>
        public List<ChatChannelListButton> Buttons { get; }

        /// <summary>
        ///     The currently select chat channel button.
        /// </summary>
        public ChatChannelListButton SelectedButton { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="overlay"></param>
        public ChatChannelList(ChatOverlay overlay) : base(new ScalableVector2(overlay.ChannelContainer.Width + 1,
                overlay.ChannelContainer.Height - overlay.ChannelHeader.Height),
            new ScalableVector2(overlay.ChannelContainer.Width, overlay.ChannelContainer.Height - overlay.ChannelHeader.Height))
        {
            Overlay = overlay;
            Buttons = new List<ChatChannelListButton>();

            Parent = Overlay.ChannelContainer;
            Y = Overlay.ChannelHeaderContainner.Height;

            Tint = Colors.DarkGray;
            Alpha = 0.85f;

            // Scrolling Options.
            Scrollbar.Tint = Color.White;
            Scrollbar.Width = 3;
            Scrollbar.X -= 3;
            ScrollSpeed = 150;
            EasingType = Easing.EaseOutQuint;
            TimeToCompleteScroll = 1500;
        }

        /// <inheritdoc />
        ///  <summary>
        ///  </summary>
        ///  <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Only allow the container to be scrollable if the mouse is actually on top of the area.
            InputEnabled = GraphicsHelper.RectangleContains(ScreenRectangle, MouseManager.CurrentState.Position);

            base.Update(gameTime);
        }

        /// <summary>
        ///     Initializes the chat channels.
        /// </summary>
        public void InitializeChannel(ChatChannel channel, bool autoSelectChannel = true)
        {
            var button = new ChatChannelListButton(this, channel);

            // Calculate the y position of the channel
            button.Y = (ChatManager.JoinedChatChannels.Count - 1) * button.Height;

            // Automatically select the first channel that comes in.
            Overlay.ChannelMessageContainers.Add(channel, new ChatMessageContainer(Overlay, channel));

            if (autoSelectChannel)
                button.SelectChatChannel();
            else
            {
                // Reslect the current channel
                Overlay.ChannelMessageContainers[channel].Visible = false;
            }

            Buttons.Add(button);
            AddContainedDrawable(button);

            CalculateContainerHeight();
        }

        /// <summary>
        ///     Used to realign the chat channel list buttons (when closing chat channels.)
        /// </summary>
        public void RealignButtons()
        {
            for (var i = 0; i < Buttons.Count; i++)
            {
                var button = Buttons[i];

                button.Y = button.Height * i;
            }

            CalculateContainerHeight();
        }

        /// <summary>
        ///     Calculates the height of the container for proper scrolling.
        /// </summary>
        private void CalculateContainerHeight()
        {
            if (Buttons.Count == 0)
                return;

            var totalHeight = Buttons.First().Height * Buttons.Count;

            // Calculate the new height of the container based on how many channels there are.
            ContentContainer.Height = totalHeight > Height
                ? totalHeight
                : Overlay.ChannelContainer.Height - Overlay.ChannelHeader.Height;
        }
    }
}