using UnityEngine;
using UnityEngine.InputSystem;

namespace DiasGames.Components
{
    public class PauseComponent : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private bool _isPaused;

        CursorLockMode lockMode;
        bool visible;

        private void Start()
        {
            visible = Cursor.visible;
            lockMode = Cursor.lockState;
            Time.timeScale = _isPaused ? 0f : 1f;
        }

        private void OnPause(InputValue value)
        {
            if (value.isPressed)
                OnPause(!_isPaused);
        }

        public void OnPause(bool paused)
        {
            _isPaused = paused;

            Time.timeScale = _isPaused ? 0f : 1f;
            if (pauseMenu)
                pauseMenu.SetActive(_isPaused);

            Cursor.visible = paused ? true : visible;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.None;
            
        }
        public void OnPauseforchangeCutscene(bool paused)
        {
            _isPaused = paused;

            Time.timeScale = _isPaused ? 0f : 1f;

            Cursor.visible = paused ? true : visible;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.None;

        }

        public void MobilePause(bool pressed)
        {
            if (pressed)
                OnPause(!_isPaused);
        }
    }
}