using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.QualificationType;

[Mapper]
public static partial class QualificationTypeMapper
{
    public static partial Domain.Entities.QualificationType FromStringNameToQualificationType(this string name);
}