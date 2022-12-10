using System.IO;

namespace Notepad.Actions;

internal class FileActions
{
    public static GUI.Controls.Notepad Current = GUI.Controls.Notepad.Current;

    /// <summary>
    /// Writes the content of the main text box to a StreamWriter object.
    /// </summary>
    private void WriteToFile()
    {
        StreamWriter writer = new("Notepad");
        writer.Write(Current.TextBox.Text);
        writer.Close();
    }

    /// <summary>
    /// Reads the content of a text file, and writes it to the main text box.
    /// </summary>
    /// <param name="file">The file that will be read from.</param>
    public void ReadFromFile(string file)
    {
        StreamReader reader = new(file);
        Current.TextBox.Text = reader.ReadToEnd();
        reader.Close();
    }
}
