# CorrespondentFix

Mod to remove the noob-trap of Correspondent legacy, and make it actually useful.

## Overview

CorrespondentFix tweaks how the Correspondent legacy works in Sunless Sea. Previously, it only preserved your old chart, allowing you to see the locations of all previously discovered tiles. However, this also retained discovered landmarks, preventing new captains from gaining the fragments typically earned from discovering them. This created a giant noob-trap. CorrespondentFix fixes this by removing all discovery data from each tile in the dead characters' chart while preserving its layout and the fog state on the map screen.

## **Compiling the plugin**

To develop and build the plugin, there are a couple of prerequisites. Clone the repository:

```bash
https://github.com/MagicJinn/SDLS.git
cd SDLS
```

After this, you need to acquire a DLL CorrespondentFix relies on. Create a `dependencies` folder, and find `Sunless.Game.dll` in your `SunlessSea\Sunless Sea_Data\Managed` folder. Copy it into the `dependencies` folder. After this, you should be able to compile the project with the following command:

```bash
dotnet build -c Release -p:Optimize=true
```

The DLL should be located in `bin/Release/net35`.
