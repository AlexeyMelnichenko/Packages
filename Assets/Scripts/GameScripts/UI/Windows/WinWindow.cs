using System.Threading.Tasks;
using Core.UI;
using UnityEngine;

namespace GameScripts.UI.Windows
{
    public class WinWindow : WindowWithIntent<WinWindowIntent>
    {
        [SerializeField] private WindowAnimationController _animationController;

        public Game Game => Intent.Game;

        protected override void OnOpening()
        {
        }

        protected override Task AnimateOpening()
        {
            return _animationController.OpenAnimation();
        }

        protected override Task AnimateClosing()
        {
            return _animationController.CloseAnimation();
        }
    }

    public class WinWindowIntent : EmptyIntent
    {
        public readonly Game Game;

        public WinWindowIntent(Game game)
        {
            Game = game;
        }
    }
}
