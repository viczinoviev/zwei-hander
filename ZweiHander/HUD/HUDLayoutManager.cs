using Microsoft.Xna.Framework;

namespace ZweiHander.HUD
{
    // Encapsulates all HUD component position calculations
    public class HUDLayoutManager
    {
        private readonly int _screenWidth;
        private readonly int _screenHeight;
        private readonly int _screenCenterX;

        // HUD layout constants
        private const int HEADS_UP_HUD_Y = 56;
        private const int UNPAUSED_BACKGROUND_HEIGHT = 112;

        // Health display positioning
        private const int HEALTH_DISPLAY_X_OFFSET = 104;
        private const int HEALTH_DISPLAY_Y = 72;

        // Open HUD Y positions (where components end up when slid in)
        private const int INVENTORY_HUD_OPEN_Y = 56;
        private const int MAP_HUD_OPEN_Y = 245;

        // Distance components travel during open/close animation
        public int OpenHUDAnimationOffset => _screenHeight - UNPAUSED_BACKGROUND_HEIGHT;

        public HUDLayoutManager(int screenWidth, int screenHeight)
        {
            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _screenCenterX = screenWidth / 2;
        }

        // Always-visible HUD at top
        public Vector2 GetHeadsUpHUDPosition() => new(_screenCenterX, HEADS_UP_HUD_Y);

        // Always-visible health display
        public Vector2 GetHealthDisplayPosition() => new(_screenCenterX + HEALTH_DISPLAY_X_OFFSET, HEALTH_DISPLAY_Y);

        // Inventory HUD starts off-screen, slides down to open position
        public Vector2 GetInventoryHUDPosition() => new(_screenCenterX, INVENTORY_HUD_OPEN_Y - OpenHUDAnimationOffset);

        // Map HUD starts off-screen, slides down to open position
        public Vector2 GetMapHUDPosition() => new(_screenCenterX, MAP_HUD_OPEN_Y - OpenHUDAnimationOffset);
    }
}
