// <copyright file="KeyboardTests.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Tests.Core.Input.Keyboard;

using System;
using FinalEngine.Input.Keyboards;
using Moq;
using NUnit.Framework;

public class KeyboardTests
{
    private Keyboard keyboard;

    private Mock<IKeyboardDevice> keyboardDevice;

    [Test]
    public void ConstructorShouldHookOntoKeyDownEventWhenKeyboardDeviceIsNotNull()
    {
        // Assert
        this.keyboardDevice.VerifyAdd(
            x =>
        {
            x.KeyDown += It.IsAny<EventHandler<KeyEventArgs>>();
        }, Times.Once);
    }

    [Test]
    public void ConstructorShouldHookOntoKeyUpEventWhenKeyboardDeviceIsNotNull()
    {
        // Assert
        this.keyboardDevice.VerifyAdd(
            x =>
            {
                x.KeyUp += It.IsAny<EventHandler<KeyEventArgs>>();
            }, Times.Once);
    }

    [Test]
    public void DeviceKeyDownShouldSetIsCapsLockedToFalseWhenCapsLockIsFalse()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Modifiers = KeyModifiers.None,
        });

        // Act
        bool actual = this.keyboard.IsCapsLocked;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void DeviceKeyDownShouldSetIsCapsLockedToTrueWhenCapsLockIsTrue()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Modifiers = KeyModifiers.CapsLock,
        });

        // Act
        bool actual = this.keyboard.IsCapsLocked;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void DeviceKeyDownShouldSetIsNumLockedToFalseWhenNumLockIsFalse()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Modifiers = KeyModifiers.None,
        });

        // Act
        bool actual = this.keyboard.IsNumLocked;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void DeviceKeyDownShouldSetIsNumLockedToTrueWhenNumLockIsTrue()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Modifiers = KeyModifiers.NumLock,
        });

        // Act
        bool actual = this.keyboard.IsNumLocked;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void DeviceKeyDownShouldThrowArgumentNullExceptionWhenEventDataIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.keyboardDevice.Raise(
                x =>
            {
                x.KeyDown += null;
            }, new object[] { new object(), null });
        });
    }

    [Test]
    public void DeviceKeyUpShouldThrowArgumentNullExceptionWhenEventDataIsNull()
    {
        // Act and assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            this.keyboardDevice.Raise(
                x =>
            {
                x.KeyUp += null;
            }, new object[] { new object(), null });
        });
    }

    [Test]
    public void DisposeShouldDeregisterKeyDownEventWhenInvoked()
    {
        // Act
        this.keyboard.Dispose();

        // Assert
        this.keyboardDevice.VerifyRemove(
            x =>
            {
                x.KeyDown -= It.IsAny<EventHandler<KeyEventArgs>>();
            }, Times.Once);
    }

    [Test]
    public void DisposeShouldDeregisterKeyUpEventWhenInvoked()
    {
        // Act
        this.keyboard.Dispose();

        // Assert
        this.keyboardDevice.VerifyRemove(
            x =>
        {
            x.KeyUp -= It.IsAny<EventHandler<KeyEventArgs>>();
        }, Times.Once);
    }

    [Test]
    public void IsAltDownShouldReturnFalseWhenAltIsNotDownDuringCurrentFrame()
    {
        // Act
        bool actual = this.keyboard.IsAltDown;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsAltDownShouldReturnTrueWhenAltLeftIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.LeftAlt,
        });

        // Act
        bool actual = this.keyboard.IsAltDown;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsAltDownShouldReturnTrueWhenAltRightIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.RightAlt,
        });

        // Act
        bool actual = this.keyboard.IsAltDown;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsControlDownShouldReturnFalseWhenControlIsNotDownDuringCurrentFrame()
    {
        // Act
        bool actual = this.keyboard.IsControlDown;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsControlDownShouldReturnTrueWhenControlLeftIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.LeftControl,
        });

        // Act
        bool actual = this.keyboard.IsControlDown;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsControlDownShouldReturnTrueWhenControlRightIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.RightControl,
        });

        // Act
        bool actual = this.keyboard.IsControlDown;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsKeyDownShouldReturnFalseWhenKeyIsNotDownDuringCurrentFrame()
    {
        // Arrange
        const Key key = Key.V;

        // Act
        bool actual = this.keyboard.IsKeyDown(key);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsKeyDownShouldReturnTrueWhenKeyIsDownDuringCurrentFrame()
    {
        // Arrange
        const Key key = Key.A;

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        // Act
        bool actual = this.keyboard.IsKeyDown(key);

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsKeyPressedShouldReturnFalseWhenKeyIsNotDownDuringCurrentFrame()
    {
        // Arrange
        const Key key = Key.LeftAlt;

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        this.keyboard.Update();

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyUp += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        // Act
        bool actual = this.keyboard.IsKeyPressed(key);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsKeyPressedShouldReturnTrueWhenKeyIsDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
    {
        // Arrange
        const Key key = Key.G;

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        // Act
        bool actual = this.keyboard.IsKeyPressed(key);

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsKeyReleasedShouldReturnFalseWhenKeyIsDownDuringCurrentFrameAndNotDownDuringPreviousFrame()
    {
        // Arrange
        const Key key = Key.LeftBracket;

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        // Act
        bool actual = this.keyboard.IsKeyReleased(key);

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsKeyReleasedShouldReturnTrueWhenKeyIsNotDownDuringCurrentFrameAndDownDuringPreviousFrame()
    {
        // Arrange
        const Key key = Key.Apostrophe;

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        this.keyboard.Update();

        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyUp += null;
        }, new KeyEventArgs()
        {
            Key = key,
        });

        // Act
        bool actual = this.keyboard.IsKeyReleased(key);

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsShiftDownShouldReturnFalseWhenShiftIsNotDownDuringCurrentFrame()
    {
        // Act
        bool actual = this.keyboard.IsShiftDown;

        // Assert
        Assert.False(actual);
    }

    [Test]
    public void IsShiftDownShouldReturnTrueWhenShiftLeftIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.LeftShift,
        });

        // Act
        bool actual = this.keyboard.IsShiftDown;

        // Assert
        Assert.True(actual);
    }

    [Test]
    public void IsShitDownShouldReturnTrueWhenShiftRightIsDownDuringCurrentFrame()
    {
        // Arrange
        this.keyboardDevice.Raise(
            x =>
        {
            x.KeyDown += null;
        }, new KeyEventArgs()
        {
            Key = Key.RightShift,
        });

        // Act
        bool actual = this.keyboard.IsShiftDown;

        // Assert
        Assert.True(actual);
    }

    [SetUp]
    public void Setup()
    {
        // Arrange
        this.keyboardDevice = new Mock<IKeyboardDevice>();
        this.keyboard = new Keyboard(this.keyboardDevice.Object);
    }

    [TearDown]
    public void Teardown()
    {
        this.keyboard.Dispose();
    }
}
