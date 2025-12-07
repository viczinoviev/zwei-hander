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
            Closed,      // HUD closed, only the HeadUpHUD being displayed
            SlidingIn,   // HUD components moving down (opening)
            Open,        // HUD open, inventory visible
            SlidingOut   // HUD components moving up (closing)
        }

        public bool IsHUDOpen
        {
            get => _isHUDOpen;
            set => SetHUDOpen(value);
        }

        private List<IHUDComponent> _components;
        private IHUDComponent _headsUpHud;
        private IHUDComponent _healthDisplay;
        private InventoryHUD _inventoryHUD;
        private MapHUD _mapHUD;
        private HUDSprites _hudSprites;
        private IPlayer _player;
        private bool _isHUDOpen;
        private Texture2D _pixelTexture; // Cached 1x1 white pixel for drawing rectangles
        private HUDLayoutManager _layoutManager; // Calculates all HUD component positions

        // Animation state fields
        private AnimationState _animationState = AnimationState.Closed;
        private float _currentHUDYOffset = 0f; // Current Y offset for the entire HUD
        private const float CLOSED_HUD_Y_OFFSET = 0f; // HUD offset when closed
        private const float SLIDE_SPEED = 1200f; // Pixels per second

        // Background animation fields
        private float _currentBackgroundHeight = 112f; // Current animated background height
        private const float CLOSED_HUD_BACKGROUND_HEIGHT = 112f; // Background height when HUD closed
        private const float OPEN_HUD_BACKGROUND_HEIGHT = 480f; // Background height when HUD open
        
        public HUDManager(IPlayer player, HUDSprites hudSprites, bool hudOpen)
        {
            _player = player;
            _hudSprites = hudSprites;
            _isHUDOpen = hudOpen;
            _layoutManager = new HUDLayoutManager(
                GraphicsDeviceManager.DefaultBackBufferWidth,
                GraphicsDeviceManager.DefaultBackBufferHeight
            );

            BuildHUD();
        }
        
        public void SetUniverse(Map.Universe universe)
        {
            _mapHUD?.SetUniverse(universe);
        }

        private void BuildHUD()
        {
            // Use layout manager for all position calculations
            _inventoryHUD = new InventoryHUD(_hudSprites, _layoutManager.GetInventoryHUDPosition(), _player);
            _mapHUD = new MapHUD(_hudSprites, _layoutManager.GetMapHUDPosition());
            _headsUpHud = new HeadsUpHUD(_hudSprites, _layoutManager.GetHeadsUpHUDPosition(), _player);
            _healthDisplay = new HealthDisplay(_player, _hudSprites, _layoutManager.GetHealthDisplayPosition());

            // Add all components to the list
            _components = new List<IHUDComponent>
            {
                _headsUpHud,
                _inventoryHUD,
                _healthDisplay,
                _mapHUD
            };
        }

        public void SetHUDOpen(bool isOpen)
        {
            if (_isHUDOpen == isOpen)
                return; // No need to change if nothing changed

            _isHUDOpen = isOpen;

            // Trigger animation state transition
            if (_isHUDOpen)
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
            float targetHUDOffset = isSlidingIn ? _layoutManager.OpenHUDAnimationOffset : CLOSED_HUD_Y_OFFSET;
            float targetBackgroundHeight = isSlidingIn ? OPEN_HUD_BACKGROUND_HEIGHT : CLOSED_HUD_BACKGROUND_HEIGHT;

            // Animate both values toward their targets
            bool hudFinished = ApplyTransition(ref _currentHUDYOffset, targetHUDOffset, deltaTime);
            bool backgroundFinished = ApplyTransition(ref _currentBackgroundHeight, targetBackgroundHeight, deltaTime);

            // Transition to final state when animation completes
            if (hudFinished && backgroundFinished)
            {
                _animationState = isSlidingIn ? AnimationState.Open : AnimationState.Closed;
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
            // Get selected slot index and equip/use the item
            int slotIndex = _inventoryHUD.GetSelectedItemSlot();
            _player.EquipItemSlot(slotIndex);
            _player.UseEquippedItem();
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
