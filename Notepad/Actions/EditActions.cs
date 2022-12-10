using Microsoft.UI.Xaml;

namespace Notepad.Actions;

internal static class EditActions
{
    public static GUI.Controls.Notepad Current = GUI.Controls.Notepad.Current;

    public static void MakeReadOnly(object sender, RoutedEventArgs args) => Current.TextBox.IsReadOnly = Current.TextBox.IsReadOnly == false;
}
