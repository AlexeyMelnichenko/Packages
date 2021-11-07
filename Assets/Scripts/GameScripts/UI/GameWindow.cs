using System.Threading.Tasks;
using Core.UI;
using DG.Tweening;
using GameScripts;
using UnityEngine;

namespace UI
{
    public class GameWindow : WindowWithIntent<GameIntent>
    {
        protected override Task AnimateOpening()
        {
            _content.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            return _content.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }
    }

    public class GameIntent : EmptyIntent
    {
        public readonly Game Game;

        public GameIntent(Game game)
        {
            Game = game;
        }
    }
}