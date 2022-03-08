using Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationController : IInitializable
    {
        public ISdk Sdk { get; }

        public ApplicationController(ISdk sdk)
        {
            Sdk = sdk;
        }
        
        public void Initialize()
        {
            Application.targetFrameRate = 60;
            
            Sdk.Initialize();
            
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }
    }
}