Thank you for using our asset.

You can open the SoundManager window by navigating to [Window] > [CSILib] > [SoundManager].

To play sounds within your game, a few setup steps are required.

1. First, create a new GameObject in your scene and add the SoundManager script to it as a component.
2. Next, you will need to assign the AudioMixer and SoundListSO assets to the SoundManager component in the Inspector. You can find these assets located in the `csiimnida/CSILib/SoundManager` folder. Drag the AudioMixer to the "Mixer" field and the SoundListSO to the "SoundListSo" field.
3. Finally, you can play a sound from your code by calling: `SoundManager.Instance.PlaySound(/*SoundName*/);`