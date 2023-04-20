using System;
using System.IO;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using MathCore.WinAPI.pInvoke;
using MathCore.WinAPI.Windows;
using System.Text;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GetProcess
{
    class Program
    {
        public struct MSG
        {
            IntPtr hwnd;
            uint message;
            IntPtr wParam;
            IntPtr lParam;
            int time;
            POINT pt;
        }

    static Color GenerateRandomColor()
        {
            Random rand = new Random();
            int r = rand.Next(0, 100000000) % 255;
            int g = rand.Next(0, 100000000) % 255;
            int b = rand.Next(0, 100000000) % 255;
            Color color = Color.FromArgb(r, g, b);
            return color;
        }
        static void DrawAllRectangles(IntPtr[] windows)
        {
            int i = 0;
            foreach(IntPtr windowPTR in windows)
            {
                List<IntPtr> recursive_ptrs = User32.GetAllChildHandles(windowPTR);
                if (recursive_ptrs.Count > 0) 
                {
                    DrawAllRectangles(recursive_ptrs.ToArray());
                }
                Window window = new Window(windowPTR);
                Graphics g = Graphics.FromHwnd(IntPtr.Zero);
                g.DrawRectangle(new Pen(GenerateRandomColor(), 3f), window.Rectangle);
                g.DrawString(i.ToString(), new Font("Arial", 14), new SolidBrush(Color.Red), new PointF(window.Rectangle.X + 10, window.Rectangle.Y + 10));
                i++;
            }
        } //2 - textbox /
        static void Main(string[] args)
        {
            //Process[] p = Process.GetProcessesByName("FTK Imager");

            Process ftk_imager = Process.Start("C:/Users/ilya/Desktop/FtkImager/FTK Imager.exe");

            Process macros = new Process();
            ProcessStartInfo macros_info = new ProcessStartInfo();
            macros_info.FileName = "C:\\Program Files (x86)\\MacroRecorder\\MacroRecorder.exe";
            macros_info.Arguments = "-play=\"C:\\Users\\ilya\\Desktop\\GetProcess\\Macros\\image_mounting.mrf\"";
            macros.StartInfo = macros_info;
            macros.Start();

           

            IntPtr mainWindow = ftk_imager.MainWindowHandle;
            //while(true)
            //{
            //    User32.MSG mes;
            //    User32.GetMessage(out mes, mainWindow, 0, 10);
            //    Console.WriteLine(mes);
            //}
            Window windowMain = new Window(mainWindow);

            windowMain.Rectangle = new Rectangle(0, 0, 500, 500);

            List<IntPtr> windows = User32.GetAllChildHandles(mainWindow);
            //DrawAllRectangles(windows.ToArray());

            Window[] modal = Window.Find(w => w.Text.ToLower().Contains("mount image to drive"));
            IntPtr modalPTR = modal[0].Handle;
            var handles = User32.GetAllChildHandles(modalPTR);
            DrawAllRectangles(handles.ToArray());
            Window TextBox = new Window(handles[2]);
            List<Window> wins = new List<Window>();
            foreach(var h in handles)
            {
                wins.Add(new Window(h));
            }
            wins[3].Click();
            TextBox.Text = "C:\\Users\\ilya\\Downloads\\image1.E01";
            
            //IntPtr panelWithButtons = windows[10];
            //var menu = new Window(panelWithButtons);

            //var a = new PointF[] { new PointF(menu.Location.X + 58, menu.Location.Y + 10), new PointF(menu.Location.X + 57, menu.Location.Y + 9), new PointF(menu.Location.X + 59, menu.Location.Y + 11) };
            //graph2.DrawPolygon(new Pen(Color.Red, 3f), a);
            //var pPoint = GCHandle.Alloc(new Point(60, 12));
            //var lParam = GCHandle.ToIntPtr(pPoint);
            //User32.SendMessage(panelWithButtons, WM.LBUTTONDBLCLK, IntPtr.Zero, lParam);
            //menu.Click(new Point(menu.Location.X + 60, menu.Location.Y + 12));
            //pPoint.Free();

            //int r=0,g=0,b=0;
            //Color color = new Color();

            //int i = 0;
            //foreach (IntPtr window in windows)
            //{
            //    Window element = new Window(window);
            //    Graphics graph = Graphics.FromHwnd(IntPtr.Zero);
            //    Random rand = new Random();
            //    r = rand.Next(0, 100000000) % 255;
            //    g = rand.Next(0, 100000000) % 255;
            //    b = rand.Next(0, 100000000) % 255;
            //    color = Color.FromArgb(r, g, b);

            //    graph.DrawRectangle(new Pen(color, 3f), element.Rectangle);
            //    graph.DrawString(i.ToString(), new Font("Arial", 14), new SolidBrush(Color.Red), new PointF(element.Rectangle.X + 10, element.Rectangle.Y + 10));
            //    element.Click();
            //    i++;
            //}


            //IntPtr panelWithButtons = windows[10];
            //var pPoint = GCHandle.Alloc(new Point(58, 10));
            //var lParam = GCHandle.ToIntPtr(pPoint);
            //User32.SendMessage(panelWithButtons, WM.LBUTTONDOWN, IntPtr.Zero, lParam);
            //User32.SendMessage(panelWithButtons, WM.NCHITTEST, IntPtr.Zero, IntPtr.Zero);
            //User32.SendMessage(panelWithButtons, WM.ERASEBKGND, IntPtr.Zero, IntPtr.Zero);
            //pPoint.Free();


            //var menu = new Window(panelWithButtons);
            //User32.SetFocus(panelWithButtons);
            //menu.Click(new Point(menu.Location.X+20, menu.Location.Y+1));
            //var m2 = User32.GetMenu(panelWithButtons);
            //var mm2 = new Window(m2);
            //List<IntPtr> btns = User32.GetAllChildHandles(m2);
            //IntPtr oneMorePanel = User32.GetAllChildHandles(panelWithButtons)[0];
            //var menu = new Window(User32.GetMenu(oneMorePanel));
            //User32.

            //List<IntPtr> buttons = User32.GetAllChildHandles(panelWithButtons);


            //int i = 0;
            //foreach (IntPtr window in buttons)
            //{
            //    Window element = new Window(window);
            //    Graphics graph = Graphics.FromHwnd(IntPtr.Zero);
            //    Random rand = new Random();
            //    r = rand.Next(0, 100000000) % 255;
            //    g = rand.Next(0, 100000000) % 255;
            //    b = rand.Next(0, 100000000) % 255;
            //    color = Color.FromArgb(r, g, b);

            //    graph.DrawRectangle(new Pen(color, 3f), element.Rectangle);
            //    graph.DrawString(i.ToString(), new Font("Arial", 14), new SolidBrush(Color.Red), new PointF(element.Rectangle.X + 10, element.Rectangle.Y + 10));
            //    element.Click();
            //    i++;
            //}




            /*var p = Process.GetProcessesByName("mspaint");

            foreach(var proc in p)
            {
                proc.Kill();
            }

            var window = new Window(p[0].MainWindowHandle);
            //var window = Window.Find(x => x.Text.ToLower().Contains("photoshow"))[0];

            window.Text = "Пиписька";



            if (window != null)
            {
                var allChildWindows = User32.GetAllChildHandles(window.Handle);

                var number3 = new Window(allChildWindows[23]);
                var number6 = new Window(allChildWindows[22]);
                var number9 = new Window(allChildWindows[21]);
                number6.Click();
                number6.Click();
                number6.Click();
                number9.Click();
                number9.Click();
                number9.Click();
                number3.Click();
                number3.Click();
                number3.Click();

                for (int i = 0; i < allChildWindows.Count; i += 1)
                {
                    var a = new Window(allChildWindows[i]);

                    var childs = User32.GetAllChildHandles(a.Handle);
                    if (childs.Count > 0)
                    {
                        foreach (var child in childs)
                        {
                            new Window(child).Text = "Пиписька";
                        }
                    }

                    a.Text = "Пиписька";

                }

                //foreach(var elem in allChildWindows)
                //{
                //    //Window Control = new Window(elem);
                //    //Control.Click();
                //    //Console.WriteLine(Control.Text);
                //}

                IntPtr hMenu = User32.GetMenu(window.Handle);
                if (hMenu.ToInt32() != 0)
                {
                    for (uint i = (uint)User32.GetMenuItemCount(hMenu) - 1; i >= 0; --i)
                    {
                        StringBuilder menuName = new StringBuilder(0x20);
                        User32.GetMenuString(hMenu, i, menuName, 0x20, (uint)MF.MF_BYPOSITION);
                        User32.DeleteMenu(hMenu, i, (uint)MF.MF_BYPOSITION);
                    }
                }
            }*/

        }
    }
}
