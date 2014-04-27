using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DummyProject.Objects
{
    public enum HoldableObjectType
    {
        /// <summary>
        /// não tá segurando nada
        /// </summary>
        Nothing,
        /// <summary>
        /// mouse tá segurando uma construção
        /// </summary>
        Building,
        SelectionTexture
    }
}
