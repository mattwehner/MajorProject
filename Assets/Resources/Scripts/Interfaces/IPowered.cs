using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Object_Specific
{
    public interface IPowered
    {
        bool PoweredOn { get; set; }
    }
}
