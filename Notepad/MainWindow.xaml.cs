// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace Notepad;

/// <summary>
/// Displays the main window, containing various controls to work with.
/// </summary>
public sealed partial class MainWindow
{
    public new static MainWindow Current { get; private set; }

    public MainWindow()
    {
        if (Current != null) throw new ArgumentException("An instance of MainWindow already exists.");
        Current = this;

        this.InitializeComponent();

        this.Title = "Notepad";

        // TODO
        //AppWindowTitleBar titlebar = AppWindow.TitleBar;
        //titlebar.ExtendsContentIntoTitleBar = true;
        //this.SetTitleBar(<>);
    }
}