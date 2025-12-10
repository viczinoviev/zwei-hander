using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Damage;

/// <summary>
/// Handles ticking events
/// </summary>
/// <param name="frequency">Frequency of ticks in seconds</param>
/// <param name="start_with_tick">Whether to right at or after a tick</param>
public class Ticker(double frequency = 0, bool start_with_tick = false)
{
    /// <summary>
    /// Countdown until next tick
    /// </summary>
    private double Countdown = start_with_tick ? 0 : frequency;

    /// <summary>
    /// Frequency of ticks in seconds
    /// </summary>
    private double Frequency = frequency;

    /// <summary>
    /// Reset this ticker
    /// </summary>
    public void Reset() {  Countdown = Frequency; }

    /// <summary>
    /// Updates this ticker.
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    /// <returns>If this ticked during the duration specified</returns>
    public bool Tick(GameTime gameTime)
    {
        Countdown -= gameTime.ElapsedGameTime.TotalSeconds;
        if (Countdown <= 0)
        {
            Countdown += Frequency;
            return true;
        }
        return false;
    }
}
