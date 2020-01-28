﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Input;

namespace VoxelNet.Input
{
    public static class Input
    {
        private static Vector2 mouseDelta = new Vector2(0,0);

        private static MouseState currentMouseState, previousMouseState;

        static Input()
        {

            Program.Window.KeyDown += (sender, args) =>
            {
                if (args.IsRepeat)
                    return;

                var inputs = Program.Settings.Input.Settings.Where(x => x.Main.KeyButton == args.Key);
                foreach (var input in inputs)
                {
                    input.KeyDown?.Invoke();
                }
            };
            Program.Window.KeyUp += (sender, args) =>
            {
                var inputs = Program.Settings.Input.Settings.Where(x => x.Main.KeyButton == args.Key);
                foreach (var input in inputs)
                {
                    input.KeyUp?.Invoke();
                }
            };
            Program.Window.MouseDown += (sender, args) =>
            {
                var inputs = Program.Settings.Input.Settings.Where(x => x.Main.MouseButton == args.Button);
                foreach (var input in inputs)
                {
                    input.KeyDown?.Invoke();
                }
            };
            Program.Window.MouseUp += (sender, args) =>
            {
                var inputs = Program.Settings.Input.Settings.Where(x => x.Main.MouseButton == args.Button);
                foreach (var input in inputs)
                {
                    input.KeyUp?.Invoke();
                }
            };
        }

        public static InputSetting GetSetting(string input)
        {
            return Program.Settings.Input.GetSetting(input);
        }

        public static void Update()
        {
            currentMouseState = Mouse.GetState();
            if (currentMouseState != previousMouseState)
            {
                mouseDelta = new Vector2(currentMouseState.X - previousMouseState.X, currentMouseState.Y - previousMouseState.Y);
            }
            else
            {
                mouseDelta = Vector2.Zero;
            }

            previousMouseState = currentMouseState;
        }

        public static Vector2 GetMouseDelta()
        {
            return mouseDelta;
        }
    }
    
}
