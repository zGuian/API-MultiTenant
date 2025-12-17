using GearCore.Monolith.Core.UserCore.Converters;

namespace GearCore.Monolith.Core.Commons.Utils.Converters
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            UserConverter.UserConverters();
        }
    }
}
