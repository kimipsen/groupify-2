using System;

namespace app.Setup;

public static class JsonConfigurationExtensions
{
    public static IMvcBuilder AddJsonConfiguration(this IMvcBuilder builder, IConfiguration configuration)
    {
        builder.AddJsonOptions(static options => {
            options.JsonSerializerOptions.Converters.Add(new VogenTypesFactory());
        });
        return builder;
    }
}
