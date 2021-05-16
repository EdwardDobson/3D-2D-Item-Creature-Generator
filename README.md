# Final Year Project
## 3D AND 2D EDITOR TO CREATE RPG CREATURES AND ITEMS ##

Third year final project that can create items and creatures within the Unity Eegine written in C#.

This project users a combination of scriptable objects to save the items/creatures data and custom editor windows. 

[How to import Unity Packages](https://docs.unity3d.com/Manual/AssetPackagesImport.html)

### ITEM CREATION TUTORIAL ###

Creating an item is a multi stage process following the steps below should yield you a complete item. For the purposes of this example I will be creating a sword. I written tutorial can be seen below, or you can watch [my showcase video](https://user-images.githubusercontent.com/47003895/118391006-06914f00-b62a-11eb-94ff-b1eb8bcdb4dd.mp4)


* Clicking on the window "Item + Creature Builder" at the top will give you a drop down called builder click it.
This will display the hub window. This window houses all of the systems that my project offers.

* The first window to use would be the folder window, when opened you will be able to remove/add folders from the Assets/Resources/BuiltItems directory.
You can create your own folder now for the items created later.

* The next window to use is the Material Builder after filing in all of the information you should be able to build a material these are used as modifiers for item/creature parts.
  ***If you are making a 2D weapon make sure to make 2D materials.***

* Now with your materials created lets move to the Weapon Part Builder. Just like the material builder add in all the details and add the material you want.

* The final window is the Weapon Builder. There are a few options to configure in this window such as the amount of slots (*add as many as needed*). You can also move your slots around so your item looks correct. You can view your item via the Show Camera button.

* After clicking builder weapon it will now be added into the folder you have chosen and can be added into your scene.



