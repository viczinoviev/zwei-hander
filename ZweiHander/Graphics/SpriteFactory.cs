using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace ZweiHander.Graphics;

/// <summary>
/// Tool to manage sprites and their animations.
/// </summary>
public abstract class SpriteFactory
{
    /// <summary>
    /// Stores source texture loaded
    /// </summary>
    private Texture2D _texture;

    /// <summary>
    /// Stores texture regions created
    /// </summary>
    protected Dictionary<string, TextureRegion> _regions = [];

    /// <summary>
    /// Stores animations created
    /// </summary>
    protected Dictionary<string, Animation> _animations = [];

    /// <summary>
    /// Creates a new region and adds it to this texture atlas.
    /// </summary>
    /// <param name="name">The name to give the texture region.</param>
    /// <param name="x">The top-left x-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.</param>
    /// <param name="y">The top-left y-coordinate position of the region boundary relative to the top-left corner of the source texture boundary.</param>
    /// <param name="width">The width, in pixels, of the region.</param>
    /// <param name="height">The height, in pixels, of the region.</param>
    private void AddRegion(string name, int x, int y, int width, int height)
    {
        TextureRegion region = new TextureRegion(_texture, x, y, width, height);
        _regions.Add(name, region);
    }

    /// <summary>
    /// Gets the region from this texture atlas with the specified name.
    /// </summary>
    /// <param name="name">The name of the region to retrieve.</param>
    /// <returns>The TextureRegion with the specified name.</returns>
    private TextureRegion GetRegion(string name)
    {
        return _regions[name];
    }

    /// <summary>
    /// Adds the given animation to this texture atlas with the specified name.
    /// </summary>
    /// <param name="animationName">The name of the animation to add.</param>
    /// <param name="animation">The animation to add.</param>
    private void AddAnimation(string animationName, Animation animation)
    {
        _animations.Add(animationName, animation);
    }

    /// <summary>
    /// Creates a new sprite atlas based a texture atlas xml configuration file.
    /// </summary>
    /// <param name="content">The content manager used to load the texture for the atlas.</param>
    /// <param name="fileName">The path to the xml file, relative to the content root directory.</param>
    protected void FromFile(ContentManager content, string fileName)
    {
        XElement root = LoadXmlDocument(content, fileName);
        LoadTexture(content, root);
        LoadRegions(root);
        LoadAnimations(root);
    }

    /// <summary>
    /// Loads and parses the XML document from the specified file.
    /// </summary>
    /// <param name="content">The content manager.</param>
    /// <param name="fileName">The path to the xml file, relative to the content root directory.</param>
    /// <returns>The root element of the XML document.</returns>
    private static XElement LoadXmlDocument(ContentManager content, string fileName)
    {
        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        using (XmlReader reader = XmlReader.Create(stream))
        {
            XDocument doc = XDocument.Load(reader);
            return doc.Root;
        }
    }

    /// <summary>
    /// Loads the texture specified in the XML root element.
    /// </summary>
    /// <param name="content">The content manager used to load the texture.</param>
    /// <param name="root">The root element of the XML document.</param>
    private void LoadTexture(ContentManager content, XElement root)
    {
        string texturePath = root.Element("Texture").Value;
        _texture = content.Load<Texture2D>(texturePath);
    }

    /// <summary>
    /// Loads all texture regions defined in the XML root element.
    /// </summary>
    /// <param name="root">The root element of the XML document.</param>
    private void LoadRegions(XElement root)
    {
        var regions = root.Element("Regions")?.Elements("Region");

        if (regions == null) return;

        foreach (var region in regions)
        {
            string name = region.Attribute("name")?.Value;
            int x = int.Parse(region.Attribute("x")?.Value ?? "0");
            int y = int.Parse(region.Attribute("y")?.Value ?? "0");
            int width = int.Parse(region.Attribute("width")?.Value ?? "0");
            int height = int.Parse(region.Attribute("height")?.Value ?? "0");

            if (!string.IsNullOrEmpty(name))
            {
                AddRegion(name, x, y, width, height);
            }
        }
    }

    /// <summary>
    /// Loads all animations defined in the XML root element.
    /// </summary>
    /// <param name="root">The root element of the XML document.</param>
    private void LoadAnimations(XElement root)
    {
        var animationElements = root.Element("Animations")?.Elements("Animation");

        if (animationElements == null) return;

        foreach (var animationElement in animationElements)
        {
            string name = animationElement.Attribute("name")?.Value;
            float delayInMilliseconds = float.Parse(animationElement.Attribute("delay")?.Value ?? "0");
            TimeSpan delay = TimeSpan.FromMilliseconds(delayInMilliseconds);

            List<TextureRegion> frames = ParseAnimationFrames(animationElement);

            Animation animation = new Animation(frames, delay);
            AddAnimation(name, animation);
        }
    }

    /// <summary>
    /// Parses all frame elements for a single animation.
    /// </summary>
    /// <param name="animationElement">The animation XML element.</param>
    /// <returns>A list of texture regions representing the animation frames.</returns>
    private List<TextureRegion> ParseAnimationFrames(XElement animationElement)
    {
        List<TextureRegion> frames = [];
        var frameElements = animationElement.Elements("Frame");

        if (frameElements == null) return frames;

        foreach (var frameElement in frameElements)
        {
            string regionName = frameElement.Attribute("region").Value;
            TextureRegion region = GetRegion(regionName);
            frames.Add(region);
        }

        return frames;
    }
}