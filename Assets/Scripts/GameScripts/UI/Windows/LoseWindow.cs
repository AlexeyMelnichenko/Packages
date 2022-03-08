using System.Threading.Tasks;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI.Windows
{
    public class LoseWindow : WindowWithIntent<LoseIntent>
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _newGameButtonClick;
        [SerializeField] private WindowAnimationController _animationController;

        private void Awake()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
            _newGameButtonClick.onClick.AddListener(OnNewGameButtonClick);
        }

        protected override Task AnimateOpening()
        {
            return _animationController.OpenAnimation();
        }
        
        protected override Task AnimateClosing()
        {
            return _animationController.CloseAnimation();
        }

        private void OnRestartButtonClick()
        {
            Close();
            Intent.Result = LoseWindowResult.RestartGame;
        }
        
        private void OnCloseButtonClick()
        {
            Close();
            Intent.Result = LoseWindowResult.Close;
        }

        private void OnNewGameButtonClick()
        {
            Close();
            Intent.Result = LoseWindowResult.NewGame;
        }
    }

    public class LoseIntent : EmptyIntent
    {
        public LoseWindowResult Result { get; set; }
    }

    public enum LoseWindowResult
    {
        NewGame, RestartGame, Close
    }
}