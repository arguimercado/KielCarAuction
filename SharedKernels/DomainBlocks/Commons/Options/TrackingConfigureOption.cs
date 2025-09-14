namespace DomainBlocks.Commons.Options;


public record TrackingConfigureOption(bool AsTracking = true);
public record TrackingWithChildConfigureOption(bool AsTracking = true,bool IncludeChildren = false);

