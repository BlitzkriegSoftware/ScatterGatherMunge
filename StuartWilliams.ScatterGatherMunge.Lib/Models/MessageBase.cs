using Newtonsoft.Json;

using StuartWilliams.ScatterGatherMunge.Lib.Attributes;
using StuartWilliams.ScatterGatherMunge.Lib.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StuartWilliams.ScatterGatherMunge.Lib.Models
{

    /// <summary>
    /// Base: Message
    /// </summary>
    public class MessageBase
    {
        /// <summary>
        /// Message Version
        /// </summary>
        [Required]
        [MessagePromotedProperty]
        [MessageInjectedProperty]
        public MessageVersionKind Version { get; set; } = MessageVersionKind.V1;

        /// <summary>
        /// Unique Message Id
        /// </summary>
        [Required]
        [MinLength(8)]
        [MaxLength(72)]
        [MessagePromotedProperty]
        [MessageInjectedProperty]
        public string MessageId {get;set;}

        /// <summary>
        /// Date Time of the Message
        /// </summary>
        [Required]
        [MessagePromotedProperty]
        [MessageInjectedProperty]
        public DateTime MessageDateStamp { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Message Kind
        /// </summary>
        [Required]
        [MessagePromotedProperty]
        public Enums.MessageKind MessageKind { get; set; } = MessageKind.Unknown;

        /// <summary>
        /// Enrichmement: Retry Count
        /// </summary>
        [MessageEnrichment]
        [MessagePromotedProperty]
        public int RetryCount { get; set; } = 0;

        /// <summary>
        /// History
        /// </summary>
        [MessageEnrichment]
        public List<MessageHistoryItem> History { get; set; } = new();

        /// <summary>
        /// Message Data
        /// <para>
        /// Note: if you do not need unique keys or fast lookups, this could be <c><![CDATA[List<KeyValuePair>]]></c>
        /// </para>
        /// <para>
        /// This could also be a more structured data structure, but that comes with additional complexity
        /// </para>
        /// </summary>
        [Required]
        [MessageEnrichment]
        public Dictionary<string, object> MessageData { get; set; } = new();

        /// <summary>
        /// Check validity of message
        /// </summary>
        /// <param name="validationErrors">Validation Errors</param>
        /// <returns>True if ok</returns>
        public virtual bool IsValid(out List<ValidationResult> validationErrors)
        {
            #region "Validation Result Builder Data"
            var validContext = new ValidationContext(this);
            validationErrors = new();
            #endregion

            #region "Custom Validations"
            var names = new List<string>()
                     {
                         nameof(MessageData)
                     };

            // If you want this to be a valid use case remove this clause
            if (this.MessageData is null)
            {
                var vr = new ValidationResult(errorMessage: "Required to be present", memberNames: names);
                validationErrors.Add(vr);
                return false;
            }

            // If you want this to be a valid use case remove this clause
            if (this.MessageData.Count <= 0)
            {
                var vr = new ValidationResult(errorMessage: "Dictionary must have at least one entry", memberNames: names);
                validationErrors.Add(vr);
                return false;
            }
            #endregion

            #region "Standard System.ComponentModel.DataAnnotations Validation"
            if (Validator.TryValidateObject(this, validContext, validationErrors))
            {
                return true;
            }
            else
            {
                return false;
            }
            #endregion
        }

        /// <summary>
        /// Get the last Sub-Message Id
        /// </summary>
        /// <param name="subMessageId">Latest Sub-Message-Id</param>
        /// <returns>True if so</returns>
        public bool SubIdentityTryGet(out string subMessageId)
        {
            subMessageId = string.Empty;

            if((this.History != null) && (this.History.Count > 0))
            {
                DateTime early = DateTime.MinValue;
                foreach(var h in this.History)
                {
                    if(h.Stamp > early)
                    {
                        if (!string.IsNullOrWhiteSpace(h.Id))
                        {
                            subMessageId = h.Id;
                        }
                    }
                }
                return !string.IsNullOrWhiteSpace(subMessageId);
            }

            return false;
        }

        /// <summary>
        /// To String
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

    }
}
