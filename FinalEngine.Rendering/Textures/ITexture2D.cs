// <copyright file="ITexture2D.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Textures;

public interface ITexture2D : ITexture
{
    Texture2DDescription Description { get; }
}
