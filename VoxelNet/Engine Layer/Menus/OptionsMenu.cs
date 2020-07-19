﻿using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using VoxelNet.Assets;
using VoxelNet.Entities;

namespace VoxelNet.Menus
{
    public class OptionsMenu : Menu
    {
        private GUIStyle titleStyle = (GUIStyle)GUI.LabelStyle.Clone();

        public Menu PreviousMenu { get; set; }

        bool fullscreenVal = false;

        private IList<DisplayResolution> possibleDisplayResolutions;
        private int selectedRes = 0;

        public OptionsMenu()
        {
            var cur = DisplayDevice.GetDisplay(DisplayIndex.Default).SelectResolution(
                Program.Settings.WindowWidth, Program.Settings.WindowHeight, Program.Settings.BitsPerPixel, Program.Settings.RefreshRate);

            possibleDisplayResolutions = DisplayDevice.GetDisplay(DisplayIndex.Default).AvailableResolutions.Where(x => x.BitsPerPixel == cur.BitsPerPixel).ToList();

            selectedRes = possibleDisplayResolutions.IndexOf(cur);
        }

        public override void Show()
        {
            titleStyle.HorizontalAlignment = HorizontalAlignment.Middle;
            titleStyle.VerticalAlignment = VerticalAlignment.Middle;
            titleStyle.FontSize = 92;

            fullscreenVal = Program.Settings.Fullscreen;

            base.Show();
        }

        public override void OnGUI()
        {
            GUI.Label("OPTIONS", new Rect(0, 256f / (1280f / Program.Window.Height), Program.Window.Width, 72), titleStyle);

            var cur = possibleDisplayResolutions[selectedRes];

            if (GUI.Button($"Size: {cur.Width} X {cur.Height} - {cur.RefreshRate}hz", new Rect(Program.Window.Width / 2f - 198, Program.Window.Height / 2f - 27, 198 * 2, 18 * 2)))
            {
                if (++selectedRes >= possibleDisplayResolutions.Count)
                    selectedRes = 0;
            }

            string fcString = fullscreenVal ? "On" : "Off";

            if (GUI.Button($"Fullscreen: {fcString}", new Rect(Program.Window.Width / 2f - 198, Program.Window.Height / 2f + 27, 198 * 2, 18 * 2)))
            {
                fullscreenVal = !fullscreenVal;
            }

            if (GUI.Button("Back", new Rect(Program.Window.Width / 2f - 198, Program.Window.Height / 2f + 81, 190, 18 * 2)))
            {
                if (PreviousMenu != null)
                    PreviousMenu.Show();

                Close();

                fullscreenVal = Program.Settings.Fullscreen;
            }

            if (GUI.Button("Apply", new Rect(Program.Window.Width / 2f + 8, Program.Window.Height / 2f + 81, 190, 18 * 2)))
            {
                if (PreviousMenu != null)
                    PreviousMenu.Show();

                Close();

                Program.Settings.ApplyWindowSettings(cur, fullscreenVal);
            }
        }
    }
}
