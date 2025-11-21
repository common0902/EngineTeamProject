using System;
using System.IO;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace csiimnida.CSILib.SoundManager.Editor
{
    [UnityEditor.CustomEditor(typeof(SoundSo))]
    public class SoundSOEditor : UnityEditor.Editor
    {
        private readonly string tempText = "TempAudioSource";
        private VisualTreeAsset visualTreeAsset = default;
        private Label nameLabel;
        private TextField nameField;
        
        private Button playButton,pushButton,stopButton;
        private Slider playSlider;
        private ObjectField audioClipField;
        
        private AudioSource TempSource;
        private Toggle randPitchToggle;
        private FloatField minPitchField, maxPitchField;
        private Slider pitchSlider;
        
        private bool isPlaying = false;
        private SoundSo soundcs;
        
        
        
        private string _rootFolderPath;
        private SoundListSo soundListSo;

        private int lan = 0;
        
        
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();
            lan = PlayerPrefs.GetInt("SoundManagerLan", 0);
            InitializeWindow();
            visualTreeAsset.CloneTree(root);
            SetValue(root);
            SetEvents(root);
            
            return root;
        }

        private void SetEvents(VisualElement root)
        {
            playButton.clicked+= HandlePlayButton;
            nameField.RegisterValueChangedCallback(HandleAssetNameChange);
            audioClipField.RegisterValueChangedCallback(HandleAudioChange);
            randPitchToggle.RegisterValueChangedCallback(HandleChangeRandPitchValue);
            minPitchField.RegisterValueChangedCallback(HandleChangeMinValue);
            maxPitchField.RegisterValueChangedCallback(HandleChangeMaxValue);
            
            pushButton = root.Q<Button>("PushBt");
            pushButton.clicked += () =>
            {
                if (TempSource == null) return;
                if (isPlaying)
                {
                    TempSource.Pause();
                    isPlaying = false;
                    if (lan == 0)
                    {
                        pushButton.text = $"resume";
                    }
                    else
                    {
                        pushButton.text = $"재개";
                    }
                }
                else
                {
                    if (lan == 0)
                    {
                        pushButton.text = $"Pause";
                    }
                    else
                    {
                        pushButton.text = $"일시정지";
                    }
                    SetSetting();
                    isPlaying = true;
                    TempSource.UnPause();
                }
            };
            stopButton = root.Q<Button>("StopBt");
            stopButton.clicked += () =>
            {
                if (TempSource == null) return;
                TempSource.Stop();
                EndSound();
            };
            
            
            if (audioClipField.value == null)
            {
                Debug.LogWarning("AudioClip is Null");
            }
            minPitchField.value = soundcs.MinPitch;
            maxPitchField.value = soundcs.MaxPitch;
            if (soundcs.RandomPitch)
            {
                pitchSlider.SetEnabled(false);
                minPitchField.SetEnabled(true);
                maxPitchField.SetEnabled(true);
            }
            else
            {
                pitchSlider.SetEnabled(true);
                minPitchField.SetEnabled(false);
                maxPitchField.SetEnabled(false);
            }
            
        }

        private void HandlePlayButton()
        {

            if (soundcs.clip == null)
            {
                if(lan == 0)
                {
                    EditorUtility.DisplayDialog("!!!Warning!!!","AudioClip cannot be null.", "Ok");
                    
                }else{
                    EditorUtility.DisplayDialog("!!!경고!!!","AudioClip은 null일 수 없습니다.", "네");
                    
                }
                return;
            }
            if (TempSource == null)
            {
                TempSource = new GameObject().AddComponent<AudioSource>();
                TempSource.gameObject.AddComponent<DestroyTempAudio>();
                TempSource.gameObject.name = tempText;
            }

            TempSource.Stop();
            EndSound();
                
            SetSetting();
            TempSource.Play();
            isPlaying = true;
            pushButton.SetEnabled(true);
            stopButton.SetEnabled(true);
        }

        private void SetValue(VisualElement root)
        {
            soundcs = (SoundSo)target;
            EditorApplication.update += Update; // Update


            nameField = root.Q<TextField>("SoundNameField");
            nameField.value = soundcs.name;

            nameLabel = root.Q<Label>("NameLabel");
            nameLabel.text = nameField.value;

            audioClipField = root.Q<ObjectField>("AudioField");
            audioClipField.value = soundcs.clip;

            
            randPitchToggle = root.Q<Toggle>("RendomPitchBt");
            minPitchField = root.Q<FloatField>("MinValue");
            maxPitchField = root.Q<FloatField>("MaxValue");
            pitchSlider = root.Q<Slider>("PitchField");
            
            playSlider = root.Q<Slider>("PlayBar");
            
            playButton = root.Q<Button>("PlayBt");
            
        }
        
        private void HandleChangeMaxValue(ChangeEvent<float> evt)
        {
            if (!soundcs.RandomPitch) return;
            if(soundcs.MinPitch > evt.newValue)
            {
                if(lan == 0)
                {
                    EditorUtility.DisplayDialog("!Warning!","The maximum pitch must be greater than the minimum pitch.", "ok");
                }else{
                    EditorUtility.DisplayDialog("!경고!","최대 피치는 최소 피치보다 커야 합니다.", "네");
                }
                (evt.target as FloatField)?.SetValueWithoutNotify(evt.previousValue);
                return;
            }
            if (evt.newValue > 3)
            {
                if (lan == 0)
                {
                    EditorUtility.DisplayDialog("!Warning!","The minimum pitch must be less than 3.", "ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("!경고!","최소 피치는 3보다 작아야 합니다.", "네");
                }
                (evt.target as FloatField)?.SetValueWithoutNotify(3);
                return;
            }
            soundcs.MaxPitch = evt.newValue;
            EditorUtility.SetDirty(soundcs);
            AssetDatabase.SaveAssets();
        }

        private void HandleChangeMinValue(ChangeEvent<float> evt)
        {
            if (!soundcs.RandomPitch) return;
            if(soundcs.MaxPitch < evt.newValue)
            {
                if (lan == 0)
                {
                    EditorUtility.DisplayDialog("!Warning!","The minimum pitch must be less than the maximum pitch.", "ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("!경고!","최소 피치는 최대 피치보다 작아야 합니다.", "네");
                }
                (evt.target as FloatField)?.SetValueWithoutNotify(evt.previousValue);
                return;
            }

            if (evt.newValue < -3)
            {
                if (lan == 0)
                {
                    EditorUtility.DisplayDialog("!Warning!","Minimum pitch must be greater than -3.", "ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("!경고!","최소 피치는 -3보다 커야 합니다.", "네");
                }
                (evt.target as FloatField)?.SetValueWithoutNotify(-3);
                return;
            }

            soundcs.MinPitch = evt.newValue;
            EditorUtility.SetDirty(soundcs);
            AssetDatabase.SaveAssets();
        }

        private void HandleChangeRandPitchValue(ChangeEvent<bool> evt)
        {
            if (evt.newValue)
            {
                pitchSlider.SetEnabled(false);
                minPitchField.SetEnabled(true);
                maxPitchField.SetEnabled(true);
            }
            else
            {
                pitchSlider.SetEnabled(true);
                minPitchField.SetEnabled(false);
                maxPitchField.SetEnabled(false);
            }

            soundcs.RandomPitch = evt.newValue;
            EditorUtility.SetDirty(soundcs);
            AssetDatabase.SaveAssets();
        }

        private void HandleAudioChange(ChangeEvent<Object> evt)
        {
            if (evt.newValue == null)
            {
                if (lan == 0)
                {
                    EditorUtility.DisplayDialog("!!!Warning!!!","AudioClip cannot be null.", "ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("!!!경고!!!","AudioClip은 null일 수 없습니다.", "네");
                }
                (evt.target as ObjectField)?.SetValueWithoutNotify(evt.previousValue);
                return;
            }
            soundcs.clip = evt.newValue as AudioClip;
            EditorUtility.SetDirty(soundcs);
            AssetDatabase.SaveAssets();
            
        }

        private void EndSound()
        {
            isPlaying = false;
            if (lan == 0)
            {
                pushButton.text = $"Pause";
            }
            else
            {
                pushButton.text = $"일시정지";
            }
            pushButton.SetEnabled(false);
            stopButton.SetEnabled(false);
            playSlider.value = 0;
        }
        private void Update()
        {
            if (!isPlaying) return;
            if (TempSource == null)  return;
            if (TempSource.pitch < 0)
            {
                if (TempSource.time <= 0)
                {
                    EndSound();
                }
            }
            else
            {
                 if (TempSource.time >= TempSource.clip.length) EndSound();
            }
            float playbackProgress = TempSource.time / TempSource.clip.length;
            playSlider.value = playbackProgress;
        }
        

        private void SetSetting()
        {
            TempSource.clip = soundcs.clip;
            TempSource.loop = soundcs.loop;
            TempSource.priority = soundcs.Priority;
            TempSource.volume = soundcs.volume;
            TempSource.pitch = soundcs.pitch;
            TempSource.panStereo = soundcs.stereoPan;
            TempSource.spatialBlend = 0;
            if (soundcs.RandomPitch)
            {
                TempSource.pitch = Random.Range(soundcs.MinPitch, soundcs.MaxPitch);
            }
            if (TempSource.pitch < 0)
            {
                TempSource.time = 1;
            }
        }

        public void ResetSound()
        {
            EditorApplication.update -= Update;
            if (TempSource == null) return;
            DestroyImmediate(TempSource.gameObject);
        }
        private void OnDestroy()
        {
            ResetSound();
        }
            
        private void HandleAssetNameChange(ChangeEvent<string> evt)
        {
            if (string.IsNullOrEmpty(evt.newValue))
            {
                if (lan == 0)
                {
                    EditorUtility.DisplayDialog("!Warning!","There can't be no name!", "ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("!경고!","이름은 없을 수 없어요!", "네");
                }
                (evt.target as TextField).SetValueWithoutNotify(evt.previousValue);
                return;
            }
            string assetPath = AssetDatabase.GetAssetPath(target);
            string newName = $"{evt.newValue}";
            string message = AssetDatabase.RenameAsset(assetPath, newName);
            if (string.IsNullOrEmpty(message))
            {
                target.name = newName;
                soundcs.soundName = newName;
                nameLabel.text = newName;
            }
            else
            {
                (evt.target as TextField).SetValueWithoutNotify(evt.previousValue);
                EditorUtility.DisplayDialog("Error", message, "OK");
            }
            
        }
        private void InitializeWindow()
        {
            MonoScript monoScript = MonoScript.FromScriptableObject(this);
            string scriptPath = AssetDatabase.GetAssetPath(monoScript);
            _rootFolderPath = Directory.GetParent(Path.GetDirectoryName(scriptPath)).FullName.Replace("\\", "/");
            _rootFolderPath = "Assets" + _rootFolderPath.Substring(Application.dataPath.Length);
            if (soundListSo == null)
            {
                string filePath = $"{_rootFolderPath}/SoundListSO.asset";
                soundListSo = AssetDatabase.LoadAssetAtPath<SoundListSo>(filePath);
                if (soundListSo == null)  //로드를 할려고 하는데 없었어.
                {
                    Debug.LogWarning("SoundListSO not found. Creating a new one.");
                    soundListSo = ScriptableObject.CreateInstance<SoundListSo>();
                    AssetDatabase.CreateAsset(soundListSo, filePath);
                }
            }

            if (lan == 0)
            {
                visualTreeAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundSO_en_vr.uxml");
            }
            else
            {
                visualTreeAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundSO.uxml");
            }
            Debug.Assert(visualTreeAsset != null,$"Visual tree asset is null");
        }
    }
}


