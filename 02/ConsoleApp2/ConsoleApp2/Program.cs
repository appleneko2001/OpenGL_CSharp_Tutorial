using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args) //程式啟動入口 在這裡開始運作代碼
        {
            using(GlWindow window = new GlWindow()) 
            {
                //暫時性建立GlWindow物件 離開這一段代碼之後
                //物件會自己清理並不再可用 
                window.Run(); //啟動OpenGL工作區 啟動所有事件迴圈

                //如果需要限制事件迴圈的頻率 可以使用 window.Run(30)
                //將畫面處理和物件動態更新迴圈控制在30次一秒 又或者 
                //可以使用這個 window.Run(30, 60) 將物件動態更新控制
                //在30次一秒 而畫面處理將控制在60次一秒
            }
            //離開上面一段代碼之後 到達這裡將會停止程式運作並關閉
        }
    }
}
