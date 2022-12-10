// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using System.Collections.Generic;
using Microsoft.UI.Xaml.Controls;

namespace Notepad.GUI.Controls;

public sealed partial class MenuBar
{
    private readonly List<MenuFlyoutItem> _disabledItems;

    public MenuBar()
    {
        this.InitializeComponent();

        _disabledItems = new List<MenuFlyoutItem>
        {
            RedoItem, UndoItem, CutItem, CopyItem, PasteItem, DeleteItem, SelectItem, 
            ReadItem, FindItem, FindNextItem, FindPrevItem, ReplaceItem, PreferencesItem, ImportItem
        };

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
