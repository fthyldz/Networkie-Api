using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Networkie.Persistence.EntityFrameworkCore.Converters;

public class UtcDateTimeConverter() : ValueConverter<DateTime, DateTime>(
    v => v.Kind == DateTimeKind.Utc ? v : v.ToUniversalTime(),
    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));