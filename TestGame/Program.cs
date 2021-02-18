﻿// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Input.Mouse;
    using FinalEngine.IO;
    using FinalEngine.IO.Invocation;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using FinalEngine.Rendering;
    using FinalEngine.Rendering.Buffers;
    using FinalEngine.Rendering.OpenGL;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using FinalEngine.Rendering.Pipeline;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;
    using OpenTK.Windowing.GraphicsLibraryFramework;

    internal static class Program
    {
        private static void Main()
        {
            var settings = new NativeWindowSettings()
            {
                API = ContextAPI.OpenGL,
                APIVersion = new Version(4, 6),

                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,

                AutoLoadBindings = false,

                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new OpenTK.Mathematics.Vector2i(1280, 720),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);
            var window = new OpenTKWindow(nativeWindow);

            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            var mouseDevice = new OpenTKMouseDevice(nativeWindow);

            var keyboard = new Keyboard(keyboardDevice);
            var mouse = new Mouse(mouseDevice);

            var file = new FileInvoker();
            var directory = new DirectoryInvoker();

            var fileSystem = new FileSystem(file, directory);

            var opengl = new OpenGLInvoker();
            var bindings = new GLFWBindingsContext();

            var renderContext = new OpenGLRenderContext(opengl, bindings, nativeWindow.Context);
            var renderDevice = new OpenGLRenderDevice(opengl);

            IInputAssembler inputAssembler = renderDevice.InputAssembler;
            IRasterizer rasterizer = renderDevice.Rasterizer;
            IOutputMerger outputMerger = renderDevice.OutputMerger;
            IPipeline pipeline = renderDevice.Pipeline;
            IGPUResourceFactory factory = renderDevice.Factory;

            IShaderProgram? program = factory.CreateShaderProgram(
                new List<IShader>()
                {
                    factory.CreateShader(PipelineTarget.Vertex, File.ReadAllText("Resources\\Shaders\\shader.vert")),
                    factory.CreateShader(PipelineTarget.Fragment, File.ReadAllText("Resources\\Shaders\\shader.frag")),
                });

            pipeline.SetShaderProgram(program);

            IInputLayout? inputLayout = factory.CreateInputLayout(
                new List<InputElement>()
                {
                    new InputElement(0, 3, InputElementType.Float, 0),
                    new InputElement(1, 4, InputElementType.Float, 3 * sizeof(float)),
                });

            inputAssembler.SetInputLayout(inputLayout);

            float[] vertices =
            {
                -0.5f, -0.5f, 0.0f, 1, 0, 0, 1,
                0.5f, -0.5f, 0.0f, 0, 1, 0, 1,
                0.0f, 0.5f, 0.0f, 0, 0, 1, 1,
            };

            int[] indices =
            {
                0, 1, 2,
            };

            IVertexBuffer vertexBuffer = factory.CreateVertexBuffer(BufferUsageType.Dynamic, Array.Empty<float>(), 1000 * sizeof(float), 7 * sizeof(float));
            IIndexBuffer indexBuffer = factory.CreateIndexBuffer(BufferUsageType.Dynamic, Array.Empty<int>(), 3 * sizeof(int));

            while (!window.IsExiting)
            {
                keyboard.Update();
                mouse.Update();

                inputAssembler.UpdateVertexBuffer(vertexBuffer, vertices, 7 * sizeof(float));
                inputAssembler.UpdateIndexBuffer(indexBuffer, indices);

                inputAssembler.SetVertexBuffer(vertexBuffer);
                inputAssembler.SetIndexBuffer(indexBuffer);

                renderDevice.Clear(Color.CornflowerBlue);
                renderDevice.DrawIndices(PrimitiveTopology.Triangle, 0, indexBuffer.Length);

                renderContext.SwapBuffers();
                window.ProcessEvents();
            }

            renderContext.Dispose();
            window.Dispose();
            nativeWindow.Dispose();
        }
    }
}