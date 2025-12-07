using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Numerics;
using ZweiHander.Graphics.SpriteStorages;
using ZweiHander.PlayerFiles;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace ZweiHander.HUD
{
    public class HUDManager
    {
        private enum AnimationState
        {
            Unpaused,    // Game running, only the HeadUpHUD being displayed
            SlidingIn,   // HUD components moving down
            Paused,      // Game is paused, inventory visible
            SlidingOut   // Transitioning back to running state, components  are moving up
        }

        public bool Paused
        {
            get => _paused;
            set => SetPaused(value);
        }

        private List<IHUDComponent> _components;
        private IHUDComponent _headsUpHud;
        private IHUDComponent _healthDisplay;
        private InventoryHUD _inventoryHUD;
        private MapHUD _mapHUD;
        private HUDSprites _hudSprites;
        private IPlayer _player;
        private bool _paused;
        private Texture2D _pixelTexture; // Cached 1x1 white pixel for drawing rectangles

        /**
         * Screen size is 480 pixels. The HeadUpHUD height is 56. Sprites are all scaled by x2
         * So the distance the HUD components needs to travel to reach the bottom of the screen is
         * 480 - 2(56) = 368
         */

        // Fields for the HUD items. 
        private AnimationState _animationState = AnimationState.Unpaused;
        private float _currentHUDYOffset = 0f; // Current Y offset for the entire HUD
        private const float UNPAUSED_HUD_Y_OFFSET = 0f; // HUD offset when unpaused
        private const float PAUSED_HUD_Y_OFFSET = 368f; // HUD offset when paused (moves down)
        private const float SLIDE_SPEED = 1200f; // Pixels per second

        // Fields for the black background.
        private float _currentBackgroundHeight = 112f; // Current animated background height
        private const float UNPAUSED_BACKGROUND_HEIGHT = 112f; // Background height when unpaused
        private const float PAUSED_BACKGROUND_HEIGHT = 480f; // Background height when paused

        // HUD layout constants

        public const int HEADS_UP_HUD_X = 256;
        public const int HEADS_UP_HUD_Y = 56;

        private const int HEALTH_DISPLAY_X_OFFSET = 104; // X position for health display
        private const int HEALTH_DISPLAY_Y = 72; // Y position for health display
        
        public HUDManager(IPlayer player, HUDSprites hudSprites, bool gamePaused)
        {
            _player = player;
            _hudSprites = hudSprites;
            _paused = gamePaused;

            BuildHUD();
        }
        
        public void SetUniverse(Map.Universe universe)
        {
            _mapHUD?.SetUniverse(universe);
        }

        private void BuildHUD()
        {
            int screen_center_x = GraphicsDeviceManager.DefaultBackBufferWidth / 2;

            // TODO: calculate these values dynamically, consider changing access params for sprites in components
            
            // InventoryHUD: base + 368 = 56, so base = -312 (off-screen above)
            // MapHUD: base + 368 = 245, so base = -123 (off-screen above)
            _inventoryHUD = new InventoryHUD(_hudSprites, new Vector2(screen_center_x, -312), _player);
            _mapHUD = new MapHUD(_hudSprites, new Vector2(screen_center_x, -123));
            
            _headsUpHud = new HeadsUpHUD(_hudSprites, new Vector2(screen_center_x, HEADS_UP_HUD_Y), _player);
            _healthDisplay = new HealthDisplay(
                _player,
                _hudSprites,
                new Vector2(screen_center_x + HEALTH_DISPLAY_X_OFFSET, HEALTH_DISPLAY_Y)
            );

            // Add all components to the list
            _components = new List<IHUDComponent>
            {
            
                _headsUpHud,
                _inventoryHUD,
                _healthDisplay,
                _mapHUD
            };
        }

        public void SetPaused(bool paused)
        {
            if (_paused == paused)
                return; // No need to change if nothing changed

            _paused = paused;

            // Trigger animation state transition
            if (_paused)
            {
                _animationState = AnimationState.SlidingIn;
            }
            else
            {
                _animationState = AnimationState.SlidingOut;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsAnimating())
            {
                UpdateAnimation(gameTime);
            }

            UpdateComponents(gameTime);
        }

        /// <summary>
        /// Checks if the HUD is currently animating
        /// </summary>
        private bool IsAnimating()
        {
            return _animationState == AnimationState.SlidingIn ||
                   _animationState == AnimationState.SlidingOut;
        }

        /// <summary>
        /// Updates the HUD animation (offset and background height)
        /// </summary>
        private void UpdateAnimation(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Determine target values based on animation direction
            bool isSlidingIn = _animationState == AnimationState.SlidingIn;
            float targetHUDOffset = isSlidingIn ? PAUSED_HUD_Y_OFFSET : UNPAUSED_HUD_Y_OFFSET;
            float targetBackgroundHeight = isSlidingIn ? PAUSED_BACKGROUND_HEIGHT : UNPAUSED_BACKGROUND_HEIGHT;

            // Animate both values toward their targets
            bool hudFinished = ApplyTransition(ref _currentHUDYOffset, targetHUDOffset, deltaTime);
            bool backgroundFinished = ApplyTransition(ref _currentBackgroundHeight, targetBackgroundHeight, deltaTime);

            // Transition to final state when animation completes
            if (hudFinished && backgroundFinished)
            {
                _animationState = isSlidingIn ? AnimationState.Paused : AnimationState.Unpaused;
            }
        }
        
        /// <summary>
        /// Updates currentValue toward targetValue at SLIDE_SPEED
        /// </summary>
        /// <param name="currentValue">Current value to update</param>
        /// <param name="targetValue">Target value to reach</param>
        /// <param name="deltaTime">Time elapsed since last frame</param>
        /// <returns>True when transition is complete</returns>
        private bool ApplyTransition(ref float currentValue, float targetValue, float deltaTime)
        {
            float delta = targetValue - currentValue;
            float movement = Math.Sign(delta) * SLIDE_SPEED * deltaTime;

            // snap to destination if close enough
            if (Math.Abs(movement) >= Math.Abs(delta))
            {
                currentValue = targetValue;
                return true;
            }

            // Apply movement toward target
            currentValue += movement;
            return false;
        }

        /// <summary>
        /// Updates all HUD components
        /// </summary>
        private void UpdateComponents(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public void SelectNextInventoryItem()
        {
            _inventoryHUD.SelectNext();
        }

        public void SelectPreviousInventoryItem()
        {
            _inventoryHUD.SelectPrevious();
        }

        public void ConfirmInventorySelection()
        {
            var selected = _inventoryHUD.GetSelectedItem();
            if (!selected.HasValue)
                return;

            switch (selected.Value)
            {
                case InventoryHUD.OrderedUsable.Bow:
                    // Bow uses UsingItem1 in your PlayerHandler
                    _player.UseItem1();
                    break;

                case InventoryHUD.OrderedUsable.Boomerang:
                    // Boomerang uses UsingItem2
                    _player.UseItem2();
                    break;
                case InventoryHUD.OrderedUsable.Bomb:
                    _player.UseItem3();
                    break;
                case InventoryHUD.OrderedUsable.Fire:
                    // Fire uses UsingItem4
                    _player.UseItem4();
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Lazy initialization of pixel texture (only created once)
            if (_pixelTexture == null)
            {
                _pixelTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                _pixelTexture.SetData(new[] { Color.White });
            }

            // Draws a black ground, since some HUD components don't span the whole screen horizontally
            spriteBatch.Draw(
                _pixelTexture,
                new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, (int)_currentBackgroundHeight),
                Color.Black
            );

            // Draw 2px white separator line at the bottom of the HUD
            spriteBatch.Draw(
                _pixelTexture,
                new Rectangle(0, (int)_currentBackgroundHeight, spriteBatch.GraphicsDevice.Viewport.Width, 2),
                Color.White
            );

            // Draw all HUD components
            Vector2 hudOffset = new Vector2(0, _currentHUDYOffset);
            foreach (var component in _components)
            {
                component.Draw(spriteBatch, hudOffset);
            }
        }
    }
}
