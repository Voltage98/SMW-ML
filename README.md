# Super Mario World - Machine Learning
A project by Tourmi & Voltage98 
--------
Training a Super Mario World AI that's able to beat levels on its own, as well as completing or optimizing various objectives  
[License](LICENSE)

## When cloning
It is important to either clone the repository recursively to include the submodules, or initialize the submodules after cloning, as they need to be built for the program compilation to actually work.

## Prerequisites
* Must be on Windows
* .NET SDK 6.0 or higher must be installed
* Microsoft Visual C++ 2010 SP1 Runtime (x64) must be installed for Bizhawk to run

## Building the dependencies
Run these commands starting from the root of the repository

### SharpNEAT
```
cd .\Submodules\SharpNEAT\src\
dotnet build --configuration Release
```

### BizHawk
```
cd .\Submodules\BizHawk\Dist\
.\QuickTestBuildAndPackage.bat
```

## Building the application
```
cd .\SMW-ML\
dotnet build --configuration Release
```

## Running the application
To run the application, you first need to copy a Super Mario World rom file to the root directory of the program. It must be named "swm.sfc" exactly.

## Using the application

### Main page
![Image of the main page of the application](docs/mainApp.png)  

* **[Start training](#training-page)**
* **[Play mode](#play-mode)**
* **Load population**
  * Loads an existing population (xyz.pop) into the program. Allows to continue training a population after closing and reopening the application.
* **Save population**
  * Saves the population that's been trained for at least one generation.
* **[Training Configuration](#configuration)**

### Training Page
When entering this page, the training of AIs will be started automatically, using the [app's configuration](#configuration). Note that the UI might be unresponsive if too many emulator instances are running at once. Please do not close any emulators manually, as this will break the application.

#### Training Tab
![Image of the training page's training tab](docs/training-training.png)  

* **Neural Network visualization**
  * Shows the first emulator's neural network structure and values. When the emulators finish booting, the emulator being represented will be the one on the bottom. The refresh rate of the preview will depend on the computer's performances, and having too many emulators running at once will affect it.
* **Stop Training**
  * Will stop training at the end of the current generation. Please be patient if the population size is big, and not many emulators are running at once. It will return to the [main page](#main-page) once it is done.
  
#### Statistics Tab
![Image of the training page's statistics tab](docs/training-stats.png)  

This tab shows statistics of the current training session. They are updated at the end of every generation.
* **Current generation**
  * Shows the current generation number. Note that this number is reset whenever starting a new training session, even after loading an existing population.
* **Best genome's fitness**
  * The best AI's current score.
* **Best genome's complexity**
  * The amount of connections in the neural network of the best AI.
* **Mean fitness**
  * The average score of all the AIs.
* **Mean complexity**
  * The average complexity (amount of connections in the neural network) of all the AIs.
* **Maximum complexity**
  * The maximum complexity of all the AIs.
* **Evaluations per minute**
  * The amount of AIs that are evaluated per minute.
* **Total evaluations so far**
  * The amount of AIs evaluated since the start of training.

### Play Mode
![Image of the play mode page](docs/playmode.png)  

This page is used to make specific genomes play on specific levels, both of them being manually selected. It is a good way to check the abilities of a genome on levels that aren't part of its training-set.
* **Load Genome**
  * Loads the genome to use for the play mode. Selecting a new genome while the play mode is running reloads the save state and starts it again using the new genome.
* **Load save state**
  * Loads the save state to use for the play mode. Selecting a new save state while the play mode is running will load it into the emulator right away.
* **Start**
  * Loads the save state and starts up the AI. Can only be started when a save state and a genome have been provided.
* **Stop**
  * Stops the current AI from running.
* **Exit**
  * Exits play mode. Only enabled once play mode has been stopped.

### Configuration

#### Training
![Image of the neural network configuration menu](docs/config-training.png)  

* **Number of AIs**
  * Determines the total population size of the training. Making it too big will make evolution really slow, while making it too small will make break-throughs extremely rare.
* **Species count**
  * Determines the number of species to use for the NEAT algorithm. A higher value will make breakthroughs more common while training, but a value that's too high will be detrimental to the evolution of the individual species. The amount of AIs per species is equal to `Number of AI / Species Count`
* **Elitism proportion**
  * The percentage of species to keep in each generation. Should be higher than 0, but lower than 1. New species will be created from the species that are kept, either by sexual reproduction, or asexual reproduction
* **Selection proportion**
  * The percentage of AIs to keep between each generation, within a species. Should be higher than 0, but lower or equal to 1. New AIs will be created within the species based on the AIs that are kept.

#### Application
![Image of the app configuration menu](docs/config-app.png)  

* **Multithread**
  * This is the amount of emulators which will be booted while training. It is recommended to not put this value higher than the amount of cores within your computer, as performance will be greatly affected. For the fastest training, at the cost of using up all of the CPU resources available, set to the exact amount of cores in your computer. Otherwise, set to a lower value.
* **Communication Port with Arduino**
  * Communication port with an Arduino that's connected to the PC. Should be left like it is if no arduinos are connected. Used so we can preview the inputs on an actual physical controller.
  * [See ./ArduinoSNESController](ArduinoSNESController)
* **When to stop the training**
  * UNIMPLEMENTED
* **Save states to use**
  * By clicking the button, you can select the save states you want to use for training. At least one must be selected.

#### Objectives
![Image of the objectives configuration menu](docs/config-objectives.png)  

This page lists all of the available training objectives. Some of them cannot be disabled, but the multiplier can be set to 0 so it won't affect the score, at least. The values pictured are the recommended values when training AIs from nothing, but you may experiment at your leisure.
* **Enabled**
  * Whether or not this objective will affect the score.
  * Also, if the objective has a stop condition, the stop condition will not actually stop the level if it is disabled.
* **Multiplier**
  * The amount by which to multiply the objective's score.

##### Objectives information

* **Died**
  * The amount of points to attribute to an AI that died. 
  * It is recommended to set this value to a negative value to discourage AIs from killing themselves.
* **Distance travelled**
  * Points to attribute for each tile the AI traverses. This is based on the maximum distance, so going back and forth will not give more points.
* **Stopped moving**
  * Stops the current level if the AI has stopped progressing through the level. This is based on the maximum distance reached so far, not the current position.
  * It is recommended to leave this enabled, but you may disable it if you want AIs to stay on the level for up to the maximum duration.
  * A negative amount of points is recommended to discourage the AI from idling, moving back and forth, and looping forever.
* **Time taken**
  * Gives some points based on the amount of seconds the AI had left before its training being shutdown. 
  * Note that these points are given even if the AI died or stopped moving.
  * Do not set this value too high unless you also raise the penalty for dying and not moving, as the AI might be encouraged to kill itself or idle as soon as possible to max out its score.
* **Won level**
  * The amount of points to attribute if the AI wins a level. Ideally, this should be a high value to encourage actually finishing levels.
* **Coins**
  * The amount of points to give the AI per coin collected.
* **Yoshi Coins**
  * The amount of points to give the AI per Yoshi Coin collected
* **1-ups**
  * The amount of points to give the AI per 1-up collected from any source.
* **High Score**
  * The amount of points to give the AI for its displayed high-score, divided by 1000. So a high-score of 55200 with a multiplier of 2 will give a total amount of points of 110.4 to the AI.
  
#### Neural
![Image of the neural configuration menu](docs/config-neural.png)  

This page lists all of the inputs and outputs that can be toggled for the neural networks, as well as the distance (in tiles) that the AI can see.  
⚠ Changing any of these values will make previously trained AIs incompatible with the application ⚠
* **View distance horizontal**
  * The horizontal distance that the AI can see for, in tiles, not including the tile the AI is on. This means that if we set both the horizontal and vertical distances to 4, a 9x9 grid of inputs will be used.
* **View distance vertical**
  * The vertical distance that the AI can see for, in tiles, not including the tile the AI is on. This means that if we set both the horizontal and vertical distances to 4, a 9x9 grid of inputs will be used.
* **Input nodes**
  * Tiles : The tiles the AI can stand on. V*H total nodes.
  * Dangers : The dangerous tiles around the AI. Includes dangerous tiles as well as dangerous sprites. V*H total nodes.
  * Goodies : The "good" tiles around the AI. Includes coins, powerups, blocks that contain items. V*H total nodes.
  * On ground : Whether or not the AI is touching the ground.
  * In water : Whether or not the AI is in water
  * Raising : Whether or not the AI is raising, both out of a jump as well as while swimming.
  * Sinking : Whether or not the AI is falling, both out of a jump as well as while swimming.
  * Can jump out of water : Whether or not the AI will get out of the water by jumping.
  * Carrying : Whether or not the AI is carrying something.
  * Can Climb : ⚠Not guaranteed to always be right. Whether or not the AI can climb at the moment.
  * Max Speed : Whether or not the AI has reached maximum speed.
  * Message box : Whether or not there currently is a message box open.
  * Internal clock : Timed bias value. Alternates between on and off every couple frames.
  * Bias : Bias value. Always on.
* **Output nodes**
  * The output nodes are self-explanatory, there is one for each button available on an SNES controller.