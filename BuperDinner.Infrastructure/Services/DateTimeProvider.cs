using BuperDinner.Application.Common.Interfaces.Services;

namespace BuperDinner.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}