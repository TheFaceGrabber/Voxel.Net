﻿using System;
using System.Windows.Forms;
using VoxelNet;
using VoxelNet.Input;

namespace VoxelNet
{
    public static class Program
    {
        public const string PROGRAMTITLE = "Voxel.Net";

        public static Settings Settings;
        public static Window Window { get; private set; }

        public static bool IsRunning;

        [STAThread]
        static void Main()
        {

#if (!DEBUG)
            try
            {
#endif
                IsRunning = true;
                LoadSettings();
                using (Window = new Window(Settings.WindowWidth, Settings.WindowHeight, PROGRAMTITLE))
                {
                    Window.Run();
                }
                IsRunning = false;
#if (!DEBUG)
            }
            catch(Exception ex)
            {
                IsRunning = false;
                MessageBox.Show(ex.Message + " " + ex.StackTrace, "Fatal Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }

        static void LoadSettings()
        {
            //TODO Load from file
            Settings = Settings.Load();
        }
    }
}
