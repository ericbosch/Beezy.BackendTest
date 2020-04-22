using System;
using Beezy.BackendTest.Domain;
#pragma warning disable 1591

namespace Beezy.BackendTest.Api.Services
{
    public class DateService : IDateService
    {
        public DateTime Now() => DateTime.UtcNow.Date;
    }
}
