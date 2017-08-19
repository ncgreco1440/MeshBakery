# Mesh Bakery

Mesh batch tool for Unity3D

## Intro
---

Mesh Bakery is an open source tool for optimizing draw calls within Unity3D by combining similar meshes into one.

## Documentation
---

Mesh Bakery is a tool that attempts to optimize draw calls within Unity3D. Once the MeshBakery is attached and started it will run around and scoop up arbitrary meshes that have been specified by the application's author from within the Unity Editor. It will then create Batches based on unique mesh names and materials of each Batch Ingredient, and as each one is created it is also baked. As the baking completes all original MeshRenderers contained with the batch are disabled.

In order to use MeshBakery simply attach it to a GameObject which parents several identical meshes with matching materials. Use the Batch Ingredient data member to add to the list of Batch Indegredients. Each Batch Ingredient will contain a single mesh and a single material. Hit the play button and upon Start() the MeshBakery will create brand new GameObjects located at 0, 0, 0 in world space that contain the newly baked meshes which have the appropriate materials assigned.

[MeshBakery Component Setup](https://drive.google.com/file/d/0B9R4-NvDHM5vYW5vRTZMcjJqYWc/view)


### Important Notes
---

There is a chance that scene load times will noticeably increase depending on how many meshes you wish to bake however during testing it was shown that a total list of over 3,400 meshes could be parsed, batched, and baked in 399 ms. The result was 1.1 million triangles rendering on screen in a total of 44 batches and 20 SetPass calls. One important note is that shadow distance was turned down to 20 in order to maximize total performance, however shadow distance should always be tuned appropriately for your game. 

### Benchmarks
---

All benchmarks were completed using a single MeshBakery parenting 1,152 GameObjects containing 3 more GameObjects each with it's own MeshRenderer constructing a modular fence. In total, 3,456 meshes were batched in the scene.

|Batch Mode|Batches|SetPass Calls|Render Thread|Image|
|:--:|:--:|:--:|:--:|:--:|
|Dynamic Batching|1327|187|3.0 ms|[link](https://drive.google.com/file/d/0B9R4-NvDHM5vaGx4WWZmcU9sTUU)|
|Static Batching|420|372|1.4 ms|[link](https://drive.google.com/open?id=0B9R4-NvDHM5va0NMT203NnA5clE)|
|GPU Instancing|192|187|2.9 ms|[link](https://drive.google.com/open?id=0B9R4-NvDHM5vNGNnRThxYl9zb2M)|
|MeshBakery|44|23|0.5 ms|[link](https://drive.google.com/open?id=0B9R4-NvDHM5vdExfZVJKVUJFcGc)|
