using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Pong.Inputs
{
    public class KeyboardInputService : IInputService
    {
        const string axisName = "Vertical";

        public bool GetVerticalInput(out float value)
        {
            value = Input.GetAxisRaw(axisName);
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
        }
    }
}
