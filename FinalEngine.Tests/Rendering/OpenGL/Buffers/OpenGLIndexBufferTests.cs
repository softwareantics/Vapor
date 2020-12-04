﻿// <copyright file="OpenGLIndexBufferTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Rendering.OpenGL.Buffers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FinalEngine.Rendering.OpenGL.Buffers;
    using FinalEngine.Rendering.OpenGL.Invocation;
    using Moq;
    using NUnit.Framework;
    using OpenTK.Graphics.OpenGL4;

    [ExcludeFromCodeCoverage]
    [SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "This is done in TearDown.")]
    public class OpenGLIndexBufferTests
    {
        private const int ID = 306;

        private readonly int[] data = { 3, 5, 7, 1, 3, 6, 7, 1, 5, 3, };

        private OpenGLIndexBuffer<int> indexBuffer;

        private Mock<IOpenGLInvoker> invoker;

        [Test]
        public void BindShouldInvokeBindBufferIDWhenIndexBufferIsNotDisposed()
        {
            // Arrange
            this.invoker.Reset();

            // Act
            this.indexBuffer.Bind();

            // Assert
            this.invoker.Verify(x => x.BindBuffer(BufferTarget.ElementArrayBuffer, ID), Times.Once);
        }

        [Test]
        public void BindShouldThrowObjectDisposedExceptionWhenIndexBufferIsDisposed()
        {
            // Arrange
            this.indexBuffer.Dispose();

            // Act and assert
            Assert.Throws<ObjectDisposedException>(() => this.indexBuffer.Bind());
        }

        [Test]
        public void ConstructorShouldInvokeBindBufferIDWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.BindBuffer(BufferTarget.ElementArrayBuffer, ID), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeBindBufferZeroWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.BindBuffer(BufferTarget.ElementArrayBuffer, 0), Times.Once);
        }

        [Test]
        public void ConstructorShouldInvokeGenBufferWhenParametersAreNotNull()
        {
            // Assert
            this.invoker.Verify(x => x.GenBuffer(), Times.Once);
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenDataIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLIndexBuffer<int>(new Mock<IOpenGLInvoker>().Object, null, 0));
        }

        [Test]
        public void ConstructorShouldThrowArgumentNullExceptionWhenInvokerIsNull()
        {
            // Arrange, act and assert
            Assert.Throws<ArgumentNullException>(() => new OpenGLIndexBuffer<int>(null, Array.Empty<int>(), 0));
        }

        [Test]
        public void DisposeShouldInvokeDeleteBufferWhenInvoked()
        {
            // Act
            this.indexBuffer.Dispose();

            // Assert
            this.invoker.Verify(x => x.DeleteBuffer(ID), Times.Once);
        }

        [Test]
        public void LengthShouldReturnSameAsConstructorInputWhenInvoked()
        {
            // Act
            int actual = this.indexBuffer.Length;

            // Assert
            Assert.AreEqual(this.data.Length, actual);
        }

        [SetUp]
        public void Setup()
        {
            // Arrange
            this.invoker = new Mock<IOpenGLInvoker>();
            this.invoker.Setup(x => x.GenBuffer()).Returns(ID);

            this.indexBuffer = new OpenGLIndexBuffer<int>(this.invoker.Object, this.data, this.data.Length * sizeof(int));
        }

        [TearDown]
        public void Teardown()
        {
            this.indexBuffer.Dispose();
        }
    }
}