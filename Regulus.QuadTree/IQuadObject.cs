using Regulus.Utility;
using System;

namespace Regulus.Collection
{
    public interface IQuadObject
    {
        event System.Action<IQuadObject> BoundsChanged;

        Rect Bounds { get; }
    }
}
