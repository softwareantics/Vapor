﻿// <copyright file="Program.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace TestGame
{
    using System;
    using FinalEngine.Input.Keyboard;
    using FinalEngine.Platform.Desktop.OpenTK;
    using FinalEngine.Platform.Desktop.OpenTK.Invocation;
    using OpenTK.Mathematics;
    using OpenTK.Windowing.Common;
    using OpenTK.Windowing.Desktop;

    /// <summary>
    ///     The main program.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///     Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            var settings = new NativeWindowSettings()
            {
                WindowBorder = WindowBorder.Fixed,
                WindowState = WindowState.Normal,

                Size = new Vector2i(1024, 768),

                StartVisible = true,
            };

            var nativeWindow = new NativeWindowInvoker(settings);
            var window = new OpenTKWindow(nativeWindow);

            var keyboardDevice = new OpenTKKeyboardDevice(nativeWindow);
            var mouseDevice = new OpenTKMouseDevice(nativeWindow);

            Keyboard.Initialize(keyboardDevice);

            mouseDevice.ButtonUp += (s, e) => Console.WriteLine(e.Button);

            mouseDevice.ButtonDown += (s, e) => Console.WriteLine(e.Button);

            mouseDevice.Scroll += (s, e) => Console.WriteLine(e.Offset);

            mouseDevice.Move += (s, e) => Console.WriteLine(e.Location);

            while (!window.IsExiting)
            {
                window.ProcessEvents();

                if (Keyboard.IsKeyDown(Key.A))
                {
                    Console.WriteLine("A is down");
                }

                if (Keyboard.IsKeyReleased(Key.A))
                {
                    Console.WriteLine("A is released");
                }

                if (Keyboard.IsKeyPressed(Key.B))
                {
                    Console.WriteLine("B is pressed.");
                }

                Keyboard.Update();
            }

            window.Dispose();
            nativeWindow.Dispose();
        }
    }
}