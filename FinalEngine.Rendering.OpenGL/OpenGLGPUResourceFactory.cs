﻿// <copyright file="OpenGLGPUResourceFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Rendering.OpenGL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.OpenGL.Pipeline;
    using FinalEngine.Rendering.Pipeline;

    public class OpenGLGPUResourceFactory : IGPUResourceFactory
    {
        private readonly IOpenGLInvoker invoker;

        public OpenGLGPUResourceFactory(IOpenGLInvoker invoker)
        {
            this.invoker = invoker ?? throw new ArgumentNullException(nameof(invoker), $"The specified {nameof(invoker)} parameter cannot be null.");
        }

        public IIndexBuffer CreateIndexBuffer<T>(T[] data, int sizeInBytes)
            where T : struct
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"The specified {nameof(data)} parameter cannot be null.");
            }

            return new OpenGLIndexBuffer<T>(this.invoker, data, sizeInBytes);
        }

        public IInputLayout CreateInputLayout(IEnumerable<InputElement> elements)
        {
            if (elements == null)
            {
                throw new ArgumentNullException(nameof(elements), $"The specified {nameof(elements)} parameter cannot be null.");
            }

            return new OpenGLInputLayout(this.invoker, elements);
        }

        public IShader CreateShader(PipelineTarget target, string sourceCode)
        {
            if (string.IsNullOrWhiteSpace(sourceCode))
            {
                throw new ArgumentNullException(nameof(sourceCode), $"The specified {nameof(sourceCode)} parameter cannot be null, empty or contain only whitespace");
            }

            return new OpenGLShader(this.invoker, target, sourceCode);
        }

        public IShaderProgram CreateShaderProgram(IEnumerable<IShader> shaders)
        {
            if (shaders == null)
            {
                throw new ArgumentNullException(nameof(shaders), $"The specified {nameof(shaders)} parameter cannot be null.");
            }

            return new OpenGLShaderProgram(this.invoker, shaders.Cast<IOpenGLShader>());
        }

        public IVertexBuffer CreateVertexBuffer<T>(T[] data, int sizeInBytes, int stride)
            where T : struct
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), $"The specified {nameof(data)} parameter cannot be null.");
            }

            return new OpenGLVertexBuffer<T>(this.invoker, data, sizeInBytes, stride);
        }
    }
}