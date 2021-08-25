# 2DPopUpSystem

![image](https://user-images.githubusercontent.com/15717398/130739961-c5f685a2-3756-4aa2-b7e9-713b4ec9a7a4.png)

1.	The show and hide animations style (currently have In Out or Bottom).
2.	Pre-made pop-up will not change during the set up of the scene and will remain as it is in the inspector.
3.	The title shown in the top of the pop-up
4.	The content shown in the pop-up
5.	Is the image from the project or loads from the web (also No.9)
6.	The image URL
7.	The background color of the image -> if you want to change the transparency of the background set the color to white and change the alfa variable
8.	The text shown on the button
9.	See No.5
10.	The sprite used as the image of the button

How to use:

Just add one of the pop-up prefabs to the scene, change the size and position of the different objects in the pop-up, change the variables to your liking.
To show the pop-up use the function:

```PopUpManager.AddPopUpToQueue(popUp);```

You can use the menu script on a button and attach a pop up to it like in the sample scene.

