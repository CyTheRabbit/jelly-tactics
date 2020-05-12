using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class SoundSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer m_soundMixer = null;
        [Space]
        [SerializeField] private Image m_soundButton = null;
        [SerializeField] private Sprite m_soundOnSprite = null;
        [SerializeField] private Sprite m_soundOffSprite = null;
        [Space]
        [SerializeField] private Image m_musicButton = null;
        [SerializeField] private Sprite m_musicOnSprite = null;
        [SerializeField] private Sprite m_musicOffSprite = null;

        private bool soundOn = true;
        private bool musicOn = true;

        public void ToggleSound()
        {
            soundOn = !soundOn;
            m_soundMixer.SetFloat("Sound Volume", soundOn ? 0 : -80);
            m_soundButton.sprite = soundOn ? m_soundOnSprite : m_soundOffSprite;
        }

        public void ToggleMusic()
        {
            musicOn = !musicOn;
            m_soundMixer.SetFloat("Music Volume", musicOn ? 0 : -80);
            m_musicButton.sprite = musicOn ? m_musicOnSprite : m_musicOffSprite;
        }
    }
}