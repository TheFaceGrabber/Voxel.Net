﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using VoxelNet.Input;

namespace VoxelNet.Input
{
    public class InputSettings
    {
        public List<InputSetting> Settings { get; private set; } = new List<InputSetting>();

        public InputSettings()
        {
            Settings.Add(new InputSetting("Jump", new Interaction(Key.Space)));
            Settings.Add(new InputSetting("Sprint", new Interaction(Key.ShiftLeft)));
            Settings.Add(new InputSetting("Destroy Block", new Interaction(MouseButton.Left)));
            Settings.Add(new InputSetting("Interact", new Interaction(MouseButton.Right)));
            Settings.Add(new InputSetting("Inventory", new Interaction(Key.Tab)));
        }

        public InputSetting GetSetting(string name)
        {
            return Settings.FirstOrDefault(x => x.Name == name);
        }
    }
}
