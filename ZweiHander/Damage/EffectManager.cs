using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZweiHander.Damage;
/// <summary>
/// Stores and updates effects. Acts as a mapping between activate effects and remaining duration in seconds
/// </summary>
public class EffectManager : Dictionary<Effect, double>
{
    /// <summary>
    /// Current activate effects
    /// </summary>
    public ICollection<Effect> CurrentEffects { get => Keys; }

    /// <summary>
    /// Checks if specified effect is active
    /// </summary>
    /// <param name="effect">effect to check</param>
    /// <returns>whether effect is active</returns>
    public bool Contains(Effect effect) {  return ContainsKey(effect); }

    /// <summary>
    /// Tickers for effects that have overtime effects
    /// </summary>
    private readonly Dictionary<Effect, Ticker> Tickers = [];

    /// <summary>
    /// Effects which ticked on most recent update
    /// </summary>
    public ICollection<Effect> Ticked { get; } = [];

    /// <summary>
    /// Updates active effects; including duration and overtime effects
    /// </summary>
    /// <param name="gameTime">A snapshot of the game timing values provided by the framework.</param>
    public void Update(GameTime gameTime)
    {
        double dt = gameTime.ElapsedGameTime.TotalSeconds;
        Ticked.Clear();
        double newDuration;
        foreach (var (effect, duration) in this)
        {
            if (Tickers.TryGetValue(effect, out Ticker ticker))
            {
                if (ticker.Tick(gameTime)) Ticked.Add(effect);
            }
            else
            {
                Tickers[effect] = new Ticker(0.5);
                Ticked.Add(effect);
            }
            newDuration = duration - dt;

            if (newDuration <= 0)
            {
                Remove(effect);
                Ticked.Remove(effect);
            }
            else
            {
                this[effect] = newDuration;
            }
        }
    }
}
