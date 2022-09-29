using System.Text.Json.Serialization;

namespace Trigger;

public class Job
{
    [JsonPropertyName("jobId")] public string JobId { get; set; }

    [JsonPropertyName("jobName")] public string JobName { get; set; }

    [JsonPropertyName("description")] public string Description { get; set; }

    [JsonPropertyName("statusCode")] public string StatusCode { get; set; }

    [JsonPropertyName("hasError")] public bool HasError { get; set; }

    [JsonPropertyName("latestErrorMessage")]
    public string LatestErrorMessage { get; set; }

    [JsonPropertyName("submitterId")] public string SubmitterId { get; set; }

    [JsonPropertyName("creatorId")] public string CreatorId { get; set; }

    [JsonPropertyName("siteId")] public string SiteId { get; set; }

    [JsonPropertyName("providerId")] public string ProviderId { get; set; }

    [JsonPropertyName("providerReference")]
    public string ProviderReference { get; set; }

    [JsonPropertyName("poReference")] public string PoReference { get; set; }

    [JsonPropertyName("dueDate")] public DateTime DueDate { get; set; }

    [JsonPropertyName("createdDate")] public DateTime CreatedDate { get; set; }

    [JsonPropertyName("modifiedDate")] public DateTime ModifiedDate { get; set; }

    [JsonPropertyName("archived")] public bool Archived { get; set; }

    [JsonPropertyName("customData")] public string CustomData { get; set; }

    [JsonPropertyName("shouldQuote")] public bool ShouldQuote { get; set; }

    [JsonPropertyName("connectorName")] public string ConnectorName { get; set; }

    [JsonPropertyName("connectorVersion")] public string ConnectorVersion { get; set; }

    [JsonPropertyName("serviceType")] public string ServiceType { get; set; }


    [JsonPropertyName("globalTrackingId")] public string GlobalTrackingId { get; set; }

    [JsonPropertyName("assetTaskCount")] public int AssetTaskCount { get; set; }

    [JsonPropertyName("supportAssetTaskCount")]
    public int SupportAssetTaskCount { get; set; }

    [JsonPropertyName("isAcknowledged")] public bool IsAcknowledged { get; set; }
}