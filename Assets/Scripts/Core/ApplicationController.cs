using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationController : IInitializable
    {
        public SDK.SDK SDK { get; } = new SDK.SDK();
        
        public void Initialize()
        {
            Application.targetFrameRate = 60;

            SDK.Initialize();
        }
    }
}