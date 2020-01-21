﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoxelNet.Assets
{
    public interface IExportable
    {
        string Path { get; }
        void Export();
    }
}
