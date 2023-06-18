using OpenTK;
//using OpenTK.Windowing.Common;
//using OpenTK.Windowing.Desktop;

namespace ModelProj.Game
{
    internal static class Program
    {
        private const int FPS = 144;


        public static void Main(string[] args)
        {
            //#region settings

            //var gameSettings = GameWindowSettings.Default;
            //gameSettings.RenderFrequency = FPS;
            //gameSettings.UpdateFrequency = FPS;

            //var uiSettings = NativeWindowSettings.Default;
            //uiSettings.APIVersion = Version.Parse("3.3");
            //uiSettings.Size = new OpenTK.Vector2(800,600);
            //uiSettings.Title = "LearnOpenGL";
            //uiSettings.NumberOfSamples = 4;

            //uiSettings.WindowState = WindowState.Normal;
            //uiSettings.WindowBorder = WindowBorder.Resizable;
            //uiSettings.IsEventDriven = false;
            //uiSettings.StartFocused = true;
            //uiSettings.Flags = ContextFlags.ForwardCompatible;
            //uiSettings.API = ContextAPI.OpenGL;
            //#endregion

            // This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block.
            using (var game = new Game1(1280, 720, "Textures Slice Classification"))
            {
                //Run takes a double, which is how many frames per second it should strive to reach.
                //You can leave that out and it'll just update as fast as the hardware will allow it.
                game.Run(60.0);
            }

        }
    }
}