using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Redarbor.Api.Configurations.Definitions;

public class AutoTagDescriptionsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var tagData = context
            .ApiDescriptions
            .Select(data => new
            {
                _tag = data
                    .ActionDescriptor
                    .EndpointMetadata
                    .OfType<TagsAttribute>()
                    .FirstOrDefault()?
                    .Tags
                    .FirstOrDefault(),
                _desc = data
                    .ActionDescriptor
                    .EndpointMetadata
                    .OfType<EndpointDescriptionAttribute>()
                    .FirstOrDefault()?
                    .Description
            })
            .Where(d => !string.IsNullOrWhiteSpace(d._tag))
            .GroupBy(d => d._tag)
            .Select(d => new OpenApiTag
            {
                Name = d.Key!,
                Description = d
                    .Select(e => e._desc)
                    .FirstOrDefault(f => !string.IsNullOrWhiteSpace(f))
            })
            .ToList();

        swaggerDoc.Tags = tagData;
    }
}
