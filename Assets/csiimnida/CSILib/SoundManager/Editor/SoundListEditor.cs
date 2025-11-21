using System;
using System.Collections.Generic;
using System.IO;
using csiimnida.CSILib.SoundManager.RunTime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace csiimnida.CSILib.SoundManager.Editor
{
    public class SoundListEditor : EditorWindow
    {
        private VisualTreeAsset visualTreeAsset = default,sounditemAsset = default;

        private UnityEditor.Editor _cachedEditor;
        private VisualElement _inspectorView;
        private string _rootFolderPath;
        private SoundListSo soundListSo;
        
        private Button _createBtn;
        private ScrollView _itemView;
        private List<SoundItemUI> _itemList;
        private SoundItemUI _selectedItem;
        
        
        
        
        [MenuItem("Window/CSILib/SoundManager")]
        public static void ShowWindow()
        {
            SoundListEditor wnd = GetWindow<SoundListEditor>();
            wnd.titleContent = new GUIContent("SoundManagerEditor");
        }    
        
        private void InitializeWindow(bool en_vr = false)
        {
            MonoScript monoScript = MonoScript.FromScriptableObject(this);
            string scriptPath = AssetDatabase.GetAssetPath(monoScript);
            _rootFolderPath = Directory.GetParent(Path.GetDirectoryName(scriptPath)).FullName.Replace("\\", "/");
            _rootFolderPath = "Assets" + _rootFolderPath.Substring(Application.dataPath.Length);
            if (soundListSo == null)
            {
                string filePath;
                if(en_vr)
                    filePath = $"{_rootFolderPath}/SoundSO_en_vr.asset";
                else
                {
                    filePath = $"{_rootFolderPath}/SoundListSO.asset";
                }
                soundListSo = AssetDatabase.LoadAssetAtPath<SoundListSo>(filePath);
                if (soundListSo == null)  //로드를 할려고 하는데 없었어.
                {
                    Debug.LogWarning("SoundListSO not found. Creating a new one.");
                    soundListSo = ScriptableObject.CreateInstance<SoundListSo>();
                    AssetDatabase.CreateAsset(soundListSo, filePath);
                }
            }

            if (en_vr)
            {
                visualTreeAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundListUIs/SoundListEditor.uxml");
                Debug.Assert(visualTreeAsset != null,$"Visual tree asset is null");
                sounditemAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundListUIs/SoundItemUI.uxml");
                Debug.Assert(sounditemAsset != null,$"itemAsset is null");
            }
            else
            {
                visualTreeAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundListUIs/SoundListEditor.uxml");
                Debug.Assert(visualTreeAsset != null,$"Visual tree asset is null");
                sounditemAsset =AssetDatabase.LoadAssetAtPath<VisualTreeAsset>($"{_rootFolderPath}/Editor/UIS/SoundListUIs/SoundItemUI.uxml");
                Debug.Assert(sounditemAsset != null,$"itemAsset is null");
            }
            
        }
        public void CreateGUI()
        {
            InitializeWindow();
        
        
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            visualTreeAsset.CloneTree(root);
            SetElements(root);
        
        
        }
        private void SetElements(VisualElement root)
        {
            _createBtn = root.Q<Button>("CreateBtn");
            _createBtn.clicked += HandleCreateBtn;
        
            _itemView = root.Q<ScrollView>("ItemView");

            _inspectorView = root.Q<VisualElement>("InspectorView");
        
            _itemList = new List<SoundItemUI>();
            root.Q<Button>("LenBtn").clicked += () =>
            {
                if (_selectedItem == null) return;
                if (_selectedItem.SoundItem == null) return;
                if (PlayerPrefs.GetInt("SoundManagerLan",0) == 0)
                {
                    PlayerPrefs.SetInt("SoundManagerLan",1);
                }
                else
                {
                    PlayerPrefs.SetInt("SoundManagerLan",0);
                }
                ReloadSettingSO();
            };
            GeneratePoolItems();
        }
        private void HandleCreateBtn()
        {
            SoundSo newItem = ScriptableObject.CreateInstance<SoundSo>();
            Guid itemGuid = Guid.NewGuid();
            newItem.soundName = itemGuid.ToString();
        
            if(Directory.Exists($"{_rootFolderPath}/Sounds") == false)
                Directory.CreateDirectory($"{_rootFolderPath}/Sounds");
            AssetDatabase.CreateAsset(newItem,$"{_rootFolderPath}/Sounds/{newItem.soundName}.asset");
        
            soundListSo.AddSound(newItem);
            EditorUtility.SetDirty(soundListSo);
            EditorUtility.SetDirty(newItem);
            AssetDatabase.SaveAssets();
        
        
            GeneratePoolItems();
        }

        public override void SaveChanges()
        {
            base.SaveChanges();
            Debug.Log("Saved");
        }

        private void GeneratePoolItems()
        {
            _itemView.Clear();
            _itemList.Clear();
            _inspectorView.Clear();
            if (soundListSo.GetSoundList() == null) return;
            foreach (var item in soundListSo.GetSoundList())
            {
                TemplateContainer itemTemplate = sounditemAsset.Instantiate();
                SoundItemUI itemUI = new SoundItemUI(itemTemplate, item);
                _itemView.Add(itemTemplate);
                _itemList.Add(itemUI);

                itemUI.Name = item.soundName;
                if(_selectedItem != null && _selectedItem.SoundItem == item)
                {
                    HandleItemSelect(itemUI);
                }
                itemUI.OnSelectEvent += HandleItemSelect;
                itemUI.OnDeleteEvent += HandleItemDelete;
            }
        
        }
        private void HandleItemDelete(SoundItemUI target)
        {
            if (EditorUtility.DisplayDialog("Delete Sound Item", $"Are you sure you want to delete {target.Name}?", "Yes",
                    "No") == false)
            {
                return;
            }
            soundListSo.RemoveSound(target.SoundItem);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(target.SoundItem)); //실질적인 삭제
            EditorUtility.SetDirty(soundListSo); //더티 플래그 설정
            AssetDatabase.SaveAssets(); //에셋 저장
        
            if(_selectedItem == target)
            {
                _selectedItem = null;
                //여기서 나중에 인스펙터 클리어까지 하게 된다.
            }
        
            GeneratePoolItems();
        }

        private void OnEnable()
        {
            Reset();
        }

        private void OnBecameInvisible()
        {
            Reset();

        }

        private void Reset()
        {
            if (_cachedEditor != null)
            {
                var soundEditor = _cachedEditor as SoundSOEditor;
                if (soundEditor != null)
                {
                    soundEditor.ResetSound();
                }
            }
        }

        private void OnBeforeRemovedAsTab()
        {
        }

        private void OnDestroy()
        {

        }

        public void ReloadSettingSO()
        {
            _inspectorView.Clear();
            UnityEditor.Editor.CreateCachedEditor(_selectedItem.SoundItem, null, ref _cachedEditor);
            VisualElement inspectorContent = _cachedEditor.CreateInspectorGUI();
                    
            SerializedObject serializedObject = new SerializedObject(_selectedItem.SoundItem);
            inspectorContent.Bind(serializedObject);
            _selectedItem.Name = _selectedItem.SoundItem.name;
            inspectorContent.TrackSerializedObjectValue(serializedObject, so =>
            {
                _selectedItem.Name = _selectedItem.SoundItem.name;
            });
            _inspectorView.Add(inspectorContent);
        }
        private void HandleItemSelect(SoundItemUI target)
        {
            _inspectorView.Clear();
            if(_selectedItem != null)
                _selectedItem.IsActive = false;
            _selectedItem = target;
            _selectedItem.IsActive = true;
        
            UnityEditor.Editor.CreateCachedEditor(_selectedItem.SoundItem, null, ref _cachedEditor);
            VisualElement inspectorContent = _cachedEditor.CreateInspectorGUI();
        
            SerializedObject serializedObject = new SerializedObject(_selectedItem.SoundItem);
            inspectorContent.Bind(serializedObject);
            _selectedItem.Name = _selectedItem.SoundItem.name;
            inspectorContent.TrackSerializedObjectValue(serializedObject, so =>
            {
                _selectedItem.Name = _selectedItem.SoundItem.name;
            });
            
            _inspectorView.Add(inspectorContent);
        }
    }
}