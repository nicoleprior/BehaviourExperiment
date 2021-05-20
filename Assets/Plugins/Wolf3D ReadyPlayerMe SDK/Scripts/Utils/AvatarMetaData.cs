using Newtonsoft.Json;

namespace Wolf3D.ReadyPlayerMe.AvatarSDK
{
    public class AvatarMetaData
    {
        public readonly int MetaDataVersion = 1;
        public string BodyType;
        public string OutfitGender;
        public int OutfitVersion;

        public bool IsOutfitMasculine => OutfitGender == "masculine";
        public bool IsFullbody => BodyType == "fullbody";

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
