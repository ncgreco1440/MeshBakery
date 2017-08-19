# Mesh Bakery

Mesh batch tool for Unity3D

## Intro

Mesh Bakery is an open source tool for optimizing draw calls within Unity3D by combining similar meshes into one.

## Documentation

Mesh Bakery is a tool that attempts to optimize draw calls within Unity3D. Once the MeshBakery is attached and started it will run around and scoop up arbitrary meshes that have been specified by the application's author from within the Unity Editor. It will then create Batches based on unique mesh names and materials of each Batch Ingredient, and as each one is created it is also baked. As the baking completes all original MeshRenderers contained with the batch are disabled.

In order to use MeshBakery simply attach it to a GameObject which parents several identical meshes with matching materials. Use the Batch Ingredient data member to add to the list of Batch Indegredients. Each Batch Ingredient will contain a single mesh and a single material. Hit the play button and upon Start() the MeshBakery will create brand new GameObjects located at 0, 0, 0 in world space that contain the newly baked meshes which have the appropriate materials assigned.

[MeshBakery Component Setup](https://drive.google.com/file/d/0B9R4-NvDHM5vYW5vRTZMcjJqYWc/view)