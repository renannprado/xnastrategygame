using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyGameLibrary
{
    public interface LoadableGameComponent
    {
        String RootAssetPath { get; set; }
        int AssetID { get; set; }
    }
}
