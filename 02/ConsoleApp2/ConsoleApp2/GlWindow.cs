using System; 
using OpenTK; //GameWindow類
using OpenTK.Input; //鍵盤滑鼠事件交互
using OpenTK.Graphics.OpenGL; //使用OpenGL功能
namespace ConsoleApp2
{
    public class GlWindow : GameWindow 
    {
        int program = 0; //染色器程式ID 
        int buffer = 0; //暫存區域ID
        Vector3[] triangle_3d = { //立體三角形狀數據
           new Vector3 (-0.5f, -0.5f, 0.0f),
           new Vector3 ( 0.4f , -0.4f, 0.0f),
           new Vector3 (-0.1f,  0.5f , 0.0f)
        };
        protected override void OnLoad(EventArgs e)
        {
            //當窗體準備好後會被呼叫的地方
            string vert = //頂點染色器代碼
@"#version 330 core
layout (location = 0)in vec3 vertex;
void main() {
gl_Position = vec4(vertex, 1.0);
}
";
            string frag = //片段染色器代碼
@"#version 330 core
out vec4 FragColor;
void main() {
FragColor = vec4(0.5,0.7,1.0,1.0);
}
";
            int v = GL.CreateShader(ShaderType.VertexShader),
                f = GL.CreateShader(ShaderType.FragmentShader);
            //準備頂點染色器和片段染色器代碼區域
            GL.ShaderSource(v, vert); //將頂點染色器代碼寫入 下面的
            //片段染色器也需要用一樣的方式寫入
            GL.ShaderSource(f, frag);
            GL.CompileShader(v); //編譯頂點染色器代碼 
            GL.CompileShader(f); //編譯片段染色器代碼
            program = GL.CreateProgram(); //準備染色器程式區域
            GL.AttachShader(program, v); //灌入已經編譯好的頂點染色器二進制程式
            GL.AttachShader(program, f);  //灌入編譯好的片段染色器二進制程式
            GL.LinkProgram(program); //將兩個染色器聯繫起來 變成已經準備好使用的染色器程式單元 
            GL.DeleteShader(v); GL.DeleteShader(f); 
            //因為上面的兩個代碼區域已經不再需要 可以刪除掉 用於釋出顯示卡空間

            buffer = GL.GenBuffer(); //準備頂點暫存對象
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer); //選擇剛剛準備好的暫存對象 並以陣列數據暫存作為目標
            GL.BufferData(BufferTarget.ArrayBuffer,  //將數據存入到頂點暫存對象儲存區
                sizeof(float) * 3 * triangle_3d.Length,  //數據總長度
                triangle_3d, BufferUsageHint.StaticDraw); //數據本身和標記使用方式
            GL.VertexAttribPointer(0,             //設定頂點屬性指針, 選擇第一個頂點屬性
                3, VertexAttribPointerType.Float, //因為我們的數據是三維向量 這裡要寫3, 數據是單精度浮點數 要選VertexAttribPointerType.Float
                false, 3 * sizeof(float), 0); //Normalized要選擇false 否則會被限制在0到1之間, stride是要被選擇的區域 3* sizeof(float) = 3 * 4 = 12 
            //選擇12個byte數據, 最後一個參數是偏移 因為我們的頂點數據沒有其他的東西 不需要設定
            GL.EnableVertexAttribArray(0); //啟用頂點屬性陣列
            
            base.OnLoad(e);
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape) //如果用戶按下Esc
                Exit(); //關閉OpenGL工作區 停止所有迴圈
            base.OnKeyDown(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {  
            //物件動態更新迴圈, 因為沒有什麼需要動態更新的物件 只需要
            //留下下面的一段代碼就好
            base.OnUpdateFrame(e); 
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //畫面處理迴圈, 建議在這邊處理OpenGL的功能使用
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //清理顏色暫存區和深度暫存區
            GL.ClearColor(0.1f, 0.12f, 0.15f, 1.0f); //更換背景板顏色
            GL.UseProgram(program); //選擇要使用的染色器程式
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffer); //選擇要使用的物件頂點數據
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3); //畫出三角物件
            SwapBuffers(); //傳遞已經畫好的數據到畫面上
            base.OnRenderFrame(e);
        }
    }
}
