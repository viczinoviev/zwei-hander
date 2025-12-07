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
        private HUDAnimator _animator; // Handles open/close animation

        // Animation constants
        private const float SLIDE_SPEED = 1200f;
        private const float CLOSED_HUD_BACKGROUND_HEIGHT = 112f;
        private const float OPEN_HUD_BACKGROUND_HEIGHT = 480f;
        
        public HUDManager(IPlayer player, HUDSprites hudSprites, bool hudOpen)
        {
            _player = player;
            _hudSprites = hudSprites;
            _isHUDOpen = hudOpen;
            _layoutManager = new HUDLayoutManager(
                GraphicsDeviceManager.DefaultBackBufferWidth,
                GraphicsDeviceManager.DefaultBackBufferHeight
            );
            _animator = new HUDAnimator(
                _layoutManager.OpenHUDAnimationOffset,
                OPEN_HUD_BACKGROUND_HEIGHT,
                CLOSED_HUD_BACKGROUND_HEIGHT,
                SLIDE_SPEED
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

            // Trigger animation
            if (_isHUDOpen)
            {
                _animator.Open();
            }
            else
            {
                _animator.Close();
            }
        }

        public void Update(GameTime gameTime)
        {
            // Update animation
            _animator.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            // Update all HUD components
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
                new Rectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width, (int)_animator.CurrentBackgroundHeight),
                Color.Black
            );

            // Draw 2px white separator line at the bottom of the HUD
            spriteBatch.Draw(
                _pixelTexture,
                new Rectangle(0, (int)_animator.CurrentBackgroundHeight, spriteBatch.GraphicsDevice.Viewport.Width, 2),
                Color.White
            );

            // Draw all HUD components with current animation offset
            Vector2 hudOffset = new Vector2(0, _animator.CurrentYOffset);
            foreach (var component in _components)
            {
                component.Draw(spriteBatch, hudOffset);
            }
        }
    }
}
