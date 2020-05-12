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
        [Space]
        [SerializeField] private float m_soundVolume = 0;
        [SerializeField] private float m_musicVolume = 0;

        private bool soundOn = true;
        private bool musicOn = true;


        private void Start()
        {
            m_soundMixer.SetFloat("Sound Volume", m_soundVolume);
            m_soundMixer.SetFloat("Music Volume", m_musicVolume);
        }


        public void ToggleSound()
        {
            soundOn = !soundOn;
            m_soundMixer.SetFloat("Sound Volume", soundOn ? m_soundVolume : -80);
            m_soundButton.sprite = soundOn ? m_soundOnSprite : m_soundOffSprite;
        }

        public void ToggleMusic()
        {
            musicOn = !musicOn;
            m_soundMixer.SetFloat("Music Volume", musicOn ? m_musicVolume : -80);
            m_musicButton.sprite = musicOn ? m_musicOnSprite : m_musicOffSprite;
        }
    }
}