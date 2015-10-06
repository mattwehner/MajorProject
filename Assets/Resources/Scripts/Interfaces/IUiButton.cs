using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Resources.Scripts.Interfaces
{
    public interface IUiButton
    {
        void OnTriggerEnter2D();
        void OnTriggerExit2D();
        void ButtonAction();
    }
}
