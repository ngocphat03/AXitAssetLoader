# AssetLoader

The `AXitAssetLoader` is a flexible asset management system designed to work with both Unity’s **Addressables** and **Resources** systems. It supports asynchronous asset loading with coroutine or UniTask and can be easily integrated with dependency injection frameworks like **VContainer** or **Zenject**.

# Installation

1. Open Unity and go to **Package Manager** via **Windows > Package Manager**.
2. Click on the **Plus (+)** icon > **Add Package from git URL**.
3. Paste the following link in the URL field: `https://github.com/ngocphat03/AssetLoader.git`
4. Click **Add** to install the package into your project.

# **Required Symbols:**

1. `ADDRESSABLES_ASSET_LOADED`
    
    Enables support for Unity’s Addressables system.
    
2. `UNITASK`
    
    Enables the use of UniTask for asynchronous programming.
    

**Note:** If you don’t add the `ADDRESSABLES_ASSET_LOADED` symbol, the system will use Resources instead of Addressables for loading assets.

**Switching Between Addressables and Resources**

```csharp
GameObject asyncLoadResult;
#if ADDRESSABLES_ASSET_LOADED
		 asyncLoadResult = await DependencyLocator.AssetLoader.LoadAssetAsync<GameObject>(key: "MyAsset").ToUniTask();
#else
    this.StartCoroutine(DependencyLocator.AssetLoader.LoadAssetAsync<GameObject>(key: "MyAsset")
                                             .ToCoroutine(result =>
                                             {
		                                             asyncLoadResult = result;
                                             }));
#endif
```

# **Setup Instructions for AXitAssetLoader**

The `AssetLoaderMonoInstall` can be configured to work standalone or with dependency injection (DI) frameworks like **VContainer** or **Zenject**. Follow the steps below for different setups.

## **1. MonoBehaviour Setup (Standalone)**

### Steps:

1. **Create a GameObject:**
    - In the Unity Editor, go to the **Hierarchy** window.
    - Right-click and select **Create Empty** to create a new GameObject.
2. **Attach the Script:**
    - Drag the `AssetLoaderMonoInstall.cs` script from the **Project** window and drop it onto the GameObject.
3. **Configure Persistence (Optional):**
    - Select the GameObject in the **Hierarchy**.
    - In the **Inspector**, find the `isPersistentAcrossScenes` option:
        - **Enabled (default)**: The GameObject will persist across scene changes.
        - **Disabled**: Uncheck the box if you don't need this behavior.

## **2. Setup with VContainer (Dependency Injection)**

🚧 *Feature is coming soon.*

Support for **VContainer** is under development. Stay tuned for updates.

## **3. Setup with Zenject (Dependency Injection)**

🚧*Feature is coming soon.*

Integration with **Zenject** will be available in an upcoming release.

### **Summary:**

- **Standalone Mode:** Use `AssetLoaderMonoInstall` as a standard MonoBehaviour.
- **VContainer or Zenject:** Replace with appropriate DI installers to enable dependency injection.

# How to use AXitAssetLoader

## 1. Load asset with coroutine

```csharp
this.StartCoroutine(DependencyLocator.AssetLoader.LoadAssetAsync<GameObject>(key: "TestAsset")
                                             .ToCoroutine(result =>
                                             {
                                                 GameObject asyncLoadResult = result;
                                             }));
```

## 2. Load Asset with UniTask

```csharp
GameObject asyncLoadResult = await DependencyLocator.AssetLoader.LoadAssetAsync<GameObject>(key: "TestAsset").ToUniTask();
```

# **Summary**

- Use `AssetLoaderMonoInstall` for standalone setups or integrate with DI frameworks like **VContainer** or **Zenject** for advanced workflows.
- Leverage conditional symbols `ADDRESSABLES_ASSET_LOADED` and `UNITASK` to adapt the loader to your project’s needs.
- Supports both coroutine-based and UniTask-based asynchronous workflows for asset loading.