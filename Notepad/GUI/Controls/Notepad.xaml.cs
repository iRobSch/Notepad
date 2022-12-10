// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;

namespace Notepad.GUI.Controls;

public sealed partial class Notepad
{
    public static Notepad Current { get; private set; }

    public Notepad()
    {
        if (Current != null) throw new ArgumentException("An instance of Notepad already exists.");
        Current = this;

        this.InitializeComponent();
    }
}
