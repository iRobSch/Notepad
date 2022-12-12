// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;
using Notepad.Actions;

namespace Notepad.GUI.Controls;

public sealed partial class MenuBar
{
    public static MenuBar Current { get; private set; }

    private readonly List<MenuFlyoutItem> _disabledItems;

    public MenuBar()
    {
        if (Current != null) throw new ArgumentException("An instance of MenuBar already exists.");
        Current = this;

        this.InitializeComponent();

        _disabledItems = new List<MenuFlyoutItem>
        {
            SaveItem, RedoItem, UndoItem, CutItem, CopyItem, PasteItem, DeleteItem, SelectItem, 
            FindItem, FindNextItem, FindPrevItem, ReplaceItem, PreferencesItem, ImportItem
        };

        SaveItem.Click += (_, _) => FileActions.SaveFile();
        SaveAsItem.Click += (_, _) => FileActions.SaveNewFile();
        Openitem.Click += (_, _) => FileActions.OpenFile();
        ReadItem.Click += EditActions.MakeReadOnly; // TODO turn into XML.

        this.DisableItems();
    }

    private void DisableItems()
    {
        foreach (MenuFlyoutItem item in _disabledItems)
        {
            item.IsEnabled = false;
        }
    }
}
