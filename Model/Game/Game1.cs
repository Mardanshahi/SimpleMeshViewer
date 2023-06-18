using Assimp;
using Library;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Diagnostics;
//using OpenTK.Mathematics;
//using OpenTK.Windowing.Common;

namespace ModelProj.Game;

public class Game1 : GameWindow
{
    const string ShaderLocation = "../../../Game/Shaders/";
    ShaderProgram shader;

    FirstPersonPlayer player;
    Model backpack;
    Model cube;

    Objects.Light light;
    Objects.Material material;
    private Vector2 relativeMousePosition;

    //protected GameWindow Window;

    public Game1(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)//, GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
    {
        //new Debug();
    }
    protected override void OnLoad(EventArgs e)
    {
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);

        shader = new ShaderProgram(
        ShaderLocation + "vertex.glsl",
        ShaderLocation + "fragment.glsl",
        true);

        player = new FirstPersonPlayer(shader.DefaultProjection, shader.DefaultView)
            .SetPosition(new OpenTK.Vector3(0, 0, 3))
            .SetDirection(new OpenTK.Vector3(0, 0, -1));


        light = new Objects.Light()
            .SetPosition(1f, 1f, 3f);

        material = PresetMaterial.Silver;

        // this method is not done in the best way so i will likely try and improve it in the future once i have used it more
        backpack = Model.FromFile(
            "../../../../Assets/Oricube1/", "box-uv2.obj",
            out var textures,
            shader.DefaultModel,
            new[] { TextureType.Diffuse, TextureType.Specular }
        );

        shader.UniformLight("light", light)
            .UniformMaterial("material", material, textures[TextureType.Diffuse][0], textures[TextureType.Specular][0]);

        cube = new Model(PresetMesh.Cube, shader.DefaultModel);

        // attach player functions to window
        //Window.Resize += newWin => player.Camera.Resize(newWin.Size);
    }

    protected override void OnResize(EventArgs e)
    {
        player.Camera.Resize(Width / (float)Height);

        GL.Viewport(0,0, Width, Height);
        
        base.OnResize(e);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        KeyboardState input = Keyboard.GetState();
        if (input.IsKeyDown(Key.Escape))
            Exit();
        player.Update(args, Keyboard.GetState(), relativeMousePosition);
        shader.Uniform3("cameraPos", player.Camera.Position);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Enable(EnableCap.DepthTest);
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        shader.SetActive(ShaderType.FragmentShader, "main");
        backpack.Transform(OpenTK.Vector3.Zero, OpenTK.Vector3.Zero, 1f);
        backpack.Draw();

        shader.SetActive(ShaderType.FragmentShader, "light");
        cube.Transform(light.Position, OpenTK.Vector3.Zero, 0.2f);
        cube.Draw();

        Context.SwapBuffers();
    }

    protected override void OnUnload(EventArgs e)
    {
        GL.BindVertexArray(0);
        GL.UseProgram(0);

        backpack.Delete();
        cube.Delete();

        shader.Delete();
    }

    private Vector2 startMousePos = Vector2.Zero;

    /// <summary>
    /// Get the mouse pos relative to an origin (default origin is the start mouse pos upon window creation)
    /// </summary>
    /// <returns>mouse position</returns>
    //public Vector2 GetRelativeMouse() => Window.MousePosition - startMousePos;
    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        relativeMousePosition = new Vector2(e.X, e.Y) - startMousePos;
        base.OnMouseMove(e);
    }
}
