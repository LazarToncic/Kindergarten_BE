using Kindergarten.Application.Common.Dto.Kindergarten;
using Riok.Mapperly.Abstractions;

namespace Kindergarten.Application.Common.Mappers.Kindergarten;

[Mapper]
public static partial class KindergartenMapper
{
    public static partial Domain.Entities.Kindergarten FromCreateKindergartenDtoToKindergarten(this CreateKindergartenDto createKindergartenDto);
}