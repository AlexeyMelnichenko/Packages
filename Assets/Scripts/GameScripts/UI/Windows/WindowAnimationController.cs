using System.Threading.Tasks;
using UnityEngine;

namespace GameScripts.UI.Windows
{
    [RequireComponent(typeof(Animator))]
    public class WindowAnimationController : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int OPEN = Animator.StringToHash("Open");
        private static readonly int CLOSE = Animator.StringToHash("Close");

        private TaskCompletionSource<object> _animationTaskSource;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public Task OpenAnimation()
        {
            _animator.SetTrigger(OPEN);
            _animationTaskSource = new TaskCompletionSource<object>();
            return _animationTaskSource.Task;
        }
        
        public Task CloseAnimation()
        {
            _animator.SetTrigger(CLOSE);
            _animationTaskSource = new TaskCompletionSource<object>();
            return _animationTaskSource.Task;
        }

        public void OnAnimationComplete()
        {
            _animationTaskSource?.TrySetResult(default);
        }
    }
}
