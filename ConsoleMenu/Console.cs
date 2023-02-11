﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using VanillaConsole = System.Console;

namespace WXNUtils
{
    public static class Console
    {
        public static class Utils
        {
            static ManualResetEvent _quitEvent = new ManualResetEvent(false);
            public static void SetupConsoleKeepOpen()
            {
                VanillaConsole.CancelKeyPress += (sender, eArgs) => {
                    _quitEvent.Set();
                    eArgs.Cancel = true;
                };
            }

            public static void KeepConsoleOpen()
            {
                _quitEvent.WaitOne();
            }
            
            private const int ATTACH_PARENT_PROCESS = -1;
            [DllImport("kernel32.dll")]
            private static extern bool AttachConsole(int dwProcessId);

             /// <summary>
            /// /// Attach Programm to its own console (For JetBrains Rider)
            /// </summary>
            public static void AttachToConsole()
            {
                AttachConsole(ATTACH_PARENT_PROCESS);
            }

             public static void DisableCursor()
             {
                 VanillaConsole.CursorVisible = false;
             }
        }
        public static class Data
        {
            public static class Colors
            {
                public static bool colored_output = true;
                public static ConsoleColor int_color = ConsoleColor.Blue;
                public static ConsoleColor double_color = ConsoleColor.Cyan;
                public static ConsoleColor float_color = ConsoleColor.DarkCyan;
            }

            public static class Formatting
            {
                
            }
        }
        #region AllDifferentWriteLineTypes

        public static void WriteLine(string text)
        {
            VanillaConsole.WriteLine(text);
        }

        #endregion
        private static void ConfiguredWriteline(string text, ConsoleColor color, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            VanillaConsole.OutputEncoding = Encoding.UTF8;
            ConsoleColor prevColor = VanillaConsole.BackgroundColor;
            ConsoleColor prevForeColor = VanillaConsole.ForegroundColor;
            if (Data.Colors.colored_output)
            {
                VanillaConsole.BackgroundColor = color;
                VanillaConsole.ForegroundColor = foregroundColor;
            }
            VanillaConsole.WriteLine(text + " ");
            if (Data.Colors.colored_output)
            {
                
                VanillaConsole.BackgroundColor = prevColor;
                VanillaConsole.ForegroundColor = prevForeColor;
            }
        }

        
        public static void Success(string text,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            ConfiguredWriteline(" ✓ (" + lineNumber + "|" + caller + ") " + text, ConsoleColor.Green, ConsoleColor.Black);
        }
        public static void Info(string text,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            ConfiguredWriteline(" ◈ (" + lineNumber + "|" + caller + ") " + text, ConsoleColor.Blue, ConsoleColor.Black);
        }
        public static void Error(string text,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            ConfiguredWriteline(" ☓ (" + lineNumber + "|" + caller + ") " + text, ConsoleColor.DarkRed, ConsoleColor.Black);
        }
        public static void Warning(string text,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            ConfiguredWriteline(" ⌬ (" + lineNumber + "|" + caller + ") " + text, ConsoleColor.DarkYellow, ConsoleColor.Black);
        }

        public static void WriteLine<T>(List<T> List, bool verbose = true)
        {
            ConfiguredWriteline("List contains " + typeof(T) + "(" + List.Count + ")", ConsoleColor.DarkMagenta, ConsoleColor.Black);
            if(!verbose)
                return;

            for (int i = 0; i < List.Count; i++)
            {
                if (i % 2 == 0)
                {
                    ConfiguredWriteline("ListIdx(" + i + "): " + List[i], ConsoleColor.DarkGray);
                }
                else
                {
                    ConfiguredWriteline("ListIdx(" + i + "): " + List[i], ConsoleColor.Black);
                }
            }
        }

        public static void WriteLine(int IntNumber)
        {
            ConfiguredWriteline("Int: " + IntNumber, Data.Colors.int_color, ConsoleColor.Black);
        }
        public static void WriteLine(double DoubleNumber)
        {
            ConfiguredWriteline("Double: " + DoubleNumber, Data.Colors.double_color, ConsoleColor.Black);
        }
        public static void WriteLine(float FloatNumber)
        {
            ConfiguredWriteline("Float: " + FloatNumber, Data.Colors.float_color, ConsoleColor.Black);
        }
    }
}