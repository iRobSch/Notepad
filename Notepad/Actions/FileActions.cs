using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Microsoft.UI.Xaml.Controls;
using WinRT.Interop;

namespace Notepad.Actions;

internal class FileActions
{
    /// <summary>
    /// An abbreviated variable to access the main text box.
    /// </summary>
    public static TextBox Current = GUI.Controls.Notepad.Current.TextBox;

    /// <summary>
    /// The window on which to display the various dialogs.
    /// </summary>
    private static readonly IntPtr Handler = WindowNative.GetWindowHandle(MainWindow.Current);

    /// <summary>
    /// Stores the file on the user's computer to save the content to.
    /// </summary>
    private static StorageFile _saveFile;
    /// <summary>
    /// Indicates whether the file has been saved before or not.
    /// </summary>
    private static bool _saved;

    /// <summary>
    /// Opens a file dialog to open a file from the user's computer.
    /// </summary>
    public static async void OpenFile()
    {
        FileOpenPicker picker = new()
        {
            ViewMode = PickerViewMode.Thumbnail,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
        };
        InitializeWithWindow.Initialize(picker, Handler);
        picker.FileTypeFilter.Add(".txt");
        
        StorageFile file = await picker.PickSingleFileAsync();
        if (file != null) ReadFromFile(file);
    }

    /// <summary>
    /// Opens a file dialog to save the file to the user's computer as a new text file.
    /// </summary>
    public static async void SaveNewFile()
    {
        FileSavePicker savePicker = new()
        {
            DefaultFileExtension = ".txt",
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            SuggestedFileName = "New file"
        };
        InitializeWithWindow.Initialize(savePicker, Handler);
        savePicker.FileTypeChoices.Add("Text file", new List<string> {".txt"});
        savePicker.FileTypeChoices.Add("All files", new List<string> {".*"}); // TODO

        _saveFile = await savePicker.PickSaveFileAsync();
        if (_saveFile != null) WriteToFile();

        // This part toggles the SaveItem on the menu bar so the
        // user can now freely save the file without a save picker.
        _saved = true;
        GUI.Controls.MenuBar.Current.SaveItem.IsEnabled = true;
    }

    /// <summary>
    /// Saves the text file without opening a save dialog.
    /// </summary>
    public static async void SaveFile()
    {
        if (_saved) await FileIO.WriteTextAsync(_saveFile, Current.Text);
    }

    /// <summary>
    /// Writes the content of the main text box to a local text file.
    /// </summary>
    public static async void WriteToFile()
    {
        await FileIO.WriteTextAsync(_saveFile, Current.Text);
        // TODO file.Name
    }

    /// <summary>
    /// Reads the content of a text file, and writes it to the main text box.
    /// </summary>
    /// <param name="file">The file that will be read from.</param>
    public static async void ReadFromFile(StorageFile file)
    {
        IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
        StreamReader reader = new(stream.AsStream());
        Current.Text = await reader.ReadToEndAsync();
    }
}