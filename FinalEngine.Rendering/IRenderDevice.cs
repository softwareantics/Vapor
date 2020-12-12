﻿// <copyright file="IRenderDevice.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering
{
    using System.Drawing;

    public enum PrimitiveTopology
    {
        Line,

        LineStrip,

        Triangle,

        TriangleStrip,
    }

    public interface IRenderDevice : IGPUResourceFactory, IInputAssembler, IOutputMerger, IPipeline, IRasterizer
    {
        void Clear(Color color, float depth = 1.0f, int stencil = 0);

        void DrawIndices(PrimitiveTopology topology, int first, int count);
    }
}