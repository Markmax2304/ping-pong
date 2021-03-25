using UnityEngine;

namespace Pong.Movement
{
    public interface IMovable
    {
        Vector3 ActualPosition { get; set; }
        void MoveTo(Vector3 pos);
    }
}
