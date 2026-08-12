using System;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// A SKU (stock-keeping unit) — the sellable product in Subscription API v2.
    /// Returned by GET /skus and GET /skus/{id}.
    /// </summary>
    public class DiscordSku : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("application_id")]
        public ulong ApplicationId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("description")]
        public string Description { get; private set; }

        [JsonProperty("type")]
        public SkuType Type { get; private set; }

        [JsonProperty("flags")]
        public SkuFlags Flags { get; private set; }

        [JsonProperty("available")]
        public bool Available { get; private set; }
    }

    public enum SkuType
    {
        Durable = 1,
        Consumable = 2,
        Subscription = 3,
        Bundle = 4
    }

    [Flags]
    public enum SkuFlags
    {
        Available = 1 << 0,
        GuildSubscription = 1 << 1,
        UserSubscription = 1 << 2
    }

    /// <summary>
    /// A subscription plan (billing tier) for a SKU in Subscription API v2.
    /// Returned by GET /skus/{sku.id}/subscription-plans.
    /// </summary>
    public class DiscordSubscriptionPlan : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("sku_id")]
        public ulong SkuId { get; private set; }

        [JsonProperty("name")]
        public string Name { get; private set; }

        [JsonProperty("currency")]
        public string Currency { get; private set; }

        [JsonProperty("price")]
        public int Price { get; private set; }

        [JsonProperty("interval")]
        public SubscriptionPlanInterval Interval { get; private set; }

        [JsonProperty("interval_count")]
        public int IntervalCount { get; private set; }

        [JsonProperty("tax_inclusive")]
        public bool TaxInclusive { get; private set; }
    }

    public enum SubscriptionPlanInterval
    {
        Day = 1,
        Week = 2,
        Month = 3,
        Year = 4
    }

    /// <summary>
    /// An entitlement (Subscription API v2 shape). v2 adds sku_id, guild_id,
    /// application_id, starts_at/ends_at, and renewal metadata.
    /// Returned by GET /entitlements.
    /// </summary>
    public class DiscordEntitlementV2 : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("sku_id")]
        public ulong SkuId { get; private set; }

        [JsonProperty("application_id")]
        public ulong ApplicationId { get; private set; }

        [JsonProperty("user_id")]
        public ulong? UserId { get; private set; }

        [JsonProperty("guild_id")]
        public ulong? GuildId { get; private set; }

        [JsonProperty("type")]
        public EntitlementType Type { get; private set; }

        [JsonProperty("starts_at")]
        public DateTime? StartsAt { get; private set; }

        [JsonProperty("ends_at")]
        public DateTime? EndsAt { get; private set; }

        [JsonProperty("consumed")]
        public bool Consumed { get; private set; }

        [JsonProperty("renewal")]
        public EntitlementRenewalInfo Renewal { get; private set; }

        [JsonProperty("deactivated_at")]
        public DateTime? DeactivatedAt { get; private set; }

        [JsonProperty("expires_at")]
        public DateTime? ExpiresAt { get; private set; }
    }

    public enum EntitlementType
    {
        Purchase = 1,
        PremiumSubscription = 2,
        DeveloperGift = 3,
        TestModePurchase = 4,
        FreePurchase = 5,
        UserGift = 6,
        PremiumPurchase = 7,
        ApplicationSubscription = 8
    }

    public class EntitlementRenewalInfo
    {
        [JsonProperty("type")]
        public EntitlementRenewalType Type { get; private set; }
    }

    public enum EntitlementRenewalType
    {
        Renew = 1,
        Revoke = 2,
        BillingPause = 3
    }

    /// <summary>
    /// A subscription (Subscription API v2 shape), keyed by sku_id + subscription_plan_id
    /// rather than the v1 plan_id. Returned by GET /users/@me/subscriptions/{id}.
    /// </summary>
    public class DiscordSubscriptionV2 : Controllable
    {
        [JsonProperty("id")]
        public ulong Id { get; private set; }

        [JsonProperty("user_id")]
        public ulong UserId { get; private set; }

        [JsonProperty("sku_id")]
        public ulong SkuId { get; private set; }

        [JsonProperty("subscription_plan_id")]
        public ulong SubscriptionPlanId { get; private set; }

        [JsonProperty("status")]
        public DiscordSubscriptionStatus Status { get; private set; }

        [JsonProperty("current_period_start")]
        public DateTime CurrentPeriodStart { get; private set; }

        [JsonProperty("current_period_end")]
        public DateTime CurrentPeriodEnd { get; private set; }

        [JsonProperty("canceled_at")]
        public DateTime? CanceledAt { get; private set; }
    }

    /// <summary>
    /// Query parameters for listing entitlements (Subscription API v2).
    /// </summary>
    public class EntitlementQuery
    {
        [JsonProperty("sku_ids")]
        public string SkuIds { get; set; }

        [JsonProperty("guild_id")]
        public ulong? GuildId { get; set; }

        [JsonProperty("user_id")]
        public ulong? UserId { get; set; }

        [JsonProperty("limit")]
        public uint? Limit { get; set; }

        [JsonProperty("excluded_ended")]
        public bool? ExcludedEnded { get; set; }

        [JsonProperty("in_progress")]
        public bool? InProgress { get; set; }
    }
}
