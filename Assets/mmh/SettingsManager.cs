using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;      // AudioMixer 쓸 거면
using TMPro;                  // TextMeshPro 쓸 거면

public class SettingsManager : MonoBehaviour
{
    [Header("UI References")]
    public Slider volumeSlider;       // 0~4 정수
    public TMP_Text volumeText;       // "0%", "25%" ... 표시

    [Header("Audio")]
    public AudioMixer audioMixer;     // AudioMixer 사용 시
    // (만약 AudioListener.volume만 쓰실 거면 이걸 지우고 AudioListener.volume=normalized; 사용)

    private const float STEPS = 4f;   // 0~4 단위 → 4로 나누면 0~1.0

    void Start()
    {
        // 슬라이더 세팅
        volumeSlider.wholeNumbers = true;
        volumeSlider.minValue = 0;
        volumeSlider.maxValue = STEPS;

        // 값 불러오기 (없으면 4칸 = 100%)
        float saved = PlayerPrefs.GetFloat("VolumeStep", STEPS);
        volumeSlider.value = saved;

        // 이벤트 등록
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);

        // 초기 반영
        OnVolumeChanged(saved);
    }

    public void OnVolumeChanged(float step)
    {
        float normalized = step / STEPS;       // 0.0 0.25 0.5 0.75 1.0

        // 1) 화면에 표시
        volumeText.text = $"{normalized * 100:0}%";

        // 2) 실제 볼륨 반영
        if (audioMixer != null)
        {
            // AudioMixer에 ExposedParam "MasterVolume"이 있다고 가정
            //  볼륨(데시벨) 단위로 변환: 20*log10(볼륨비율)
            float dB = Mathf.Log10(Mathf.Max(normalized, 0.0001f)) * 20f;
            audioMixer.SetFloat("MasterVolume", dB);
        }
        else
        {
            AudioListener.volume = normalized;
        }

        // 3) 다음 실행을 위해 저장
        PlayerPrefs.SetFloat("VolumeStep", step);
    }
}