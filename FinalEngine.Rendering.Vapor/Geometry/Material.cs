// <copyright file="Material.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.Vapor.Geometry;

using System;
using FinalEngine.Rendering.Textures;
using FinalEngine.Resources;

public sealed class Material : IMaterial
{
    private static readonly ITexture2D DefaultDiffuseTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_diffuse.png");

    private static readonly ITexture2D DefaultNormalTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_normal.png");

    private static readonly ITexture2D DefaultSpecularTexture = ResourceManager.Instance.LoadResource<ITexture2D>("Resources\\Textures\\default_specular.png");

    private ITexture2D? diffuseTexture;

    private ITexture2D? normalTexture;

    private ITexture2D? specularTexture;

    public Material()
    {
        this.Shininess = 32.0f;
    }

    public ITexture2D DiffuseTexture
    {
        get { return this.diffuseTexture ??= DefaultDiffuseTexture; }
        set { this.diffuseTexture = value; }
    }

    public ITexture2D NormalTexture
    {
        get { return this.normalTexture ??= DefaultNormalTexture; }
        set { this.normalTexture = value; }
    }

    public float Shininess { get; set; }

    public ITexture2D SpecularTexture
    {
        get { return this.specularTexture ??= DefaultSpecularTexture; }
        set { this.specularTexture = value; }
    }

    public void Bind(IPipeline pipeline)
    {
        ArgumentNullException.ThrowIfNull(pipeline, nameof(pipeline));

        pipeline.SetTexture(this.DiffuseTexture, 0);
        pipeline.SetTexture(this.SpecularTexture, 1);
        pipeline.SetTexture(this.NormalTexture, 2);
    }
}
