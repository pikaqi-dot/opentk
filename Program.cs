using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ShaderExample
{
    public class Program
    {
        private  static  Shader _shader=null!;
        private static int _vertexArrayObject; // VAO
        private static int _vertexBufferObject; // VBO
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
            // 定义顶点数据（一个简单的三角形）
            float[] vertices = {
                -0.5f, -0.5f, 0.0f, // 左下角
                 0.5f, -0.5f, 0.0f, // 右下角
                 0.0f,  0.5f, 0.0f  // 顶部
            };

            // 生成并绑定 VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            // 生成并绑定 VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            // 将顶点数据复制到 VBO
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // 设置顶点属性指针
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // 解绑 VBO 和 VAO
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }
        private static void OnRenderFrame(GameWindow window, FrameEventArgs args)
        {
            // 清空屏幕
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // 使用着色器程序
            _shader?.Use();

            // 绑定 VAO
            GL.BindVertexArray(_vertexArrayObject);

            // 绘制三角形
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            // 解绑 VAO
            GL.BindVertexArray(0);

            // 交换缓冲区
            window.SwapBuffers();
        }
    }
}
