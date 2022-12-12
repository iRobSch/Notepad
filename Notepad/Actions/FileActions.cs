using System;
using System.IO;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using WinRT.Interop;

namespace Notepad.Actions;

internal class FileActions
{
    public static GUI.Controls.Notepad Current = GUI.Controls.Notepad.Current;

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
        InitializeWithWindow.Initialize(picker, Handler); // Select the window on which to display the dialog.
        picker.FileTypeFilter.Add(".txt");
        
        StorageFile file = await picker.PickSingleFileAsync();
        if (file != null) ReadFromFile(file);
    }

    /// <summary>
    /// Writes the content of the main text box to a StreamWriter object.
    /// </summary>
    private void WriteToFile()
    {
        //Stream stream = new FileStream(FileMode.Open);
        StreamWriter writer = new("Notepad");
        writer.Write(Current.TextBox.Text);
        writer.Close();
    }

    /// <summary>
    /// Reads the content of a text file, and writes it to the main text box.
    /// </summary>
    /// <param name="file">The file that will be read from.</param>
    public static async void ReadFromFile(StorageFile file)
    {
        IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read);
        StreamReader reader = new(stream.AsStream());
        GUI.Controls.Notepad.Current.TextBox.Text = await reader.ReadToEndAsync();
    }
}
