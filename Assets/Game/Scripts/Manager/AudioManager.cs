using System.Collections.Generic;
using UnityEngine;

namespace Game.Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Range(0f, 1f)] public float masterVolume = 1f;
        public bool muted = false;

        List<AudioSource> sources = new List<AudioSource>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void RegisterSource(AudioSource s)
        {
            if (s == null) return;
            if (!sources.Contains(s))
            {
                sources.Add(s);
                UpdateSourceVolume(s);
            }
        }

        public void UnregisterSource(AudioSource s)
        {
            if (s == null) return;
            sources.Remove(s);
        }

        void UpdateSourceVolume(AudioSource s)
        {
            if (s == null) return;
            s.volume = muted ? 0f : masterVolume;
        }

        public void SetMasterVolume(float v)
        {
            masterVolume = Mathf.Clamp01(v);
            foreach (var s in sources) UpdateSourceVolume(s);
        }

        public void SetMuted(bool m)
        {
            muted = m;
            foreach (var s in sources) UpdateSourceVolume(s);
        }

        public void PlayOneShot(AudioClip clip, Vector3 position)
        {
            if (clip == null) return;
            AudioSource.PlayClipAtPoint(clip, position, muted ? 0f : masterVolume);
        }

        public void Play(AudioSource s)
        {
            if (s == null) return;
            RegisterSource(s);
            UpdateSourceVolume(s);
            if (!s.isPlaying) s.Play();
        }

        public void Stop(AudioSource s)
        {
            if (s == null) return;
            if (s.isPlaying) s.Stop();
        }
    }
}