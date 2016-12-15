# WikipediaVisualization
An Oculus Rift Visualization experiment using Unity 3d.

![Image of Visualization](http://i.imgur.com/atHOqqt.png)
![Image of Visualization](http://i.imgur.com/ERNttlq.png)
![Image of Desk from Visualization](http://i.imgur.com/blTKab0.png)

#Data Sources
* https://www.cs.umd.edu/~vs/vews_dataset_v1.1.zip   (~30MB)
* https://datahub.io/dataset/english-wikipedia-reverts	(~13GB)

Csv files are converted to Json before processing in Unity.

#Technologies Used:
* Unity
  * A wonderfully streamlined Game Engine with a lot of support
* Graph Maker (Library for Unity)
  * Simplified creating graphs in Unity
* Oculus Rift
  * Ongoing goal was to experiment with the Oculus
* ProtoBuf
  * A library implementing Protocol buffers for efficient data serialization
  
#What we achieved:
* Loaded in all data provided (~30MB) and cross referenced it with the actual revision log for all of Wikipedia at that time (~13GB). After parsing we saved the data into the WikipediaVisualization/Cache folder (totals 110MB), these files are compressed serialized caches of the data needed to render the scene.
* Worked with Unity to render a dome with each tile representing a Wikipedia page ○ The Material is changed according to if the majority of edits for that page were vandal or benign.
*  We created a gaze detection algorithm to approximate how future eye tracking may work. When a user looks at a wikipedia page for a second or two it will become "active", its title will be overlayed, and it'll get pulled toward the user to indicate its state. The page's data will automatically be rendered on the graphs on the desk. The title only displays on active pages in order to prevent the user (or computer) from being overwhelmed with too much data. ○ In addition to gaze detection the project also pops up a tooltip upon looking at a data point with the actual data point value.

#Controls
  * Oculus Rift is all that is required. 
  * Without an Oculus:
    * Left Mouse button to rotate Camera
    * Right Mouse button to activate wikipedia pages 

#Presentation
 * [Link to Google Slides Presentation](https://docs.google.com/a/uncc.edu/presentation/d/1-OWO1GVsEJFMeWijXpf4m7mIm1rSpP4ZB4rcKcsLqFg/edit?usp=sharing)
