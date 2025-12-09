using System;

namespace ZweiHander.HUD
{
    // Handles HUD open/close animation state and transitions
    public class HUDAnimator
    {
        public enum AnimationState
        {
            Closed,   // HUD is closed
            Opening,  // HUD is sliding in
            Open,     // HUD is fully open
            Closing   // HUD is sliding out
        }

        private AnimationState _state;
        private float _currentYOffset;
        private float _currentBackgroundHeight;

        // Animation targets
        private readonly float _openYOffset;
        private readonly float _closedYOffset;
        private readonly float _openBackgroundHeight;
        private readonly float _closedBackgroundHeight;
        private readonly float _slideSpeed;

        public AnimationState State => _state;
        public float CurrentYOffset => _currentYOffset;
        public float CurrentBackgroundHeight => _currentBackgroundHeight;
        public bool IsAnimating => _state == AnimationState.Opening || _state == AnimationState.Closing;

        public HUDAnimator(float openYOffset, float openBackgroundHeight, float closedBackgroundHeight, float slideSpeed)
        {
            _openYOffset = openYOffset;
            _closedYOffset = 0f;
            _openBackgroundHeight = openBackgroundHeight;
            _closedBackgroundHeight = closedBackgroundHeight;
            _slideSpeed = slideSpeed;

            // Start in closed state
            _state = AnimationState.Closed;
            _currentYOffset = _closedYOffset;
            _currentBackgroundHeight = _closedBackgroundHeight;
        }

        // Trigger opening animation
        public void Open()
        {
            if (_state == AnimationState.Closed || _state == AnimationState.Closing)
            {
                _state = AnimationState.Opening;
            }
        }

        // Trigger closing animation
        public void Close()
        {
            if (_state == AnimationState.Open || _state == AnimationState.Opening)
            {
                _state = AnimationState.Closing;
            }
        }

        // Update animation state, moving toward target values
        public void Update(float deltaTime)
        {
            if (!IsAnimating) return;

            // Determine target values based on current animation direction
            bool isOpening = _state == AnimationState.Opening;
            float targetYOffset = isOpening ? _openYOffset : _closedYOffset;
            float targetBackgroundHeight = isOpening ? _openBackgroundHeight : _closedBackgroundHeight;

            // Move both values toward targets
            bool yOffsetFinished = ApplyTransition(ref _currentYOffset, targetYOffset, deltaTime);
            bool backgroundFinished = ApplyTransition(ref _currentBackgroundHeight, targetBackgroundHeight, deltaTime);

            // Transition to final state when animation completes
            if (yOffsetFinished && backgroundFinished)
            {
                _state = isOpening ? AnimationState.Open : AnimationState.Closed;
            }
        }

        // Moves current value toward target
        private bool ApplyTransition(ref float current, float target, float deltaTime)
        {
            float delta = target - current;
            float movement = Math.Sign(delta) * _slideSpeed * deltaTime;

            // Snap to target if close enough
            if (Math.Abs(movement) >= Math.Abs(delta))
            {
                current = target;
                return true;
            }

            current += movement;
            return false;
        }
    }
}
