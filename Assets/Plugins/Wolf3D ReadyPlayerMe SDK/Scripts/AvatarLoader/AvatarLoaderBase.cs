using System;
using UnityEngine;
using System.Collections;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    public abstract class AvatarLoaderBase
    {
        // Avatar download timeout
        public int Timeout { get; set; } = 20;

        // OnAvatarLoaded callback
        protected Action<GameObject> OnAvatarLoaded = null;
        
        //Postfix to remove from names for correction
        protected const string Wolf3DPrefix = "Wolf3D_";

        //Animation avatar and controllers
        protected const string MaleAnimationTargetName = "AnimationTargets/MaleAnimationTarget";
        protected const string FemaleAnimationTargetName = "AnimationTargets/FemaleAnimationTarget";

        protected const string MaleAnimatorControllerName = "AnimatorControllers/MaleFullbody";
        protected const string FemaleAnimatorControllerName = "AnimatorControllers/FemaleFullbody";

        //Texture property IDs
        protected static readonly string[] ShaderProperties = new[] {
            "_MainTex",
            "_BumpMap",
            "_EmissionMap",
            "_OcclusionMap",
            "_MetallicGlossMap"
        };

        /// <summary>
        ///     Load Avatar GameObject from given GLB url.
        /// </summary>
        /// <param name="url">GLB Url acquired from readyplayer.me</param>
        /// <param name="callback">Callback method that returns reference to Avatar GameObject</param>
        public void LoadAvatar(string url, Action<GameObject> callback = null)
        {
            OnAvatarLoaded = callback;

            AvatarUri uri = new AvatarUri(url);
            LoadAvatarAsync(uri).Run();
        }

        // Makes web request for downloading avatar model and imports the model.
        protected abstract IEnumerator LoadAvatarAsync(AvatarUri uri);
        
        // Downloading avatar model
        protected abstract IEnumerator DownloadAvatar(AvatarUri uri);

        /// <summary>
        ///     Restructure avatar bones and add gender spesific animation avatar and animator controller.
        /// </summary>
        protected void RestructureAndSetAnimator(GameObject avatar, AvatarMetaData avatarMetaData)
        {
            #region Restructure
            GameObject armature = new GameObject();
            armature.name = "Armature";

            armature.transform.parent = avatar.transform;
            if(avatarMetaData.OutfitVersion == 1)
            {
                armature.transform.localScale *= 0.01f;
            }

            Transform hips = avatar.transform.Find("Hips");
            hips.parent = armature.transform;
            #endregion

            #region SetAnimator
            if (avatarMetaData.IsFullbody)
            {
                string animationAvatarName = (avatarMetaData.IsOutfitMasculine ? MaleAnimationTargetName : FemaleAnimationTargetName) + OutfitVersion(avatarMetaData.OutfitVersion);
                Avatar animationAvatar = Resources.Load<Avatar>(animationAvatarName);
                RuntimeAnimatorController animatorController = Resources.Load<RuntimeAnimatorController>(avatarMetaData.IsOutfitMasculine ? MaleAnimatorControllerName : FemaleAnimatorControllerName);

                Animator animator = armature.AddComponent<Animator>();
                animator.runtimeAnimatorController = animatorController;
                animator.avatar = animationAvatar;
                animator.applyRootMotion = true;
            }
            #endregion
        }

        private string OutfitVersion(int version) 
        {
            switch (version)
            {
                case 1: return "";
                case 2: return "V2";
                default: return "";
            }
        }   

        #region Set Names
        /// <summary>
        ///     Name avatar assets for make them easier to view in profiler.
        ///     Naming is 'Wolf3D.Avatar_<Type>_<Name>'
        /// </summary>
        protected void SetAvatarAssetNames(GameObject avatar)
        {
            Renderer[] renderers = avatar.GetComponentsInChildren<SkinnedMeshRenderer>();

            foreach (var renderer in renderers)
            {
                string assetName = renderer.name.Replace(Wolf3DPrefix, "");

                renderer.name = $"Wolf3D.Avatar_Renderer_{assetName}";
                renderer.sharedMaterial.name = $"Wolf3D.Avatar_Material_{assetName}";
                SetTextureNames(renderer, assetName);
                SetMeshName(renderer, assetName);
            }
        }

        /// <summary>
        ///     Set a name for the texture for finding it in the Profiler.
        /// </summary>
        /// <param name="renderer">Renderer to find the texture in.</param>
        /// <param name="assetName">Name of the asset.</param>
        /// <param name="propertyID">Property ID of the texture.</param>
        private void SetTextureNames(Renderer renderer, string assetName)
        {
            foreach (string propertyName in ShaderProperties)
            {
                int propertyID = Shader.PropertyToID(propertyName);

                if (renderer.sharedMaterial.HasProperty(propertyID))
                {
                    var texture = renderer.sharedMaterial.GetTexture(propertyID);

                    if (texture != null)
                    {
                        texture.name = $"Wolf3D.Avatar{propertyName}_{assetName}";
                    }
                }
            }
        }

        /// <summary>
        ///     Set a name for the mesh for finding it in the Profiler.
        /// </summary>
        /// <param name="renderer">Renderer to find the mesh in.</param>
        /// <param name="assetName">Name of the asset.</param>
        private void SetMeshName(Renderer renderer, string assetName)
        {
            if (renderer is SkinnedMeshRenderer)
            {
                (renderer as SkinnedMeshRenderer).sharedMesh.name = $"Wolf3D.Avatar_SkinnedMesh_{assetName}";
            }
            else if (renderer is MeshRenderer)
            {
                MeshFilter meshFilter = renderer.GetComponent<MeshFilter>();

                if (meshFilter != null)
                {
                    meshFilter.sharedMesh.name = $"Wolf3D.Avatar_Mesh_{assetName}";
                }
            }
        }
        #endregion
    }
}
