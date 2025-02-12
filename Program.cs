using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ShaderExample
{
    public class Program
    {
        private static Shader _shader;
        private static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "OpenTK Shader Example"
            };

            using (var window = new GameWindow(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Load += OnLoad;
                window.RenderFrame += (FrameEventArgs e) => OnRenderFrame(window, e);
                window.Run();
            }
        }
        private static void OnLoad()
        {
            // Use relative paths to the shaders
            string vertexPath = Path.Combine("shaders", "vertex_shader.glsl");
            string fragmentPath = Path.Combine("shaders", "fragment_shader.glsl");

            _shader = new Shader(vertexPath, fragmentPath);
        }
        private static void OnRenderFrame(GameWindow window, FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Render your scene here
            _shader.Use();
            Console.WriteLine("Rendering frame");

            // Swap buffers
            window.SwapBuffers();
        }
    }
}
