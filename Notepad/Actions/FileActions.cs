using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using WinRT.Interop;

namespace Notepad.Actions;

internal class FileActions
{
    /// <summary>
    /// An abbreviated variabele to access to main text box.
    /// </summary>
    public static GUI.Controls.Notepad Current = GUI.Controls.Notepad.Current;
    /// <summary>
    /// The window on which to display the various dialogs.
    /// </summary>
    private static readonly IntPtr Handler = WindowNative.GetWindowHandle(MainWindow.Current);

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
        FileSavePicker picker = new()
        {
            DefaultFileExtension = ".txt",
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
            SuggestedFileName = "New file"
        };
        InitializeWithWindow.Initialize(picker, Handler);
        picker.FileTypeChoices.Add("Text file", new List<string> {".txt"});

        StorageFile file = await picker.PickSaveFileAsync();
        if (file != null) WriteToFile(file);
    }

    /// <summary>
    /// Saves the text file without opening a save dialog.
    /// </summary>
    public static async void SaveFile()
    {
        // TODO
    }

    /// <summary>
    /// Writes the content of the main text box to a local text file.
    /// </summary>
    /// <param name="file">The file to save the content to.</param>
    public static async void WriteToFile(StorageFile file)
    {
        CachedFileManager.DeferUpdates(file);
        await FileIO.WriteTextAsync(file, Current.TextBox.Text);
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
        Current.TextBox.Text = await reader.ReadToEndAsync();
    }
}