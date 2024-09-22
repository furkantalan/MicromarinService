using AutoMapper;
using MongoDB.Bson;


namespace Micromarin.Domain.Mappings.Converter;

public class StringToObjectIdConverter : ITypeConverter<string, ObjectId>
{
    public ObjectId Convert(string source, ObjectId destination, ResolutionContext context)
    {
        return string.IsNullOrWhiteSpace(source) || !ObjectId.TryParse(source, out _) ? ObjectId.Empty : new ObjectId(source);
    }
}