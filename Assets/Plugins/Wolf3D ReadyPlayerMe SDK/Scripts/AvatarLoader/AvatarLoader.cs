using System;
using UnityEngine;
using System.Collections;
using Siccity.GLTFUtility;
using UnityEngine.Networking;
using Newtonsoft.Json;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    /// <summary>
    ///     Loads avatar models from URL and instantates to the current scene.
    /// </summary>
    public class AvatarLoader
    {
        // Avatar download timeout
        public int Timeout { get; set; } = 20;

        /// <summary>
        ///     Load Avatar GameObject from given GLB url.
        /// </summary>
        /// <param name="url">GLB Url acquired from readyplayer.me</param>
        /// <param name="callback">Callback method that returns reference to Avatar GameObject</param>
        public void LoadAvatar(string url, Action<GameObject> callback = null)
        {
            LoadOperation operation = new LoadOperation();
            operation.Timeout = Timeout;
            operation.LoadAvatar(url, callback);
        }

        /// <summary>
        /// LoadOperation is a simplified avatar loader without local download and cacheing of models.
        /// Operations are encaptulated not to lose the data of the avatar since they load asyncronously.
        /// </summary>
        class LoadOperation : AvatarLoaderBase
        {
            // Avatar GLB model bytes in memory.
            private byte[] avatarBytes;
            private AvatarMetaData avatarMetaData;

            // Makes web request for downloading avatar model into memory and imports the model.
            protected override IEnumerator LoadAvatarAsync(AvatarUri uri)
            {
                yield return DownloadAvatar(uri).Run();

                Importer.ImportGLBAsync(avatarBytes, new ImportSettings() { useLegacyClips = true }, OnImportFinished);
            }

            // Download avatar model into memory and cache bytes
            protected override IEnumerator DownloadAvatar(AvatarUri uri)
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {
                    Debug.LogError("AvatarLoader.LoadAvatarAsync: Please check your internet connection.");
                }
                else
                {
                    using (UnityWebRequest request = new UnityWebRequest(uri.AbsoluteUrl))
                    {
                        request.downloadHandler = new DownloadHandlerBuffer();

                        yield return request.SendWebRequest();

                        if (request.downloadedBytes == 0)
                        {
                            Debug.LogError("AvatarLoader.LoadAvatarAsync: Please check your internet connection.");
                        }
                        else
                        {
                            avatarBytes = request.downloadHandler.data;
                        }
                    }

                    using (UnityWebRequest request = UnityWebRequest.Get(uri.MetaDataUrl))
                    {
                        yield return request.SendWebRequest();

                        avatarMetaData = JsonConvert.DeserializeObject<AvatarMetaData>(request.downloadHandler.text);
                    }
                }
            }

            // GLTF Utility Callback for finished model load operation
            private void OnImportFinished(GameObject avatar)
            {
                avatar.name = "Avatar";
                SetAvatarAssetNames(avatar);
                RestructureAndSetAnimator(avatar, avatarMetaData);
                OnAvatarLoaded?.Invoke(avatar);
            }
        }
    }
}
