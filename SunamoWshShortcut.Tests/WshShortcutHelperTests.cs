// variables names: ok
namespace SunamoWshShortcut.Tests;

public class WshShortcutHelperTests
{
    [Fact]
    public void CreateLnk_CreatesShortcutPointingToTarget()
    {
        var workDirectory = Path.Combine(Path.GetTempPath(), "SunamoWshShortcutTests", Guid.NewGuid().ToString("N"));
        var targetFile = Path.Combine(workDirectory, "target.txt");
        Directory.CreateDirectory(workDirectory);
        File.WriteAllText(targetFile, "x");
        try
        {
            var shortcutPath = WshShortcutHelper.CreateLnk(Path.Combine(workDirectory, "links"), targetFile, "test");

            Assert.True(File.Exists(shortcutPath));
            Assert.Equal(targetFile, WshShortcutHelper.GetTargetPath(shortcutPath), ignoreCase: true);
        }
        finally
        {
            Directory.Delete(workDirectory, true);
        }
    }

    [Fact]
    public void CreateLnk_ExistingShortcut_IsLeftUntouched()
    {
        var workDirectory = Path.Combine(Path.GetTempPath(), "SunamoWshShortcutTests", Guid.NewGuid().ToString("N"));
        var firstTarget = Path.Combine(workDirectory, "first.txt");
        Directory.CreateDirectory(workDirectory);
        File.WriteAllText(firstTarget, "x");
        try
        {
            var shortcutPath = WshShortcutHelper.CreateLnk(workDirectory, firstTarget);
            var secondPath = WshShortcutHelper.CreateLnk(workDirectory, firstTarget);

            Assert.Equal(shortcutPath, secondPath);
        }
        finally
        {
            Directory.Delete(workDirectory, true);
        }
    }
}
