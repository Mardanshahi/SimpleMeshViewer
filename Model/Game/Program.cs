using OpenTK;
//using OpenTK.Windowing.Common;
//using OpenTK.Windowing.Desktop;

namespace ModelProj.Game;

internal static class Program
{
    private const int FPS = 144;


    public static void Main(string[] args)
    {
        #region settings
    
        var gameSettings = GameWindowSettings.Default;
        gameSettings.RenderFrequency = FPS;
        gameSettings.UpdateFrequency = FPS;
        
        var uiSettings = NativeWindowSettings.Default;
        uiSettings.APIVersion = Version.Parse("3.3");
        uiSettings.Size = new OpenTK.Vector2(800,600);
        uiSettings.Title = "LearnOpenGL";
        uiSettings.NumberOfSamples = 4;

        uiSettings.WindowState = WindowState.Normal;
        uiSettings.WindowBorder = WindowBorder.Resizable;
        uiSettings.IsEventDriven = false;
        uiSettings.StartFocused = true;
        uiSettings.Flags = ContextFlags.ForwardCompatible;
        uiSettings.API = ContextAPI.OpenGL;
        #endregion


        using var game = new Game1();
        game.InitWindow(gameSettings, uiSettings);
           // .CursorState = CursorState.Grabbed;
        game.Run();
    }
}