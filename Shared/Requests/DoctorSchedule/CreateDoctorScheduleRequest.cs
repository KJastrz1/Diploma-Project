using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Shared.Utils;

namespace Shared.Requests.DoctorSchedule;

public class CreateDoctorScheduleRequest
{
    [Required(ErrorMessage = "DoctorId is required.")]
    public required Guid DoctorId { get; set; }

    [Required(ErrorMessage = "Day is required.")]
    public required DayOfWeek Day { get; set; }

    [Required(ErrorMessage = "StartTime is required.")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public required TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "EndTime is required.")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public required TimeSpan EndTime { get; set; }

    [Required(ErrorMessage = "VisitDuration is required.")]
    [JsonConverter(typeof(TimeSpanJsonConverter))]
    public required TimeSpan VisitDuration { get; set; }
}

