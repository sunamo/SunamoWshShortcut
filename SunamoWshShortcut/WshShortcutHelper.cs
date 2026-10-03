namespace SunamoWshShortcut;

/// <summary>
/// Vytváří zástupce (.lnk) ve Windows přes Windows Script Host s pozdní vazbou, takže knihovna nepotřebuje COM interop assembly ani Fody.Costura.
/// </summary>
public static class WshShortcutHelper
{
    /// <summary>
    /// Vytvoří v adresáři <paramref name="sourceDirectory"/> zástupce na <paramref name="targetFile"/> a vrátí cestu k souboru .lnk; existuje-li už, nechá ho beze změny.
    /// </summary>
    /// <param name="sourceDirectory">Adresář, kam se zástupce uloží (vytvoří se, pokud neexistuje).</param>
    /// <param name="targetFile">Soubor, na který zástupce ukazuje.</param>
    /// <param name="description">Popis zástupce.</param>
    /// <returns>Cesta k souboru .lnk.</returns>
    public static string CreateLnk(string sourceDirectory, string targetFile, string description = "")
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceDirectory);
        ArgumentException.ThrowIfNullOrEmpty(targetFile);

        Directory.CreateDirectory(sourceDirectory);
        var shortcutPath = Path.Combine(sourceDirectory, Path.GetFileNameWithoutExtension(targetFile) + ".lnk");
        if (File.Exists(shortcutPath))
        {
            return shortcutPath;
        }

        var shellType = Type.GetTypeFromProgID("WScript.Shell")
            ?? throw new InvalidOperationException("Windows Script Host (WScript.Shell) není dostupný.");
        dynamic shell = Activator.CreateInstance(shellType)!;
        try
        {
            dynamic shortcut = shell.CreateShortcut(shortcutPath);
            try
            {
                shortcut.Description = description;
                shortcut.WorkingDirectory = Path.GetDirectoryName(targetFile) ?? string.Empty;
                shortcut.TargetPath = targetFile;
                shortcut.Save();
            }
            finally
            {
                Marshal.FinalReleaseComObject(shortcut);
            }
        }
        finally
        {
            Marshal.FinalReleaseComObject(shell);
        }

        return shortcutPath;
    }

    /// <summary>
    /// Přečte cíl existujícího zástupce (.lnk); vrátí prázdný řetězec, když ho nelze zjistit.
    /// </summary>
    /// <param name="shortcutPath">Cesta k souboru .lnk.</param>
    /// <returns>Cesta, na kterou zástupce ukazuje.</returns>
    public static string GetTargetPath(string shortcutPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(shortcutPath);

        var shellType = Type.GetTypeFromProgID("WScript.Shell")
            ?? throw new InvalidOperationException("Windows Script Host (WScript.Shell) není dostupný.");
        dynamic shell = Activator.CreateInstance(shellType)!;
        try
        {
            dynamic shortcut = shell.CreateShortcut(shortcutPath);
            try
            {
                return (string)shortcut.TargetPath ?? string.Empty;
            }
            finally
            {
                Marshal.FinalReleaseComObject(shortcut);
            }
        }
        finally
        {
            Marshal.FinalReleaseComObject(shell);
        }
    }
}
