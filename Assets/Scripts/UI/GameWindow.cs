using System.Threading.Tasks;
using Core.UI;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class GameWindow : WindowBase
    {
        protected override Task AnimateOpening()
        {
            _content.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            return _content.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).AsyncWaitForCompletion();
        }
    }
}