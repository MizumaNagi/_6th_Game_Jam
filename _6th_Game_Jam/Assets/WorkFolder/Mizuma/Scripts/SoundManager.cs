using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonClass<SoundManager>
{
    [SerializeField] private GameObject bgmParent;
    [SerializeField] private int bgmChannelValue;
    [SerializeField] private AudioClip[] bgmClips;

    [SerializeField] private GameObject seParent;
    [SerializeField] private int seChannelValue;
    [SerializeField] private AudioClip[] seClips;

    private List<AudioSource> bgmSources = new List<AudioSource>();
    private List<AudioSource> seSources = new List<AudioSource>();
    private SoundOption defaultBGMOption = new SoundOption();
    private SoundOption defaultSEOption = new SoundOption();

    private void Start()
    {
        for (int i = 0; i < bgmChannelValue; i++)
        {
            MakeAudioSource(AudioType.BGM);
        }

        for (int i = 0; i < seChannelValue; i++)
        {
            MakeAudioSource(AudioType.SE);
        }

        defaultBGMOption.isMute = 0;
        defaultBGMOption.isLoop = 1;
        defaultBGMOption.isPlayOnAwake = 1;
        defaultBGMOption.volume = 0.2f;
        defaultBGMOption.pitch = 1f;
        defaultBGMOption.playSecond = 0f;

        defaultSEOption.isMute = 0;
        defaultSEOption.isLoop = 0;
        defaultSEOption.isPlayOnAwake = 1;
        defaultSEOption.volume = 0.2f;
        defaultSEOption.pitch = 1f;
        defaultSEOption.playSecond = 0f;

        //PlayBGM(BGMName.Title);
    }

    /// <summary>
    /// BGM���Đ�����
    /// </summary>
    /// <param name="bgmName">�Đ�����Clip</param>
    /// <returns>�Đ��ɐ���������</returns>
    public bool PlayBGM(BGMName bgmName, SoundOption overrideSoundOption = null)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.BGM, false);

        if (source == null)
        {
            Debug.LogError("AudioSource���������");
            return false;
        }

        source.clip = bgmClips[(int)bgmName];

        if(overrideSoundOption != null) PlaySound(AudioType.BGM, source, overrideSoundOption);
        else PlaySound(AudioType.BGM, source);

        return true;
    }

    /// <summary>
    /// SE���Đ�����
    /// </summary>
    /// <param name="seName">�Đ�����Clip</param>
    /// <returns>�Đ��ɐ���������</returns>
    public bool PlaySE(SEName seName, SoundOption overrideSoundOption = null)
    {
        AudioSource source = FindNotUseAudioSource(AudioType.SE, true);

        if (source.isPlaying)
        {
            Debug.LogError("��Đ�����AusioSource�����Ă���");
            return false;
        }

        source.clip = seClips[(int)seName];

        if (overrideSoundOption != null) PlaySound(AudioType.SE, source, overrideSoundOption);
        else PlaySound(AudioType.SE, source);

        return true;
    }

    
    /// <summary>
    /// Sound��ݒ�A�Đ�����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="source"></param>
    /// <param name="targetOption"></param>
    private void PlaySound(AudioType type, AudioSource source, SoundOption targetOption = null)
    {
        SoundOption defaultOption = null;
        if (type == AudioType.BGM) defaultOption = defaultBGMOption;
        else defaultOption = defaultSEOption;

        float waitSecond = -1f;
        float fadeSecond = -1f;
        if (targetOption != null)
        {
            if (targetOption.isMute != -1) source.mute = Convert.ToBoolean(targetOption.isMute);
            else source.mute = Convert.ToBoolean(defaultOption.isMute);

            if (targetOption.isPlayOnAwake != -1) source.playOnAwake = Convert.ToBoolean(targetOption.isPlayOnAwake);
            else source.playOnAwake = Convert.ToBoolean(defaultOption.isPlayOnAwake);

            if (targetOption.isLoop != -1) source.loop = Convert.ToBoolean(targetOption.isLoop);
            else source.loop = Convert.ToBoolean(defaultOption.isLoop);

            if (targetOption.volume != -1f) source.volume = targetOption.volume;
            else source.volume = defaultOption.volume;

            if (targetOption.pitch != -1f) source.pitch = targetOption.pitch;
            else source.pitch = defaultOption.pitch;

            if (targetOption.waitSecond != -1f) waitSecond = targetOption.waitSecond;
            else waitSecond = defaultOption.waitSecond;

            if (targetOption.fadeSecond != -1f) fadeSecond = targetOption.fadeSecond;
            else fadeSecond = defaultOption.fadeSecond;

            if (targetOption.playSecond != -1f) source.time = targetOption.playSecond;
            else source.time = defaultOption.playSecond;
        }
        else
        {
            source.mute = Convert.ToBoolean(defaultOption.isMute);
            source.playOnAwake = Convert.ToBoolean(defaultOption.isPlayOnAwake);
            source.loop = Convert.ToBoolean(defaultOption.isLoop);
            source.volume = defaultOption.volume;
            source.pitch = defaultOption.pitch;
            waitSecond = defaultOption.waitSecond;
            fadeSecond = defaultOption.fadeSecond;
            source.time = defaultOption.playSecond;
        }

        if (waitSecond != -1f && waitSecond > 0) { StartCoroutine(DelaySoundPlay(waitSecond, fadeSecond, source)); return; }
        if (fadeSecond != -1f && fadeSecond > 0) { throw new NotImplementedException("�t�F�[�h�@�\�͍���ĂȂ���"); }

        source.Play();
    }

    public void StopBGM(BGMName bgmName)
    { 
        foreach(AudioSource bgm in bgmSources.ToArray())
        {
            if(bgm.clip == bgmClips[(int)bgmName])
            {
                bgm.Stop();
            }
        }
    }

    public void StopSE(SEName seName)
    {
        foreach (AudioSource se in seSources.ToArray())
        {
            if (se.clip == seClips[(int)seName])
            {
                se.Stop();
            }
        }
    }

    /// <summary>
    /// BGMorSE�̉��ʂ�ύX����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="volume"></param>
    public void ChgSoundVolume(AudioType type, float volume)
    {
        if(type == AudioType.BGM)
        {
            defaultBGMOption.volume = volume;

            for(int i = 0; i < bgmSources.Count; i++)
            {
                bgmSources[i].volume = volume;
            }
        }
        else
        {
            defaultSEOption.volume = volume;

            for (int i = 0; i < seSources.Count; i++)
            {
                seSources[i].volume = volume;
            }
        }
    }

    private IEnumerator DelaySoundPlay(float waitTime, float fadeTime, AudioSource source)
    {
        yield return new WaitForSeconds(waitTime);
        source.Play();
    }

    /// <summary>
    /// �Đ����Ă��Ȃ�AudioSource�������Ă���
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <param name="noHitIsMakeAudioSource">�S�čĐ����̍ېV����AudioSource����邩</param>
    /// <returns>��Đ�����AudioSource</returns>
    private AudioSource FindNotUseAudioSource(AudioType type, bool noHitIsMakeAudioSource)
    {
        if (type == AudioType.BGM)
        {
            foreach (AudioSource source in bgmSources)
            {
                return source;
            }

            if (noHitIsMakeAudioSource)
            {
                AudioSource newSource = MakeAudioSource(AudioType.BGM);
                bgmSources.Add(newSource);
                return newSource;
            }
        }
        else
        {
            foreach (AudioSource source in seSources)
            {
                if (source.isPlaying == false)
                {
                    return source;
                }
            }

            if (noHitIsMakeAudioSource)
            {
                AudioSource newSource = MakeAudioSource(AudioType.SE);
                seSources.Add(newSource);
                return newSource;
            }
        }

        return FindUseAudioSource(type);
    }

    /// <summary>
    /// �Đ�����AudioSource�������Ă���
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>�~�߂Ĕ�Đ����ɂȂ���AudioSource</returns>
    private AudioSource FindUseAudioSource(AudioType type)
    {
        if (type == AudioType.BGM)
        {
            foreach (AudioSource source in bgmSources)
            {
                if (source.isPlaying == true)
                {
                    source.Stop();
                    return source;
                }
            }
        }
        else
        {
            foreach (AudioSource source in seSources)
            {
                if (source.isPlaying == true)
                {
                    source.Stop();
                    return source;
                }
            }
        }
        Debug.LogError("�g�p,���g�pAudioSource����");
        return null;
    }

    /// <summary>
    /// �V����AudioSource�����
    /// </summary>
    /// <param name="type">BGM or SE</param>
    /// <returns>���ꂽAudioSource</returns>
    private AudioSource MakeAudioSource(AudioType type)
    {
        AudioSource source;

        if (type == AudioType.BGM)
        {
            source = bgmParent.AddComponent<AudioSource>();
            bgmSources.Add(source);
            source.playOnAwake = true;
            source.loop = true;
        }
        else
        {
            source = seParent.AddComponent<AudioSource>();
            seSources.Add(source);
            source.playOnAwake = false;
            source.loop = false;
        }

        return source;
    }
}

public enum AudioType
{
    BGM,
    SE
}

public enum BGMName
{
    Title,
    Main
}

public enum SEName
{
    Collect_Item,
    On_Damage,
    Take_Damage,
    Kill_Enemy,
    Running,
    Push_Button
}